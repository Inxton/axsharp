// AXSharp.CompilerTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Reflection;
using AXSharp.Compiler;

namespace AXSharp.CompilerTests;

public class AxProjectTests
{
    private readonly string testFolder;

    public AxProjectTests()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        var executingAssemblyFileInfo
            = new FileInfo(Assembly.GetExecutingAssembly().FullName);
#pragma warning restore CS8604 // Possible null reference argument.

        testFolder = executingAssemblyFileInfo.Directory!.FullName;
    }

    [Fact]
    public void should_get_project_name()
    {
        var project = new AxProject(Path.Combine(testFolder, @"samples//units//"));
        var expected = Path.Combine(testFolder, @"samples//units//");
        var actual = project.ProjectFolder;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void should_get_project_file()
    {
        var project = new AxProject(Path.Combine(testFolder, @"samples//units//"));
        var expected = Path.Combine(testFolder, @"samples//units//apax.yml");
        var actual = project.ProjectFile;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void should_retrive_project_information()
    {
        var project = new AxProject(Path.Combine(testFolder, @"samples//units//"));
#pragma warning disable CS0618
        var expected = new Apax { Name = "units" };
#pragma warning restore CS0618
        var actual = project.ProjectInfo;

        Assert.NotNull(actual);
        Assert.Equal(expected.Name, actual.Name);
    }

    [Fact]
    public void should_get_src_folder()
    {
        var project = new AxProject(Path.Combine(testFolder, @"samples//units//"));
        var expected = Path.Combine(project.ProjectFolder, "src");
        var actual = project.SrcFolder;


        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void should_retrieve_source_files()
    {
        var project = new AxProject(Path.Combine(testFolder, @"samples//units//"));
        var expected = Directory.EnumerateFiles(Path.Combine(project.ProjectFolder, "src"), "*.st",
            SearchOption.AllDirectories);
        var actual = project.Sources;


        Assert.NotNull(actual);
        Assert.Equal(expected.Count(), actual.Count());
    }
}