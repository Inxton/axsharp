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

public class NoSourceOriginProviderTests
{
    private readonly string testFolder;

    public NoSourceOriginProviderTests()
    {
        var fi = new FileInfo(Assembly.GetExecutingAssembly().FullName!);
        testFolder = fi.Directory!.FullName;
    }

    [Fact]
    public void Resolves_none_mode_rooted_at_src()
    {
        var project = new AxProject(Path.Combine(testFolder, "samples", "units"));

        var origin = new NoSourceOriginProvider().Resolve(project);

        Assert.Equal(SourceOriginMode.None, origin.Mode);
        Assert.Equal(project.SrcFolder, origin.BaseFolder);
    }
}
