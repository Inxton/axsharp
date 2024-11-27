// AXSharp.ConnectorTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector.ValueTypes;
using AXSharp.Connector.ValueTypes.Online;

namespace AXSharp.ConnectorTests.Identity
{
    using AXSharp.Connector.Identity;
    using System;
    using Xunit;
    using NSubstitute;
    using System.Collections.Generic;
    using AXSharp.Connector;

    public class TwinIdentityProviderTests
    {
        private TwinIdentityProvider _testClass;
        private Connector _connector;

        public TwinIdentityProviderTests()
        {
            _connector = new DummyConnector();
            _testClass = new TwinIdentityProvider(_connector);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new TwinIdentityProvider(_connector);

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void CanConstructParameterless()
        {
            // Act
#pragma warning disable CS0618
            var instance = new TwinIdentityProvider();
#pragma warning restore CS0618

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void CannotConstructWithNullConnector()
        {
            Assert.Throws<ArgumentNullException>(() => new TwinIdentityProvider(default(Connector)));
        }

        [Fact]
        public void CanCallAddIdentity()
        {
            // Arrange
            var twinObject = Substitute.For<ITwinIdentity>();
            var identityVar = Substitute.For<OnlinerULInt, IOnline<ulong>>();
            identityVar.Cyclic.Returns(10101ul);
            identityVar.LastValue.Returns(10101ul);
            twinObject.Identity.Returns(identityVar);

            // Act
            _testClass.AddIdentity(twinObject);

            // Assert
            _testClass.Identities.First(p => p.Key == twinObject.Identity.Cyclic);
        }

        [Fact]
        public void CannotCallAddIdentityWithNullTwinObject()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.AddIdentity(default(ITwinIdentity)));
        }

        [Fact]
        public void CanCallGetTwinByIdentityWithObject()
        {
            // Arrange
            var identity = (ulong)1525885;
            var twinObject = Substitute.For<ITwinIdentity>();
            var identityVar = Substitute.For<OnlinerULInt, IOnline<ulong>>();
            identityVar.Cyclic.Returns(identity);
            identityVar.LastValue.Returns(identity);
            twinObject.Identity.Returns(identityVar);
            _testClass.AddIdentity(twinObject);

            // Act
            var result = _testClass.GetTwinByIdentity(twinObject);

            // Assert
            Assert.Equal(identity, result.Identity.LastValue);
        }

