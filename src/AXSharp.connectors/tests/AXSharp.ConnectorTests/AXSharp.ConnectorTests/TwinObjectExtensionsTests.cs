// AXSharp.ConnectorTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Xunit;
using AXSharp.Connector;
using AXSharp.Connector.Localizations;
using Newtonsoft.Json.Serialization;

namespace AXSharp.Connector.Tests
{
    using AXSharp.Connector;
    using System;
    using Xunit;
    using NSubstitute;
    using System.Collections.Generic;
    using AXSharp.Connector.ValueTypes;
    using System.Threading.Tasks;
    using System.Globalization;

    public static class TwinObjectExtensionsTests
    {
        [Fact]
        public static void CanCallMakePrimitiveReadOnly()
        {
            // Arrange
            var structure = Substitute.For<ITwinPrimitive>();

            // Act
            structure.MakeReadOnly();

            // Assert
            Assert.Equal(ReadWriteAccess.Read, structure.ReadWriteAccess);
        }

        [Fact]
        public static void CanCallMakeTwinObjectReadOnly()
        {
            // Arrange
            var structure = Substitute.For<ITwinObject>();
            var primitive1 = Substitute.For<ITwinPrimitive>();
            structure.AddValueTag(primitive1);

            // Act
            structure.MakeReadOnly();

            // Assert
            Assert.Equal(ReadWriteAccess.Read, primitive1.ReadWriteAccess);
        }


        [Fact]
        public static void CanCallMakeTwinObjectRecursivelyReadOnly()
        {
            // Arrange
            var structure = Substitute.For<ITwinObject>();
            structure.GetChildren().Returns(new[] { Substitute.For<ITwinObject>() });
            structure.GetChildren().First().GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>() });
            var primitive1 = Substitute.For<ITwinPrimitive>();
            structure.AddValueTag(primitive1);

            // Act
            structure.MakeReadOnly();

