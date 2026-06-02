// AXSharp.ConnectorTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

namespace AXSharp.ConnectorTests.Attributes
{
    using AXSharp.Connector;
    using System;
    using System.Reflection;
    using Xunit;

    public class SourceLibraryAttributeTests
    {
        [Fact]
        public void Exposes_name_and_version()
        {
            var instance = new SourceLibraryAttribute("@ax/foo", "1.2.3");

            Assert.Equal("@ax/foo", instance.Name);
            Assert.Equal("1.2.3", instance.Version);
        }

        [Fact]
        public void Is_assembly_scoped_single_use()
        {
            var usage = typeof(SourceLibraryAttribute)
                .GetCustomAttribute<AttributeUsageAttribute>();

            Assert.NotNull(usage);
            Assert.Equal(AttributeTargets.Assembly, usage!.ValidOn);
            Assert.False(usage.AllowMultiple);
        }
    }
}
