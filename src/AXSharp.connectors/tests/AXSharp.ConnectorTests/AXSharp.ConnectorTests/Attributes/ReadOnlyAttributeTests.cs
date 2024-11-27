// AXSharp.ConnectorTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.ConnectorTests.Attributes
{
    using AXSharp.Connector;
    using System;
    using Xunit;

    public class ReadOnlyAttributeTests
    {
        private ReadOnlyAttribute _testClass;

        public ReadOnlyAttributeTests()
        {
            _testClass = new ReadOnlyAttribute();
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ReadOnlyAttribute();

            // Assert
            Assert.NotNull(instance);
        }
    }
}