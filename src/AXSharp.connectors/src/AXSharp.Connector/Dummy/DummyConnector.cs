// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector.ValueTypes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AXSharp.Connector;

/// <summary>
///     Provides a connector without real target system connections.
/// </summary>
public class DummyConnector : Connector
{
    /// <summary>
    ///     This method does not have effect on <see cref="DummyConnector" />
    /// </summary>
    public override Connector BuildAndStart()
    {
        RwCycle();
        return this;
    }

    private volatile object _lock = new();

    private void RwCycle()
    {
        Task.Run(() =>
            {
                while (true)
                {
                    lock (_lock)
                    {
                        RwCycleCount++;
                        Thread.Sleep((int)CyclicRwDuration);
                        WriteBatchAsync(NextCycleWriteSet.Values).Wait();
                        ReadBatchAsync(NextPeriodicReadSet.Values).Wait();
                    }
                }
                // ReSharper disable once FunctionNeverReturns
            }
        );
    }

    /// <summary>
    ///     Reads batch of value items from the plc.
    /// </summary>
    /// <param name="primitives">Value items to be read.</param>
    public override async Task ReadBatchAsync(IEnumerable<ITwinPrimitive> primitives)
    {
        ArgumentNullException.ThrowIfNull(primitives);

        await Task.Run(() =>
        {
            lock (_lock)
            {
                foreach (var item in primitives)
                    item.GetType().GetMethod("UpdateRead", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(item,
                        new[] { item.GetType().GetProperty("Cyclic").GetValue(item) });
            }
        });
    }

    /// <summary>
    ///     Writes batch of value items to the plc.
    /// </summary>
    /// <param name="primitives">Value items to be written.</param>
    public override async Task WriteBatchAsync(IEnumerable<ITwinPrimitive> primitives)
    {
        ArgumentNullException.ThrowIfNull(primitives);

        await Task.Run(() =>
        {
            lock (_lock)
            {
                foreach (var twinPrimitive in primitives)
                {
                    var item = (OnlinerBase)twinPrimitive;
                    item.SetCyclicValue(item.GetCyclicValue<object>());
                }
            }
        });
    }

    /// <summary>
    ///     This method does not have effect on dummy connector.
    /// </summary>
    public override void ReloadConnector()
    {
    }

    internal override async Task ReadBatchAsyncCyclic(IEnumerable<ITwinPrimitive> primitives)
    {
        ArgumentNullException.ThrowIfNull(primitives);

        await Task.Run(() =>
        {
            lock (_lock)
            {
                foreach (var item in primitives)
                    item.GetType().GetMethod("UpdateRead", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(item,
                        new[] { item.GetType().GetProperty("Cyclic").GetValue(item) });
            }
        });
    }

    internal override async Task WriteBatchAsyncCyclic(IEnumerable<ITwinPrimitive> primitives)
    {
        ArgumentNullException.ThrowIfNull(primitives);

        await Task.Run(() =>
        {
            lock (_lock)
            {
                foreach (var twinPrimitive in primitives)
                {
                    var item = (OnlinerBase)twinPrimitive;
                    item.SetCyclicValue(item.GetCyclicValue<object>());
                }
            }
        });
    }
}