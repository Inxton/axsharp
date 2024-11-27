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

    public static class DummyConnectorExtensionsTests
    {
        [Fact]
        public static void CanCallCreateDummy()
        {
            // Arrange
            var adapterBuilder = ConnectorAdapterBuilder.Build();

            // Act
            var result = adapterBuilder.CreateDummy();

            // Assert
            Assert.IsType<DummyConnector>(result.GetConnector(new object []{}));
        }


        [Fact]
        public static void CannotCallCreateDummyWithNullAdapterBuilder()
        {
            Assert.Throws<ArgumentNullException>(() => default(ConnectorAdapterBuilder).CreateDummy());
        }
    }
}