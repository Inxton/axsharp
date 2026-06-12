// AXSharp.Connector.S71500.WebAPITests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AXSharp.Connector;
using AXSharp.Connector.S71500.WebApi;
using Xunit;

namespace AXSharp.Connector.S71500.WebAPITests
{
    /// <summary>
    ///     Offline tests for the cyclic-write-delivery guarantee (swap-don't-clear, failure re-merge,
    ///     re-login drop) and the dispatch-time payload materialization. No PLC required.
    /// </summary>
    public class CyclicWriteDeliveryTests
    {
        private class StubConnector : Connector
        {
            public List<List<ITwinPrimitive>> Batches { get; } = new();
            public Func<List<ITwinPrimitive>, Task> OnWriteBatch { get; set; }

            public override Connector BuildAndStart() => this;

            public override Task ReadBatchAsync(IEnumerable<ITwinPrimitive> primitives,
                eAccessPriority priority = eAccessPriority.Normal, int chunkSize = 250, int interChunkDelay = 250)
                => Task.CompletedTask;

            internal override Task ReadBatchAsyncCyclic(IEnumerable<ITwinPrimitive> primitives,
                eAccessPriority priority = eAccessPriority.Normal, int chunkSize = 250, int interChunkDelay = 250)
                => Task.CompletedTask;

            public override Task WriteBatchAsync(IEnumerable<ITwinPrimitive> primitives,
                eAccessPriority priority = eAccessPriority.Normal, int chunkSize = 250, int interChunkDelay = 250)
                => Task.CompletedTask;

            internal override async Task WriteBatchAsyncCyclic(IEnumerable<ITwinPrimitive> primitives,
                eAccessPriority priority = eAccessPriority.Normal, int chunkSize = 250, int interChunkDelay = 250)
            {
                var snapshot = primitives.ToList();
                Batches.Add(snapshot);
                if (OnWriteBatch != null) await OnWriteBatch(snapshot);
            }

            public override void ReloadConnector()
            {
            }

            public override string TargetPlatformMoniker => "stub";

            public Task RunCyclicWrite() => CyclicWrite();
        }

        private static (StubConnector connector, WebApiBool p1, WebApiBool p2) CreateStub()
        {
            var connector = new StubConnector();
            var p1 = new WebApiBool(connector, "", "p1");
            var p2 = new WebApiBool(connector, "", "p2");
            return (connector, p1, p2);
        }

        // ----- 4.1 set-during-in-flight survives ------------------------------------------------

        [Fact]
        public async Task value_assigned_during_inflight_batch_is_written_next_cycle()
        {
            var (connector, p1, p2) = CreateStub();

            var gate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            connector.OnWriteBatch = _ =>
            {
                entered.TrySetResult(true);
                return gate.Task;
            };

            p1.Cyclic = true;
            var firstCycle = connector.RunCyclicWrite();
            await entered.Task;

            // Batch in flight — these assignments must not be wiped by the cycle completing.
            p2.Cyclic = true;
            p1.Cyclic = false;

            gate.TrySetResult(true);
            await firstCycle;

            Assert.Single(connector.Batches);
            Assert.Contains(p1, connector.Batches[0]);

            Assert.Equal(2, connector.NextCycleWriteSet.Count);
            Assert.Contains(connector.NextCycleWriteSet.Values, p => ReferenceEquals(p, p1));
            Assert.Contains(connector.NextCycleWriteSet.Values, p => ReferenceEquals(p, p2));

            connector.OnWriteBatch = null;
            await connector.RunCyclicWrite();

            Assert.Equal(2, connector.Batches.Count);
            Assert.Contains(p1, connector.Batches[1]);
            Assert.Contains(p2, connector.Batches[1]);
            Assert.Empty(connector.NextCycleWriteSet);
        }

        // ----- 4.2 failed batch stays pending ----------------------------------------------------

        [Fact]
        public async Task failed_batch_returns_primitives_to_pending_and_newest_value_wins()
        {
            var (connector, p1, _) = CreateStub();

            connector.OnWriteBatch = _ => throw new InvalidOperationException("comm failure");

            p1.Cyclic = true;
            await Assert.ThrowsAsync<InvalidOperationException>(() => connector.RunCyclicWrite());

            // Failed write stays pending — exactly one entry for the symbol.
            Assert.Single(connector.NextCycleWriteSet);
            Assert.Contains(connector.NextCycleWriteSet.Values, p => ReferenceEquals(p, p1));

            // Concurrent re-assignment must not duplicate the pending entry; the value lives on
            // the primitive, so the retry carries the newest value automatically.
            p1.Cyclic = false;
            Assert.Single(connector.NextCycleWriteSet);
            Assert.Equal(false, ((IWebApiPrimitive)p1).PeekPlcWriteRequestData.Params["value"]);

            connector.OnWriteBatch = null;
            await connector.RunCyclicWrite();

            Assert.Contains(p1, connector.Batches.Last());
            Assert.Empty(connector.NextCycleWriteSet);
        }

