// AXSharp.Connector.S71500.WebAPITests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using AXSharp.Connector;
using AXSharp.Connector.S71500.WebApi;
using AXSharp.Connector.ValueTypes;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Xunit;

namespace AXSharp.Connector.S71500.WebAPITests.Primitives
{
    /// <summary>
    /// PLC-free tests for string capacity normalization of write requests.
    /// Values written to a string primitive must be truncated to the declared
    /// capacity (exactly), falling back to 254 when no capacity is known.
    /// </summary>
    public abstract class StringCapacityNormalizationTests<T> where T : OnlinerBase<string>, new()
    {
        protected abstract T Create();

        protected abstract T Create(ITwinObject parent, string symbolTail);

        private static string WrittenValue(T primitive)
        {
            return (string)((IWebApiPrimitive)primitive).PlcWriteRequestData.Params["value"];
        }

        private static string PeekedValue(T primitive)
        {
            return (string)((IWebApiPrimitive)primitive).PeekPlcWriteRequestData.Params["value"];
        }

        [Fact]
        public void should_pass_through_value_below_declared_capacity()
        {
            var primitive = Create();
            primitive.Capacity = 10;
            primitive.Cyclic = new string('a', 9);

            Assert.Equal(new string('a', 9), WrittenValue(primitive));
        }

        [Fact]
        public void should_pass_through_value_at_declared_capacity()
        {
            var primitive = Create();
            primitive.Capacity = 10;
            primitive.Cyclic = new string('a', 10);

            Assert.Equal(new string('a', 10), WrittenValue(primitive));
        }

        [Fact]
        public void should_truncate_value_exceeding_declared_capacity_to_exact_capacity()
        {
            var primitive = Create();
            primitive.Capacity = 10;
            primitive.Cyclic = new string('a', 10) + "X";

            Assert.Equal(new string('a', 10), WrittenValue(primitive));
        }

        [Fact]
        public void should_truncate_to_default_254_when_capacity_not_set()
        {
            var primitive = Create();
            primitive.Cyclic = new string('a', 300);

            Assert.Equal(new string('a', 254), WrittenValue(primitive));
        }

        [Fact]
        public void should_pass_through_254_chars_when_capacity_not_set()
        {
            var primitive = Create();
            primitive.Cyclic = new string('a', 254);

            Assert.Equal(new string('a', 254), WrittenValue(primitive));
        }

        [Fact]
        public void should_normalize_value_in_peeked_write_request()
        {
            var primitive = Create();
            primitive.Capacity = 10;
            primitive.Cyclic = new string('a', 10) + "X";

            Assert.Equal(new string('a', 10), PeekedValue(primitive));
        }

        [Fact]
        public void should_peek_oversized_value_without_throwing_when_capacity_not_set()
        {
            var primitive = Create();
            primitive.Cyclic = new string('a', 300);

            Assert.Equal(new string('a', 254), PeekedValue(primitive));
        }

        [Fact]
        public void should_treat_unset_cyclic_value_as_empty_string()
        {
            var primitive = Create();

            Assert.Equal(string.Empty, PeekedValue(primitive));
            Assert.Equal(string.Empty, WrittenValue(primitive));
        }

        [Fact]
        public void should_log_warning_when_truncating()
        {
            var sink = new InMemorySink();
            var connector = new WebApiConnector();
            connector.SetLoggerConfiguration(new LoggerConfiguration().WriteTo.Sink(sink).CreateLogger());
            var primitive = Create(connector, "myStringSymbol");
            primitive.Capacity = 10;
            primitive.Cyclic = new string('a', 11);

            _ = WrittenValue(primitive);

            Assert.Contains(sink.Events, e => e.Level == LogEventLevel.Warning);
        }

        [Fact]
        public void should_not_log_warning_when_value_fits()
        {
            var sink = new InMemorySink();
            var connector = new WebApiConnector();
            connector.SetLoggerConfiguration(new LoggerConfiguration().WriteTo.Sink(sink).CreateLogger());
            var primitive = Create(connector, "myStringSymbol");
            primitive.Capacity = 10;
            primitive.Cyclic = new string('a', 10);

            _ = WrittenValue(primitive);

            Assert.DoesNotContain(sink.Events, e => e.Level == LogEventLevel.Warning);
        }

        private class InMemorySink : ILogEventSink
        {
            public List<LogEvent> Events { get; } = new();

            public void Emit(LogEvent logEvent)
            {
                Events.Add(logEvent);
            }
        }
    }

    public class WebApiStringCapacityNormalizationTests : StringCapacityNormalizationTests<WebApiString>
    {
        protected override WebApiString Create() => new();

        protected override WebApiString Create(ITwinObject parent, string symbolTail) => new(parent, symbolTail, symbolTail);
    }

    public class WebApiWStringCapacityNormalizationTests : StringCapacityNormalizationTests<WebApiWString>
    {
        protected override WebApiWString Create() => new();

        protected override WebApiWString Create(ITwinObject parent, string symbolTail) => new(parent, symbolTail, symbolTail);

        [Fact]
        public void should_honor_declared_capacity_greater_than_254()
        {
            var primitive = Create();
            primitive.Capacity = 1000;
            primitive.Cyclic = new string('a', 1001);

            Assert.Equal(new string('a', 1000), (string)((IWebApiPrimitive)primitive).PlcWriteRequestData.Params["value"]);
        }

        [Fact]
        public void should_pass_through_value_at_declared_capacity_greater_than_254()
        {
            var primitive = Create();
            primitive.Capacity = 1000;
            primitive.Cyclic = new string('a', 1000);

            Assert.Equal(new string('a', 1000), (string)((IWebApiPrimitive)primitive).PlcWriteRequestData.Params["value"]);
        }
    }
}
