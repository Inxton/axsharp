// AXSharp.Connector
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AXSharp.Connector;

/// <summary>
/// Carries the measurements of a dispatched batch.
/// </summary>
/// <param name="ChunkTasks">Completion task per chunk, in the order the chunks were provided.
/// In serial mode chunks that were never enqueued (an earlier chunk faulted) are absent from the array tail.</param>
/// <param name="TotalMs">Whole-batch duration from batch start to completion of the last chunk.</param>
/// <param name="QueueWaitMs">Time from batch start until the first chunk started executing.</param>
public sealed record BatchDispatchResult(Task[] ChunkTasks, double TotalMs, double QueueWaitMs)
{
    /// <summary>Execution part of the whole-batch duration (total minus initial queue wait).</summary>
    public double ExecutionMs => TotalMs - QueueWaitMs;
}

/// <summary>
/// Describes a breach of the <see cref="eAccessPriority.UserInterface"/> latency budget.
/// </summary>
/// <param name="TotalMs">Whole-batch duration.</param>
/// <param name="ChunkCount">Number of chunks in the batch.</param>
/// <param name="QueueWaitMs">Time spent waiting for the first dispatch slot.</param>
/// <param name="ExecutionMs">Time spent executing (total minus queue wait).</param>
public sealed record UiLatencyBreach(double TotalMs, int ChunkCount, double QueueWaitMs, double ExecutionMs);

/// <summary>
///     Schedules PLC access requests by <see cref="eAccessPriority"/> class.
///     <para>Ordering: <c>High</c> before <c>UserInterface</c> before <c>Normal</c> before <c>Low</c>; FIFO within a class.
///     <c>Custom</c> schedules as <c>Normal</c>.</para>
///     <para>Concurrency: N worker loops are the sole concurrency mechanism (no semaphore); in-flight work never exceeds N.
///     One worker is dedicated to <c>UserInterface</c>/<c>High</c> (structural reservation) so user-facing work never
///     queues behind background work; with N = 1 the single worker serves all classes and the reservation is void.</para>
///     <para>Starvation: a waiting <c>Low</c> item is promoted one class per <see cref="AgingIntervalMs"/>, capped at
///     <c>Normal</c> — aged work never enters the reserved user-interface lane.</para>
/// </summary>
public sealed class PriorityRequestDispatcher
{
    private const int HighIdx = 0;
    private const int UiIdx = 1;
    private const int NormalIdx = 2;
    private const int LowIdx = 3;
    private const int ClassCount = 4;

    private sealed class WorkItem
    {
        private static long _seqSource;

        public WorkItem(Func<Task> work)
        {
            Work = work;
            EnqueuedTick = Environment.TickCount64;
            // TickCount64 granularity (~15 ms) makes ticks unusable for FIFO ordering of items
            // enqueued close together — the sequence number is the ordering key, ticks drive aging only.
            Seq = Interlocked.Increment(ref _seqSource);
            Tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        }

        public Func<Task> Work { get; }
        public long EnqueuedTick { get; }
        public long Seq { get; }
        public long? PausedSinceTick { get; set; }
        public TaskCompletionSource<bool> Tcs { get; }
    }

    private readonly object _sync = new();
    private readonly Queue<WorkItem>[] _queues;
    private readonly SemaphoreSlim _generalSignal = new(0);
    private readonly SemaphoreSlim _uiSignal = new(0);

    private bool _workersStarted;
    private bool _paused;
    private int _inFlight;
    private TaskCompletionSource<bool> _drained;

    /// <summary>
    ///     Creates the dispatcher with the given number of worker slots.
    /// </summary>
    /// <param name="workerCount">Number of concurrent slots; clamped to 1..4 — the PLC WebAPI destabilizes above 4 concurrent requests.</param>
    public PriorityRequestDispatcher(int workerCount)
    {
        WorkerCount = workerCount > 4 ? 4 : workerCount < 1 ? 1 : workerCount;
        _queues = new Queue<WorkItem>[ClassCount];
        for (var i = 0; i < ClassCount; i++) _queues[i] = new Queue<WorkItem>();
    }

    /// <summary>Gets the effective worker (concurrency slot) count, clamped to 1..4.</summary>
    public int WorkerCount { get; }

    /// <summary>Milliseconds of waiting after which a queued item is promoted one priority class (capped at <c>Normal</c>).</summary>
    public int AgingIntervalMs { get; set; } = 1000;

    /// <summary>Milliseconds a queued item may spend in the paused state before it faults with a re-authentication error.</summary>
    public int PausedEnqueueTtlMs { get; set; } = 10_000;

    /// <summary>Latency budget in milliseconds applied to whole <see cref="eAccessPriority.UserInterface"/> batches.</summary>
    public int UserInterfaceLatencyBudgetMs { get; set; } = 1500;

    /// <summary>Invoked when a <see cref="eAccessPriority.UserInterface"/> batch exceeds <see cref="UserInterfaceLatencyBudgetMs"/>.</summary>
    public Action<UiLatencyBreach> OnUiLatencyBreach { get; set; }