        [Fact]
        public void CannotCallGetTwinByIdentityWithObjectWithNullObj()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.GetTwinByIdentity(default(object)));
        }

        [Fact]
        public void CanCallGetTwinByIdentityWithITwinIdentity()
        {
            // Arrange
            var twinObject = Substitute.For<ITwinIdentity>();
            var identityVar = Substitute.For<OnlinerULInt, IOnline<ulong>>();
            identityVar.Cyclic.Returns(10101ul);
            identityVar.LastValue.Returns(10101ul);
            twinObject.Identity.Returns(identityVar);
            _testClass.AddIdentity(twinObject);

            // Act
            var result = _testClass.GetTwinByIdentity(twinObject);

            // Assert
            Assert.Equal(10101ul, result.Identity.LastValue);
        }

        [Fact]
        public void CanCallGetTwinByIdentityWithITwinIdentityAsObjectofTwinObject()
        {
            // Arrange
            var twinObject = Substitute.For<ITwinIdentity>();
            var identityVar = Substitute.For<OnlinerULInt, IOnline<ulong>>();
            identityVar.Cyclic.Returns(10101ul);
            identityVar.LastValue.Returns(10101ul);
            twinObject.Identity.Returns(identityVar);
            _testClass.AddIdentity(twinObject);

            // Act
            var result = _testClass.GetTwinByIdentity((object)twinObject);

            // Assert
            Assert.Equal(10101ul, result.Identity.LastValue);
        }

        [Fact]
        public void CanCallGetTwinByIdentityWithITwinIdentityAsObjectofObject()
        {
            // Arrange
            var twinObject = Substitute.For<ITwinIdentity>();
            var identityVar = Substitute.For<OnlinerULInt, IOnline<ulong>>();
            identityVar.Cyclic.Returns(10101ul);
            identityVar.LastValue.Returns(10101ul);
            twinObject.Identity.Returns(identityVar);
            _testClass.AddIdentity(twinObject);
            var obj = new object();
            // Act
            var result = _testClass.GetTwinByIdentity(obj);

            // Assert
            Assert.Same(obj, result);
        }

        [Fact]
        public void CannotCallGetTwinByIdentityWithITwinIdentityWithNullObj()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.GetTwinByIdentity(default(ITwinIdentity)));
        }

        [Fact]
        public void CanCallGetTwinByIdentityWithIdentity()
        {
            // Arrange
            var identity = (ulong)1309875540;
            var twinObject = Substitute.For<ITwinIdentity>();
            var identityVar = Substitute.For<OnlinerULInt, IOnline<ulong>>();
            identityVar.Cyclic.Returns(identity);
            identityVar.LastValue.Returns(identity);
            twinObject.Identity.Returns(identityVar);
            _testClass.AddIdentity(twinObject);

            // Act
            var result = _testClass.GetTwinByIdentity(identity);

            // Assert
            Assert.Equal(identity, result.Identity.Cyclic);
        }

        [Fact]
        public async void CanCallReadIdentities()
        {
            // Arrange
            var connector = Substitute.For<DummyConnector>();
            var testClass = Substitute.For<TwinIdentityProvider>(connector);

            // Act
            var result = await testClass.ReadIdentitiesAsync();

            // Assert
            connector.Received().ReadBatchAsync(result).Wait();
        }

        [Fact]
        public async void CanCallRefreshIdentities()
        {
            // Arrange
            var connector = Substitute.For<DummyConnector>();
            var testClass = Substitute.For<TwinIdentityProvider>(_connector);

            // Act
            await testClass.ConstructIdentitiesAsync();

            // Assert
            await testClass.Received().ReadIdentitiesAsync();
            await testClass.Received().SortIdentitiesAsync();
        }

        [Fact]
        public async void CanCallSortIdentities()
        {
            // Arrange
            var testClass = Substitute.For<TwinIdentityProvider>();

            var identityVar_2 = Substitute.For<OnlinerULInt, IOnline<ulong>>();
            identityVar_2.LastValue.Returns(2ul);
            var obj2 = Substitute.For<ITwinIdentity>();
            obj2.Identity.Returns(identityVar_2);
            
            var identityVar_1 = Substitute.For<OnlinerULInt, IOnline<ulong>>();
            var obj1 = Substitute.For<ITwinIdentity>();
            obj1.Identity.Returns(identityVar_1);
            identityVar_1.LastValue.Returns(1ul);
            
            var identityVar_3 = Substitute.For<OnlinerULInt, IOnline<ulong>>();
            identityVar_3.LastValue.Returns(3ul);
            var obj3 = Substitute.For<ITwinIdentity>();
            obj3.Identity.Returns(identityVar_3);

            testClass.AddIdentity(obj3);
            testClass.AddIdentity(obj2);
            testClass.AddIdentity(obj1);

            // Act
            await testClass.SortIdentitiesAsync();

            // Assert
            Assert.Equal(1ul, testClass.Identities.ElementAt(0).Key);
            Assert.Equal(2ul, testClass.Identities.ElementAt(1).Key);
            Assert.Equal(3ul, testClass.Identities.ElementAt(2).Key);
        }

        [Fact]
        public void CanGetIdentities()
        {
            // Assert
            Assert.IsType<SortedDictionary<ulong, ITwinIdentity>>(_testClass.Identities);
        }

        [Fact]
        public void CanGetIdentitiesCount()
        {
            // Assert
            Assert.IsType<long>(_testClass.IdentitiesCount);
        }
    }
}