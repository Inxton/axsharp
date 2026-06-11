# Design: Priority dispatcher for WebApi connector

## Context

The S7-1500 WebAPI connector throttles all PLC requests through `antiThrottlingSemaphore` (`WebApiConnector.cs:352`), a plain FIFO `SemaphoreSlim` sized by `ConcurrentRequestMaxCount` (clamped ≤ 4 in `Connector.cs:221`). `eAccessPriority` only selects chunk shape via `BatchSettings` (`Connector.cs:93`); it has no effect on ordering. Consequences:

- `High`, `UserInterface`, `Normal` are behaviorally identical — `BatchSettings` maps them all to `(null, null)`.
- `Low`'s 500 ms `interChunkDelay` runs inside the semaphore hold (`WebApiConnector.cs:444–462`): background work occupies 1 of ≤ 4 slots doing nothing.
- A `High` request arriving while `Low` chunks queue waits like everyone else.

Hard environmental constraint: the PLC WebAPI safely accepts only a limited number of concurrent requests (typically 4); exceeding it stalls requests and destabilizes the PLC. Request payloads also have a byte-length limit, handled today by `ApiRequestSplitterByBytes` wired into `ApiHttpClientRequestHandler` (both constructors, `WebApiConnector.cs:69/73` and `120/121`).

**The cyclic loop is also the polling transport for rendered variables.** Blazor `RenderableContent` holders subscribe primitives with an interval; per-interval tasks (`Polling.cs:61`) call `primitive.Poll()` → `AddToNextPeriodicReadSet(this)` (`OnlinerBase.cs:166`), so polled variables flow through `NextPeriodicReadSet` and ride the same `UserInterface`-class cyclic batch. (`Subscribed`/`StartSubscriptionPolling` is a separate, not-cyclic-wired path — commented line `Connector.cs:487`.) Three consequences: (1) polled variables are "user is looking at this screen right now" traffic, which justifies keeping the cyclic loop on the `UserInterface` class; (2) whole-batch latency telemetry (D9) measures exactly what rendered controls show — effective on-screen staleness ≈ poll interval + batch duration, so the budget bounds user-perceived freshness; (3) a poll arriving during an in-flight cyclic read and wiped by `ClearPeriodicReadSet` self-heals — the poll task re-adds it next interval, worst case one missed tick.

Decisions already made with the user: keep the current priority-class mapping (cyclic loop = `UserInterface`, direct calls = `Normal`); aging as the starvation policy; TDD; full backward compatibility. Additional user constraints settled during review: **`UserInterface` items must never queue behind lower-priority work**, and `UserInterface` enqueue-to-completion latency is monitored against a **settable budget (default 1500 ms, arbitrary positive value)** — breaches are logged, never failed (no hard timeout).

## Goals / Non-Goals

**Goals:**
- Real priority scheduling at the single choke point where chunks reach `RequestHandler.ApiBulkAsync`.
- Zero public-API change; behavior identical to today when all traffic is one class.
- Concurrency cap never exceeded, even transiently — PLC stability is the binding constraint.
- Bounded worst-case wait for low-priority work (aging).
- `UserInterface`/`High` work dispatches immediately — never queued behind `Normal`/`Low`/aged items.
- `UserInterface` latency observable against a user-settable budget (default 1.5 s).
- Dispatcher pauses during re-login so queued work doesn't burn Polly retries against a dead session.

**Non-Goals:**
- Remapping which priority class the cyclic loop or direct `GetAsync`/`SetAsync` calls use (explicit user decision: keep current mapping; revisit later).
- Fixing the lost-writes `Clear()` bug in `CyclicWrite`, the no-op `ReloadConnector`, or the bare-catch in the RW loop (separate follow-ups).
- Changing chunk-size resolution, `BatchSettings` semantics, or the byte-length request splitting.
- Per-primitive (per-tag) priorities.

## Decisions

### D1: Dispatcher as a pure, transport-free class in the base project
`PriorityRequestDispatcher` lives in `src/AXSharp.connectors/src/AXSharp.Connector/Connector/`, holding only `Func<Task>` work items — no HTTP, no Siemens API types. Rationale: unit-testable without a PLC (TDD requirement), reusable by other connector implementations later. Alternative considered: WebAPI-project-internal class — rejected, no upside, blocks reuse.