            // Assert
            Assert.Equal(ReadWriteAccess.Read, primitive1.ReadWriteAccess);
            Assert.Equal(ReadWriteAccess.Read, structure.GetChildren().First().GetValueTags().First().ReadWriteAccess);
        }

        [Fact]
        public static void CanCallMakeTwinPrimitiveReadOnly()
        {
            // Arrange

            var primitive1 = Substitute.For<ITwinPrimitive>();

            // Act
            primitive1.MakeReadOnly();

            // Assert
            Assert.Equal(ReadWriteAccess.Read, primitive1.ReadWriteAccess);
        }

        [Fact]
        public static void CannotCallMakeReadOnlyWithNullStructure()
        {
            Assert.Throws<ArgumentNullException>(() => default(ITwinObject).MakeReadOnly());
        }

        [Fact]
        public static async Task CanCallReadAsync()
        {
            // Arrange
            var structure = Substitute.For<ITwinObject>();
            structure.GetConnector().Returns(Substitute.For<DummyConnector>());

            var primitive = Substitute.For<OnlinerBool>();
            structure.AddValueTag(primitive);

            // Act
            await structure.ReadAsync();

            // Assert

            structure.GetConnector().Received().ReadBatchAsync(structure.Received().RetrievePrimitives());
        }

        [Fact]
        public static async Task CannotCallReadAsyncWithNullStructure()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => default(ITwinObject).ReadAsync());
        }

        [Fact]
        public static async Task CanCallWriteAsync()
        {
            // Arrange
            var structure = Substitute.For<ITwinObject>();
            structure.GetConnector().Returns(Substitute.For<DummyConnector>());
            structure.GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>() });
            structure.GetChildren().Returns(new[] { Substitute.For<ITwinObject>() });
            structure.GetChildren().First().GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>() });


            // Act
            var primitives = await structure.WriteAsync();

            // Assert
            structure.Received().WriteAsync();
        }

        [Fact]
        public static async Task CannotCallWriteAsyncWithNullStructure()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => default(ITwinObject).WriteAsync());
        }

        [Fact]
        public static void CanCallRetrieveValueTags()
        {
            // Arrange
            var onlineObject = Substitute.For<ITwinObject>();
            var nestedOnlineObject = Substitute.For<ITwinObject>();

            onlineObject.GetChildren().Returns(new[] { nestedOnlineObject });
            onlineObject.GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>() });
            nestedOnlineObject.GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>() });

            var valueTags = new List<ITwinPrimitive>();

            // Act
            var result = onlineObject.RetrievePrimitives(valueTags);

            // Assert
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public static void CannotCallRetrieveValueTagsWithNullOnlineObject()
        {
            Assert.Throws<ArgumentNullException>(() => default(ITwinObject).RetrievePrimitives(new List<ITwinPrimitive>()));
        }

        [Fact]
        public static void CanCallSubscribeShadowValueChange()
        {


            var obj = Substitute.For<ITwinObject>();
            obj.GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>() });
            obj.GetChildren().Returns(new[] { Substitute.For<ITwinObject>() });
            obj.GetChildren().First().GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>() });
            OnlinerBase.ValueChangeDelegate valueChangeDelegate = (x, y, z) => { };

            // Act
            var subscribed = obj.SubscribeShadowValueChange(valueChangeDelegate);

            // Assert
            Assert.Equal(4, subscribed.Count());

            foreach (var subforedit in subscribed)
            {
                Assert.Equal(1, subforedit.ShadowValueChange.GetInvocationList().Length);
            }
        }

        [Fact]
        public static void CannotCallSubscribeShadowValueChangeWithNullObj()
        {
            Assert.Throws<ArgumentNullException>(() => default(ITwinObject).SubscribeShadowValueChange((x, y, z) => { }));
        }

        [Fact]
        public static void CannotCallSubscribeShadowValueChangeWithNullValueChangeDelegate()
        {
            Assert.Throws<ArgumentNullException>(() => Substitute.For<ITwinObject>().SubscribeShadowValueChange(default(OnlinerBase.ValueChangeDelegate)));
        }

        [Fact]
        public static void CanCallUnSubscribeShadowValueChange()
        {

            var obj = Substitute.For<ITwinObject>();
            obj.GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>() });
            obj.GetChildren().Returns(new[] { Substitute.For<ITwinObject>() });
            obj.GetChildren().First().GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>() });
            OnlinerBase.ValueChangeDelegate valueChangeDelegate = (x, y, z) => { };

            // Act
            var subscribed = obj.SubscribeShadowValueChange(valueChangeDelegate);

            // Assert
            Assert.Equal(4, subscribed.Count());

            foreach (var subforedit in subscribed)
            {
                Assert.Equal(1, subforedit.ShadowValueChange.GetInvocationList().Length);
            }

            // Act
            obj.UnSubscribeShadowValueChange();

            // Assert
            foreach (var subforedit in subscribed)
            {
                Assert.Null(subforedit.ShadowValueChange);
            }
        }

        [Fact]
        public static void CannotCallUnSubscribeShadowValueChangeWithNullObj()
        {
            Assert.Throws<ArgumentNullException>(() => default(ITwinObject).UnSubscribeShadowValueChange());
        }

        [Fact]
        public static void CanCallSubscribeEditValueChange()
        {
            // Arrange
            var obj = Substitute.For<ITwinObject>();
            obj.GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>() });
            obj.GetChildren().Returns(new[] { Substitute.For<ITwinObject>() });
            obj.GetChildren().First().GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>() });
            OnlinerBase.ValueChangeDelegate valueChangeDelegate = (x, y, z) => { };

            // Act
            var subscribed = obj.SubscribeEditValueChange(valueChangeDelegate);

            // Assert
            Assert.Equal(4, subscribed.Count());

            foreach (var subforedit in subscribed)
            {
                Assert.Equal(1, subforedit.EditValueChange.GetInvocationList().Length);
            }
        }

        [Fact]
        public static void CannotCallSubscribeEditValueChangeWithNullObj()
        {
            Assert.Throws<ArgumentNullException>(() => default(ITwinObject).SubscribeEditValueChange((x, y, z) => { }));
        }

        [Fact]
        public static void CannotCallSubscribeEditValueChangeWithNullValueChangeDelegate()
        {
            Assert.Throws<ArgumentNullException>(() => Substitute.For<ITwinObject>().SubscribeEditValueChange(default(OnlinerBase.ValueChangeDelegate)));
        }

        [Fact]
        public static void CanCallUnSubscribeEditValueChange()
        {
            var obj = Substitute.For<ITwinObject>();
            obj.GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>() });
            obj.GetChildren().Returns(new[] { Substitute.For<ITwinObject>() });
            obj.GetChildren().First().GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>() });
            OnlinerBase.ValueChangeDelegate valueChangeDelegate = (x, y, z) => { };

            // Act
            var subscribed = obj.SubscribeShadowValueChange(valueChangeDelegate);

            // Assert
            Assert.Equal(4, subscribed.Count());

            foreach (var subforedit in subscribed)
            {
                Assert.Equal(1, subforedit.EditValueChange.GetInvocationList().Length);
            }


            // Act
            obj.UnSubscribeEditValueChange();

            // Assert
            foreach (var subforedit in subscribed)
            {
                Assert.Null(subforedit.EditValueChange);
            }
        }

        [Fact]
        public static void CannotCallUnSubscribeEditValueChangeWithNullObj()
        {
            Assert.Throws<ArgumentNullException>(() => default(ITwinObject).UnSubscribeEditValueChange());
        }

        [Fact()]
        public static void CanCallMakeReadOnce()
        {
            var obj = Substitute.For<ITwinObject>();
            obj.GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>() });
            obj.GetChildren().Returns(new[] { Substitute.For<ITwinObject>() });
            obj.GetChildren().First().GetValueTags().Returns(new[] { Substitute.For<ITwinPrimitive>(), Substitute.For<ITwinPrimitive>() });

            obj.MakeReadOnce();
        }

        [Fact]
        public static void CannotCallMakeReadOnceWithNullStructure()
        {
            Assert.Throws<ArgumentNullException>(() => default(ITwinObject).MakeReadOnce());
        }

        [Fact]
        public static async Task CanCallOnlineToShadowAsync()
        {
            // Arrange
            var connector = new DummyConnector();
            var obj = Substitute.For<ITwinObject>();
            var onlinerb = new OnlinerByte();
            obj.GetValueTags().Returns(new[] { onlinerb });
            obj.GetConnector().Returns(connector);
            onlinerb.Cyclic = 155;


            // Act
            await obj.OnlineToShadowAsync();

            // Assert
            Assert.Equal(155, onlinerb.Shadow);
        }

        [Fact]
        public static async Task CannotCallOnlineToShadowAsyncWithNullObj()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => default(ITwinObject).OnlineToShadowAsync());
        }

        [Fact]
        public static async Task CanCallShadowToOnlineAsync()
        {
            // Arrange
            var connector = new DummyConnector();
            var obj = Substitute.For<ITwinObject>();
            var onlinerb = new OnlinerByte();
            obj.GetValueTags().Returns(new[] { onlinerb });
            obj.GetConnector().Returns(connector);
            onlinerb.Shadow = 180;


            // Act
            await obj.ShadowToOnlineAsync();

            // Assert
            Assert.Equal(180, onlinerb.Cyclic);
        }

        [Fact]
        public static async Task CannotCallShadowToOnlineAsyncWithNullObj()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => default(ITwinObject).ShadowToOnlineAsync());
        }

        private class MyTestPoco
        {

        }

        private class CreatePocoTestClass : ITwinObject
        {
            public string Symbol { get; }
            public string AttributeName { get; }
            public string HumanReadable { get; }

            public string GetAttributeName(CultureInfo culture)
            {
                return this.Translate(this.AttributeName, culture);
            }

            public string GetHumanReadable(CultureInfo culture)
            {
                return this.Translate(this.HumanReadable, culture);
            }

            public ITwinObject GetParent()
            {
                throw new NotImplementedException();
            }

            public string GetSymbolTail()
            {
                throw new NotImplementedException();
            }

            public void Poll()
            {
                throw new NotImplementedException();
            }

            public Translator Interpreter { get; }

            public IEnumerable<ITwinObject> GetChildren()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<ITwinElement> GetKids()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<ITwinPrimitive> GetValueTags()
            {
                throw new NotImplementedException();
            }

            public void AddChild(ITwinObject twinObject)
            {
                throw new NotImplementedException();
            }

            public void AddValueTag(ITwinPrimitive twinPrimitive)
            {
                throw new NotImplementedException();
            }

            public void AddKid(ITwinElement kid)
            {
                throw new NotImplementedException();
            }

            public Connector GetConnector()
            {
                throw new NotImplementedException();
            }

            public Task<T> OnlineToPlain<T>()
            {
                throw new NotImplementedException();
            }

            public Task PlainToOnline<T>(T plain)
            {
                throw new NotImplementedException();
            }

            public Task<T> ShadowToPlain<T>()
            {
                throw new NotImplementedException();
            }

            public Task PlainToShadow<T>(T plain)
            {
                throw new NotImplementedException();
            }

            public Task<bool> AnyChangeAsync<T>(T plain)
            {
                throw new NotImplementedException();
            }

            public MyTestPoco CreateEmptyPoco()
            {
                return new MyTestPoco();
            }
        }

        [Fact]
        public static void CanCallCreatePOCO()
        {
            // Arrange
            var obj = new CreatePocoTestClass();
            
            // Act
            var result = obj.CreatePoco();

            // Assert
            Assert.IsType<MyTestPoco>(result);
        }
    }
}