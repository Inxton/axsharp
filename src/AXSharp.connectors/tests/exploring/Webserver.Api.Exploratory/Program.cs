// Webserver.Api.Exploratory
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Diagnostics;
using Siemens.Simatic.S7.Webserver.API.Services;
using System.Net;
using System.Net.Http.Headers;
using AXSharp.Connector;
using ix_integration_plc;
using Siemens.Simatic.S7.Webserver.API.Exceptions;
using Siemens.Simatic.S7.Webserver.API.Models;
using Siemens.Simatic.S7.Webserver.API.Services.RequestHandling;
using Siemens.Simatic.S7.Webserver.API.Services.PlcProgram;
using Siemens.Simatic.S7.Webserver.API.Services.IdGenerator;

namespace Webserver.Api.Exploratory
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            //var serviceFactory = new ApiStandardServiceFactory();
            //var reqHandler = await serviceFactory.GetApiHttpClientRequestHandlerAsync("192.168.0.1", "Everybody", "");
            //var apiBrowseResponse = await reqHandler.ApiBrowseAsync();

            //var client = serviceFactory.GetHttpClient("192.168.0.1", "Everybody", string.Empty);


            //var ReqIdGenerator = new GUIDGenerator();
            //var RequestParameterChecker = new ApiRequestParameterChecker();
            //var ApiRequestFactory = new ApiRequestFactory(ReqIdGenerator, RequestParameterChecker);
            //var ApiResponseChecker = new ApiResponseChecker();

            ////var b = serviceFactory.GetPlcProgramHandler(new ApiHttpClientRequestHandler(client, ApiRequestFactory,
            ////    ApiResponseChecker));



            //var requestHandler = new ApiHttpClientRequestHandler(client, ApiRequestFactory, ApiResponseChecker);





            //foreach (var valueTag in Entry.Plc.all_primitives.GetValueTags())
            //{
            //    var sw = new Stopwatch();
            //    sw.Start();
            //    requestHandler.PlcProgramRead<object>($"\"TGlobalVariablesDB\".{valueTag.Symbol}");
                
            //    //Console.WriteLine(requestHandler.PlcProgramRead<object>($"\"TGlobalVariablesDB\".{valueTag.Symbol}").Result);
            //    sw.Stop();
            //    Console.WriteLine(sw.ElapsedMilliseconds);
            //}

            //Console.WriteLine("---------------------------------------------");

            foreach (var valueTag in Entry.Plc.all_primitives.GetValueTags())
            {
                var sw = new Stopwatch();
                sw.Start();

                Entry.Plc.all_primitives.myBOOL.GetAsync().Wait();
                //requestHandler.PlcProgramRead<object>($"\"TGlobalVariablesDB\".{valueTag.Symbol}");

                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);
            }

            return 0;
        }
    }
}