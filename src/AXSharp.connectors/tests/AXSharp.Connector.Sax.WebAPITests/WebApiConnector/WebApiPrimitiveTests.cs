// AXSharp.Connector.S71500.WebAPITests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AXSharp.Connector.S71500.WebApi;
using AXSharp.Connector.ValueTypes;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;



namespace AXSharp.Connector.S71500.WebAPITests.Primitives
{
    public abstract class WebApiPrimitiveTests<T, N> where T : OnlinerBase<N>, new()
    {

        protected const int WaitTimeForCyclicOperations = 100;
        protected abstract string SymbolTail { get; }
        protected abstract N Max { get; }
        protected abstract N Mid { get; }
        protected abstract N Min { get; }


        protected static WebApiConnector Connector { get; } = TestConnector.TestApiConnector as WebApiConnector;

        protected T webApiPrimitive;
        protected WebApiBool minMatches;
        protected WebApiBool maxMatches;

        protected ITestOutputHelper Output { get; }

        public WebApiPrimitiveTests(ITestOutputHelper output)
        {
            Output = output;
            TestConnector.TestApiConnector.ReadWriteCycleDelay = 2;
            TestConnector.TestApiConnector.ConcurrentRequestMaxCount = 4;
            TestConnector.TestApiConnector.ConcurrentRequestDelay = 10;
            webApiPrimitive = Activator.CreateInstance(typeof(T), Connector, "", SymbolTail) as T;
            minMatches = new WebApiBool(Connector, "", $"minsmatch.{SymbolTail}");
            maxMatches = new WebApiBool(Connector, "", $"maxsmatch.{SymbolTail}");

            webApiPrimitive.Capacity = 254;
        }


        [Fact]
        public void should_create_new_instance_of_webapiprimitive()
        {
            Assert.IsType<T>(webApiPrimitive);
        }

