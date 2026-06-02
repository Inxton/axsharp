// AXSharp.CompilerTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System.IO;
using System.Reflection;
using AXSharp.Compiler;

namespace AXSharp.CompilerTests.SourceOrigin;

public class ApaxSourceOriginProviderTests
{
    private readonly string testFolder;

    public ApaxSourceOriginProviderTests()
    {
        var fi = new FileInfo(Assembly.GetExecutingAssembly().FullName!);
        testFolder = fi.Directory!.FullName;
    }

    [Fact]
    public void Resolves_library_mode_rooted_at_src()
    {
        var project = new AxProject(Path.Combine(testFolder, "samples", "units"));

        var origin = new ApaxSourceOriginProvider().Resolve(project);

        Assert.Equal(SourceOriginMode.Library, origin.Mode);
        Assert.Equal("units", origin.PackageName);
        Assert.Equal("0.0.0", origin.PackageVersion);
        Assert.Equal(project.SrcFolder, origin.BaseFolder);
    }

    [Fact]
    public void Maps_missing_apax_metadata_to_empty_strings()
    {
        using var fixture = new TempAxProjectFixture("type: app\n");
        var project = new AxProject(fixture.Root);

        var origin = new ApaxSourceOriginProvider().Resolve(project);

        Assert.Equal(SourceOriginMode.Library, origin.Mode);
        Assert.Equal(string.Empty, origin.PackageName);
        Assert.Equal(string.Empty, origin.PackageVersion);
    }
}
