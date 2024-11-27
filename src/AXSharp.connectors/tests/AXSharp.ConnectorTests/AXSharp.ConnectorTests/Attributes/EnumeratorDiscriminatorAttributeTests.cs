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

    public class EnumeratorDiscriminatorAttributeTests
    {
        private EnumeratorDiscriminatorAttribute _testClass;
        private Type _enumeratorType;

        public EnumeratorDiscriminatorAttributeTests()
        {
            _enumeratorType = typeof(string);
            _testClass = new EnumeratorDiscriminatorAttribute(_enumeratorType);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new EnumeratorDiscriminatorAttribute(_enumeratorType);

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void CannotConstructWithNullEnumeratorType()
        {
            Assert.Throws<ArgumentNullException>(() => new EnumeratorDiscriminatorAttribute(default(Type)));
        }

        [Fact]
        public void EnumeratorTypeIsInitializedCorrectly()
        {
            Assert.Same(_enumeratorType, _testClass.EnumeratorType);
        }
    }
}