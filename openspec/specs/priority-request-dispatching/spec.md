# priority-request-dispatching

## Purpose

Schedule PLC WebAPI requests by access-priority class so that user-interface traffic stays responsive while background work proceeds without starvation, within the concurrency limits the PLC WebAPI tolerates.

## Requirements

### Requirement: Requests are scheduled by access-priority class
The connector SHALL dispatch PLC WebAPI requests in order of access-priority class (`High` before `UserInterface` before `Normal` before `Low`). Within a class, requests SHALL be dispatched FIFO. Requests enqueued with `eAccessPriority.Custom` SHALL be scheduled as class `Normal`; `Custom`'s batch-shaping semantics (caller-supplied chunk size and inter-chunk delay) SHALL remain unchanged.

#### Scenario: Higher class preempts queued lower class
- **WHEN** a `Low` chunk is queued and a `High` chunk is enqueued before a worker becomes free
- **THEN** the `High` chunk is dispatched before the queued `Low` chunk

#### Scenario: FIFO within a class
- **WHEN** two `Normal` chunks are enqueued in sequence
- **THEN** they are dispatched in enqueue order

#### Scenario: Custom schedules as Normal
- **WHEN** a chunk is enqueued with priority `Custom`
- **THEN** it is scheduled with the same precedence as a `Normal` chunk while using the caller-supplied chunk size and inter-chunk delay

### Requirement: Concurrent in-flight requests never exceed the configured cap
The dispatcher SHALL never allow more than `ConcurrentRequestMaxCount` work items in flight at any instant — including during startup, pause/resume transitions, and after a work-item failure. The cap SHALL remain user-settable via the existing `maxConcurrentRequest` constructor parameter and SHALL remain clamped to at most 4, because the PLC WebAPI destabilizes above that.

#### Scenario: Cap holds under saturation
- **WHEN** more chunks are enqueued than `ConcurrentRequestMaxCount` across mixed priority classes
- **THEN** the number of simultaneously executing work items never exceeds `ConcurrentRequestMaxCount`

#### Scenario: Cap holds across pause and resume
- **WHEN** the dispatcher is paused with a backlog and then resumed
- **THEN** at no instant do in-flight work items exceed `ConcurrentRequestMaxCount`

#### Scenario: Configured value above the ceiling is clamped
- **WHEN** the connector is constructed with `maxConcurrentRequest` greater than 4
- **THEN** the dispatcher runs with at most 4 workers

### Requirement: Waiting requests age to prevent starvation
A queued work item's effective priority class SHALL improve by one class per elapsed aging interval (default 1000 ms), **capped at `Normal`** — aged work SHALL never reach `UserInterface` or `High` effective class, so it cannot contest the reserved user-interface capacity. Low-priority work still has a bounded worst-case wait: it reaches `Normal` within one aging interval and proceeds FIFO-oldest from there within the non-reserved slots.

#### Scenario: Low completes under sustained higher-priority load
- **WHEN** a `Low` chunk is enqueued and a continuous stream of `Normal` chunks keeps the non-reserved workers busy
- **THEN** the `Low` chunk is dispatched within a bound determined by the aging interval rather than waiting indefinitely

#### Scenario: Aged work never enters the user-interface lane
- **WHEN** a `Low` chunk has waited longer than two aging intervals
- **THEN** its effective class is `Normal`, and a concurrently enqueued `UserInterface` chunk still dispatches before it

### Requirement: UserInterface requests do not queue behind lower-priority work
The dispatcher SHALL reserve one concurrency slot exclusively for `UserInterface` and `High` class items: items of effective class `Normal` or lower SHALL occupy at most N−1 of the N worker slots. An arriving `UserInterface` or `High` item SHALL dispatch immediately whenever fewer than N items of those classes are in flight. `UserInterface` items MAY wait only behind other `UserInterface`/`High` items occupying all N slots — never behind `Normal`, `Low`, or aged work.

#### Scenario: UI dispatches immediately while background work saturates non-reserved slots
- **WHEN** N−1 `Normal`/`Low` chunks are in flight and more are queued, and a `UserInterface` chunk is enqueued
- **THEN** the `UserInterface` chunk dispatches immediately into the reserved slot without waiting for any lower-class item to finish

