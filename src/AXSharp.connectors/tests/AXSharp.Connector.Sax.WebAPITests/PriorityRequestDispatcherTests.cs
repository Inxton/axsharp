// AXSharp.Connector.S71500.WebAPITests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AXSharp.Connector;
using Xunit;

namespace AXSharp.Connector.S71500.WebAPITests
{
    public class PriorityRequestDispatcherTests
    {
        private static readonly TimeSpan TestTimeout = TimeSpan.FromSeconds(20);

        private static async Task WithTimeout(Task task, string description)
        {
            var completed = await Task.WhenAny(task, Task.Delay(TestTimeout));
            Assert.True(ReferenceEquals(completed, task), $"Timed out waiting for: {description}");
            await task;
        }

        /// <summary>Occupies every worker so subsequently enqueued items must queue.</summary>
        private static async Task<TaskCompletionSource<bool>> SaturateAsync(PriorityRequestDispatcher dispatcher)
        {
            var gate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var started = new CountdownEvent(dispatcher.WorkerCount);
            for (var i = 0; i < dispatcher.WorkerCount; i++)
                // UserInterface items are taken by the dedicated worker as well as the generals.
                _ = dispatcher.EnqueueAsync(() =>
                {
                    started.Signal();
                    return gate.Task;
                }, eAccessPriority.UserInterface);

            await Task.Run(() => started.Wait(TestTimeout));
            Assert.Equal(dispatcher.WorkerCount, dispatcher.InFlightCount);
            return gate;
        }

        // ----- 1.2 ordering ---------------------------------------------------------------

        [Fact]
        public async Task higher_class_dequeues_before_queued_lower_class_and_fifo_within_class()
        {
            var dispatcher = new PriorityRequestDispatcher(1) { AgingIntervalMs = 60_000 };
            var gate = await SaturateAsync(dispatcher);

            var order = new ConcurrentQueue<string>();

            Task Track(string name)
            {
                order.Enqueue(name);
                return Task.CompletedTask;
            }

            var low = dispatcher.EnqueueAsync(() => Track("low"), eAccessPriority.Low);
            var normal1 = dispatcher.EnqueueAsync(() => Track("normal1"), eAccessPriority.Normal);
            var high = dispatcher.EnqueueAsync(() => Track("high"), eAccessPriority.High);
            var normal2 = dispatcher.EnqueueAsync(() => Track("normal2"), eAccessPriority.Normal);
            var ui = dispatcher.EnqueueAsync(() => Track("ui"), eAccessPriority.UserInterface);

            gate.TrySetResult(true);
            await WithTimeout(Task.WhenAll(low, normal1, high, normal2, ui), "ordered items");

            Assert.Equal(new[] { "high", "ui", "normal1", "normal2", "low" }, order.ToArray());
        }

        [Fact]
        public async Task custom_schedules_as_normal()
        {
            var dispatcher = new PriorityRequestDispatcher(1) { AgingIntervalMs = 60_000 };
            var gate = await SaturateAsync(dispatcher);

            var order = new ConcurrentQueue<string>();
            var low = dispatcher.EnqueueAsync(() => { order.Enqueue("low"); return Task.CompletedTask; }, eAccessPriority.Low);
            var custom = dispatcher.EnqueueAsync(() => { order.Enqueue("custom"); return Task.CompletedTask; }, eAccessPriority.Custom);
            var normal = dispatcher.EnqueueAsync(() => { order.Enqueue("normal"); return Task.CompletedTask; }, eAccessPriority.Normal);

            gate.TrySetResult(true);
            await WithTimeout(Task.WhenAll(low, custom, normal), "custom/normal/low items");

            // Custom rides the Normal class: FIFO among {custom, normal}, both before low.
            Assert.Equal(new[] { "custom", "normal", "low" }, order.ToArray());
        }

        // ----- 1.3 / 1.4 concurrency cap ---------------------------------------------------

        [Theory]
        [InlineData(4, 4)]
        [InlineData(10, 4)] // 1.4: clamped to 4
        [InlineData(0, 1)]
        public async Task in_flight_never_exceeds_worker_count(int requested, int expected)
        {
            var dispatcher = new PriorityRequestDispatcher(requested);
            Assert.Equal(expected, dispatcher.WorkerCount);

            var current = 0;
            var maxObserved = 0;

            async Task Work()
            {
                var now = Interlocked.Increment(ref current);
                InterlockedExtensions.Max(ref maxObserved, now);
                await Task.Delay(20);
                Interlocked.Decrement(ref current);
            }

            var classes = new[]
            {
                eAccessPriority.Low, eAccessPriority.Normal, eAccessPriority.UserInterface, eAccessPriority.High,
                eAccessPriority.Custom
            };
            var tasks = Enumerable.Range(0, 40).Select(i => dispatcher.EnqueueAsync(Work, classes[i % classes.Length]));

            await WithTimeout(Task.WhenAll(tasks), "saturation batch");
            Assert.True(maxObserved <= expected, $"Observed {maxObserved} concurrent items, cap is {expected}.");
        }

