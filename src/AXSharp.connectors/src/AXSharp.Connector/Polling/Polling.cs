using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector.ValueTypes;
using Newtonsoft.Json.Linq;
using Polly;

namespace AXSharp.Connector
{
    internal class Polling
    {

        private static ConcurrentSet<ITwinPrimitive> PollingPool { get; } = new();


        private Polling(ITwinElement twinObject,
                            object holdingObject,
                            int interval)
        {

        }

        private static ConcurrentDictionary<int, Task> PollingTasks { get; } = new();


        internal static void Add(ITwinElement obj, int interval, object holder)
        {
            switch (obj)
            {
                case ITwinPrimitive primitive:
                    if (PollingPool.Add(primitive))
                    {
                        AddToPolling(interval, primitive as OnlinerBase, holder);
                    }
                    else
                    {
                        UpdatePolling(interval, primitive as OnlinerBase, holder);
                    }
                    break;
                case ITwinObject twinObject:
                    foreach (var primitive in twinObject.RetrievePrimitives())
                    {
                        if (PollingPool.Add(primitive))
                        {
                            AddToPolling(interval, primitive as OnlinerBase, holder);
                        }
                        else
                        {
                            UpdatePolling(interval, primitive as OnlinerBase, holder);
                        }
                    }
                    break;
            }

            if (!PollingTasks.ContainsKey(interval))
            {
                PollingTasks[interval] = Task.Run(() =>
                {
                    while (true)
                    {
                        List<OnlinerBase> list = new List<OnlinerBase>();
                        foreach (var primitive in PollingPool) list.Add((OnlinerBase)primitive);
                        foreach (var twinPrimitive in
                                 list.Where(p => p.PollingInterval == interval && p.PollingHolders.Any(a => a.Key is not null)))
                        {
                            twinPrimitive.Poll();
                        }

                        Task.Delay(interval).Wait();
                    }
                });
            }
        }

        private static Policy _retryPolicy = Policy
            .Handle<InvalidOperationException>()
            .WaitAndRetry(5, retryAttempt => TimeSpan.FromMilliseconds(100));

        private static void AddHolder(OnlinerBase primitive, object holder)
        {
            _retryPolicy.Execute(() =>
            {
                if (!primitive.PollingHolders.TryAdd(holder, 0))
                {
                    if (primitive.PollingHolders.ContainsKey(holder))
                        return;

                    throw new InvalidOperationException($"Failed to add: ({holder.GetHashCode()})");
                }
            });

            primitive.PollingHolders.TryAdd(holder, 0);
        }

        private static void RemoveHolder(OnlinerBase primitive, object holder)
        {

            _retryPolicy.Execute(() =>
            {
                byte o = 0;
                if (!primitive.PollingHolders.TryRemove(holder, out o))
                {
                    if (!primitive.PollingHolders.ContainsKey(holder))
                        return;

                    throw new InvalidOperationException($"Failed to remove value with key: {holder.GetHashCode()}");
                }
            });
        }

        private static void UpdatePolling(int interval, OnlinerBase primitive, object holder)
        {
            AddHolder(primitive, holder);

            primitive.PollingInterval = primitive.PollingInterval > interval
                ? interval
                : primitive.PollingInterval;
        }

        private static void AddToPolling(int interval, OnlinerBase primitive, object holder)
        {
            primitive.PollingInterval = interval;
            AddHolder(primitive, holder);
        }

        internal static void Remove(ITwinElement obj, object holder)
        {
            byte dummy;
            switch (obj)
            {
                case ITwinPrimitive primitive:
                    RemoveHolder((OnlinerBase)primitive, holder);
                    if (((OnlinerBase)primitive).PollingHolders.Count <= 0)
                        PollingPool.Remove(primitive);
                    break;
                case ITwinObject twinObject:
                    foreach (var primitive in twinObject.RetrievePrimitives())
                    {
                        RemoveHolder((OnlinerBase)primitive, holder);
                        if (((OnlinerBase)primitive).PollingHolders.Count <= 0)
                            PollingPool.Remove(primitive);

                    }
                    break;
            }
        }
    }
}