        // ----- 4.4 re-login drops pending writes with audit --------------------------------------

        [Fact]
        public void clear_pending_writes_drains_set_and_reports_dropped_symbols()
        {
            var (connector, p1, p2) = CreateStub();

            p1.Cyclic = true;
            p2.Cyclic = true;
            Assert.Equal(2, connector.NextCycleWriteSet.Count);

            var dropped = connector.ClearPendingWrites();

            Assert.Equal(2, dropped.Count);
            Assert.Contains(p1.Symbol, dropped);
            Assert.Contains(p2.Symbol, dropped);
            Assert.Empty(connector.NextCycleWriteSet);

            // Writes after the drop behave under the normal guarantee.
            p1.Cyclic = false;
            Assert.Single(connector.NextCycleWriteSet);
        }

        // ----- 3.1a payload materializes at dispatch time -----------------------------------------

        [Fact]
        public void write_request_payload_reflects_current_cyclic_to_write()
        {
            var connector = new WebApiConnector();
            var primitive = new WebApiBool(connector, "", "valueAtDispatch");
            IWebApiPrimitive api = primitive;

            primitive.Cyclic = true;
            Assert.Equal(true, api.PlcWriteRequestData.Params["value"]);

            primitive.Cyclic = false;
            Assert.Equal(false, api.PlcWriteRequestData.Params["value"]);
        }

        [Fact]
        public async Task queued_write_sends_value_current_at_dispatch_not_at_enqueue()
        {
            var connector = new WebApiConnector();
            var primitive = new WebApiBool(connector, "", "queuedValue");
            IWebApiPrimitive api = primitive;

            var dispatcher = new PriorityRequestDispatcher(1);
            var gate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var blockerStarted = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            _ = dispatcher.EnqueueAsync(() =>
            {
                blockerStarted.TrySetResult(true);
                return gate.Task;
            }, eAccessPriority.Normal);
            await blockerStarted.Task;

            primitive.Cyclic = true; // value at enqueue time

            object capturedPayload = null;
            var queuedWrite = dispatcher.EnqueueAsync(() =>
            {
                // D12: the payload materializes here, on the worker at dispatch time.
                capturedPayload = api.PlcWriteRequestData.Params["value"];
                return Task.CompletedTask;
            }, eAccessPriority.Normal);

            primitive.Cyclic = false; // re-assigned while the work item waits in the queue

            gate.TrySetResult(true);
            await queuedWrite;

            Assert.Equal(false, capturedPayload);

            // Repeated assignments produced a single pending entry on the connector.
            Assert.Single(connector.NextCycleWriteSet);
        }

        // ----- 3.5b pause cannot deadlock on caller-side failure handling ---------------------------

        [Fact]
        public async Task pause_completes_when_relogin_is_triggered_from_callers_context()
        {
            var dispatcher = new PriorityRequestDispatcher(2);

            var gate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var started = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var inFlight = dispatcher.EnqueueAsync(() =>
            {
                started.TrySetResult(true);
                return gate.Task;
            }, eAccessPriority.Normal);
            await started.Task;

            var faulted = dispatcher.EnqueueAsync(() => throw new InvalidOperationException("permission denied"),
                eAccessPriority.UserInterface);

            // Caller context: observe the failure OUTSIDE any work item, then trigger the pause —
            // the drain only waits for the unrelated in-flight item, never for itself.
            await Assert.ThrowsAsync<InvalidOperationException>(() => faulted);
            var pause = dispatcher.PauseAsync();
            Assert.False(pause.IsCompleted); // unrelated item still in flight

            gate.TrySetResult(true);
            var completed = await Task.WhenAny(pause, Task.Delay(TimeSpan.FromSeconds(10)));
            Assert.True(ReferenceEquals(completed, pause), "PauseAsync drain deadlocked.");

            dispatcher.Resume();
            await inFlight;
        }

        // ----- 3.6 latency breach never touches AccessStatus -----------------------------------------

        [Fact]
        public void latency_breach_updates_aggregates_and_never_touches_access_status()
        {
            var connector = new WebApiConnector();
            var primitive = new WebApiBool(connector, "", "breachProbe");

            Assert.False(primitive.AccessStatus.Failure);
            Assert.Equal(0, connector.UiLatencyBreachCount);

            connector.ReportUiLatencyBreach(new UiLatencyBreach(2500, 4, 300, 2200));

            Assert.Equal(1, connector.UiLatencyBreachCount);
            // "Late" is not "untrustworthy": the failure indicator that drives the Blazor
            // is-invalid state must stay untouched.
            Assert.False(primitive.AccessStatus.Failure);
            Assert.True(string.IsNullOrEmpty(primitive.AccessStatus.FailureReason));
        }
    }
}