### D2: Workers ARE the concurrency cap (semaphore removed)
N worker loops (N = `ConcurrentRequestMaxCount`) dequeue and execute work items; `antiThrottlingSemaphore`/`AntiThrottling()`/`ReleaseConcurrent()` are deleted. Rationale: one mechanism instead of two; the ≤ N in-flight invariant becomes structural rather than protocol-based (no forgot-to-release class of bug). Alternative: keep semaphore + priority queue in front — rejected, two coupled mechanisms, delay-while-holding-slot bug stays possible.

### D3: Per-class FIFO queues + O(4) head comparison with aging capped at `Normal`
Four FIFO queues (`High`, `UserInterface`, `Normal`, `Low`); `Custom` schedules as `Normal` (its param-pass-through semantics live entirely in batch shaping, unchanged). At dequeue: effective class = declared class − ⌊waitMs / AgingIntervalMs⌋ (default 1000 ms/step), **capped at `Normal`** — aged work never reaches `UserInterface`/`High` effective class, so it cannot contest the reserved UI capacity (D8) or front-run genuine UI traffic via the oldest-first tie-break. Compare the four queue heads, pick best effective class, tie → oldest. Starvation still bounded: `Low` reaches `Normal` in one interval, FIFO-oldest from there. Rationale for heads-not-heap: aging changes effective priority continuously — a heap would need re-keying; comparing 4 heads is O(4) per dequeue and trivially testable. Alternative: `PriorityQueue` keyed (class, seq) — rejected, cannot age without re-heaping. Alternative: strict priority — rejected by user (starvation); weighted drain — rejected (tuning knob, muddier latency guarantees); aging to the top — rejected (aged backlog would systematically outrank fresh UI clicks).

### D4: Callers await a per-item `TaskCompletionSource`
`EnqueueAsync(workItem, class)` returns the TCS task. Preserves existing await semantics: `CyclicWrite` still completes before `CyclicRead` each cycle (write-before-read ordering); exceptions propagate via `TCS.SetException`, so `CommExceptionBehaviour.ReThrow` reaches the original caller; a faulted work item must not kill its worker loop.

### D5: `interChunkDelay` between enqueues, never inside a worker — and concurrent chunks when delay is zero
`ReadBatchAsync`/`WriteBatchAsync` apply `await Task.Delay(interChunkDelay)` caller-side between successive chunk enqueues. Structurally fixes the `Low`-holds-slot defect — workers never sleep.

Chunk dispatch rule within one batch call: **`interChunkDelay == 0` → enqueue all chunks at once and `await Task.WhenAll` their completions; `interChunkDelay > 0` → enqueue serially with the delay between enqueues.** Rationale: with D10's finite UI chunk size, a large cyclic read becomes many chunks — serial dispatch (today's behavior) makes whole-set refresh ≈ numChunks × chunkTime (e.g. 20 × 100 ms = 2 s, blowing the latency budget on big projects), while concurrent dispatch spreads chunks across workers (≈ ÷N). `Low` keeps its gentle serial pacing via its 500 ms delay. Backpressure is unaffected: the cyclic loop still awaits the whole batch before the next cycle, so no cross-cycle pile-up (the 10 ms `ReadWriteCycleDelay` sits between completions, not on a fixed-rate tick). Trade-off: during a cyclic burst, UI chunks may briefly occupy all N slots — `Normal`/`Low` get slots between chunk completions; acceptable, UI responsiveness is the stated priority.

### D6: Pause/resume instead of fail-fast during re-login — with paused-TTL and a hardened re-login
`PauseAsync()` stops dequeueing and waits for in-flight items to finish; queued items stay queued. `ReLoginToConnectorApi` pauses on entry, resumes after the PLC is back in `Run`. Rationale: queued work hammering a dead session burns 5× Polly retries per item and floods logs. Alternative: cancel queued items immediately — rejected, callers would see spurious failures for a transient auth event.

**Paused-TTL (decided with user):** re-login can legitimately last hours (PLC in STOP, operator downloading a program). Without a bound, direct UI calls enqueued during the pause hang indefinitely and the queue grows unbounded. While paused, any queued item whose paused-wait exceeds `PausedEnqueueTtl` (default 10 s, settable) faults with a clear "connector re-authenticating" error. Short relogins stay transparent (items within TTL dispatch on resume); long outages produce clean, bounded failures instead of eternal spinners. Alternatives: accept-and-document — rejected (unbounded queue + hung UI for hours); hard queue cap — rejected (arbitrary rejection order, no time semantics).

