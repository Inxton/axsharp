// AXSharp.ConnectorTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Xunit;
using AXSharp.Connector.ValueTypes;
using Xunit.Sdk;


namespace AXSharp.Connector.ValueTypes.Tests
{
    using AXSharp.Connector.ValueTypes;
    using System;
    using Xunit;
    using NSubstitute;
    using AXSharp.Connector;

    public class OnlinerBaseTests
    {
        private class TestOnlinerBase : OnlinerBase
        {
            public TestOnlinerBase() : base()
            {
                
            }

            public TestOnlinerBase(ITwinObject parent, string readableTail, string symbolTail)
                : base(parent, readableTail, symbolTail)
            {

            }

            public void PublicOnValueChangeEvent(object newValue)
            {
                base.OnValueChangeEvent(newValue);
            }

            public void PublicNotifyPropertyChanged(string propertyName)
            {
                base.NotifyPropertyChanged(propertyName);
            }

            public string PublicSymbolTail { get => base.SymbolTail; set => base.SymbolTail = value; }
            public ITwinObject PublicParent { get => base.Parent; set => base.Parent = value; }
            public override void FromOnlineToShadow()
            {
                
            }

            public override void FromShadowToOnline()
            {
                
            }
        }

        private TestOnlinerBase _testClass;

        public OnlinerBaseTests()
        {
            _testClass = new TestOnlinerBase();
        }





        [Fact]
        public void CanCallGetParent()
        {
            // Act
            var result = _testClass.GetParent();

            // Assert
            Assert.True(result is ITwinObject);
        }






        [Fact]
        public void CanSetAndGetAttributeFormatString()
        {
            // Arrange
            var testValue = "TestValue13148088";

            // Act
            _testClass.AttributeFormatString = testValue;

            // Assert
            Assert.Equal(testValue, _testClass.AttributeFormatString);
        }

        [Fact]
        public void CanSetAndGetPublicSymbolTail()
        {
            // Arrange
            var testValue = "TestValue732938127";

            // Act
            _testClass.PublicSymbolTail = testValue;

            // Assert
            Assert.Equal(testValue, _testClass.PublicSymbolTail);
        }

        [Fact]
        public void CanSetAndGetPublicParent()
        {
            // Arrange
            var testValue = Substitute.For<ITwinObject>();

            // Act
            _testClass.PublicParent = testValue;

            // Assert
            Assert.Same(testValue, _testClass.PublicParent);
        }

        [Fact]
        public void CanCallSubscribeForPeriodicReading()
        {
            var symbol = "some.symbolik";
            var parent = NSubstitute.Substitute.For<ITwinObject>();
            parent.GetConnector().Returns(new DummyConnector());

            var testClass = new TestOnlinerBase(parent, "", symbol);
            // Act
            testClass.SubscribeForPeriodicReading();

            Assert.True(testClass.PublicParent.GetConnector().Subscribed.ContainsKey(symbol));

        }

        [Fact]
        public void CanSetAndGetSymbol()
        {
            // Arrange
            var testValue = $"parent-symbol.456";
            var parent = Substitute.For<ITwinObject>();
            parent.Symbol.Returns("parent-symbol");
            var testClass = new TestOnlinerBase(parent, "133", "456");

            // Assert
            Assert.Equal(testValue, testClass.Symbol);
        }

        [Fact]
        public void CanSetAndGetAttributeName()
        {
            // Arrange
            var testValue = "TestValue921086738";

            // Act
            _testClass.AttributeName = testValue;

            // Assert
            Assert.Equal(testValue, _testClass.AttributeName);
        }

        [Fact]
        public void CanSetAndGetHumanReadable()
        {
            // Arrange
            var testValue = $"parent-hr.133";
            var parent = Substitute.For<ITwinObject>();
            parent.HumanReadable.Returns("parent-hr");
            var testClass = new TestOnlinerBase(parent, "133", "456");

            // Assert
            Assert.Equal(testValue, testClass.HumanReadable);
        }

        [Fact()]
        public void MakeReadOnceTest()
        {
            var parent = Substitute.For<ITwinObject>();
            var testClass = new TestOnlinerBase(parent, "133", "456");

            testClass.MakeReadOnce();

            Assert.True(testClass.ReadOnce);
        }
    }
}