#### Scenario: Lower classes never use the reserved slot
- **WHEN** only `Normal` and `Low` items are queued and N−1 are already in flight
- **THEN** the queued items wait; the reserved slot stays idle for `UserInterface`/`High` arrivals

### Requirement: UserInterface latency budget is monitored
The connector SHALL measure `UserInterface` latency at **whole-batch level** — from batch start to completion of the batch's last chunk (a single direct call is a one-chunk batch) — against a latency budget. The budget SHALL be settable to an arbitrary positive value via a public connector property (`UserInterfaceLatencyBudget`), defaulting to 1500 ms. A breach SHALL be logged (warning, with queue-wait vs. execution-time split so a slow PLC is distinguishable from a scheduler problem) and surfaced via connector-level telemetry alongside the existing performance surface (`CyclicRwDuration`/`RwCycleCount`): `LastUiBatchDurationMs` and `UiLatencyBreachCount` (with change notification). A latency breach MUST NOT touch `PrimitiveAccessStatus` — in particular not the `Failure` flag or `FailureReason`, which mean "value untrustworthy" and drive the `is-invalid` UI state; a breach means the value is late, a different semantic. The request SHALL still complete normally (no hard timeout). Per-chunk and per-primitive latency recording are intentionally not provided (minimal surface).

#### Scenario: Breach is recorded, not failed
- **WHEN** a `UserInterface` batch's duration exceeds the latency budget
- **THEN** a warning is logged with the measured latency and the connector aggregates update, and the requests complete normally

#### Scenario: Custom budget value applies
- **WHEN** the application sets the latency budget to a custom value (e.g. 500 ms or 5000 ms)
- **THEN** breach detection uses that value instead of the 1500 ms default

#### Scenario: Whole-batch breach detected despite fast chunks
- **WHEN** a cyclic `UserInterface` read of many chunks completes each chunk quickly but the full batch exceeds the budget
- **THEN** a breach is logged with the total duration and chunk count

#### Scenario: Breach does not poison the failure indicator
- **WHEN** a `UserInterface` batch breaches the latency budget while its PLC accesses succeeded
- **THEN** `AccessStatus.Failure` remains `false` and Blazor controls bound to it do not enter the `is-invalid` state

### Requirement: Zero-delay batches dispatch chunks concurrently
When a batch's effective `interChunkDelay` is 0, the connector SHALL enqueue all its chunks immediately and await their joint completion, allowing chunks to run on multiple workers concurrently. When the effective delay is greater than 0, chunks SHALL be enqueued serially with the delay elapsing between enqueues.

#### Scenario: UserInterface batch spreads across workers
- **WHEN** a `UserInterface` batch of multiple chunks is dispatched with zero inter-chunk delay and several workers are free
- **THEN** its chunks execute concurrently rather than one at a time

#### Scenario: Low batch keeps serial pacing
- **WHEN** a `Low` batch with a 500 ms inter-chunk delay is dispatched
- **THEN** chunks are enqueued one at a time with the delay between enqueues

### Requirement: UserInterface bulk requests are bounded in size
The default `BatchSettings` for the `UserInterface` class SHALL specify a finite chunk size (tuned against the real controller) so that no single `UserInterface` bulk request can monopolize a dispatch slot beyond the latency budget. The whole-set-in-one-request behavior SHALL no longer apply to the `UserInterface` class.

#### Scenario: Large cyclic set is chunked
- **WHEN** the cyclic read set is larger than the `UserInterface` default chunk size
- **THEN** it is dispatched as multiple bounded bulk requests instead of one unbounded request

### Requirement: Work-item failures propagate to the awaiting caller and spare the worker
An exception thrown by a dispatched work item SHALL be observed by the caller awaiting that item (preserving `CommExceptionBehaviour.ReThrow` semantics) and SHALL NOT terminate the worker loop or affect other queued items.

#### Scenario: Caller observes the failure
- **WHEN** a dispatched work item throws
- **THEN** the task returned to the enqueuing caller faults with that exception

#### Scenario: Worker survives a failure
- **WHEN** a work item throws
- **THEN** the worker continues and dispatches the next queued item