**No-self-deadlock constraint:** comm-failure handling (`HandleCommFailure`, including the re-login trigger) must run in the awaiting caller's context, never inside a dispatched work item — `PauseAsync` drains in-flight items, and a work item that itself waits for pause would deadlock the drain. D4's TCS propagation makes the caller's `catch` the natural home; this is a hard constraint, not a style choice.

**Re-login hardening (existing defects, lethal once pause exists):**
1. *Truly async:* `ReLoginToConnectorApi` (`WebApiConnector.cs:217–249`) is `async Task` with zero awaits — `Task.Delay(2000).Wait()` and `.Result` make the whole loop run synchronously on the caller's thread before the method returns its Task (an async method runs synchronously until its first await). From a Blazor circuit this freezes the UI for seconds or forever. Convert to `await Task.Delay` / `await PlcReadOperatingMode()`.
2. *Never rethrow:* the current `catch (Exception ex) { throw ex; }` (l.244–247) exits the retry loop inside a fire-and-forget Task → exception unobserved, `IsRwLoopSuspended` stuck `true` forever — and with pause, a frozen dispatcher and every queued caller awaiting indefinitely. Re-login failures are logged and retried; the loop never terminates the connector into a dead state.
3. *Single-flight:* `PermissionDenied` hits all in-flight chunks at once → today each `HandleCommFailure` fires its own re-login loop (session churn, `IsRwLoopSuspended` flapping). Gate: first trigger runs the loop, concurrent triggers await the same task.

### D7: Dispatcher sits ABOVE the request handler — splitting untouched
The dispatcher wraps calls *to* `RequestHandler.ApiBulkAsync`; `ApiRequestSplitterByBytes` keeps handling the WebAPI byte-length limit below it. One dispatched work item may expand into several sequential HTTP requests inside the handler — all within that item's single worker slot, so ≤ N in-flight HTTP requests still holds. Do not move, replace, or re-implement the splitter.

### D8: Reserved slot — `UserInterface` never queues behind lower classes (structural model)
Items of effective class `Normal` or lower occupy at most N−1 of the N worker slots; one slot only ever serves `UserInterface`/`High`. An arriving UI/High item therefore dispatches immediately unless all N slots already hold UI/High work — the only queueing UI can experience is behind its own class, which the latency telemetry (D9) surfaces. Rationale: UI cannot bypass the concurrency cap (hard PLC constraint, D2), so "no queuing" is delivered by reserving capacity, not by exceeding it. Works only together with the aging ceiling (D3) — otherwise aged items would consume the reserved slot.

**Implementation model: structural reservation by worker role (not counted admission).** Workers 1..N−1 are *general* — they serve the best effective head across all four queues (class order, aging, oldest tie-break). Worker N is *dedicated* — it serves only the `UserInterface`/`High` queues, else idles. The lower-lane bound (≤ N−1) follows from worker count, not from runtime accounting: no `inflightLower` counter, no atomic check-and-claim, no admission race to test for. Same worker code; role = predicate over which queues the worker may dequeue from. Counters exist only for `PauseAsync` drain tracking and telemetry. Alternative considered: uniform workers + counted admission (`inflightLower < N−1` check at dequeue) — rejected: check-and-claim must be atomic under a shared lock and a missed pulse can strand admissible work; the structural model removes both bug habitats.

**Wake-up scheme:** single lock over queue state; enqueue, completion, and resume kick all sleeping workers; each worker re-checks its allowed queues in a loop (spurious wakeups harmless, ≤ 4 workers → contention negligible). Async kick (semaphore-based) rather than `Monitor.Wait` so pool threads are not blocked while idle.

**Edge case N = 1** (`ConcurrentRequestMaxCount` clamps to ≥ 1): structural reservation would starve background work forever (the only worker would be dedicated). Fallback: with N = 1 the single worker is general and the reservation is void — UI can wait at most one in-flight lower-class request (bounded by one bounded chunk, D10); the latency telemetry (D9) makes any resulting breach visible. Documented limitation, not an error.

