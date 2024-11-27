// AXSharp.Connector.S71500.WebAPITests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using AXSharp.Connector.S71500.WebApi;
using AXSharp.Connector.ValueTypes;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using BindingFlags = System.Reflection.BindingFlags;

namespace AXSharp.Connector.S71500.WebAPITests
{
    public class WebApiConnectorTests
    {

        private static string TargetIp { get; } = Environment.GetEnvironmentVariable("AX_WEBAPI_TARGET") ?? "10.10.101.1";

        private readonly ITestOutputHelper output;

        public WebApiConnectorTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        //[Fact]
        //public async void should_authenticate_against_real_system()
        //{
        //    var connector = new WebApiConnector(TargetIp, "Everybody", "");
        //    var response = await connector.Authenticate(TargetIp, "Everybody", "");
        //    output.WriteLine(response.ToString());
        //    Assert.True(connector.DidAuthenticate);
        //}

        [Fact]
        public async Task should_write_bool()
        {
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true).BuildAndStart() as WebApiConnector;
            //await connector.Authenticate(TargetIp, "Everybody", "");
            var myBOOL = new WebApiBool(connector, "", "myBOOL");
            var actual = await connector.WriteAsync<bool>(myBOOL, true);

            Assert.True(actual);
        }

        [Fact]
        public async Task should_read_bool()
        {
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);
            var myBOOL = new WebApiBool(connector, "", "myBOOL");
            await connector.WriteAsync<bool>(myBOOL, true);
            var response = await connector.ReadAsync<bool>("myBOOL");
            output.WriteLine(response.ToString());
            Assert.Equal(response.result, true);
        }

        [Fact]
        public async Task should_write_byte()
        {
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);
            var myBYTE = new WebApiByte(connector, "", "myBYTE");
            var actual = await connector.WriteAsync<byte>(myBYTE, 155);

            Assert.True(155 == actual);
        }

        [Fact]
        public async Task should_read_byte()
        {
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);
            var myBYTE = new WebApiByte(connector, "", "myBYTE");
            await connector.WriteAsync<byte>(myBYTE, 158);
            var response = await connector.ReadAsync<byte>("myBYTE");
            output.WriteLine(response.ToString());
            Assert.Equal(response.result, 158);
        }

        [Fact]
        public async Task should_read_lint()
        {
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);
            var myLINT = new WebApiLInt(connector, "", "myLINT");
            await connector.WriteAsync<long>(myLINT, 9223372036854775807);

            var response = await connector.ReadAsync<long>("myLINT");
            output.WriteLine(response.ToString());
            Assert.Equal(9223372036854775807, response.result);
        }

        [Fact]
        public async Task should_batch_read_primitives()
        {
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);


            var myBOOL = new WebApiBool(connector, "", "myBOOL");
            var myBYTE = new WebApiByte(connector, "", "myBYTE");
            var myWORD = new WebApiWord(connector, "", "myWORD");
        //    myDWORD	        :       DWORD	      ;
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
        //   myLDATE	        :       LDATE	      ;
            var myTIME_OF_DAY = new WebApiTimeOfDay(connector, "", "myTIME_OF_DAY");
            //   myLTIME_OF_DAY	:       LTIME_OF_DAY  ;
            var myDATE_AND_TIME = new WebApiDateTime(connector, "", "myDATE_AND_TIME");
            //   myLDATE_AND_TIME :      LDATE_AND_TIME;
            //   myCHAR	        :       CHAR	      ;
            //   myWCHAR	        :       WCHAR	      ;
            var mySTRING = new WebApiString(connector, "", "mySTRING");
            //   myWSTRING	    :       WSTRING	      ;


            await connector.WriteAsync<byte>(myBYTE, 42);
            await connector.WriteAsync<bool>(myBOOL, true);
            await connector.WriteAsync<short>(myINT, 43);

            IEnumerable<ITwinPrimitive> primitives = new ITwinPrimitive[]
            {
                myBOOL ,
                myBYTE ,
                myWORD ,
          //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
            };
            var sw = new Stopwatch();
            output.WriteLine(primitives.Count().ToString());
            for (int i = 0; i < 10; i++)
            {
                sw.Restart();
                await connector.ReadBatchAsync(primitives);
                output.WriteLine(sw.ElapsedMilliseconds.ToString());
            }
            
            Assert.Equal(42, myBYTE.Cyclic);
            Assert.True(myBOOL.Cyclic);
            Assert.Equal(43, myINT.Cyclic);
        }

        [Fact]
        public async Task should_batch_write_primitives()
        {
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);
            

            

            var myBOOL = new WebApiBool(connector, "", "myBOOL");
            var myBYTE = new WebApiByte(connector, "", "myBYTE");
            var myWORD = new WebApiWord(connector, "", "myWORD");
            //    myDWORD	        :       DWORD	      ;
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
            //   myLDATE	        :       LDATE	      ;
            var myTIME_OF_DAY = new WebApiTimeOfDay(connector, "", "myTIME_OF_DAY");
            //   myLTIME_OF_DAY	:       LTIME_OF_DAY  ;
            var myDATE_AND_TIME = new WebApiDateTime(connector, "", "myDATE_AND_TIME");
            //   myLDATE_AND_TIME :      LDATE_AND_TIME;
            //   myCHAR	        :       CHAR	      ;
            //   myWCHAR	        :       WCHAR	      ;
            var mySTRING = new WebApiString(connector, "", "mySTRING");
            //   myWSTRING	    :       WSTRING	      ;

            myBOOL.Cyclic = true;
            myBYTE.Cyclic = 144;
            myINT.Cyclic = 2544;

            IEnumerable<ITwinPrimitive> primitives = new ITwinPrimitive[]
            {
                myBOOL ,
                myBYTE ,
                myWORD ,
          //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                ////   myLDATE	        :
                myTIME_OF_DAY,
                ////   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                ////   myLDATE_AND_TIME
                ////   myCHAR	        :
                ////   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
            };
            var sw = new Stopwatch();
            output.WriteLine(primitives.Count().ToString());
            for (int i = 0; i < 1; i++)
            {
                sw.Restart();
                var a = connector.WriteBatchAsync(primitives);
                //var b = connector.ReadBatchAsync(primitives);
                //var c = connector.ReadBatchAsync(primitives);
                //var d = connector.ReadBatchAsync(primitives);

                a.Wait();
                //b.Wait();
                //c.Wait();
                //d.Wait();
                output.WriteLine(sw.ElapsedMilliseconds.ToString());
            }



            Assert.Equal(144, await myBYTE.GetAsync());
            Assert.True(await myBOOL.GetAsync());
            Assert.Equal(2544, await myINT.GetAsync());
        }


        [Fact]
        public async Task should_batch_read_a_lot_of_primitives()
        {
#if RELEASE
    return;
#endif
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);

           

            var myBOOL = new WebApiBool(connector, "", "myBOOL");
            var myBYTE = new WebApiByte(connector, "", "myBYTE");
            var myWORD = new WebApiWord(connector, "", "myWORD");
            //    myDWORD	        :       DWORD	      ;
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
            //   myLDATE	        :       LDATE	      ;
            var myTIME_OF_DAY = new WebApiTimeOfDay(connector, "", "myTIME_OF_DAY");
            //   myLTIME_OF_DAY	:       LTIME_OF_DAY  ;
            var myDATE_AND_TIME = new WebApiDateTime(connector, "", "myDATE_AND_TIME");
            //   myLDATE_AND_TIME :      LDATE_AND_TIME;
            //   myCHAR	        :       CHAR	      ;
            //   myWCHAR	        :       WCHAR	      ;
            var mySTRING = new WebApiString(connector, "", "mySTRING");
            //   myWSTRING	    :       WSTRING	      ;

            await connector.WriteAsync<byte>(myBYTE, 42);
            await connector.WriteAsync<bool>(myBOOL, true);
            await connector.WriteAsync<short>(myINT, 43);

            IEnumerable<ITwinPrimitive> primitives = new ITwinPrimitive[]
            {
                myBOOL ,
                myBYTE ,
                myWORD ,
          //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myBOOL ,
                myBYTE ,
                myWORD ,
                //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :



            };
            var sw = new Stopwatch();
            output.WriteLine(primitives.Count().ToString());
            for (int i = 0; i < 9; i++)
            {
                sw.Restart();
                await connector.ReadBatchAsync(primitives);
                output.WriteLine(sw.ElapsedMilliseconds.ToString());
            }

            Assert.Equal(42, myBYTE.Cyclic);
            Assert.True(myBOOL.Cyclic);
            Assert.Equal(43, myINT.Cyclic);
        }

        [Fact]
        public async Task should_report_failure_when_unable_to_read_single_item()
        {
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);
            connector.ExceptionBehaviour = CommExceptionBehaviour.Ignore;
            var myBYTE = new WebApiByte(connector, "", "myBYTE_does_not_exist");
            var response = await connector.ReadAsync<byte>(myBYTE);

            Assert.Equal(true, myBYTE.AccessStatus.Failure);
            output.WriteLine(myBYTE.AccessStatus.FailureReason);
            Assert.Equal("Batch read failed.: 'During Bulk request for 1 there have been 1 Errors:\r\nFor details: Check the Property BulkResponse' [var : \"TGlobalVariablesDB\".myBYTE_does_not_exist] ", myBYTE.AccessStatus.FailureReason);
        }

        [Fact]
        public async Task should_report_failure_when_unable_to_write_single_item()
        {
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);
            connector.ExceptionBehaviour = CommExceptionBehaviour.Ignore;
            var myBYTE = new WebApiByte(connector, "", "myBYTE_does_not_exist");
            var response = await connector.WriteAsync<byte>(myBYTE,55);

            Assert.Equal(true, myBYTE.AccessStatus.Failure);
            output.WriteLine(myBYTE.AccessStatus.FailureReason);
            Assert.Equal("Batch write failed.: 'During Bulk request for 1 there have been 1 Errors:\r\nFor details: Check the Property BulkResponse' [var : \"TGlobalVariablesDB\".myBYTE_does_not_exist;value : 55] ", myBYTE.AccessStatus.FailureReason);
        }

       

        [Fact]
        public async Task should_report_failure_when_unable_to_bulk_read_items()
        {
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);
            connector.ExceptionBehaviour = CommExceptionBehaviour.Ignore;
            var myBYTE = new WebApiByte(connector, "", "myBYTE_o");
            var myWORD = new WebApiWord(connector, "", "myWORD");
            var myLWORD = new WebApiLWord(connector, "", "myLWORD");

            IEnumerable<ITwinPrimitive> primitives = new ITwinPrimitive[]
            {
                myBYTE,
                myWORD,
                myLWORD
            };

            await connector.ReadBatchAsync(primitives);

            Assert.Equal(true, myBYTE.AccessStatus.Failure);
            output.WriteLine(myBYTE.AccessStatus.FailureReason);
            Assert.Equal("Batch read failed.: 'During Bulk request for 3 there have been 1 Errors:\r\nFor details: Check the Property BulkResponse' [var : \"TGlobalVariablesDB\".myBYTE_o] ", myBYTE.AccessStatus.FailureReason);
        }

        [Fact]
        public async Task should_report_failure_when_unable_to_bulk_write_items()
        {
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);
            connector.ExceptionBehaviour = CommExceptionBehaviour.Ignore;
            var myBYTE = new WebApiByte(connector, "", "myBYTE_o");
            var myWORD = new WebApiWord(connector, "", "myWORD");
            var myLWORD = new WebApiLWord(connector, "", "myLWORD");

            IEnumerable<ITwinPrimitive> primitives = new ITwinPrimitive[]
            {
                myBYTE,
                myWORD,
                myLWORD
            };

            await connector.WriteBatchAsync(primitives);

            Assert.Equal(true, myBYTE.AccessStatus.Failure);
            output.WriteLine(myBYTE.AccessStatus.FailureReason);
            Assert.Equal("Batch write failed.: 'During Bulk request for 3 there have been 1 Errors:\r\nFor details: Check the Property BulkResponse' [var : \"TGlobalVariablesDB\".myBYTE_o;value : 0] ", myBYTE.AccessStatus.FailureReason);
        }


        [Fact]
        public void should_rethrow_exception_bulk()
        {
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);
            connector.ExceptionBehaviour = CommExceptionBehaviour.ReThrow;
            var myBYTE = new WebApiByte(connector, "", "myBYTE_o");
            var myWORD = new WebApiWord(connector, "", "myWORD");
            var myLWORD = new WebApiLWord(connector, "", "myLWORD");

            IEnumerable<ITwinPrimitive> primitives = new ITwinPrimitive[]
            {
                myBYTE,
                myWORD,
                myLWORD
            };

           Assert.Throws<System.AggregateException>(() => connector.ReadBatchAsync(primitives).Wait());

           
        }

        [Fact]
        public void should_rethrow_exception_single()
        {
            var connector = new WebApiConnector(TargetIp, "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true);
            connector.ExceptionBehaviour = CommExceptionBehaviour.ReThrow;
            var myBYTE = new WebApiByte(connector, "", "myBYTE_o");

            Assert.Throws<System.AggregateException>(() => connector.ReadAsync<byte>(myBYTE).Result);


        }
    }
}