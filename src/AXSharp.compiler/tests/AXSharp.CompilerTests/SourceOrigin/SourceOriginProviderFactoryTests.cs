// AXSharp.CompilerTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AXSharp.Compiler;

namespace AXSharp.CompilerTests.SourceOrigin;

public class SourceOriginProviderFactoryTests
{
    [Theory]
    [InlineData("auto")]
    [InlineData("AUTO")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("nonsense")]
    public void Defaults_to_git_auto_detection(string? mode)
    {
        Assert.IsType<GitSourceOriginProvider>(SourceOriginProviderFactory.Create(mode));
    }

    [Theory]
    [InlineData("apax")]
    [InlineData("APAX")]
    public void Apax_forces_library_mode(string mode)
    {
        Assert.IsType<ApaxSourceOriginProvider>(SourceOriginProviderFactory.Create(mode));
    }

    [Theory]
    [InlineData("off")]
    [InlineData("Off")]
    public void Off_disables_emission(string mode)
    {
        Assert.IsType<NoSourceOriginProvider>(SourceOriginProviderFactory.Create(mode));
    }
}
