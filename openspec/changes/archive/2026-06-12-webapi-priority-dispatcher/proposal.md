# Priority dispatcher for WebApi connector (backward-compatible)

## Why

`eAccessPriority` (4 fixed classes + `Custom`) does not actually prioritize anything: it only selects chunk shape via `BatchSettings`, while all requests funnel FIFO through a semaphore. `High`/`UserInterface`/`Normal` are behaviorally identical, and `Low` holds a concurrency slot during its 500 ms inter-chunk sleep — background traffic throttles the foreground work it was meant to yield to.

## What Changes

- New `PriorityRequestDispatcher` in the `AXSharp.Connector` base project: per-class FIFO queues, N worker loops (N = `ConcurrentRequestMaxCount`, clamped ≤ 4), aging-based starvation prevention (capped at `Normal`), pause/resume for re-login.
- `UserInterface` never queues behind lower-priority work: one of the N slots is reserved for `UserInterface`/`High`; `Normal` and below use at most N−1 slots.
- New public `UserInterfaceLatencyBudget` property (settable to any positive value, default 1500 ms): `UserInterface` whole-batch duration exceeding the budget is logged and surfaced via connector telemetry (`LastUiBatchDurationMs`, `UiLatencyBreachCount`) — `AccessStatus`/`Failure` untouched, request still completes (no hard timeout).
- `BatchSettings[UserInterface]` default chunk size becomes finite (initial 250, tuned on real controller) instead of whole-set-in-one-request, so a single bulk read cannot monopolize a slot past the budget.
- Missed-writes fix: `CyclicWrite` swaps the dirty-set (`Interlocked.Exchange`) instead of `Clear()`, and re-adds failed primitives — a `CyclicToWrite` assignment is never silently dropped across transient failures. Single audited exception: entering re-login clears pending writes with a warning listing dropped symbols (prevents stale setpoints resurrecting into changed PLC state after STOP/download).
- `WebApiConnector.ReadBatchAsync`/`WriteBatchAsync` route each chunk through the dispatcher instead of `antiThrottlingSemaphore`; the semaphore path is removed.
- `interChunkDelay` moves out of the throttled section — applied between enqueues, never while a worker slot is held (fixes the `Low`-holds-slot defect).
- `ReLoginToConnectorApi` pauses the dispatcher on entry and resumes after successful re-login, so queued work does not hammer a dead session.
- **Not breaking**: public signatures, `eAccessPriority`, `BatchSettings`, chunk-size resolution, and the byte-length request splitting (`ApiRequestSplitterByBytes`) are unchanged. With all-one-class traffic (today's reality), scheduling degenerates to current FIFO behavior minus the slot-holding sleep.

Out of scope (follow-ups): no-op `ReloadConnector`, bare-catch in the RW loop, priority remapping of the cyclic loop and direct calls.

## Capabilities

### New Capabilities
- `priority-request-dispatching`: scheduling of PLC WebAPI requests by access-priority class — ordering across classes, FIFO within a class, hard concurrency cap, aging against starvation, pause/resume during re-authentication, and exception propagation to awaiting callers.
- `cyclic-write-delivery`: guarantee that every `CyclicToWrite` assignment results in at least one successful PLC write carrying that-or-newer value across transient failures — no writes lost to the snapshot/clear race or to failed batches; pending writes are cleared (with audit log) on re-login.

### Modified Capabilities

(none — no existing specs in `openspec/specs/`)

## Impact

- `src/AXSharp.connectors/src/AXSharp.Connector/` — new `Connector/PriorityRequestDispatcher.cs` (pure, transport-free).
- `src/AXSharp.connectors/src/AXSharp.Connector.S71500.WebAPI/WebApiConnector.cs` — batch read/write wiring, re-login hook, semaphore removal.
- Tests: new dispatcher unit tests (no PLC needed) in the connectors test tree; hardware-dependent suites run against a real S7-1500 wired up by the user with the testing project pre-loaded.
- PLC-side stability constraint: the WebAPI safely accepts only a limited number of concurrent requests (typically 4); the dispatcher must never exceed the configured cap, even transiently.
