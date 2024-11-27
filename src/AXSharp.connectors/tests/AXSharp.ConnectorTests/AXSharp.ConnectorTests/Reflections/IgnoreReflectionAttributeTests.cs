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
    using System.Reflection;

    public class IgnoreReflectionAttributeTests
    {
        private IgnoreReflectionAttribute _testClass;

        public IgnoreReflectionAttributeTests()
        {
            _testClass = new IgnoreReflectionAttribute();
        }

        [IgnoreReflection]
        public bool PropertyWithIgnoreReflectionAttribute { get; set; }
        
        public bool PropertyNoIgnoreReflectionAttribute { get; set; }

        [Fact]
        public void CanCallHasIgnoreReflectionAttributeWithProperty()
        {
            // Arrange
            var @property = this.GetType().GetProperty(nameof(PropertyWithIgnoreReflectionAttribute));

            // Act
            var result = IgnoreReflectionAttribute.HasIgnoreReflectionAttribute(property);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanCallHasIgnoreReflectionAttributeWithPropertyWithoutAttribute()
        {
            // Arrange
            var @property = this.GetType().GetProperty(nameof(PropertyNoIgnoreReflectionAttribute));

            // Act
            var result = IgnoreReflectionAttribute.HasIgnoreReflectionAttribute(property);

            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void CannotCallHasIgnoreReflectionAttributeWithPropertyWithNullProperty()
        {
            Assert.Throws<ArgumentNullException>(() => IgnoreReflectionAttribute.HasIgnoreReflectionAttribute(default(PropertyInfo)));
        }

        [Fact]
        public void CanCallHasIgnoreReflectionAttributeWithPropertyAndObj()
        {
            // Arrange
            var @property = this.GetType().GetProperty(nameof(PropertyWithIgnoreReflectionAttribute));
            var obj = this;

            // Act
            var result = IgnoreReflectionAttribute.HasIgnoreReflectionAttribute(property, obj);

            // Assert
            Assert.True(result);
        }

        [IgnoreReflection]
        public class TypeWithIgnoreReflectionPropertyAttribute
        {
        }

        [Fact]
        public void CallsHasIgnoreReflectionAttributeWithPropertyAndObjWithNullProperty()
        {
            Assert.False(IgnoreReflectionAttribute.HasIgnoreReflectionAttribute(default(PropertyInfo), new object()));
        }

        [Fact]
        public void CallsHasIgnoreReflectionAttributeWithPropertyAndObjWithNullPropertyObjectTypeWithIgnoreReflectionAttribute()
        {
            Assert.True(IgnoreReflectionAttribute.HasIgnoreReflectionAttribute(default(PropertyInfo), new TypeWithIgnoreReflectionPropertyAttribute()));
        }
        
        [Fact]
        public void CannotCallHasIgnoreReflectionAttributeWithPropertyAndObjWithNullObj()
        {
            Assert.False(IgnoreReflectionAttribute.HasIgnoreReflectionAttribute(this.GetType().GetProperty(nameof(PropertyNoIgnoreReflectionAttribute)), default(object)));
        }
    }
}