Other rejected alternatives: full cap bypass for UI — violates PLC stability; preempting in-flight requests — impossible, requests on the wire cannot be cancelled PLC-side safely; gate-at-enqueue with stacked semaphores — semaphore FIFO defeats priority ordering among waiters. Cost: ~1/N background throughput while UI traffic is active; dedicated worker idles when no UI work exists.

### D9: Settable `UserInterface` latency budget with log-only breach handling
New public property `UserInterfaceLatencyBudget` (default 1500 ms, any positive value). Dispatcher stamps enqueue time; on completion of a `UserInterface` item, if enqueue-to-completion exceeds the budget: log a warning with measured latency. The request still completes — no hard timeout, because a scheduler only bounds queueing delay; a slow PLC response can breach any budget with an empty queue, and failing the user's request on that would turn transient slowness into user-visible errors. Verified against the real controller in the hardware test pass.

**Breach recording shape — never via `Failure`, connector aggregates only (minimalism pass).** `PrimitiveAccessStatus.Update(cycle, reason)` with any non-empty reason sets `Failure = true`, which Blazor `TemplateBase` maps directly to the Bootstrap `is-invalid` state (`TemplateBase.razor.cs:115`) — recording breaches there would paint whole screens red for a soft performance signal. Latency breaches therefore do not touch `PrimitiveAccessStatus` at all: warning log (with queue-wait vs. execution split) + connector-level aggregates next to the existing perf surface (`CyclicRwDuration`, Connector.cs:162): `LastUiBatchDurationMs`, `UiLatencyBreachCount` (INPC). Measurement is **whole-batch level** (batch start → last chunk complete; a direct call is a one-chunk batch) — the user-perceived signal; per-chunk and per-primitive recording were considered and trimmed as gold-plating (no requirement behind them). Hard rule: `Failure` = value untrustworthy (comm), breach = value late — distinct semantics, distinct UI surfaces.

### D10: Finite default chunk size for the `UserInterface` class
`BatchSettings[UserInterface]` gets a finite default chunk size (initial value 250, tuned on the real controller) instead of `null` → whole-set-in-one-request. A single unbounded cyclic bulk read could hold its own slot past the latency budget — the reserved slot cannot protect UI traffic from itself. This is a deliberate shaping-default change (documented in release notes); applications can still override `BatchSettings` as today.

### D11: Missed-writes fix — swap the dirty-set, never `Clear()`
`CyclicWrite` currently snapshots `NextCycleWriteSet.Values`, awaits the batch, then `Clear()`s the whole dictionary (`Connector.cs:517–523`). A `Cyclic` assignment landing between snapshot and clear is wiped without ever being written. The pending value lives on the primitive itself (`CyclicToWrite`), so the dictionary is purely a dirty-set — the fix only has to preserve *membership*, no value versioning needed:

1. **Swap, don't clear:** `Interlocked.Exchange` the pending-write dictionary with a fresh empty one; the drained snapshot is exact, and setters during the in-flight batch land in the new dictionary → written next cycle. The wipe window disappears.
2. **Failure re-merge:** if the batch fails, re-add the snapshot's primitives to the current dictionary via `TryAdd` (newest-wins is automatic — a re-dirtied primitive is already present and its `CyclicToWrite` holds the latest value). A dirty primitive stays pending until a successful write lands after its most recent assignment.
3. **Convergence:** an in-flight batch may carry an older value to the PLC; the newest value follows in the next cycle — last-writer-wins.

Guarantee delivered (user requirement): every `CyclicToWrite` assignment is followed by at least one successful PLC write of that primitive carrying that-or-newer value — with one audited exception below. Scope (minimalism pass): the swap applies to the **write set only**; `NextPeriodicReadSet` keeps its existing `Clear()` — a read wiped mid-flight self-heals via polling/auto-subscribe re-add (see Context), so the read path stays untouched (also leaves the `protected ClearPeriodicReadSet` API exactly as-is). Implementation note: the write dictionary property gets a swappable backing field; `AddToPeriodicWriteSet` must target the *current* instance (volatile read).