### Requirement: Dispatching pauses during re-authentication
While the connector re-authenticates (`ReLoginToConnectorApi`), the dispatcher SHALL stop dequeueing, SHALL let in-flight items finish, and SHALL retain queued items. After successful re-login it SHALL resume and drain the backlog in priority order. Comm-failure handling (including triggering re-login) MUST run in the awaiting caller's context, never inside a dispatched work item — otherwise the pause drain would wait on the very item that is waiting for the pause (self-deadlock).

#### Scenario: Pause holds queued work
- **WHEN** the dispatcher is paused while items are queued
- **THEN** no new items are dispatched, in-flight items run to completion, and queued items are retained (subject to the paused-TTL below)

#### Scenario: Resume drains in priority order
- **WHEN** the dispatcher is resumed with a mixed-class backlog
- **THEN** the backlog is dispatched honoring class precedence and aging

### Requirement: Queued items fault after a TTL while paused
While the dispatcher is paused, any queued item whose time spent in the paused state exceeds a configurable TTL (`PausedEnqueueTtl`, default 10 s, settable to an arbitrary positive value) SHALL fault with a clear "connector re-authenticating" error so callers see a clean failure instead of an indefinite hang. Items that survive a short pause (within TTL) SHALL dispatch normally on resume. Re-authentication MAY legitimately last hours (PLC in STOP, program download) — the TTL converts that into bounded caller waits and bounded queue growth.

#### Scenario: Short pause is transparent
- **WHEN** the dispatcher resumes within the TTL
- **THEN** queued items dispatch normally and callers observe only added latency

#### Scenario: Long pause faults waiting callers cleanly
- **WHEN** the pause lasts longer than the TTL
- **THEN** queued items past their TTL fault with a re-authentication error and the queue does not grow unbounded

### Requirement: Re-login is resilient and single-flight
`ReLoginToConnectorApi` SHALL be truly asynchronous (no synchronous blocking on the caller's thread — no `.Wait()`/`.Result`), SHALL retry on failure with logging instead of terminating (a thrown exception MUST NOT leave the connector permanently suspended), and SHALL be single-flight: concurrent triggers (e.g. `PermissionDenied` on several in-flight chunks at once) SHALL await one shared re-login attempt rather than starting parallel loops.

#### Scenario: Concurrent failures trigger one re-login
- **WHEN** several chunks fail with `PermissionDenied` at the same time
- **THEN** exactly one re-login loop runs and all triggers await its completion

#### Scenario: Re-login failure does not abandon the connector
- **WHEN** a re-login attempt throws (network error, PLC unreachable)
- **THEN** the failure is logged and the loop retries; the connector is never left suspended with no recovery path

#### Scenario: Caller thread is not blocked
- **WHEN** re-login is triggered from a UI context (e.g. Blazor circuit)
- **THEN** the calling thread is not blocked by re-login delays or PLC mode polling

### Requirement: Inter-chunk delay does not occupy a dispatch slot
The `interChunkDelay` configured for a priority class (via `BatchSettings`) or supplied with `Custom` SHALL elapse between enqueues on the caller side and SHALL NOT hold a worker or count against the concurrency cap.

#### Scenario: Low's delay does not block other traffic
- **WHEN** a `Low` batch with a 500 ms inter-chunk delay is being processed and a `Normal` chunk is enqueued during the delay
- **THEN** the `Normal` chunk dispatches immediately on a free worker instead of waiting for the delay to elapse

### Requirement: Existing behavior is preserved for unchanged callers
Public connector signatures, `eAccessPriority`, `BatchSettings` semantics, and chunk-size resolution SHALL remain unchanged. When all traffic uses a single priority class, dispatch order SHALL be FIFO-equivalent to the previous semaphore behavior. Byte-length request splitting (`ApiRequestSplitterByBytes` inside the request handler) SHALL remain in place; a dispatched work item that the handler splits into multiple HTTP requests SHALL execute them within that item's single worker slot.

#### Scenario: Single-class traffic behaves as before
- **WHEN** all enqueued chunks share one priority class
- **THEN** they dispatch FIFO with at most `ConcurrentRequestMaxCount` in flight, matching prior connector behavior

#### Scenario: Oversized bulk request still splits
- **WHEN** a dispatched chunk's bulk request exceeds the WebAPI byte-length limit
- **THEN** the request handler splits it as today and the resulting HTTP requests run sequentially within the same worker slot
