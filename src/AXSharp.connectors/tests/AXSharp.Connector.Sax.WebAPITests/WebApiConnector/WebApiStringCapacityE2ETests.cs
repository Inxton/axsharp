// AXSharp.Connector.S71500.WebAPITests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Threading.Tasks;
using AXSharp.Connector.ValueTypes;
using Xunit;

namespace AXSharp.Connector.S71500.WebAPITests.Primitives
{
    /// <summary>
    /// E2E tests against a live PLC for writes exceeding the declared string capacity.
    /// PLC vars: mySTRING_10 : STRING[10], myWSTRING_10 : WSTRING[10] (ax-test-project configuration.st).
    /// </summary>
    public abstract class WebApiStringCapacityE2ETests
    {
        protected const int WaitTimeForCyclicOperations = 100;

        protected abstract OnlinerBase<string> ShortDeclared { get; }

        protected abstract OnlinerBase<string> DefaultDeclared { get; }

        [Fact]
        public void should_propagate_declared_capacity_from_plc_declaration_to_twin()
        {
            Assert.Equal(10, ShortDeclared.Capacity);
            Assert.Equal(254, DefaultDeclared.Capacity);
        }

        [Fact]
        public async Task should_write_and_read_back_value_below_declared_capacity()
        {
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            var value = "abcdefghi"; // 9 chars
            await ShortDeclared.SetAsync(value);
            Assert.Equal(value, await ShortDeclared.GetAsync());
        }

        [Fact]
        public async Task should_write_and_read_back_value_at_declared_capacity()
        {
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            var value = "abcdefghij"; // 10 chars
            await ShortDeclared.SetAsync(value);
            Assert.Equal(value, await ShortDeclared.GetAsync());
        }

        [Fact]
        public async Task should_truncate_value_exceeding_declared_capacity_on_set_async()
        {
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await ShortDeclared.SetAsync(string.Empty);
            var written = await ShortDeclared.SetAsync("abcdefghijX"); // 11 chars
            Assert.Equal("abcdefghij", written);
            Assert.Equal("abcdefghij", await ShortDeclared.GetAsync());
        }

        [Fact]
        public async Task should_truncate_value_exceeding_declared_capacity_on_cyclic_write()
        {
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await ShortDeclared.SetAsync(string.Empty);
            ShortDeclared.Cyclic = "abcdefghijX"; // 11 chars
            ShortDeclared.AddToPeriodicQueue();
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal("abcdefghij", await ShortDeclared.GetAsync());
        }

        [Fact]
        public async Task should_truncate_oversized_value_on_default_capacity_string()
        {
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await DefaultDeclared.SetAsync(string.Empty);
            var oversized = new string('x', 300);
            var written = await DefaultDeclared.SetAsync(oversized);
            Assert.Equal(new string('x', 254), written);
            Assert.Equal(new string('x', 254), await DefaultDeclared.GetAsync());
        }
    }

    public class WebApiStringCapacityE2ETestsString : WebApiStringCapacityE2ETests
    {
        protected override OnlinerBase<string> ShortDeclared => TestConnector.SecurePlc.mySTRING_10;

        protected override OnlinerBase<string> DefaultDeclared => TestConnector.SecurePlc.mySTRING;
    }

    public class WebApiStringCapacityE2ETestsWString : WebApiStringCapacityE2ETests
    {
        protected override OnlinerBase<string> ShortDeclared => TestConnector.SecurePlc.myWSTRING_10;

        protected override OnlinerBase<string> DefaultDeclared => TestConnector.SecurePlc.myWSTRING;
    }
}