**Stale-write hazard resolution (decided with user): re-login clears pending writes.** The guarantee + hour-long re-login (PLC STOP, program download) would otherwise compose into resurrection: a setpoint assigned before the outage fires into a freshly downloaded program an hour later. On entering `ReLoginToConnectorApi`, the pending-write set is cleared with a warning log listing dropped symbols — the guarantee covers transient comm failures, not session loss. One-liner in the re-login path; alternatives (absolute guarantee, per-write staleness TTL) rejected as hazardous resp. anti-minimal.

### D12: Request payloads materialize at dispatch time, not enqueue time
Work items carry *primitive references*, never pre-built request payloads. The `Select(p => p.PlcWriteRequestData)` / `PlcReadRequestData` resolution moves **inside the work-item lambda**, executing on the worker at send time. Consequence: a write chunk that waited in the queue (busy workers, paused dispatcher) always goes on the wire with the onliner's *current* `CyclicToWrite` — "last value wins" holds end-to-end, not just in the dirty-set. Combined with per-symbol coalescing (one pending entry per variable in `NextCycleWriteSet`/`NextPeriodicReadSet`, plus `Distinct()` in batches), repeated accesses never queue duplicates and never send stale snapshots. Rationale: building payloads at enqueue would freeze values at queueing time — directly violating the user requirement that accesses use the onliner's last value. Cross-batch duplicates (direct call overlapping a cyclic batch on the same symbol) remain possible and harmless — both carry the latest value at their respective send times.

## Risks / Trade-offs

- [Transient cap overshoot during pause/resume or worker restart would destabilize the PLC] → invariant unit test asserting max observed parallelism ≤ N under stress, across pause/resume and after work-item exceptions; treat any violation as a blocker.
- [Behavior change: `Low` no longer holds a slot during its delay → other traffic gets more throughput; deployments tuned around old timing may notice] → documented as a fix in release notes; default all-one-class traffic is otherwise byte-for-byte FIFO-equivalent.
- [Behavior change: `UserInterface` default chunk size becomes finite (D10) → large cyclic sets go out as several requests instead of one] → tune the default on the real controller; `BatchSettings` override path unchanged for applications that need the old shape.
- [Reserved slot idles when no UI traffic exists → up to 1/N throughput loss for pure background workloads] → accepted; UI responsiveness is the stated priority. N stays settable (≤ 4).
- [Latency budget breaches caused by slow PLC, not queueing → telemetry could mislead toward scheduler bugs] → breach log includes queue-wait vs. execution-time split so the two causes are distinguishable.
- [Wire-profile change: cyclic was one sequential whole-set request, becomes up to N concurrent 250-chunks — PLC sees a different load shape] → explicit real-controller observation during chunk-size tuning; within the ≤ 4 cap by construction.
- [Direct-call (`Normal`) latency under sustained cyclic load may worsen vs. today's FIFO interleave — strict class precedence until aging promotes at 1 s] → measure before/after direct-call latency on the real controller; bounded by the aging interval.
- [Public-API regression slipping in unnoticed] → public-API diff gate in verification (ApiCompat/PublicApiAnalyzer); the one known `protected` member (`ClearPeriodicReadSet`) is left untouched by scoping the swap to the write set.
- [Aging interval default (1000 ms/step) wrong for some workloads] → exposed as a dispatcher property with a safe default; not on the public connector API in v1.
- [Worker loops are long-lived tasks; connector has no dispose story today] → keep workers on dedicated `Task.Run` loops mirroring the existing `StartReadWriteOps` pattern; full lifecycle/disposal is an existing repo-wide gap, out of scope.
- [Hardware-dependent verification] → user wires an S7-1500 with the testing project pre-loaded; gate on those suites passing.

## Migration Plan

Internal change, no consumer action. Deploy = NuGet package update. Rollback = previous package version. The `[Obsolete]` counter-based throttling pair (`WebApiConnector.cs:153–183`) stays as-is (already dead code).

## Open Questions

- Whether `AgingIntervalMs` should be configurable per connector instance in v1 or fixed default (lean: property on dispatcher, internal). `UserInterfaceLatencyBudget` is decided: public, settable, default 1500 ms.
- Initial `UserInterface` default chunk size (250 starting point) — final value comes from real-controller measurements.

(Resolved: dispatcher unit tests live in `AXSharp.Connector.Sax.WebAPITests` — the only non-legacy connector test project.)
