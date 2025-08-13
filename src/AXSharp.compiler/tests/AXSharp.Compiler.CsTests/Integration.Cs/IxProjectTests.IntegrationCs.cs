// AXSharp.CompilerTests
// Copyright (c)2022 MTS spol. s r.o. and Contributors All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System.Reflection;
using AXSharp.Compiler;
using AXSharp.Compiler.Cs.Onliner;
using AXSharp.Compiler.Cs.Plain;
using AXSharp.Compiler.CsTests;
using Polly;
using Xunit.Abstractions;

namespace AXSharp.CompilerTests.Integration.Cs;

public class IxProjectTestsAx : IxProjectTests
{

    public IxProjectTestsAx(ITestOutputHelper output) : base(output)
    {
        CompilerOptions = new CompilerTestOptions() { TargetPlatfromMoniker = "ax", OutputProjectFolder = @"samples\units\ix\ax" };
    }

    protected override CompilerTestOptions CompilerOptions { get; }

    protected override string ExpectedFolder => @"samples\units\expected\ax\.g\";
    
}

public class IxProjectTestsTia : IxProjectTests
{
    protected override CompilerTestOptions CompilerOptions { get; }
    
    protected override string ExpectedFolder => @"samples\units\expected\tia\.g\";
    

    public IxProjectTestsTia(ITestOutputHelper output) : base(output)
    {
        CompilerOptions = new CompilerTestOptions() { TargetPlatfromMoniker = "tia", OutputProjectFolder = @"samples\units\ix\tia" };
    }
}

public abstract class IxProjectTests
{
    private readonly ITestOutputHelper output;

    private readonly string testFolder;

    protected abstract CompilerTestOptions CompilerOptions { get; }

    protected abstract string ExpectedFolder { get; }
   

    public IxProjectTests(ITestOutputHelper output)
    {
        this.output = output;

#pragma warning disable CS8604 // Possible null reference argument.
        var executingAssemblyFileInfo
            = new FileInfo(Assembly.GetExecutingAssembly().FullName);
#pragma warning restore CS8604 // Possible null reference argument.

        testFolder = executingAssemblyFileInfo.Directory!.FullName;
    }

