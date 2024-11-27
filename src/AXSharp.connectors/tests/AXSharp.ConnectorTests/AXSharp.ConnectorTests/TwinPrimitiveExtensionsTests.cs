// AXSharp.ConnectorTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.ConnectorTests
{
    using AXSharp.Connector;
    using T = System.String;
    using System;
    using Xunit;
    using AXSharp.Connector.ValueTypes;

    public class OnlinerTest : OnlinerBase<dynamic>
    {
        public override dynamic InstanceMinValue { get; }
        public override dynamic InstanceMaxValue { get; }
    }

    public static class TwinPrimitiveExtensionsTests
    {
        [Fact]
        public static void CanCallSetCyclicValue()
        {
            // Arrange
            var primitive = new OnlinerTest();
            var value = "TestValue1138470980";

            // Act
            primitive.SetCyclicValue<T>(value);

            // Assert
            Assert.Equal(value, primitive.Cyclic);
        }

        [Fact]
        public static void CannotCallSetCyclicValueWithNullPrimitive()
        {
            Assert.Throws<ArgumentNullException>(() => default(OnlinerBase).SetCyclicValue<T>("TestValue1332534875"));
        }

        [Fact]
        public static void CanCallSetShadowValue()
        {
            // Arrange
            var primitive = new OnlinerTest();
            var value = "TestValue1363388558";

            // Act
            primitive.SetShadowValue<T>(value);

            // Assert
            Assert.Equal(value, primitive.Shadow);
            
        }

        [Fact]
        public static void CannotCallSetShadowValueWithNullPrimitive()
        {
            Assert.Throws<ArgumentNullException>(() => default(OnlinerBase).SetShadowValue<T>("TestValue1632757984"));
        }

        [Fact]
        public static void CanCallGetCyclicValue()
        {
            // Arrange
            var expeted = "tempus imperdiet nulla malesuada pellentesque";
            var primitive = new OnlinerTest();
            primitive.Cyclic = expeted;
            // Act
            var result = primitive.GetCyclicValue<T>();

            // Assert
            Assert.Equal(expeted, result);
        }

        [Fact]
        public static void CannotCallGetCyclicValueWithNullPrimitive()
        {
            Assert.Throws<ArgumentNullException>(() => default(OnlinerBase).GetCyclicValue<T>());
        }

        [Fact]
        public static void CanCallGetLastValue()
        {
            // Arrange
            var expected = "ultricies mi quis hendrerit dolor";
            var primitive = new OnlinerTest();

#pragma warning disable CS0618
            primitive.SetLastValue = expected;
#pragma warning restore CS0618
            // Act
            var result = primitive.GetLastValue<T>();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void CannotCallGetLastValueWithNullPrimitive()
        {
            Assert.Throws<ArgumentNullException>(() => default(OnlinerBase).GetLastValue<T>());
        }

        [Fact]
        public static void CanCallGetShadowValue()
        {
            // Arrange
            var expected = "tincidunt eget nullam non nisi";
            var primitive = new OnlinerTest();
            primitive.Shadow = expected;
            // Act
            var result = primitive.GetShadowValue<T>();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void CannotCallGetShadowValueWithNullPrimitive()
        {
            Assert.Throws<ArgumentNullException>(() => default(OnlinerBase).GetShadowValue<T>());
        }
    }
}