        [Fact]
        public virtual async void should_synchron_write_max_value()
        {
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(Max);
            Assert.Equal(Max, await webApiPrimitive.GetAsync());
            Assert.True(await maxMatches.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_write_mid_value()
        {
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(Mid);
            Assert.Equal(Mid, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_write_min_value()
        {
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(Min);
            Assert.Equal(Min, await webApiPrimitive.GetAsync());
            Assert.True(await minMatches.GetAsync());
        }

        [Fact]
        public virtual async void should_write_cyclic_max_value()
        {
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive.SetAsync(Min);
            webApiPrimitive!.Cyclic = Max;
            webApiPrimitive!.AddToPeriodicQueue();
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal(Max, await webApiPrimitive.GetAsync());
            Assert.Equal(Max, webApiPrimitive.Cyclic);
            Assert.True(await maxMatches.GetAsync());
        }

        [Fact]
        public virtual async void should_write_cyclic_mid_value()
        {
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive.SetAsync(Max);
            webApiPrimitive!.Cyclic = Mid;
            webApiPrimitive!.AddToPeriodicQueue();
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal(Mid, webApiPrimitive.Cyclic);
            Assert.Equal(Mid, await webApiPrimitive.GetAsync());
        }
        [Fact]
        public virtual async void should_write_cyclic_min_value()
        {
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive.SetAsync(Max);
            webApiPrimitive!.Cyclic = Min;
            webApiPrimitive!.AddToPeriodicQueue();
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal(Min, await webApiPrimitive.GetAsync());
            Assert.Equal(Min, webApiPrimitive.Cyclic);
            Assert.True(await minMatches.GetAsync());
        }

        [Fact]
        public virtual async void use_batch_rw_connector()
        {
            return;
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            webApiPrimitive.Cyclic = Min;
            await TestConnector.TestApiConnector.WriteBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            await TestConnector.TestApiConnector.ReadBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            Assert.Equal(Min, webApiPrimitive.LastValue);

            webApiPrimitive.Cyclic = Mid;
            await TestConnector.TestApiConnector.WriteBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            await TestConnector.TestApiConnector.ReadBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            Assert.Equal(Mid, webApiPrimitive.LastValue);

            webApiPrimitive.Cyclic = Max;
            await TestConnector.TestApiConnector.WriteBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            await TestConnector.TestApiConnector.ReadBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            Assert.Equal(Max, webApiPrimitive.LastValue);

        }
    }

    public class WebApiBoolTests : WebApiPrimitiveTests<WebApiBool, bool>
    {
        public WebApiBoolTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myBOOL";
        protected override bool Max { get; } = true;
        protected override bool Mid { get; } = false;
        protected override bool Min { get; } = false;
    }

    public class WebApiByteTests : WebApiPrimitiveTests<WebApiByte, byte>
    {

        public WebApiByteTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myBYTE";
        protected override byte Max { get; } = 255;
        protected override byte Mid { get; } = 158;
        protected override byte Min { get; } = 0;
    }

    public class WebApiDateTests : WebApiPrimitiveTests<WebApiDate, DateOnly>
    {
        public WebApiDateTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myDATE";
        protected override DateOnly Max { get; } = WebApiDate.MaxValue;
        protected override DateOnly Mid { get; } = DateOnly.FromDateTime(DateTime.Today);
        protected override DateOnly Min { get; } = WebApiDate.MinValue;

        [Fact]
        public virtual async void should_synchron_write_leap_value_check_leap()
        {
            var testDate = new DateOnly(2024, 1, 1);
            for (int i = 0; i < 365*5; i++)
            {
                testDate = testDate.AddDays(1);
                TestConnector.TestApiConnector.ClearPeriodicReadSet();
                await webApiPrimitive!.SetAsync(testDate);
                var actual = await webApiPrimitive.GetAsync();
                if(testDate != actual)
                    Output.WriteLine($"Expected: {testDate} - Actual: {actual} {testDate == actual}"); 
                Assert.Equal(testDate, actual);
            }            
        }

        [Fact]
        public virtual async void should_synchron_write_leap_value_check_leap_transitions_1_1_postLeapYear()
        {
            var testDate = new DateOnly(2025, 1, 1);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(testDate);
            var actual = await webApiPrimitive.GetAsync();
            Output.WriteLine($"Expected: {testDate} - Actual: {actual} {testDate == actual}");
            Assert.Equal(testDate, actual);
        }
                

        [Fact]
        public virtual async void should_synchron_write_leap_value_check_leap_transitions_2_27_postLeapYear()
        {
            var testDate = new DateOnly(2025, 2, 27);                        
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(testDate);
            var actual = await webApiPrimitive.GetAsync();            
            Output.WriteLine($"Expected: {testDate} - Actual: {actual} {testDate == actual}");
            Assert.Equal(testDate, actual);            
        }

        [Fact]
        public virtual async void exploring()
        {
            //var _28_ = new DateOnly(1970,1,1) - new DateOnly(2025, 2, 28);
            //var _27_ = new DateOnly(1970, 1, 1) - new DateOnly(2025, 2, 27);

            
            var _26 = DateTime.FromBinary(17405280000000000);
            var _27 = DateTime.FromBinary(17406144000000000);
            var _28 = DateTime.FromBinary(17407008000000000);
            var diff = DateTime.FromBinary(17406144000000000 - 17405280000000000);
            Output.WriteLine($"26: {_26}");
            Output.WriteLine($"27: {_27}");
            Output.WriteLine($"28: {_28}");
            Output.WriteLine($"diff: {diff-new DateTime()}");
        }

        [Fact]
        public virtual async void should_synchron_write_leap_value_check_leap_transitions_2_28_postLeapYear()
        {         
            var testDate = new DateOnly(2025, 2, 28);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(testDate);
            var actual = await webApiPrimitive.GetAsync();
            Output.WriteLine($"Expected: {testDate} - Actual: {actual} {testDate == actual}");
            Assert.Equal(testDate, actual);
        }

        [Fact]
        public virtual async void should_synchron_write_leap_value()
        {
            var expected = new DateOnly(2024, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(expected);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_write_no_leap_value()
        {
            var expected = new DateOnly(2023, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(expected);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_write_first_leap_day_value()
        {
            var expected = new DateOnly(2024, 2, 29);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(expected);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_write_cyclic_leap_value()
        {
            var expected = new DateOnly(2024, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive.SetAsync(Max);
            webApiPrimitive!.Cyclic = expected;
            webApiPrimitive!.AddToPeriodicQueue();
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal(expected, webApiPrimitive.Cyclic);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }


        [Fact]
        public virtual async void should_write_cyclic_no_leap_value()
        {
            var expected = new DateOnly(2023, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive.SetAsync(Max);
            webApiPrimitive!.Cyclic = expected;
            webApiPrimitive!.AddToPeriodicQueue();
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal(expected, webApiPrimitive.Cyclic);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_read_first_leap_day_value()
        {
            var primitive = Activator.CreateInstance(typeof(WebApiDate), Connector, "", "myDATE_leap_febr") as WebApiDate;
            var expected = new DateOnly(2024, 2, 29);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
      
            Assert.Equal(expected, await primitive.GetAsync());
        }
        
        [Fact]
        public virtual async void should_synchron_read_leap_day_value()
        {
            var primitive = Activator.CreateInstance(typeof(WebApiDate), Connector, "", "myDATE_leap_late") as WebApiDate;
            var expected = new DateOnly(2024, 3, 5);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();

            Assert.Equal(expected, await primitive.GetAsync());
        }
    }

    public class WebApiDateTimeTest : WebApiPrimitiveTests<WebApiDateTime, DateTime>
    {

        public WebApiDateTimeTest(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myDATE_AND_TIME";
        protected override DateTime Max { get; } = WebApiDateTime.MaxValue;
        protected override DateTime Mid { get; } = DateTime.Today;
        protected override DateTime Min { get; } = WebApiDateTime.MinValue;

        [Fact]
        public virtual async void should_synchron_write_leap_value()
        {
            var expected = new DateTime(2024, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(expected);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_write_no_leap_value()
        {
            var expected = new DateTime(2023, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(expected);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_write_first_leap_day_value()
        {
            var expected = new DateTime(2024, 2, 29);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(expected);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_write_cyclic_leap_value()
        {
            var expected = new DateTime(2024, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive.SetAsync(Max);
            webApiPrimitive!.Cyclic = expected;
            webApiPrimitive!.AddToPeriodicQueue();
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal(expected, webApiPrimitive.Cyclic);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }


        [Fact]
        public virtual async void should_write_cyclic_no_leap_value()
        {
            var expected = new DateTime(2023, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive.SetAsync(Max);
            webApiPrimitive!.Cyclic = expected;
            webApiPrimitive!.AddToPeriodicQueue();
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal(expected, webApiPrimitive.Cyclic);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_read_first_leap_day_value()
        {
            var primitive = Activator.CreateInstance(typeof(WebApiDateTime), Connector, "", "myDATE_AND_TIME_leap_febr") as WebApiDateTime;
            var expected = new DateTime(2024, 2, 29);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();

            Assert.Equal(expected, await primitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_read_leap_day_value()
        {
            var primitive = Activator.CreateInstance(typeof(WebApiDateTime), Connector, "", "myDATE_AND_TIME_leap_late") as WebApiDateTime;
            var expected = new DateTime(2024, 3, 5);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();

            Assert.Equal(expected, await primitive.GetAsync());
        }
    }

    public class WebApiDIntTest : WebApiPrimitiveTests<WebApiDInt, int>
    {

        public WebApiDIntTest(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myDINT";
        protected override int Max { get; } = WebApiDInt.MaxValue;
        protected override int Mid { get; } = WebApiDInt.MaxValue / 2;
        protected override int Min { get; } = WebApiDInt.MinValue;
    }

    public class WebApiDWordTests : WebApiPrimitiveTests<WebApiDWord, uint>
    {
        public WebApiDWordTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myDWORD";
        protected override uint Max { get; } = uint.MaxValue;
        protected override uint Mid { get; } = uint.MaxValue / 2;
        protected override uint Min { get; } = uint.MinValue;
    }

    public class WebApiIntTests : WebApiPrimitiveTests<WebApiInt, short>
    {
        public WebApiIntTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myINT";
        protected override short Max { get; } = short.MaxValue;
        protected override short Mid { get; } = short.MaxValue / 2;
        protected override short Min { get; } = short.MinValue;
    }

    public class WebApiLIntTests : WebApiPrimitiveTests<WebApiLInt, long>
    {
        public WebApiLIntTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myLINT";
        protected override long Max { get; } = WebApiLInt.MaxValue;
        protected override long Mid { get; } = WebApiLInt.MaxValue / 2;
        protected override long Min { get; } = WebApiLInt.MinValue;
    }

    public class WebApiLRealTests : WebApiPrimitiveTests<WebApiLReal, double>
    {
        public WebApiLRealTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myLREAL";
        protected override double Max { get; } = double.MaxValue;
        protected override double Mid { get; } = double.MaxValue / 2;
        protected override double Min { get; } = double.MinValue;

        private int d = 100000;

        [Fact]
        public override async void should_synchron_write_max_value()
        {
            await webApiPrimitive!.SetAsync(Max);
            Assert.Equal((int)(Max * d), (int)(await webApiPrimitive.GetAsync() * d));
        }

        [Fact]
        public override async void should_synchron_write_mid_value()
        {
            await webApiPrimitive!.SetAsync(Mid);
            Assert.Equal((int)(Mid * d), (int)(await webApiPrimitive.GetAsync() * d));
        }

        [Fact]
        public override async void should_synchron_write_min_value()
        {
            await webApiPrimitive!.SetAsync(Min);
            Assert.Equal((int)(Min * d), (int)(await webApiPrimitive.GetAsync() * d));
        }

        [Fact]
        public override async void should_write_cyclic_max_value()
        {
            webApiPrimitive!.Cyclic = Max;
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal((int)(Max * d), (int)(await webApiPrimitive.GetAsync() * d));
        }

        [Fact]
        public override async void should_write_cyclic_mid_value()
        {
            webApiPrimitive!.Cyclic = Mid;
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal((int)(Mid * d), (int)(await webApiPrimitive.GetAsync() * d));
        }

        [Fact]
        public override async void should_write_cyclic_min_value()
        {
            webApiPrimitive!.Cyclic = Min;
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal((int)(Min * d), (int)(await webApiPrimitive.GetAsync() * d));
        }

        [Fact]
        public override async void use_batch_rw_connector()
        {
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            System.Threading.Thread.Sleep(250);
            webApiPrimitive.Cyclic = Min;
            await TestConnector.TestApiConnector.WriteBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            await TestConnector.TestApiConnector.ReadBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            Assert.Equal((int)(Min * d), (int)webApiPrimitive.LastValue);

            webApiPrimitive.Cyclic = Mid;
            await TestConnector.TestApiConnector.WriteBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            await TestConnector.TestApiConnector.ReadBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            Assert.Equal((int)(Mid * d), (int)webApiPrimitive.LastValue);

            webApiPrimitive.Cyclic = Max;
            await TestConnector.TestApiConnector.WriteBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            await TestConnector.TestApiConnector.ReadBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            Assert.Equal((int)(Max * d), (int)webApiPrimitive.LastValue);

        }
    }

    public class WebApiLTimeTests : WebApiPrimitiveTests<WebApiLTime, TimeSpan>
    {
        public WebApiLTimeTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myLTIME";
        protected override TimeSpan Max { get; } = WebApiLTime.MaxValue;
        protected override TimeSpan Mid { get; } = WebApiLTime.MaxValue;
        protected override TimeSpan Min { get; } = WebApiLTime.MinValue;
    }

    public class WebApiLWordTests : WebApiPrimitiveTests<WebApiLWord, ulong>
    {
        public WebApiLWordTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myLWORD";
        protected override ulong Max { get; } = ulong.MaxValue;
        protected override ulong Mid { get; } = ulong.MaxValue / 2;
        protected override ulong Min { get; } = ulong.MinValue;
    }

    public class WebApiRealTests : WebApiPrimitiveTests<WebApiReal, float>
    {
        public WebApiRealTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myREAL";
        protected override float Max { get; } = float.MaxValue;
        protected override float Mid { get; } = float.MaxValue / 2;
        protected override float Min { get; } = float.MinValue;

        private int d = 100000;

        [Fact]
        public override async void should_synchron_write_max_value()
        {
            await webApiPrimitive!.SetAsync(Max);
            Assert.Equal((int)(Max * d), (int)(await webApiPrimitive.GetAsync() * d));
        }

        [Fact]
        public override async void should_synchron_write_mid_value()
        {
            await webApiPrimitive!.SetAsync(Mid);
            Assert.Equal((int)(Mid * d), (int)(await webApiPrimitive.GetAsync() * d));
        }

        [Fact]
        public override async void should_synchron_write_min_value()
        {
            await webApiPrimitive!.SetAsync(Min);
            Assert.Equal((int)(Min * d), (int)(await webApiPrimitive.GetAsync() * d));
        }

        [Fact]
        public override async void should_write_cyclic_max_value()
        {
            webApiPrimitive!.Cyclic = Min;
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal((int)(Min * d), (int)(await webApiPrimitive.GetAsync() * d));
        }

        [Fact]
        public override async void should_write_cyclic_mid_value()
        {
            webApiPrimitive!.Cyclic = Mid;
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal((int)(Mid * d), (int)(await webApiPrimitive.GetAsync() * d));
        }

        [Fact]
        public override async void should_write_cyclic_min_value()
        {
            webApiPrimitive!.Cyclic = Min;
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal((int)(Min * d), (int)(await webApiPrimitive.GetAsync() * d));
        }

        [Fact]
        public override async void use_batch_rw_connector()
        {
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            System.Threading.Thread.Sleep(250);
            webApiPrimitive.Cyclic = Min;
            await TestConnector.TestApiConnector.WriteBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            await TestConnector.TestApiConnector.ReadBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            Assert.Equal((int)(Min * d), (int)webApiPrimitive.LastValue);

            webApiPrimitive.Cyclic = Mid;
            await TestConnector.TestApiConnector.WriteBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            await TestConnector.TestApiConnector.ReadBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            Assert.Equal((int)(Mid * d), (int)webApiPrimitive.LastValue);

            webApiPrimitive.Cyclic = Max;
            await TestConnector.TestApiConnector.WriteBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            await TestConnector.TestApiConnector.ReadBatchAsync(new ITwinPrimitive[] { webApiPrimitive });
            Assert.Equal((int)(Max * d), (int)webApiPrimitive.LastValue);

        }
    }

    public class WebApiSIntTests : WebApiPrimitiveTests<WebApiSInt, sbyte>
    {
        public WebApiSIntTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "mySINT";
        protected override sbyte Max { get; } = sbyte.MaxValue;
        protected override sbyte Mid { get; } = sbyte.MaxValue / 2;
        protected override sbyte Min { get; } = sbyte.MinValue;
    }

    public class WebApiStringTests : WebApiPrimitiveTests<WebApiString, string>
    {
        public WebApiStringTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "mySTRING";
        protected override string Max { get; } = "ABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMN";
        protected override string Mid { get; } = "vetvo mojho rodu!";
        protected override string Min { get; } = "";
    }

    public class WebApiTimeTests : WebApiPrimitiveTests<WebApiTime, TimeSpan>
    {
        public WebApiTimeTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myTIME";
        protected override TimeSpan Max { get; } = WebApiTime.MaxValue;
        protected override TimeSpan Mid { get; } = TimeSpan.FromMilliseconds((long)(WebApiTime.MaxValue.TotalMilliseconds / 2));
        protected override TimeSpan Min { get; } = WebApiTime.MinValue;
    }

    public class WebApiTimeOfDayTests : WebApiPrimitiveTests<WebApiTimeOfDay, TimeSpan>
    {
        public WebApiTimeOfDayTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myTIME_OF_DAY";
        protected override TimeSpan Max { get; } = WebApiTimeOfDay.MaxValue;
        protected override TimeSpan Mid { get; } = TimeSpan.FromMinutes(850);
        protected override TimeSpan Min { get; } = WebApiTimeOfDay.MinValue;
    }

    public class WebApiUDIntTests : WebApiPrimitiveTests<WebApiUdInt, uint>
    {
        public WebApiUDIntTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myUDINT";
        protected override uint Max { get; } = uint.MaxValue;
        protected override uint Mid { get; } = uint.MaxValue / 2;
        protected override uint Min { get; } = uint.MinValue;
    }

    public class WebApiULIntTests : WebApiPrimitiveTests<WebApiULInt, ulong>
    {
        public WebApiULIntTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myULINT";
        protected override ulong Max { get; } = WebApiULInt.MaxValue;
        protected override ulong Mid { get; } = WebApiULInt.MaxValue / 2;
        protected override ulong Min { get; } = WebApiULInt.MinValue;
    }

    public class WebApiUSIntTests : WebApiPrimitiveTests<WebApiUSInt, byte>
    {
        public WebApiUSIntTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myUSINT";
        protected override byte Max { get; } = WebApiUSInt.MaxValue;
        protected override byte Mid { get; } = (byte)(WebApiUSInt.MaxValue / 2);
        protected override byte Min { get; } = WebApiUSInt.MinValue;
    }

    public class WebApiWordTests : WebApiPrimitiveTests<WebApiWord, ushort>
    {
        public WebApiWordTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myWORD";
        protected override ushort Max { get; } = WebApiWord.MaxValue;
        protected override ushort Mid { get; } = (ushort)(WebApiWord.MaxValue / 2);
        protected override ushort Min { get; } = WebApiWord.MinValue;
    }

    public class WebApiWStringTests : WebApiPrimitiveTests<WebApiWString, string>
    {
        public WebApiWStringTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myWSTRING";
        protected override string Max { get; } = "ABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMN";
        protected override string Mid { get; } = "vetvo mojho rodu!";
        protected override string Min { get; } = "";
    }

    public class WebApiUIntTests : WebApiPrimitiveTests<WebApiUInt, ushort>
    {
        public WebApiUIntTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myUINT";
        protected override ushort Max { get; } = WebApiUInt.MaxValue;
        protected override ushort Mid { get; } = (ushort)(WebApiUInt.MaxValue / 2);
        protected override ushort Min { get; } = WebApiUInt.MinValue;
    }

    public class WebApiCharTests : WebApiPrimitiveTests<WebApiChar, char>
    {
        public WebApiCharTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myCHAR";
        protected override char Max { get; } = WebApiChar.MaxValue;
        protected override char Mid { get; } = (char)(WebApiChar.MaxValue / 2);
        protected override char Min { get; } = WebApiChar.MinValue;
    }

    public class WebApiWCharTests : WebApiPrimitiveTests<WebApiWChar, char>
    {
        public WebApiWCharTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myWCHAR";
        protected override char Max { get; } = WebApiWChar.MaxValue;
        protected override char Mid { get; } = (char)(WebApiWChar.MaxValue / 2);
        protected override char Min { get; } = WebApiWChar.MinValue;
    }

    public class WebApiLTimeOfDayTests : WebApiPrimitiveTests<WebApiLTimeOfDay, TimeSpan>
    {
        public WebApiLTimeOfDayTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myLTIME_OF_DAY";
        protected override TimeSpan Max { get; } = WebApiLTimeOfDay.MaxValue;
        protected override TimeSpan Mid { get; } = TimeSpan.FromMinutes(850);
        protected override TimeSpan Min { get; } = WebApiLTimeOfDay.MinValue;
    }

    public class WebApiLDateTests : WebApiPrimitiveTests<WebApiLDate, DateOnly>
    {
        public WebApiLDateTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override string SymbolTail { get; } = "myLDATE";
        protected override DateOnly Max { get; } = WebApiLDate.MaxValue;
        protected override DateOnly Mid { get; } = DateOnly.FromDateTime(DateTime.Today);
        protected override DateOnly Min { get; } = WebApiLDate.MinValue;

        [Fact]
        public virtual async void should_synchron_write_leap_value()
        {
            var expected = new DateOnly(2024, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(expected);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_write_no_leap_value()
        {
            var expected = new DateOnly(2023, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(expected);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_write_first_leap_day_value()
        {
            var expected = new DateOnly(2024, 2, 29);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(expected);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_write_cyclic_leap_value()
        {
            var expected = new DateOnly(2024, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive.SetAsync(Max);
            webApiPrimitive!.Cyclic = expected;
            webApiPrimitive!.AddToPeriodicQueue();
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal(expected, webApiPrimitive.Cyclic);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }


        [Fact]
        public virtual async void should_write_cyclic_no_leap_value()
        {
            var expected = new DateOnly(2023, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive.SetAsync(Max);
            webApiPrimitive!.Cyclic = expected;
            webApiPrimitive!.AddToPeriodicQueue();
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal(expected, webApiPrimitive.Cyclic);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_read_first_leap_day_value()
        {
            var primitive = Activator.CreateInstance(typeof(WebApiDate), Connector, "", "myLDATE_leap_febr") as WebApiDate;
            var expected = new DateOnly(2024, 2, 29);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();

            Assert.Equal(expected, await primitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_read_leap_day_value()
        {
            var primitive = Activator.CreateInstance(typeof(WebApiDate), Connector, "", "myLDATE_leap_late") as WebApiDate;
            var expected = new DateOnly(2024, 3, 5);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();

            Assert.Equal(expected, await primitive.GetAsync());
        }



    }

    public class WebApiLDateTimeTest : WebApiPrimitiveTests<WebApiLDateTime, DateTime>
    {
        public WebApiLDateTimeTest(ITestOutputHelper output) : base(output)
        {
        }
        protected override string SymbolTail { get; } = "myLDATE_AND_TIME";
        protected override DateTime Max { get; } = WebApiLDateTime.MaxValue;
        protected override DateTime Mid { get; } = DateTime.Today;
        protected override DateTime Min { get; } = WebApiLDateTime.MinValue;

        [Fact]
        public virtual async void should_synchron_write_leap_value()
        {
            var expected = new DateTime(2024, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(expected);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_write_no_leap_value()
        {
            var expected = new DateTime(2023, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(expected);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_write_first_leap_day_value()
        {
            var expected = new DateTime(2024, 2, 29);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive!.SetAsync(expected);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_write_cyclic_leap_value()
        {
            var expected = new DateTime(2024, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive.SetAsync(Max);
            webApiPrimitive!.Cyclic = expected;
            webApiPrimitive!.AddToPeriodicQueue();
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal(expected, webApiPrimitive.Cyclic);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }


        [Fact]
        public virtual async void should_write_cyclic_no_leap_value()
        {
            var expected = new DateTime(2023, 3, 4);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();
            await webApiPrimitive.SetAsync(Max);
            webApiPrimitive!.Cyclic = expected;
            webApiPrimitive!.AddToPeriodicQueue();
            await Task.Delay(WaitTimeForCyclicOperations);
            Assert.Equal(expected, webApiPrimitive.Cyclic);
            Assert.Equal(expected, await webApiPrimitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_read_first_leap_day_value()
        {
            var primitive = Activator.CreateInstance(typeof(WebApiDate), Connector, "", "myLDATE_AND_TIME_leap_febr") as WebApiDate;
            var expected = new DateOnly(2024, 2, 29);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();

            Assert.Equal(expected, await primitive.GetAsync());
        }

        [Fact]
        public virtual async void should_synchron_read_leap_day_value()
        {
            var primitive = Activator.CreateInstance(typeof(WebApiDate), Connector, "", "myLDATE_AND_TIME_leap_late") as WebApiDate;
            var expected = new DateOnly(2024, 3, 5);
            TestConnector.TestApiConnector.ClearPeriodicReadSet();

            Assert.Equal(expected, await primitive.GetAsync());
        }

    }

}