        [Fact]
        public async Task cap_holds_across_pause_resume_and_after_exceptions()
        {
            var dispatcher = new PriorityRequestDispatcher(4) { PausedEnqueueTtlMs = 60_000 };

            var current = 0;
            var maxObserved = 0;

            async Task Work(bool throwAfter)
            {
                var now = Interlocked.Increment(ref current);
                InterlockedExtensions.Max(ref maxObserved, now);
                await Task.Delay(10);
                Interlocked.Decrement(ref current);
                if (throwAfter) throw new InvalidOperationException("intentional");
            }

            var tasks = new List<Task>();
            for (var round = 0; round < 3; round++)
            {
                for (var i = 0; i < 12; i++)
                {
                    var fail = i % 3 == 0;
                    tasks.Add(dispatcher.EnqueueAsync(() => Work(fail),
                        i % 2 == 0 ? eAccessPriority.Normal : eAccessPriority.UserInterface));
                }

                await dispatcher.PauseAsync();
                Assert.Equal(0, dispatcher.InFlightCount);
                dispatcher.Resume();
            }

            foreach (var task in tasks)
                try
                {
                    await WithTimeout(task, "pause/resume batch item");
                }
                catch (InvalidOperationException)
                {
                    // intentional failures
                }

            Assert.True(maxObserved <= 4, $"Observed {maxObserved} concurrent items, cap is 4.");
        }

        // ----- 1.5 aging --------------------------------------------------------------------

        [Fact]
        public async Task low_completes_under_sustained_normal_stream_within_aging_bound()
        {
            var dispatcher = new PriorityRequestDispatcher(1) { AgingIntervalMs = 50 };
            var gate = await SaturateAsync(dispatcher);

            var completionIndex = new ConcurrentQueue<string>();

            Func<Task> Item(string name)
            {
                return async () =>
                {
                    completionIndex.Enqueue(name);
                    await Task.Delay(15);
                };
            }

            var earlierNormals = Enumerable.Range(0, 5)
                .Select(i => dispatcher.EnqueueAsync(Item($"normal-early-{i}"), eAccessPriority.Normal)).ToArray();
            var low = dispatcher.EnqueueAsync(Item("low"), eAccessPriority.Low);
            var laterNormals = Enumerable.Range(0, 20)
                .Select(i => dispatcher.EnqueueAsync(Item($"normal-late-{i}"), eAccessPriority.Normal)).ToArray();

            gate.TrySetResult(true);
            await WithTimeout(Task.WhenAll(laterNormals.Concat(earlierNormals).Append(low)), "aging stream");

            var completed = completionIndex.ToArray();
            var lowPosition = Array.IndexOf(completed, "low");
            // Aged to Normal, the Low item beats Normals enqueued after it (FIFO-oldest within the class) —
            // it must not sink to the end of the sustained stream.
            Assert.True(lowPosition < 10,
                $"Low completed at position {lowPosition} of {completed.Length}; aging did not bound its wait.");
        }

        [Fact]
        public async Task aged_low_never_outranks_fresh_userinterface_item()
        {
            var dispatcher = new PriorityRequestDispatcher(1) { AgingIntervalMs = 20 };
            var gate = await SaturateAsync(dispatcher);

            var order = new ConcurrentQueue<string>();
            var low = dispatcher.EnqueueAsync(() => { order.Enqueue("low"); return Task.CompletedTask; }, eAccessPriority.Low);

            // Let the Low item age far beyond the UserInterface class step-count.
            await Task.Delay(200);

            var ui = dispatcher.EnqueueAsync(() => { order.Enqueue("ui"); return Task.CompletedTask; }, eAccessPriority.UserInterface);

            gate.TrySetResult(true);
            await WithTimeout(Task.WhenAll(low, ui), "aged low vs fresh ui");

            Assert.Equal(new[] { "ui", "low" }, order.ToArray());
        }

        // ----- 1.6 exception isolation -------------------------------------------------------

