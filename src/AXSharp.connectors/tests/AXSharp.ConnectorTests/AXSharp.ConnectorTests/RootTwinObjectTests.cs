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
    using NSubstitute;
    using AXSharp.Connector.ValueTypes;
    using System.Security.Cryptography;

    public class RootTwinObjectTests
    {
#pragma warning disable CS0618
        private RootTwinObject _testClass;
#pragma warning restore CS0618

        public RootTwinObjectTests()
        {
#pragma warning disable CS0618
            _testClass = new RootTwinObject();
#pragma warning restore CS0618
        }

        [Fact]
        public void CanCallGetSymbolTail()
        {
            // Act
            var result = _testClass.GetSymbolTail();

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void CanCallGetChildren()
        {
            // Act
            var result = _testClass.GetChildren();

            // Assert
            Assert.True(result is IEnumerable<ITwinObject>);
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
        public void CanCallAddChild()
        {
            // Arrange
            var twinObject = Substitute.For<ITwinObject>();

            // Act
            _testClass.AddChild(twinObject);

            // Assert
            Assert.Equal(1, _testClass.GetChildren().Count());
            Assert.True(_testClass.GetChildren().First().Equals(twinObject));
        }

        [Fact]
        public void CannotCallAddChildWithNullTwinObject()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.AddChild(default(ITwinObject)));
        }

        [Fact]
        public void CanCallGetValueTags()
        {
            // Act
            var result = _testClass.GetValueTags();

            // Assert
            Assert.True(result is IEnumerable<ITwinPrimitive>);
        }

        [Fact]
        public void CanCallAddValueTag()
        {
            // Arrange
            var twinPrimitive = Substitute.For<ITwinPrimitive>();

            // Act
            _testClass.AddValueTag(twinPrimitive);

            // Assert
            Assert.Equal(1, _testClass.GetValueTags().Count());
            Assert.True(_testClass.GetValueTags().First().Equals(twinPrimitive));
        }

        [Fact]
        public void CannotCallAddValueTagWithNullTwinPrimitive()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.AddValueTag(default(ITwinPrimitive)));
        }

        [Fact]
        public void CanCallGetConnector()
        {
            // Act
            var result = _testClass.GetConnector();

            // Assert
            Assert.True(result is null);
        }

        [Fact]
        public void CanCallAddKid()
        {
            // Arrange
            var kid = Substitute.For<ITwinElement>();

            // Act
            _testClass.AddKid(kid);

            // Assert
            Assert.Equal(1, _testClass.GetKids().Count());
            Assert.True(_testClass.GetKids().First().Equals(kid));
        }

        [Fact]
        public void CannotCallAddKidWithNullKid()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.AddKid(default(ITwinElement)));
        }

        [Fact]
        public void CanCallGetKids()
        {
            // Act
            var result = _testClass.GetKids();

            // Assert
            Assert.True(result is IEnumerable<ITwinElement>);
        }

        [Fact]
        public void CanGetAttributeName()
        {
            // Assert
            Assert.Equal(string.Empty, _testClass.AttributeName);
        }

        [Fact]
        public void CanGetSymbol()
        {
            // Assert
            Assert.Equal(string.Empty, _testClass.Symbol);
        }

        [Fact]
        public void CanGetHumanReadable()
        {
            // Assert
            Assert.Equal(_testClass.HumanReadable, string.Empty);

        }

        [Fact]
        public void CanGetIdentity()
        {
            // Assert
            Assert.IsType<OnlinerULInt>(_testClass.Identity);

            Assert.Equal(0ul, _testClass.Identity.Cyclic);
        }
    }
}