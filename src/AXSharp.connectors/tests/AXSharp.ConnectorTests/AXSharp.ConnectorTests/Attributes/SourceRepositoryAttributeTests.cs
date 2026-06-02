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

    public class SourceRepositoryAttributeTests
    {
        [Fact]
        public void Exposes_url_commit_and_branch()
        {
            var instance = new SourceRepositoryAttribute(
                "https://github.com/org/repo", "a1b2c3d", "main");

            Assert.Equal("https://github.com/org/repo", instance.Url);
            Assert.Equal("a1b2c3d", instance.Commit);
            Assert.Equal("main", instance.Branch);
        }

        [Fact]
        public void Is_assembly_scoped_single_use()
        {
            var usage = typeof(SourceRepositoryAttribute)
                .GetCustomAttribute<AttributeUsageAttribute>();

            Assert.NotNull(usage);
            Assert.Equal(AttributeTargets.Assembly, usage!.ValidOn);
            Assert.False(usage.AllowMultiple);
        }
    }
}
