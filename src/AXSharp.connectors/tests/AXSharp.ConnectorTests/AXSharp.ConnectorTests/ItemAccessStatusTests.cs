// AXSharp.ConnectorTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.ConnectorTests
{
    using AXSharp.Connector;
    using System;
    using Xunit;

    public class ItemAccessStatusTests
    {
        private PrimitiveAccessStatus _testClass;

        public ItemAccessStatusTests()
        {
            _testClass = new PrimitiveAccessStatus();
        }

        [Fact]
        public void CanCallUpdate()
        {
            // Arrange
            var cycle = 466518545L;
            var failureReason = "TestValue434805367";

            // Act
            _testClass.Update(cycle, failureReason);

            // Assert
            Assert.Equal(cycle, _testClass.Cycle);
            Assert.Equal(failureReason, _testClass.FailureReason);
        }

        
        [Fact]
        public void CanSetAndGetCycle()
        {
            // Arrange
            var testValue = 976874364L;

            // Act
            _testClass.Cycle = testValue;

            // Assert
            Assert.Equal(testValue, _testClass.Cycle);
        }

        [Fact]
        public void CanSetAndGetLastAccess()
        {
            // Arrange
            var testValue = DateTime.UtcNow;

            // Act
            _testClass.LastAccess = testValue;

            // Assert
            Assert.Equal(testValue, _testClass.LastAccess);
        }

        [Fact]
        public void CanSetAndGetFailure()
        {
            // Arrange
            var testValue = true;

            // Act
            _testClass.Failure = testValue;

            // Assert
            Assert.Equal(testValue, _testClass.Failure);
        }

        [Fact]
        public void CanSetAndGetFailureReason()
        {
            // Arrange
            var testValue = "TestValue1408492924";

            // Act
            _testClass.FailureReason = testValue;

            // Assert
            Assert.Equal(testValue, _testClass.FailureReason);
        }
    }
}