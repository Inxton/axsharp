// AXSharp.ConnectorTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Castle.Components.DictionaryAdapter.Xml;

namespace AXSharp.ConnectorTests
{
    using AXSharp.Connector;
    using System;
    using Xunit;
    using Serilog;
    using AXSharp.Connector.Identity;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ConnectorTests
    {
        private class TestConnector : Connector
        {
            public TestConnector(object[] parameters) : base(parameters)
            {
            }

            public TestConnector() : base()
            {
            }

            public override Connector BuildAndStart()
            {
                return default(Connector);
            }

            public override Task ReadBatchAsync(IEnumerable<ITwinPrimitive> primitives)
            {
                return default(Task);
            }

            internal override Task ReadBatchAsyncCyclic(IEnumerable<ITwinPrimitive> primitives)
            {
                return default(Task);
            }

            public override Task WriteBatchAsync(IEnumerable<ITwinPrimitive> primitives)
            {
                return default(Task);
            }

            internal override Task WriteBatchAsyncCyclic(IEnumerable<ITwinPrimitive> primitives)
            {
                return default(Task);
            }

            public override void ReloadConnector()
            {
            }
        }

        private TestConnector _testClass;
        private object[] _parameters;

        public ConnectorTests()
        {
            _parameters = new[] { new object(), new object(), new object() };
            _testClass = new TestConnector(_parameters);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new TestConnector(_parameters);

            // Assert
            Assert.NotNull(instance);

            // Act
            instance = new TestConnector();

            // Assert
            Assert.NotNull(instance);
        }

        
        [Fact]
        public void CanCallCreateSymbol()
        {
            // Arrange
            var parent = "TestValue1088771107";
            var member = "TestValue441438622";
            var expected = $"{parent}.{member}";
            // Act
            var result = TestConnector.CreateSymbol(parent, member);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateSymbolWithInvalidMember(string value)
        {
            Assert.Throws<ArgumentException>(() => TestConnector.CreateSymbol("TestValue893873684", value));
        }

        [Fact]
        public void CanCallSuspendWriteProtection()
        {
            // Arrange
            var passPhrase = "Hoj morho vetvo mojho rodu, kto kramou rukou siahne na tvoju slobodu a co i dusu das v tom boji divokom vol nebyt ako byt otrokom!";

            // Act
            _testClass.SuspendWriteProtection(passPhrase);

            // Assert
            Assert.True(_testClass.WriteProtectionSuspended);
        }

        [Fact]
        public void CanCallResumeWriteProtection()
        {
            // Act
            _testClass.ResumeWriteProtection();

            // Assert
            Assert.False(_testClass.WriteProtectionSuspended);
        }

        [Fact]
        public void CanGetLogger()
        {
            // Assert
            Assert.True(_testClass.Logger is ILogger);
        }

        [Fact]
        public void CanSetAndGetOnline()
        {
            var connectorAdapter  = new ConnectorAdapter();
            _testClass.ConnectorAdapter = connectorAdapter;
         
            Assert.Same(connectorAdapter, _testClass.ConnectorAdapter);
        }

        [Fact]
        public void CanSetAndGetErrorCount()
        {
            _testClass.CheckProperty(x => x.ErrorCount);
        }

        [Fact]
        public void CanGetIdentityProvider()
        {
            // Assert
            Assert.IsType<TwinIdentityProvider>(_testClass.IdentityProvider);
        }

        [Fact]
        public void CanSetAndGetRwCycleCount()
        {
            _testClass.CheckProperty(x => x.RwCycleCount);
        }

        [Fact]
        public void CanSetAndGetCyclicRwDuration()
        {
            _testClass.CheckProperty(x => x.CyclicRwDuration);
        }

        [Fact]
        public void CanSetAndGetStartUpTime()
        {
            _testClass.CheckProperty(x => x.StartUpTime);
        }

        [Fact]
        public void CanSetAndGetMonitorConnector()
        {
            _testClass.MonitorConnector = false;
            _testClass.CheckProperty(x => x.MonitorConnector);
        }

        [Fact]
        public void CanSetAndGetReadWriteCycleDelay()
        {
            _testClass.CheckProperty(x => x.ReadWriteCycleDelay);
        }

        [Fact]
        public void CanGetOnlineTags()
        {
            // Assert
            Assert.True(_testClass.OnlineTags is IEnumerable<ITwinPrimitive>);
        }

        [Fact]
        public void CanSetAndGetIsRwLoopSuspended()
        {
            _testClass.CheckProperty(x => x.IsRwLoopSuspended);
        }
    }
}