    /// <summary>Gets the number of queued (not yet dispatched) items.</summary>
    public int QueuedCount
    {
        get
        {
            lock (_sync) return _queues.Sum(q => q.Count);
        }
    }

    /// <summary>Gets the number of items currently executing.</summary>
    public int InFlightCount
    {
        get
        {
            lock (_sync) return _inFlight;
        }
    }

    /// <summary>Gets whether dispatching is paused.</summary>
    public bool IsPaused
    {
        get
        {
            lock (_sync) return _paused;
        }
    }

    private static int ClassIndexOf(eAccessPriority priority)
    {
        switch (priority)
        {
            case eAccessPriority.High: return HighIdx;
            case eAccessPriority.UserInterface: return UiIdx;
            case eAccessPriority.Low: return LowIdx;
            // Custom carries caller-supplied batch shaping; it schedules as Normal.
            case eAccessPriority.Normal:
            case eAccessPriority.Custom:
            default: return NormalIdx;
        }
    }

    /// <summary>
    ///     Enqueues a single work item; the returned task completes when the item has executed
    ///     (or faults with the item's exception, or with <see cref="TimeoutException"/> if it expired while paused).
    /// </summary>
    public Task EnqueueAsync(Func<Task> work, eAccessPriority priority)
    {
        if (work is null) throw new ArgumentNullException(nameof(work));

        var item = new WorkItem(work);
        var classIdx = ClassIndexOf(priority);

        lock (_sync)
        {
            EnsureWorkersStarted();
            if (_paused) item.PausedSinceTick = Environment.TickCount64;
            _queues[classIdx].Enqueue(item);
        }

        _generalSignal.Release();
        if (classIdx <= UiIdx) _uiSignal.Release();

        return item.Tcs.Task;
    }

    /// <summary>
    ///     Dispatches a batch of chunks. With <paramref name="interChunkDelayMs"/> of 0 all chunks are enqueued at once and
    ///     run concurrently; with a positive delay chunks run serially with the delay elapsing between them (the delay never
    ///     occupies a worker slot). Measures the whole batch and raises <see cref="OnUiLatencyBreach"/> for
    ///     <see cref="eAccessPriority.UserInterface"/> batches exceeding the budget. The result's <see cref="BatchDispatchResult.ChunkTasks"/>
    ///     are settled; this method does not throw on chunk failures — inspect the tasks.
    /// </summary>
    public async Task<BatchDispatchResult> EnqueueBatchAsync(IReadOnlyList<Func<Task>> chunks, eAccessPriority priority,
        int interChunkDelayMs)
    {
        if (chunks is null) throw new ArgumentNullException(nameof(chunks));
        if (chunks.Count == 0) return new BatchDispatchResult(Array.Empty<Task>(), 0, 0);

        var stopwatch = Stopwatch.StartNew();
        long firstExecutionStartTicks = -1;

        Func<Task> Instrument(Func<Task> chunk)
        {
            return async () =>
            {
                Interlocked.CompareExchange(ref firstExecutionStartTicks, stopwatch.ElapsedMilliseconds, -1);
                await chunk().ConfigureAwait(false);
            };
        }

        Task[] chunkTasks;
        var anyFault = false;

        if (interChunkDelayMs <= 0)
        {
            chunkTasks = chunks.Select(c => EnqueueAsync(Instrument(c), priority)).ToArray();
            foreach (var t in chunkTasks)
            {
                try
                {
                    await t.ConfigureAwait(false);
                }
                catch
                {
                    anyFault = true;
                }
            }
        }
        else
        {
            var dispatched = new List<Task>(chunks.Count);
            for (var i = 0; i < chunks.Count; i++)
            {
                var task = EnqueueAsync(Instrument(chunks[i]), priority);
                dispatched.Add(task);
                try
                {
                    await task.ConfigureAwait(false);
                }
                catch
                {
                    // Serial semantics: an earlier chunk failure stops the remaining chunks.
                    anyFault = true;
                    break;
                }

                if (i < chunks.Count - 1) await Task.Delay(interChunkDelayMs).ConfigureAwait(false);
            }

            chunkTasks = dispatched.ToArray();
        }

        stopwatch.Stop();
        var queueWait = Interlocked.Read(ref firstExecutionStartTicks);
        var result = new BatchDispatchResult(chunkTasks, stopwatch.ElapsedMilliseconds,
            queueWait < 0 ? stopwatch.ElapsedMilliseconds : queueWait);

        if (!anyFault
            && priority == eAccessPriority.UserInterface
            && result.TotalMs > UserInterfaceLatencyBudgetMs)
            OnUiLatencyBreach?.Invoke(new UiLatencyBreach(result.TotalMs, chunks.Count, result.QueueWaitMs,
                result.ExecutionMs));

        return result;
    }

