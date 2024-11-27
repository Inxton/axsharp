// AXSharp.ConnectorTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.ConnectorTests.Identity
{
    using AXSharp.Connector.Identity;
    using System;
    using Xunit;
    using AXSharp.Connector.ValueTypes;

    public class NullTwinIdentityTests
    {
        private NullTwinIdentity _testClass;

        public NullTwinIdentityTests()
        {
            _testClass = new NullTwinIdentity();
        }

        [Fact]
        public void CanGetIdentity()
        {
            // Assert
            Assert.Equal(0ul, _testClass.Identity.Cyclic);
        }

        [Fact]
        public void CanGetAttributeName()
        {
            // Assert
            Assert.Equal("agnostic", _testClass.AttributeName);

            
        }

        [Fact]
        public void CanGetSymbol()
        {
            // Assert
            Assert.Equal("agnostic",_testClass.Symbol);
        }

        [Fact]
        public void CanGetHumanReadable()
        {
            // Assert
            Assert.Equal("agnostic", _testClass.HumanReadable);
        }
    }
}