        [Fact]
        public async Task work_item_exception_faults_caller_and_worker_survives()
        {
            var dispatcher = new PriorityRequestDispatcher(1);

            var faulted = dispatcher.EnqueueAsync(() => throw new InvalidOperationException("boom"),
                eAccessPriority.Normal);
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => faulted);
            Assert.Equal("boom", ex.Message);

            var survived = dispatcher.EnqueueAsync(() => Task.CompletedTask, eAccessPriority.Normal);
            await WithTimeout(survived, "item after exception");
        }

        // ----- 1.7 pause/resume ----------------------------------------------------------------

        [Fact]
        public async Task pause_waits_for_inflight_holds_queue_and_resume_drains_in_priority_order()
        {
            var dispatcher = new PriorityRequestDispatcher(1) { PausedEnqueueTtlMs = 60_000, AgingIntervalMs = 60_000 };
            var gate = await SaturateAsync(dispatcher);

            var pauseTask = dispatcher.PauseAsync();
            Assert.False(pauseTask.IsCompleted); // in-flight item still running

            var order = new ConcurrentQueue<string>();
            var low = dispatcher.EnqueueAsync(() => { order.Enqueue("low"); return Task.CompletedTask; }, eAccessPriority.Low);
            var high = dispatcher.EnqueueAsync(() => { order.Enqueue("high"); return Task.CompletedTask; }, eAccessPriority.High);

            gate.TrySetResult(true);
            await WithTimeout(pauseTask, "pause drain");
            Assert.Equal(0, dispatcher.InFlightCount);
            Assert.Equal(2, dispatcher.QueuedCount);

            await Task.Delay(100);
            Assert.False(low.IsCompleted);
            Assert.False(high.IsCompleted);
            Assert.Equal(2, dispatcher.QueuedCount); // retained, not failed

            dispatcher.Resume();
            await WithTimeout(Task.WhenAll(low, high), "resume backlog");
            Assert.Equal(new[] { "high", "low" }, order.ToArray());
        }

        // ----- 1.8 reserved slot ------------------------------------------------------------------

        [Fact]
        public async Task userinterface_dispatches_immediately_while_background_saturates_general_workers()
        {
            var dispatcher = new PriorityRequestDispatcher(2); // 1 general + 1 dedicated

            var backgroundGate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var backgroundStarted = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            var runningBackground = dispatcher.EnqueueAsync(() =>
            {
                backgroundStarted.TrySetResult(true);
                return backgroundGate.Task;
            }, eAccessPriority.Low);
            await WithTimeout(backgroundStarted.Task, "background item start");

            // More background work queues behind the single general worker; the dedicated slot must stay free.
            var queuedBackground = Enumerable.Range(0, 5)
                .Select(_ => dispatcher.EnqueueAsync(() => Task.CompletedTask, eAccessPriority.Normal)).ToArray();
            await Task.Delay(50);
            Assert.All(queuedBackground, t => Assert.False(t.IsCompleted));

            var ui = dispatcher.EnqueueAsync(() => Task.CompletedTask, eAccessPriority.UserInterface);
            await WithTimeout(ui, "UI item while background saturated");
            Assert.False(runningBackground.IsCompleted); // background blocker still holds the general slot

            backgroundGate.TrySetResult(true);
            await WithTimeout(Task.WhenAll(queuedBackground.Append(runningBackground)), "background drain");
        }

        [Fact]
        public async Task single_worker_fallback_serves_all_classes()
        {
            var dispatcher = new PriorityRequestDispatcher(1);
            // With N=1 the reservation is void: background work must still be served.
            await WithTimeout(dispatcher.EnqueueAsync(() => Task.CompletedTask, eAccessPriority.Low),
                "low item with single worker");
            await WithTimeout(dispatcher.EnqueueAsync(() => Task.CompletedTask, eAccessPriority.UserInterface),
                "ui item with single worker");
        }

        // ----- 1.9 / 1.11 latency budget -------------------------------------------------------------

        [Fact]
        public async Task whole_batch_over_budget_raises_breach_with_split_and_completes()
        {
            var dispatcher = new PriorityRequestDispatcher(1) { UserInterfaceLatencyBudgetMs = 50 };
            UiLatencyBreach breach = null;
            dispatcher.OnUiLatencyBreach = b => breach = b;

            // 1.11: each chunk fast (~30 ms), total ~90 ms — only the whole batch breaches.
            var chunks = Enumerable.Range(0, 3)
                .Select(_ => (Func<Task>)(() => Task.Delay(30)))
                .ToArray();

            var result = await dispatcher.EnqueueBatchAsync(chunks, eAccessPriority.UserInterface, 0);

            Assert.NotNull(breach);
            Assert.Equal(3, breach.ChunkCount);
            Assert.True(breach.TotalMs > 50);
            Assert.Equal(breach.TotalMs, breach.QueueWaitMs + breach.ExecutionMs, 3);
            Assert.All(result.ChunkTasks, t => Assert.True(t.IsCompletedSuccessfully));
        }

