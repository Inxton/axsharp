using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.TIA2AXSharp;
using Xunit;

namespace AXSharp.TIA2AXSharpTests
{
    public class TIA2AXSharpGeneratedTests
    {

        private DummyConnector _connector;
        public TIA2AXSharpGeneratedTests()
        {
            _connector = new DummyConnector();
        }


        [Fact]
        public async void adapter_not_null() 
        {

            var adapter = await TIA2AXSharpAdapter.CreateAdapter(_connector, "dbData.json");

            Assert.NotNull(adapter);
        }

        [Fact]
        public async void adapter_read_success()
        {

            var adapter = await TIA2AXSharpAdapter.CreateAdapter(_connector, "dbData.json");

            var variables = adapter.First().RetrievePrimitives().Take(10);

            ((OnlinerBool)variables.First()).Cyclic = true;

            await _connector.ReadBatchAsync(variables);

            Assert.NotNull(adapter);
            Assert.NotNull(variables);
            Assert.True(((OnlinerBool)variables.First()).Cyclic);
        }

        [Fact]
        public async void adapter_read_from_symbol()
        {

            var adapter = await TIA2AXSharpAdapter.CreateAdapter(_connector, "dbData.json");

            var variables = adapter.First().RetrievePrimitives().Take(10);

            ((OnlinerBool)variables.First()).Cyclic = true;

            await _connector.ReadBatchAsync(variables);

            Assert.NotNull(adapter);
            Assert.NotNull(variables);
            Assert.True(((OnlinerBool)variables.First()).Cyclic);
        }
    }
}
