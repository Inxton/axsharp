# Tasks: webapi-priority-dispatcher

## 1. Dispatcher unit tests (red)

- [ ] 1.1 Create `PriorityRequestDispatcherTests.cs` in `src/AXSharp.connectors/tests/AXSharp.Connector.Sax.WebAPITests` (no PLC needed) with TCS-gated fake work items and an injectable clock/`AgingIntervalMs`
- [ ] 1.2 Test: FIFO within a class; higher class dequeues before queued lower class; `Custom` schedules as `Normal`
- [ ] 1.3 Test: in-flight work items never exceed N under saturation, across pause/resume transitions, and after a work-item exception (max-observed-parallelism assertion; any violation is a blocker)
- [ ] 1.4 Test: N greater than 4 is clamped to 4 workers
- [ ] 1.5 Test: aging — a `Low` item enqueued under a sustained `Normal` stream completes within the aging-derived bound; effective class is capped at `Normal` (aged item never outranks a fresh `UserInterface` item)
- [ ] 1.6 Test: work-item exception faults the awaiting caller's task; worker survives and processes the next item
- [ ] 1.7 Test: `PauseAsync` stops dequeueing, in-flight completes, queued items retained unfailed; `Resume` drains backlog in priority order
- [ ] 1.8 Test: reserved slot (structural, design D8) — with N−1 `Normal`/`Low` items in flight and more queued, an arriving `UserInterface` item dispatches immediately; queued `Normal`/`Low` items never occupy the Nth slot; N=1 fallback: single worker serves all classes (reservation void, background not starved)
- [ ] 1.9 Test: latency budget — whole-batch duration (batch start → last chunk complete) over budget triggers the breach callback with queue-wait/execution-time split; under-budget batches do not; custom budget values (e.g. 500 ms, 5000 ms) honored; breached requests still complete successfully
- [ ] 1.10 Test: zero-delay batch enqueues all chunks concurrently (chunks observed in flight simultaneously when workers free); delay > 0 batch enqueues serially with delay between enqueues
- [ ] 1.11 Test: whole-batch latency — multi-chunk `UserInterface` batch whose chunks are each under budget but whose total exceeds it triggers a whole-batch breach with total duration and chunk count
- [ ] 1.12 Test: paused-TTL — pause shorter than TTL → queued items dispatch on resume; pause longer than TTL → items past TTL fault with re-authentication error (callers observe clean failure, queue bounded); custom `PausedEnqueueTtl` honored

## 2. Dispatcher implementation (green)

- [ ] 2.1 Implement `PriorityRequestDispatcher` in `src/AXSharp.connectors/src/AXSharp.Connector/Connector/PriorityRequestDispatcher.cs`: four per-class FIFO queues, work item = `Func<Task>` + enqueue timestamp + `TaskCompletionSource`, `EnqueueAsync` returns the TCS task
- [ ] 2.2 Implement N worker loops (N = `ConcurrentRequestMaxCount`, clamp ≤ 4); workers are the sole concurrency mechanism and never sleep
- [ ] 2.3 Implement aging dequeue: effective class = declared class − ⌊waitMs / AgingIntervalMs⌋ (default 1000 ms), capped at `Normal`; O(4) head comparison, tie → oldest
- [ ] 2.4 Implement `PauseAsync()`/`Resume()` per design D6, including paused-TTL (`PausedEnqueueTtl`, default 10 s, settable; expired items fault with "connector re-authenticating")
- [ ] 2.5 Implement reserved slot (design D8): `Normal`-and-below limited to N−1 slots; `UserInterface`/`High` may use all N
- [ ] 2.6 Implement latency tracking (design D9): enqueue timestamp, settable budget, breach callback carrying queue-wait vs. execution-time
- [ ] 2.7 All tests from group 1 pass

## 3. WebApiConnector wiring

