// AXSharp.ConnectorTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Compiler.CsTests
{
    using AXSharp.Connector;
    using System;
    using Xunit;

    public class CompilerOmitsAttributeTests
    {
        private CompilerOmitsAttribute _testClass;
        private string[] _omissions;
        private CompilerOmissionGroups[] _omissionsGroups;

        public CompilerOmitsAttributeTests()
        {
            _omissions = new[] { "TestValue573015768", "TestValue1326200231", "TestValue826807718" };
            _omissionsGroups = new[] { CompilerOmissionGroups.BuilderShadowerInterface, CompilerOmissionGroups.BuilderShadowerInterface, CompilerOmissionGroups.BuilderOnlinerInterface };
            _testClass = new CompilerOmitsAttribute(_omissions);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new CompilerOmitsAttribute(_omissions);

            // Assert
            Assert.NotNull(instance);

            // Act
            instance = new CompilerOmitsAttribute(_omissions);

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void CannotConstructWithNullOmissions()
        {
            Assert.Throws<ArgumentNullException>(() => new CompilerOmitsAttribute(default(string[])));
            Assert.Throws<ArgumentNullException>(() => new CompilerOmitsAttribute(default(CompilerOmissionGroups[])));
        }

        [Fact]
        public void OmissionsIsInitializedCorrectly()
        {
            _testClass = new CompilerOmitsAttribute(_omissions);
            Assert.Same(_omissions, _testClass.Omissions);
            _testClass = new CompilerOmitsAttribute(_omissions);
            Assert.Same(_omissions, _testClass.Omissions);
        }
    }
}