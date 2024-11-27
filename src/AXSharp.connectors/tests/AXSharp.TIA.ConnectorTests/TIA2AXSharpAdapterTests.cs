using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector;
using AXSharp.Connector.S71500.WebApi;
using System.Reflection;
using AXSharp.Connector.ValueTypes;
using Xunit.Abstractions;
using Siemens.Simatic.S7.Webserver.API.Services.RequestHandling;
using Siemens.Simatic.S7.Webserver.API.Services;
using System.Net;
using Siemens.Simatic.S7.Webserver.API.Services.IdGenerator;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using AXSharp.TIA2AXSharp;
using System.Diagnostics;
using System.Xml.Linq;

namespace AXSharp.TIA2AXSharpTests
{
    public class TIA2AXSharpAdapterTests
    {

        private readonly ITestOutputHelper output;
        public TIA2AXSharpAdapterTests(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Fact()]
        public async void GoAxTest()
        {

            var connector = new WebApiConnector("10.10.10.180", "Everybody", "", true, dbName:string.Empty);
            //var connector = new WebApiConnector("172.20.30.110", "Everybody", "", true, string.Empty);
            var adapter = await TIA2AXSharpAdapter.CreateAdapter(connector);


            //var root = await TIA2AXSharpAdapter.CreateTIARootObject(connector);
            //TIA2AXSharpSerializer.Serialize(root, "root.json");

            var allVariables = adapter.First().RetrievePrimitives()
               .Where(p => p.Symbol.StartsWith("\"TGlobalVariablesDB\"")).ToList();


            await connector.ReadBatchAsync(allVariables);

            var x = allVariables.FirstOrDefault(p => p.Symbol.Contains("myBOOL"));

            foreach (var variable in allVariables)
            {
                output.WriteLine($"{variable.Symbol} : {((dynamic)variable).LastValue}");
            }

            Assert.True(true, "This test needs an implementation");
            //Assert.NotNull(deserialize);


        }


        [Fact()]
        public async void GoTiaPortalDbData()
        {

            var connector = new WebApiConnector("10.10.10.180", "Everybody", "", true, dbName:string.Empty);

            //var rootObject = await TIA2AXSharpAdapter.CreateTIARootObject(connector, new[] { "DbData" });
            //TIA2AXSharpSerializer.Serialize(rootObject, "dbData.json");

            var rootObject = TIA2AXSharpSerializer.Deserialize("dbData.json");
            var adapter = await TIA2AXSharpAdapter.CreateAdapter(connector, rootObject);


            var allVariables = adapter.First(p=> p.Symbol == "\"DbData\"").RetrievePrimitives()
               .Where(p => p.Symbol.StartsWith("\"DbData\"")).ToList();


            await connector.ReadBatchAsync(allVariables.Take(1000));

            var x = allVariables.FirstOrDefault(p => p.Symbol.Contains("myBOOL"));

            foreach (var variable in allVariables)
            {
                output.WriteLine($"{variable.Symbol} : {((dynamic)variable).LastValue}");
            }

            Assert.True(true, "This test needs an implementation");

        }


        [Fact()]
        public async void GoTiaPortalDbDataFromParamTest()
        {

            var connector = new WebApiConnector("10.10.10.180", "Everybody", "", true, dbName:string.Empty);


            var adapter = await TIA2AXSharpAdapter.CreateAdapter(connector, "dbData.json");


            var allVariables = adapter.First(p => p.Symbol == "\"DbData\"").RetrievePrimitives()
               .Where(p => p.Symbol.StartsWith("\"DbData\"")).ToList();

            await connector.ReadBatchAsync(allVariables.Take(1000));

            var x = allVariables.FirstOrDefault(p => p.Symbol.Contains("myBOOL"));

            foreach (var variable in allVariables)
            {
                output.WriteLine($"{variable.Symbol} : {((dynamic)variable).LastValue}");
            }

            Assert.True(true, "This test needs an implementation");

        }

        [Fact()]
        public async void GoAdapterWithSerializationTest()
        {

            var connector = new WebApiConnector("10.10.10.180", "Everybody", "", true, dbName:string.Empty);
            var rootObject = await TIA2AXSharpAdapter.CreateTIARootObject(connector, new[] { "dbtest" });
            TIA2AXSharpSerializer.Serialize(rootObject, "test.json");

            var adapter = await TIA2AXSharpAdapter.CreateAdapter(connector, rootObject);
            var allVariables = adapter.First().RetrievePrimitives();
            await connector.ReadBatchAsync(allVariables);


            foreach (var variable in allVariables)
            {
                output.WriteLine($"{variable.Symbol} : {((dynamic)variable).LastValue}");
            }

            Assert.NotNull(allVariables);
            Assert.True(true);

        }

       

        [Fact()]
        public async void GoAdapterFromDeSerializedTest()
        {

            var connector = new WebApiConnector("10.10.10.180", "Everybody", "", true, dbName:string.Empty);


            var rootObject = TIA2AXSharpSerializer.Deserialize("test.json");

            var adapter = await TIA2AXSharpAdapter.CreateAdapter(connector, rootObject);

            var allVariables = adapter.First(p => p.Symbol == "\"DbData\"").RetrievePrimitives()
               .Where(p => p.Symbol.StartsWith("\"DbData\"")).ToList();

            await connector.ReadBatchAsync(allVariables);


            foreach (var variable in allVariables)
            {
                output.WriteLine($"{variable.Symbol} : {((dynamic)variable).LastValue}");
            }

            Assert.True(true, "This test needs an implementation");

        }

        [Fact]
        public async void Check()
        {

            ApiResponseChecker ApiResponseChecker = new();
            GUIDGenerator ReqIdGenerator = new();
            ApiRequestParameterChecker RequestParameterChecker = new();
        ServerCertificateCallback.CertificateCallback =
                (sender, cert, chain, sslPolicyErrors) => true;

            var serviceFactory = new ApiStandardServiceFactory();
            var Client = serviceFactory.GetHttpClient("10.10.10.180", "Everybody", string.Empty);

            var requestHandler = new ApiHttpClientRequestHandler(Client,
                new ApiRequestFactory(ReqIdGenerator, RequestParameterChecker), ApiResponseChecker);

            await requestHandler.ApiLogoutAsync();

            await requestHandler.ApiLoginAsync("Everybody", string.Empty, true);
        }
    }
}