- [ ] 3.1 Route each chunk in `ReadBatchAsync` through `dispatcher.EnqueueAsync(() => RetryPolicy.ExecuteAsync(…ApiBulkAsync…), priority)`; **materialize request payloads inside the work-item lambda at dispatch time (design D12)** — work items carry primitive refs, `PlcReadRequestData`/`PlcWriteRequestData` resolved on the worker; keep chunk-size resolution and `BatchSettings` untouched; do not touch `ApiRequestSplitterByBytes`/request-handler construction
- [ ] 3.1a Test: write chunk enqueued, value re-assigned before dispatch (gate workers with TCS) → wire payload carries the newer value; repeated assignments within a cycle produce a single pending entry and one request
- [ ] 3.2 Same for `WriteBatchAsync`
- [ ] 3.3 Move `interChunkDelay` to caller-side `await Task.Delay` between enqueues (never inside a worker slot); zero-delay batches enqueue all chunks and `await Task.WhenAll` (design D5); whole-batch duration measured for telemetry
- [ ] 3.4 Remove `antiThrottlingSemaphore`, `AntiThrottling()`, `ReleaseConcurrent()` (leave the `[Obsolete]` counter-based pair at l.153–183 as-is)
- [ ] 3.5 `ReLoginToConnectorApi`: `await dispatcher.PauseAsync()` on entry, `Resume()` after PLC back in `Run` (alongside existing `IsRwLoopSuspended` flips)
- [ ] 3.5a Harden `ReLoginToConnectorApi` (design D6): convert to true async (`await Task.Delay`/`await PlcReadOperatingMode()`, no `.Wait()`/`.Result`); replace `catch { throw ex; }` with log-and-retry (loop never exits into permanent suspension); add single-flight gate so concurrent `PermissionDenied` triggers await one shared re-login
- [ ] 3.5b Keep `HandleCommFailure`/re-login trigger strictly in the awaiting caller's context (never inside a dispatched work item) — guard with a unit test that a work-item-context relogin attempt cannot deadlock `PauseAsync` drain
- [ ] 3.6 Add public `UserInterfaceLatencyBudget` property (default 1500 ms, arbitrary positive value); wire dispatcher breach callback to `Logger` warning + connector aggregates (`LastUiBatchDurationMs`, `UiLatencyBreachCount`, INPC) — no `PrimitiveAccessStatus` changes; unit test asserts a breach never touches `AccessStatus` (`Failure`/`FailureReason` unchanged, Blazor `is-invalid` stays off)
- [ ] 3.7 Set finite default chunk size for `BatchSettings[UserInterface]` (initial 250, design D10); note the shaping-default change for release notes

## 4. Cyclic write delivery (missed-writes fix, design D11)

- [ ] 4.1 Test (red): value assigned to `Cyclic` while a cyclic write batch is in flight is written in the next cycle (not wiped); newly dirtied primitive outside the snapshot survives the batch completion
- [ ] 4.2 Test (red): failed batch returns snapshot primitives to pending (retried next cycle); failure + concurrent re-assignment leaves primitive pending once with newest value winning
- [ ] 4.3 Implement swap-don't-clear: `Interlocked.Exchange` on the pending-write set in `CyclicWrite`; remove `ClearPeriodicWriteSet`; failure re-merge via `TryAdd`; adds target the current dictionary instance (volatile read)
- [ ] 4.4 Test + implement: entering `ReLoginToConnectorApi` clears the pending-write set with a warning log listing dropped symbols; writes assigned after re-login behave under the normal guarantee (read set and `protected ClearPeriodicReadSet` stay untouched — minimalism/compat)
- [ ] 4.5 All group-4 tests green

## 5. Verification

- [ ] 5.1 `dotnet test src/AXSharp.connectors` — dispatcher unit tests plus existing offline tests (e.g. `WebApiConnectorFactoryTests`) green
- [ ] 5.2 Real controller (user wires S7-1500 with testing project pre-loaded): run hardware-dependent suites (WebAPI connector tests + `src/tests.integrations/integrated` as applicable, using the projects' existing connection-settings convention) — gate the change on these
- [ ] 5.3 On real controller: no stalls at `maxConcurrentRequest = 4`; mixed-priority load (cyclic set + concurrent direct reads/writes + a `Low` batch) stable; `Low` latency bounded by aging; forced `PermissionDenied` re-login pauses and resumes cleanly
- [ ] 5.4 On real controller: `UserInterface` items dispatch immediately under background saturation (reserved slot observable); whole-batch duration stays within the latency budget under mixed load; tune `UserInterface` default chunk size so a single bulk request stays well under budget; observe PLC behavior under the new wire profile (up to N concurrent chunks vs. one sequential request); measure direct-call (`Normal`) latency before/after under sustained cyclic load
- [ ] 5.4a Public-API diff gate: compare the public surface of `AXSharp.Connector` and `AXSharp.Connector.S71500.WebAPI` against the previous release (ApiCompat / PublicApiAnalyzer or `dotnet` API baseline) — only additive changes allowed
- [ ] 5.5 On real controller: hammer `Cyclic` writes during heavy cyclic read load — every assigned value (or newer) reaches the PLC; no missed writes under forced comm failures
- [ ] 5.6 Manual sanity in sandbox Blazor app: cyclic refresh unchanged, no throughput regression with defaults
