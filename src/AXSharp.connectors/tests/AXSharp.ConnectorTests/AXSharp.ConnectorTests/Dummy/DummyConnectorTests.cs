// AXSharp.ConnectorTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.ComponentModel;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.ConnectorTests
{
    using AXSharp.Connector;
    using System;
    using Xunit;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DummyConnectorTests
    {
        private DummyConnector _testClass;

        public DummyConnectorTests()
        {
            _testClass = new DummyConnector();
        }

        [Fact]
        public void CanCallBuildAndStart()
        {
            // Act
            var result = _testClass.BuildAndStart();

            Task.Delay(50).Wait();

            // Assert
            Assert.True(_testClass.RwCycleCount > 0);
        }

        [Fact]
        public async Task CanCallReadBatchAsync()
        {
#if !DEBUG
return;
#endif
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            parent.GetConnector().Returns(Substitute.For<Connector>());
            var primitives = new[] { Substitute.For<OnlinerBase<bool>>(parent, "a", "b"), 
                                     Substitute.For<OnlinerBase<bool>>(parent, "c", "d"), 
                                     Substitute.For<OnlinerBase<bool>>(parent, "e", "f") };


            foreach (var primitive in primitives)
            {
                primitive.SetCyclicValue(true);
            }

            // Act
            await _testClass.ReadBatchAsync(primitives);

            // Assert
            foreach (var primitive in primitives)
            {
                Assert.True(primitive.Cyclic);
            }
        }

        [Fact]
        public async Task CannotCallReadBatchAsyncWithNullPrimitives()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.ReadBatchAsync(default(IEnumerable<ITwinPrimitive>)));
        }

        [Fact]
        public async Task CanCallWriteBatchAsync()
        {
#if !DEBUG
return;
#endif

            // Arrange
            var parent = Substitute.For<ITwinObject>();
            parent.GetConnector().Returns(Substitute.For<Connector>());
            var primitives = new[] { Substitute.For<OnlinerBase<bool>>(parent, "a", "b"),
                Substitute.For<OnlinerBase<bool>>(parent, "c", "d"),
                Substitute.For<OnlinerBase<bool>>(parent, "e", "f") };


            foreach (var primitive in primitives)
            {
                primitive.Cyclic = true;
            }

            // Act
            await _testClass.WriteBatchAsync(primitives);

            // Assert
            foreach (var primitive in primitives)
            {
                Assert.True(primitive.Cyclic);
            }
        }

        [Fact]
        public async Task CannotCallWriteBatchAsyncWithNullPrimitives()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.WriteBatchAsync(default(IEnumerable<ITwinPrimitive>)));
        }

        [Fact]
        public void CanCallReloadConnector()
        {
            // Act
            _testClass.ReloadConnector();
        }

        [Fact]
        public void CanSetAndGetErrorCount()
        {
            _testClass.ErrorCount = 10;
            Assert.Equal(10, _testClass.ErrorCount);
        }

        [Fact]
        public void CanGetOnlineTags()
        {
            // Assert
            Assert.True(_testClass.OnlineTags is IEnumerable<ITwinPrimitive>);

        }
    }
}