    [Fact]
    public void should_get_project_name()
    {
        var project = new AXSharpProject(new AxProject(Path.Combine(testFolder, @"samples\units\")), new Type[] { },
            typeof(CsProject), this.CompilerOptions);
        var expected = Path.Combine(testFolder, this.CompilerOptions.OutputProjectFolder);
        var actual = project.OutputFolder;

        // Assert.Equal(expected, actual);
    }

    [Fact]
    public void should_create_files_from_source_to_generated_output_folder()
    {
        var project = new AXSharpProject(new AxProject(Path.Combine(testFolder, @"samples\units\")),
            new[] { typeof(CsPlainSourceBuilder) }, typeof(CsProject), this.CompilerOptions);


        Policy
            .Handle<Exception>()
            .WaitAndRetry(10, a => TimeSpan.FromSeconds(2))
            .Execute(() =>
            {
                if (Directory.Exists(project.OutputFolder)) Directory.Delete(project.OutputFolder, true);
            });


        var rootSourceFolder = Path.Combine(testFolder, @"samples\units\src");
        var expected = Directory.EnumerateFiles(
            rootSourceFolder, "*.st", SearchOption.AllDirectories).Select(p => p.Remove(0, rootSourceFolder.Length));

        project.Generate();


        var rootOutputFolder = Path.Combine(project.OutputFolder, ".g", "POCO");
        var actual = Directory.EnumerateFiles(
            rootOutputFolder, "*.*", SearchOption.AllDirectories).Select(p => p.Remove(0, rootOutputFolder.Length));


        Assert.Equal(expected.Count(), actual.Count());

        var actualList = actual.ToList();
        var index = 0;
        foreach (var exp in expected) Assert.Equal(exp.Replace(".st", ".g.cs"), actualList[index++]);
    }

    [Fact]
    public void should_clean_output_folder()
    {
        should_create_files_from_source_to_generated_output_folder();
        var project = new AXSharpProject(new AxProject(Path.Combine(testFolder, @"samples\units\")),
            new[] { typeof(CsPlainSourceBuilder) }, typeof(CsProject), this.CompilerOptions);

        Assert.True(Directory.EnumerateFiles(project.OutputFolder, "*.g.cs", SearchOption.AllDirectories).Count() > 0);

        project.CleanOutput(project.OutputFolder);

        Assert.Equal(0, Directory.EnumerateFiles(project.OutputFolder, "*.g.cs", SearchOption.AllDirectories).Count());
    }

    [Fact]
    public void should_match_expected_and_generated_whole_project()
    {
        var project = new AXSharpProject(new AxProject(Path.Combine(testFolder, @"samples\units\")),
            new[] { typeof(CsPlainSourceBuilder), typeof(CsOnlinerSourceBuilder) }, typeof(CsProject), CompilerOptions);

        if (Directory.Exists(project.OutputFolder)) Directory.Delete(project.OutputFolder, true);

      

        project.Generate();

        var rootSourceFolder = Path.Combine(testFolder, ExpectedFolder);
        var expected = Directory.EnumerateFiles(
            rootSourceFolder, "*.g.cs", SearchOption.AllDirectories).Select(p => p);


        var rootOutputFolder = Path.Combine(project.OutputFolder, ".g");
        var actual = Directory.EnumerateFiles(
            rootOutputFolder, "*.*", SearchOption.AllDirectories).Select(p => p);


        Assert.Equal(expected.Count(), actual.Count());

        var actualList = actual.ToList();
        var index = 0;
        foreach (var exp in expected)
        {
            var currentIndex = index++;
            var expectedFileContent = File.ReadAllText(exp);
            var actualFileContent = File.ReadAllText(actualList[currentIndex]);

            try
            {
                var actualFileContentLines = actualFileContent.Split("\n").Select(a => a.Trim()).ToArray();
                var expectedFileContentLines = expectedFileContent.Split("\n").Select(a => a.Trim()).ToArray();

                for (int i = 0; i < expectedFileContentLines.Length; i++)
                {
                   Assert.Equal(expectedFileContentLines[i], actualFileContentLines[i]);
                }
            }
            catch (Exception)
            {
                output.WriteLine($"-- Case: {new FileInfo(exp).Name} vs {new FileInfo(actualList[currentIndex]).Name}");
                output.WriteLine($"-- expected\n{expectedFileContent}");
                output.WriteLine($"-- actual\n{actualFileContent}");
                throw;
            }
        }
    }

    [Fact]
    public void should_generate_all_even_when_fails_somewhere()
    {
        CompilerOptions.OutputProjectFolder = Path.Combine(testFolder, @$"samples\units\ix\{CompilerOptions.TargetPlatfromMoniker}");
        var project = new AXSharpProject(new AxProject(Path.Combine(testFolder, @"samples\units\")),
            new[] { typeof(CsPlainSourceBuilder), typeof(CsOnlinerSourceBuilder) }, typeof(CsProject), CompilerOptions);

        

        if (Directory.Exists(project.OutputFolder)) Directory.Delete(project.OutputFolder, true);


        project.Generate();

        var rootSourceFolder = Path.Combine(testFolder, ExpectedFolder);
        var expected = Directory.EnumerateFiles(
            rootSourceFolder, "*.g.cs", SearchOption.AllDirectories).Select(p => p);


        var rootOutputFolder = Path.Combine(project.OutputFolder, ".g");
        var actual = Directory.EnumerateFiles(
            rootOutputFolder, "*.*", SearchOption.AllDirectories).Select(p => p);


        //Assert.Equal(expected.Count(), actual.Count());

        var actualList = actual.ToList();
        var index = 0;
        foreach (var exp in expected)
        {
            var currentIndex = index++;
            var expectedFileContent = File.ReadAllText(exp);
            var actualFileContent = File.ReadAllText(actualList[currentIndex]);
            try
            {
                Assert.Equal(expectedFileContent, actualFileContent);
            }
            catch (Exception)
            {
                output.WriteLine($"-- Case: {new FileInfo(exp).Name} vs {new FileInfo(actualList[currentIndex]).Name}");
                output.WriteLine($"-- expected\n{expectedFileContent}");
                output.WriteLine($"-- actual\n{actualFileContent}");
            }
        }
    }

    [Fact]
    public void should_retrieve_dependencies_and_use_types_from_referenced_project()
    {
        var integrationProjectsPaths = new string[]
        {
            Path.GetFullPath(Path.Combine(testFolder, @"..\..\..\..\integration\actual\lib1")),
            Path.GetFullPath(Path.Combine(testFolder, @"..\..\..\..\integration\actual\lib2")),
            Path.GetFullPath(Path.Combine(testFolder, @"..\..\..\..\integration\actual\app"))
        };

        var projects = integrationProjectsPaths.Select(p => new AXSharpProject(new AxProject(p),
            new[] { typeof(CsPlainSourceBuilder), typeof(CsOnlinerSourceBuilder) }, typeof(CsProject), CompilerOptions));

        foreach (var project in projects)
        {
            project.Generate();
        }


        

        //if (Directory.Exists(project.OutputFolder))
        //{
        //    Directory.Delete(project.OutputFolder, true);
        //}

        

        


        var rootSourceFolder = Path.Combine(testFolder, @"..\..\..\..\integration\expected\app\ix\.g");
        var expected = Directory.EnumerateFiles(
            rootSourceFolder, "*.g.cs", SearchOption.AllDirectories).Select(p => p);


        var rootOutputFolder = Path.Combine(projects.Last().OutputFolder, ".g");
        var actual = Directory.EnumerateFiles(
            rootOutputFolder, "*.*", SearchOption.AllDirectories).Select(p => p);


        //Assert.Equal(expected.Count(), actual.Count());

        var actualList = actual.ToList();
        var index = 0;
        foreach (var exp in expected)
        {
            var currentIndex = index++;
            var expectedFileContent = File.ReadAllText(exp);
            var actualFileContent = File.ReadAllText(actualList[currentIndex]);
            try
            {
                Assert.Equal(expectedFileContent, actualFileContent);
            }
            catch (Exception)
            {
                output.WriteLine($"-- Case: {new FileInfo(exp).Name} vs {new FileInfo(actualList[currentIndex]).Name}");
                output.WriteLine($"-- expected\n{expectedFileContent}");
                output.WriteLine($"-- actual\n{actualFileContent}");
                throw;
            }
        }
    }
}