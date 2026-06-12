// AXSharp.Connector.S71500.WebAPITests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AXSharp.Connector;
using AXSharp.Connector.S71500.WebApi;
using Xunit;
using Xunit.Abstractions;

namespace AXSharp.Connector.S71500.WebAPITests
{
    /// <summary>
    ///     Load scenarios for the priority dispatcher against a real S7-1500
    ///     (webapi-priority-dispatcher change, verification tasks 5.3–5.5).
    ///     Requires the wired-up controller with the testing project pre-loaded.
    /// </summary>
    public class DispatcherHardwareScenarioTests
    {
        private readonly ITestOutputHelper _output;

        public DispatcherHardwareScenarioTests(ITestOutputHelper output)
        {
            _output = output;
        }

        private static List<ITwinPrimitive> CreateLoadSet(WebApiConnector connector, int instances)
        {
            // Distinct object instances over the test project's variables — the connector
            // de-duplicates by reference, so every instance is a separate read on the wire.
            var set = new List<ITwinPrimitive>();
            for (var i = 0; i < instances; i++)
            {
                set.Add(new WebApiBool(connector, "", "myBOOL"));
                set.Add(new WebApiInt(connector, "", "myINT"));
                set.Add(new WebApiDInt(connector, "", "myDINT"));
                set.Add(new WebApiReal(connector, "", "myREAL"));
                set.Add(new WebApiLReal(connector, "", "myLREAL"));
            }

            return set;
        }

        // ----- 5.3 mixed-priority load: no stalls, Low bounded ------------------------------------

        [Fact]
        public async Task mixed_priority_load_completes_without_stalls_and_low_is_bounded()
        {
            var connector = TestConnector.TestApiConnector;

            var uiBatch = connector.ReadBatchAsync(CreateLoadSet(connector, 20), eAccessPriority.UserInterface);

            var directCalls = Enumerable.Range(0, 10)
                .Select(_ => (Task)new WebApiInt(connector, "", "myINT").GetAsync())
                .ToArray();

            var lowStopwatch = Stopwatch.StartNew();
            var lowBatch = connector.ReadBatchAsync(CreateLoadSet(connector, 30), eAccessPriority.Low);

            var all = Task.WhenAll(directCalls.Append(uiBatch).Append(lowBatch));
            var completed = await Task.WhenAny(all, Task.Delay(TimeSpan.FromSeconds(30)));
            Assert.True(ReferenceEquals(completed, all), "Mixed-priority load stalled (30 s timeout).");

            lowStopwatch.Stop();
            _output.WriteLine($"Low batch (150 items, chunked 100/500ms) completed in {lowStopwatch.ElapsedMilliseconds} ms under mixed load.");
            // Aging promotes Low to Normal within ~1 s/step; with two chunks and the 500 ms pacing
            // delay this must come nowhere near the stall territory.
            Assert.True(lowStopwatch.ElapsedMilliseconds < 15_000,
                $"Low batch took {lowStopwatch.ElapsedMilliseconds} ms — aging did not bound its wait.");
        }

        // ----- 5.4 UI within budget under background saturation + latency measurements -------------

        [Fact]
        public async Task ui_batch_stays_within_budget_under_background_saturation()
        {
            var connector = TestConnector.TestApiConnector;
            var breachesBefore = connector.UiLatencyBreachCount;

            // Saturate the general workers with background work.
            var background = connector.ReadBatchAsync(CreateLoadSet(connector, 60), eAccessPriority.Low);
            var backgroundNormal = connector.ReadBatchAsync(CreateLoadSet(connector, 40), eAccessPriority.Normal);

            var uiStopwatch = Stopwatch.StartNew();
            await connector.ReadBatchAsync(CreateLoadSet(connector, 10), eAccessPriority.UserInterface);
            uiStopwatch.Stop();

            _output.WriteLine(
                $"UI batch (50 items) under background saturation: {uiStopwatch.ElapsedMilliseconds} ms " +
                $"(LastUiBatchDurationMs={connector.LastUiBatchDurationMs}, budget={connector.UserInterfaceLatencyBudget} ms, " +
                $"breaches before={breachesBefore}, after={connector.UiLatencyBreachCount}).");

            Assert.True(uiStopwatch.ElapsedMilliseconds < connector.UserInterfaceLatencyBudget,
                $"UI batch took {uiStopwatch.ElapsedMilliseconds} ms — exceeded the {connector.UserInterfaceLatencyBudget} ms budget despite the reserved slot.");

            await Task.WhenAll(background, backgroundNormal);
        }