    /// <summary>
    ///     Stops dequeueing and completes when all in-flight items have finished. Queued items are retained
    ///     but fault with <see cref="TimeoutException"/> once they spend longer than <see cref="PausedEnqueueTtlMs"/> paused.
    /// </summary>
    public Task PauseAsync()
    {
        Task drained;
        lock (_sync)
        {
            if (!_paused)
            {
                _paused = true;
                var now = Environment.TickCount64;
                foreach (var queue in _queues)
                foreach (var item in queue)
                    item.PausedSinceTick ??= now;

                RunTtlSweep();
            }

            if (_inFlight == 0) return Task.CompletedTask;
            _drained ??= new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            drained = _drained.Task;
        }

        return drained;
    }

    /// <summary>
    ///     Resumes dispatching; the backlog drains in priority order.
    /// </summary>
    public void Resume()
    {
        int queued;
        lock (_sync)
        {
            if (!_paused) return;
            _paused = false;
            _drained = null;
            foreach (var queue in _queues)
            foreach (var item in queue)
                item.PausedSinceTick = null;

            queued = _queues.Sum(q => q.Count);
        }

        // Over-signaling is harmless (workers re-check); under-signaling would strand the backlog.
        _generalSignal.Release(Math.Max(WorkerCount, queued + 1));
        _uiSignal.Release(queued + 1);
    }

    private void RunTtlSweep()
    {
        _ = Task.Run(async () =>
        {
            while (true)
            {
                List<WorkItem> expired = null;
                lock (_sync)
                {
                    if (!_paused) return;
                    var now = Environment.TickCount64;
                    for (var i = 0; i < ClassCount; i++)
                    {
                        if (_queues[i].Count == 0) continue;
                        if (!_queues[i].Any(p =>
                                p.PausedSinceTick.HasValue && now - p.PausedSinceTick.Value > PausedEnqueueTtlMs))
                            continue;

                        var survivors = new Queue<WorkItem>(_queues[i].Count);
                        foreach (var item in _queues[i])
                            if (item.PausedSinceTick.HasValue && now - item.PausedSinceTick.Value > PausedEnqueueTtlMs)
                                (expired ??= new List<WorkItem>()).Add(item);
                            else
                                survivors.Enqueue(item);

                        _queues[i] = survivors;
                    }
                }

                if (expired != null)
                    foreach (var item in expired)
                        item.Tcs.TrySetException(new TimeoutException(
                            "The request expired while the dispatcher was paused (connector re-authenticating)."));

                var delay = Math.Clamp(PausedEnqueueTtlMs / 4, 10, 250);
                await Task.Delay(delay).ConfigureAwait(false);
            }
        });
    }

    private void EnsureWorkersStarted()
    {
        if (_workersStarted) return;
        _workersStarted = true;

        // Worker 0 is dedicated to UserInterface/High when more than one slot exists (structural reservation):
        // lower classes can occupy at most N-1 slots because only N-1 general workers may take them.
        for (var i = 0; i < WorkerCount; i++)
        {
            var dedicated = WorkerCount > 1 && i == 0;
            _ = Task.Run(() => WorkerLoopAsync(dedicated));
        }
    }

    private async Task WorkerLoopAsync(bool dedicated)
    {
        var signal = dedicated ? _uiSignal : _generalSignal;
        while (true)
        {
            await signal.WaitAsync().ConfigureAwait(false);

            while (TryTake(dedicated, out var item))
            {
                try
                {
                    await item.Work().ConfigureAwait(false);
                    item.Tcs.TrySetResult(true);
                }
                catch (Exception ex)
                {
                    item.Tcs.TrySetException(ex);
                }
                finally
                {
                    lock (_sync)
                    {
                        _inFlight--;
                        if (_paused && _inFlight == 0) _drained?.TrySetResult(true);
                    }
                }
            }
        }
    }

    private bool TryTake(bool dedicated, out WorkItem item)
    {
        lock (_sync)
        {
            item = null;
            if (_paused) return false;

            var now = Environment.TickCount64;
            var limit = dedicated ? UiIdx + 1 : ClassCount;
            var bestIdx = -1;
            var bestEffective = int.MaxValue;
            long bestSeq = long.MaxValue;

            for (var i = 0; i < limit; i++)
            {
                if (_queues[i].Count == 0) continue;
                var head = _queues[i].Peek();
                var effective = i;
                if (i > NormalIdx && AgingIntervalMs > 0)
                {
                    var steps = (int)((now - head.EnqueuedTick) / AgingIntervalMs);
                    effective = Math.Max(NormalIdx, i - steps);
                }

                if (effective < bestEffective ||
                    (effective == bestEffective && head.Seq < bestSeq))
                {
                    bestIdx = i;
                    bestEffective = effective;
                    bestSeq = head.Seq;
                }
            }

            if (bestIdx < 0) return false;

            item = _queues[bestIdx].Dequeue();
            _inFlight++;
            return true;
        }
    }
}
