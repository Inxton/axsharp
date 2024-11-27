// AXSharp.Connector.S71500.WebAPITests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector.S71500.WebApi;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AXSharp.Connector.S71500.WebAPITests.Exploratory
{
    public class Exploratory
    {
        private readonly ITestOutputHelper output;

        private static string TargetIp { get; } = Environment.GetEnvironmentVariable("AX_WEBAPI_TARGET") ?? "10.10.101.1";

        public Exploratory(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async void should_read_huge()
        {
#if RELEASE
    return;
#endif
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);

            
            var myBOOL = new WebApiBool(connector, "", "myBOOL");
            var myBYTE = new WebApiByte(connector, "", "myBYTE");
            var myWORD = new WebApiWord(connector, "", "myWORD");
            var myDWORD = new WebApiDWord(connector, "", "myDWORD");
            var myLWORD = new WebApiLWord(connector, "", "myLWORD");
            var mySINT = new WebApiSInt(connector, "", "mySINT");
            var myINT = new WebApiInt(connector, "", "myINT");
            var myDINT = new WebApiDInt(connector, "", "myDINT");
            var myLINT = new WebApiLInt(connector, "", "myLINT");
            var myUSINT = new WebApiUSInt(connector, "", "myUSINT");
            var myUINT = new WebApiUInt(connector, "", "myUINT");
            var myUDINT = new WebApiUdInt(connector, "", "myUDINT");
            var myULINT = new WebApiULInt(connector, "", "myULINT");
            var myREAL = new WebApiReal(connector, "", "myREAL");
            var myLREAL = new WebApiLReal(connector, "", "myLREAL");
            var myTIME = new WebApiTime(connector, "", "myTIME");
            var myLTIME = new WebApiLTime(connector, "", "myLTIME");
            var myDATE = new WebApiDate(connector, "", "myDATE");
            var myLDATE = new WebApiDate(connector, "", "myLDATE");
            var myTIME_OF_DAY = new WebApiTimeOfDay(connector, "", "myTIME_OF_DAY");
            var myLTIME_OF_DAY = new WebApiLTimeOfDay(connector, "", "myLTIME_OF_DAY");
            var myDATE_AND_TIME = new WebApiDateTime(connector, "", "myDATE_AND_TIME");
            var myLDATE_AND_TIME = new WebApiLDateTime(connector, "", "myLDATE_AND_TIME");
            var myCHAR = new WebApiChar(connector, "", "myCHAR");
            var myWCHAR = new WebApiWChar(connector, "", "myWCHAR");
            var mySTRING = new WebApiString(connector, "", "mySTRING");
            var myWSTRING = new WebApiWString(connector, "", "myWSTRING");

            await connector.WriteAsync<byte>(myBYTE, 42);
            await connector.WriteAsync<bool>(myBOOL, true);
            await connector.WriteAsync<short>(myINT, 43);

            List<ITwinPrimitive> huge = new List<ITwinPrimitive>();
            
                huge.Add(myBOOL);
                huge.Add(myBYTE);
                huge.Add(myWORD);
                huge.Add(myDWORD);
                huge.Add(myLWORD);
                huge.Add(mySINT);
                huge.Add(myINT);
                huge.Add(myDINT);
                huge.Add(myLINT);
                huge.Add(myUSINT);
                huge.Add(myUINT);
                huge.Add(myUDINT);
                huge.Add(myULINT);
                huge.Add(myREAL);
                huge.Add(myLREAL);
                huge.Add(myTIME);
                huge.Add(myLTIME);
                huge.Add(myDATE);
                huge.Add(myLDATE);
                huge.Add(myTIME_OF_DAY);
                huge.Add(myLTIME_OF_DAY);
                huge.Add(myDATE_AND_TIME);
                huge.Add(myLDATE_AND_TIME);
                huge.Add(myCHAR);
                huge.Add(myWCHAR);
                huge.Add(mySTRING);
                huge.Add(myWSTRING);
            

            for (int i = 0; i < 1000; i++)
            {                                                                                              
                huge.Add(new WebApiBool(connector, "", "myBOOL")                 );
                huge.Add(new WebApiByte(connector, "", "myBYTE")                 );
                huge.Add(new WebApiWord(connector, "", "myWORD")                 );
                huge.Add(new WebApiDWord(connector, "", "myDWORD")               );
                huge.Add(new WebApiLWord(connector, "", "myLWORD")               );
                huge.Add(new WebApiSInt(connector, "", "mySINT")                 );
                huge.Add(new WebApiInt(connector, "", "myINT")                   );
                huge.Add(new WebApiDInt(connector, "", "myDINT")                 );
                huge.Add(new WebApiLInt(connector, "", "myLINT")                 );
                huge.Add(new WebApiUSInt(connector, "", "myUSINT")               );
                huge.Add(new WebApiUInt(connector, "", "myUINT")                 );
                huge.Add(new WebApiUdInt(connector, "", "myUDINT")               );
                huge.Add(new WebApiULInt(connector, "", "myULINT")               );
                huge.Add(new WebApiReal(connector, "", "myREAL")                 );
                huge.Add(new WebApiLReal(connector, "", "myLREAL")               );
                huge.Add(new WebApiTime(connector, "", "myTIME")                 );
                huge.Add(new WebApiLTime(connector, "", "myLTIME")               );
                huge.Add(new WebApiDate(connector, "", "myDATE")                 );
                huge.Add(new WebApiDate(connector, "", "myLDATE")                );
                huge.Add(new WebApiTimeOfDay(connector, "", "myTIME_OF_DAY")     );
                huge.Add(new WebApiLTimeOfDay(connector, "", "myLTIME_OF_DAY")   );
                huge.Add(new WebApiDateTime(connector, "", "myDATE_AND_TIME")    );
                huge.Add(new WebApiLDateTime(connector, "", "myLDATE_AND_TIME")  );
                huge.Add(new WebApiChar(connector, "", "myCHAR")                 );
                huge.Add(new WebApiWChar(connector, "", "myWCHAR")               );
                huge.Add(new WebApiString(connector, "", "mySTRING")             );
                huge.Add(new WebApiWString(connector, "", "myWSTRING"));
            }


            


            output.WriteLine($"Number of items {huge.Count}");
            var s = new Stopwatch();
            s.Start();
            await connector.ReadBatchAsync(huge);
            s.Stop();

            output.WriteLine($"Whole {s.ElapsedMilliseconds} | per item: {((double)(s.ElapsedMilliseconds) / ((double)huge.Count))}");

            Assert.Equal(42, myBYTE.Cyclic);
            Assert.True(myBOOL.Cyclic);
            Assert.Equal(43, myINT.Cyclic);
        }

        [Fact]
        public async Task should_write_huge()
        {
#if RELEASE
    return;
#endif
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);

            

            var myBOOL = new WebApiBool(connector, "", "myBOOL");
            var myBYTE = new WebApiByte(connector, "", "myBYTE");
            var myWORD = new WebApiWord(connector, "", "myWORD");
            var myDWORD = new WebApiDWord(connector, "", "myDWORD");
            var myLWORD = new WebApiLWord(connector, "", "myLWORD");
            var mySINT = new WebApiSInt(connector, "", "mySINT");
            var myINT = new WebApiInt(connector, "", "myINT");
            var myDINT = new WebApiDInt(connector, "", "myDINT");
            var myLINT = new WebApiLInt(connector, "", "myLINT");
            var myUSINT = new WebApiUSInt(connector, "", "myUSINT");
            var myUINT = new WebApiUInt(connector, "", "myUINT");
            var myUDINT = new WebApiUdInt(connector, "", "myUDINT");
            var myULINT = new WebApiULInt(connector, "", "myULINT");
            var myREAL = new WebApiReal(connector, "", "myREAL");
            var myLREAL = new WebApiLReal(connector, "", "myLREAL");
            var myTIME = new WebApiTime(connector, "", "myTIME");
            var myLTIME = new WebApiLTime(connector, "", "myLTIME");
            var myDATE = new WebApiDate(connector, "", "myDATE");
            var myLDATE = new WebApiDate(connector, "", "myLDATE");
            var myTIME_OF_DAY = new WebApiTimeOfDay(connector, "", "myTIME_OF_DAY");
            var myLTIME_OF_DAY = new WebApiLTimeOfDay(connector, "", "myLTIME_OF_DAY");
            var myDATE_AND_TIME = new WebApiDateTime(connector, "", "myDATE_AND_TIME");
            var myLDATE_AND_TIME = new WebApiLDateTime(connector, "", "myLDATE_AND_TIME");
            var myCHAR = new WebApiChar(connector, "", "myCHAR");
            var myWCHAR = new WebApiWChar(connector, "", "myWCHAR");
            var mySTRING = new WebApiString(connector, "", "mySTRING");
            var myWSTRING = new WebApiWString(connector, "", "myWSTRING");

            await connector.WriteAsync<byte>(myBYTE, 42);
            await connector.WriteAsync<bool>(myBOOL, true);
            await connector.WriteAsync<short>(myINT, 43);

            List<ITwinPrimitive> huge = new List<ITwinPrimitive>();

           


            for (int i = 0; i < 1000; i++)
            {
                huge.Add(new WebApiBool(connector, "", "myBOOL"));
                huge.Add(new WebApiByte(connector, "", "myBYTE"));
                huge.Add(new WebApiWord(connector, "", "myWORD"));
                huge.Add(new WebApiDWord(connector, "", "myDWORD"));
                huge.Add(new WebApiLWord(connector, "", "myLWORD"));
                huge.Add(new WebApiSInt(connector, "", "mySINT"));
                huge.Add(new WebApiInt(connector, "", "myINT"));
                huge.Add(new WebApiDInt(connector, "", "myDINT"));
                huge.Add(new WebApiLInt(connector, "", "myLINT"));
                huge.Add(new WebApiUSInt(connector, "", "myUSINT"));
                huge.Add(new WebApiUInt(connector, "", "myUINT"));
                huge.Add(new WebApiUdInt(connector, "", "myUDINT"));
                huge.Add(new WebApiULInt(connector, "", "myULINT"));
                huge.Add(new WebApiReal(connector, "", "myREAL"));
                huge.Add(new WebApiLReal(connector, "", "myLREAL"));
                huge.Add(new WebApiTime(connector, "", "myTIME"));
                huge.Add(new WebApiLTime(connector, "", "myLTIME"));
                huge.Add(new WebApiDate(connector, "", "myDATE"));
                huge.Add(new WebApiDate(connector, "", "myLDATE"));
                huge.Add(new WebApiTimeOfDay(connector, "", "myTIME_OF_DAY"));
                huge.Add(new WebApiLTimeOfDay(connector, "", "myLTIME_OF_DAY"));
                huge.Add(new WebApiDateTime(connector, "", "myDATE_AND_TIME"));
                huge.Add(new WebApiLDateTime(connector, "", "myLDATE_AND_TIME"));
                huge.Add(new WebApiChar(connector, "", "myCHAR"));
                huge.Add(new WebApiWChar(connector, "", "myWCHAR"));
                huge.Add(new WebApiString(connector, "", "mySTRING"));
                huge.Add(new WebApiWString(connector, "", "myWSTRING"));
            }

            huge.Add(myBOOL);
            huge.Add(myBYTE);
            huge.Add(myWORD);
            huge.Add(myDWORD);
            huge.Add(myLWORD);
            huge.Add(mySINT);
            huge.Add(myINT);
            huge.Add(myDINT);
            huge.Add(myLINT);
            huge.Add(myUSINT);
            huge.Add(myUINT);
            huge.Add(myUDINT);
            huge.Add(myULINT);
            huge.Add(myREAL);
            huge.Add(myLREAL);
            huge.Add(myTIME);
            huge.Add(myLTIME);
            huge.Add(myDATE);
            huge.Add(myLDATE);
            huge.Add(myTIME_OF_DAY);
            huge.Add(myLTIME_OF_DAY);
            huge.Add(myDATE_AND_TIME);
            huge.Add(myLDATE_AND_TIME);
            huge.Add(myCHAR);
            huge.Add(myWCHAR);
            huge.Add(mySTRING);
            huge.Add(myWSTRING);

            myBYTE.Cyclic =  48;
            myBOOL.Cyclic = true;
            myINT.Cyclic = 55;

            
            output.WriteLine($"Number of items {huge.Count}");
            var s = new Stopwatch();
            s.Start();
            await connector.WriteBatchAsync(huge);
            s.Stop();

            output.WriteLine($"Whole {s.ElapsedMilliseconds} | per item: {((double)(s.ElapsedMilliseconds) / ((double)huge.Count))}");

            await connector.ReadBatchAsync(huge);

            Assert.Equal(48, myBYTE.Cyclic);
            Assert.True(myBOOL.Cyclic);
            Assert.Equal(55, myINT.Cyclic);
        }
    }
}