        [Fact]
        public async Task under_budget_batches_do_not_raise_breach_and_custom_budget_is_honored()
        {
            var dispatcher = new PriorityRequestDispatcher(1) { UserInterfaceLatencyBudgetMs = 5000 };
            var breaches = 0;
            dispatcher.OnUiLatencyBreach = _ => breaches++;

            await dispatcher.EnqueueBatchAsync(new Func<Task>[] { () => Task.Delay(10) },
                eAccessPriority.UserInterface, 0);
            Assert.Equal(0, breaches);

            dispatcher.UserInterfaceLatencyBudgetMs = 5; // custom tight budget
            await dispatcher.EnqueueBatchAsync(new Func<Task>[] { () => Task.Delay(60) },
                eAccessPriority.UserInterface, 0);
            Assert.Equal(1, breaches);

            // Non-UserInterface batches are not monitored.
            await dispatcher.EnqueueBatchAsync(new Func<Task>[] { () => Task.Delay(60) }, eAccessPriority.Normal, 0);
            Assert.Equal(1, breaches);
        }

        // ----- 1.10 chunk dispatch shape ---------------------------------------------------------------

        [Fact]
        public async Task zero_delay_batch_runs_chunks_concurrently()
        {
            var dispatcher = new PriorityRequestDispatcher(4);

            var current = 0;
            var maxObserved = 0;

            async Task Chunk()
            {
                var now = Interlocked.Increment(ref current);
                InterlockedExtensions.Max(ref maxObserved, now);
                await Task.Delay(60);
                Interlocked.Decrement(ref current);
            }

            var chunks = Enumerable.Range(0, 3).Select(_ => (Func<Task>)Chunk).ToArray();
            await dispatcher.EnqueueBatchAsync(chunks, eAccessPriority.UserInterface, 0);

            Assert.True(maxObserved >= 2, $"Expected concurrent chunks, max observed parallelism was {maxObserved}.");
        }

        [Fact]
        public async Task positive_delay_batch_runs_chunks_serially()
        {
            var dispatcher = new PriorityRequestDispatcher(4);

            var current = 0;
            var maxObserved = 0;

            async Task Chunk()
            {
                var now = Interlocked.Increment(ref current);
                InterlockedExtensions.Max(ref maxObserved, now);
                await Task.Delay(20);
                Interlocked.Decrement(ref current);
            }

            var chunks = Enumerable.Range(0, 4).Select(_ => (Func<Task>)Chunk).ToArray();
            await dispatcher.EnqueueBatchAsync(chunks, eAccessPriority.Low, 10);

            Assert.Equal(1, maxObserved);
        }

        // ----- 1.12 paused TTL -----------------------------------------------------------------------

        [Fact]
        public async Task pause_shorter_than_ttl_is_transparent()
        {
            var dispatcher = new PriorityRequestDispatcher(1) { PausedEnqueueTtlMs = 10_000 };
            await dispatcher.PauseAsync();

            var item = dispatcher.EnqueueAsync(() => Task.CompletedTask, eAccessPriority.Normal);
            await Task.Delay(50);
            Assert.False(item.IsCompleted);

            dispatcher.Resume();
            await WithTimeout(item, "item after short pause");
        }

        [Fact]
        public async Task pause_longer_than_ttl_faults_queued_items_cleanly()
        {
            var dispatcher = new PriorityRequestDispatcher(1) { PausedEnqueueTtlMs = 60 };
            await dispatcher.PauseAsync();

            var item = dispatcher.EnqueueAsync(() => Task.CompletedTask, eAccessPriority.Normal);
            var ex = await Assert.ThrowsAsync<TimeoutException>(() => WithTimeout(item, "ttl fault"));
            Assert.Contains("re-authenticating", ex.Message);
            Assert.Equal(0, dispatcher.QueuedCount); // queue bounded — expired item removed

            dispatcher.Resume();
        }
    }

    internal static class InterlockedExtensions
    {
        public static void Max(ref int location, int value)
        {
            int snapshot;
            while (value > (snapshot = Volatile.Read(ref location)))
                if (Interlocked.CompareExchange(ref location, value, snapshot) == snapshot)
                    return;
        }
    }
}
