// AXSharp.ConnectorLegacyTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using NUnit.Framework;
using AXSharp.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.ConnectorTests
{
    [TestFixture]
    public class OnlineAdapterTests
    {
        public class OnlineAdapterTestable : ConnectorAdapter
        {
            public OnlineAdapterTestable(Type onlineConnectorFactoryType) : base(onlineConnectorFactoryType)
            {

            }
        }

        private ConnectorAdapter adapter;

        [SetUp]
        public void Init()
        {
            adapter = new OnlineAdapterTestable(typeof(DummyConnectorFactory));
        }

        [Test]
        public void GetConnectorTest()
        {
            Assert.IsInstanceOf(typeof(DummyConnector), adapter.GetConnector(new object[] { }));
        }

        [Test]
        public void OnlineAdapterTest()
        {
            //-- Arrange
            var actual = adapter;
            var expected = typeof(OnlineAdapterTestable);

            //-- Assert
            Assert.IsInstanceOf(expected, actual);
        }

        [Test]
        public void DummyOnlineAdapterBuilderTest()
        {
            //-- Arrange
            var actual = ConnectorAdapterBuilder.Build().CreateDummy();
            var expected = typeof(ConnectorAdapter);
            
            //-- Assert
            Assert.IsInstanceOf(expected, actual);
            Assert.IsInstanceOf<DummyConnectorFactory>(actual.AdapterFactory);
        }
    }
}