        [Fact]
        public async Task direct_call_latency_under_sustained_cyclic_load_is_measured()
        {
            var connector = TestConnector.TestApiConnector;

            // Sustain cyclic traffic: poll a set through the periodic read pathway.
            var cyclicSet = CreateLoadSet(connector, 20);
            var sustained = Task.Run(async () =>
            {
                for (var round = 0; round < 10; round++)
                    await connector.ReadBatchAsync(cyclicSet, eAccessPriority.UserInterface);
            });

            var probe = new WebApiInt(connector, "", "myINT");
            var samples = new List<long>();
            for (var i = 0; i < 20; i++)
            {
                var sw = Stopwatch.StartNew();
                await probe.GetAsync();
                sw.Stop();
                samples.Add(sw.ElapsedMilliseconds);
            }

            await sustained;

            samples.Sort();
            _output.WriteLine(
                $"Direct-call (Normal) latency under sustained UI load over 20 samples: " +
                $"min={samples.First()} ms, median={samples[samples.Count / 2]} ms, max={samples.Last()} ms.");

            // Aging bounds the worst case at roughly one aging interval plus one request duration.
            Assert.True(samples.Max() < 5000,
                $"Direct call took {samples.Max()} ms under load — beyond any aging-derived bound.");
        }

        // ----- 5.6 headless equivalent of Blazor rendered-control refresh ----------------------------
        // RenderableContentControl subscribes primitives via Polling.Add → Poll() →
        // NextPeriodicReadSet → cyclic loop; the browser adds nothing to connector behavior.
        // This measures the same pathway: on-screen staleness of a polled variable.

        [Fact]
        public async Task polled_variable_refreshes_via_cyclic_loop_within_budget_like_rendered_controls()
        {
            var connector = TestConnector.TestApiConnector;
            var tag = new WebApiInt(connector, "", "myINT");
            var holder = new object();

            tag.StartPolling(50, holder);
            try
            {
                await tag.SetAsync(111);
                var sw = Stopwatch.StartNew();
                while (tag.Cyclic != 111 && sw.ElapsedMilliseconds < 5000) await Task.Delay(10);
                var initial = sw.ElapsedMilliseconds;
                Assert.Equal(111, tag.Cyclic);

                await tag.SetAsync(222);
                sw.Restart();
                while (tag.Cyclic != 222 && sw.ElapsedMilliseconds < 5000) await Task.Delay(10);
                sw.Stop();

                _output.WriteLine(
                    $"Rendered-control-equivalent staleness (poll 50 ms + cyclic loop): initial {initial} ms, refresh {sw.ElapsedMilliseconds} ms.");
                Assert.True(sw.ElapsedMilliseconds < connector.UserInterfaceLatencyBudget,
                    $"Polled refresh took {sw.ElapsedMilliseconds} ms — rendered controls would appear stale beyond the budget.");
            }
            finally
            {
                tag.StopPolling(holder);
            }
        }

        // ----- 5.5 write hammer: last value reaches the PLC ------------------------------------------

        [Fact]
        public async Task hammered_cyclic_writes_deliver_the_last_value_under_heavy_read_load()
        {
            var connector = TestConnector.TestApiConnector;
            var tag = new WebApiInt(connector, "", "myINT");

            // Heavy concurrent read load while the writes hammer.
            var load = Task.WhenAll(Enumerable.Range(0, 4).Select(_ =>
                connector.ReadBatchAsync(CreateLoadSet(connector, 30), eAccessPriority.Normal)));

            short last = 0;
            for (short i = 1; i <= 200; i++)
            {
                tag.Cyclic = i;
                last = i;
                if (i % 20 == 0) await Task.Delay(10); // bursts with brief gaps
            }

            await load;

            // Let the cyclic write loop flush the dirty set.
            await Task.Delay(1500);

            var onPlc = await tag.GetAsync();
            _output.WriteLine($"Hammered 200 cyclic writes under heavy read load; last assigned {last}, PLC holds {onPlc}.");
            Assert.Equal(last, onPlc);
        }
    }
}
