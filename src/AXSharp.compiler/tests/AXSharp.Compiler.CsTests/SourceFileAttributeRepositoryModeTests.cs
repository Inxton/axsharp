// AXSharp.Compiler.CsTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AXSharp.Compiler;
using AXSharp.Compiler.Cs;
using AXSharp.Compiler.Cs.Onliner;
using AXSharp.Compiler.Cs.Plain;
using Polly;
using Xunit;

namespace AXSharp.Compiler.CsTests;

public class SourceFileAttributeRepositoryModeTests
{
    private readonly string testFolder;

    public SourceFileAttributeRepositoryModeTests()
    {
#pragma warning disable CS8604
        var executingAssemblyFileInfo = new FileInfo(Assembly.GetExecutingAssembly().FullName);
#pragma warning restore CS8604
        testFolder = executingAssemblyFileInfo.Directory!.FullName;
    }

    private sealed class FixedOriginProvider : ISourceOriginProvider
    {
        private readonly SourceOrigin _origin;
        public FixedOriginProvider(SourceOrigin origin) => _origin = origin;
        public SourceOrigin Resolve(AxProject project) => _origin;
    }

    // In repository mode the per-type path is relativized against the repository root (here the
    // sample project folder), so the widget under src/sub becomes "src/sub/widget.st".
    [Theory]
    [InlineData(typeof(CsOnlinerSourceBuilder))]
    [InlineData(typeof(CsPlainSourceBuilder))]
    public void emits_repository_rooted_source_path(Type builder)
    {
        var projectFolder = Path.Combine(testFolder, "samples", "sourcefile");
        var origin = SourceOrigin.Repository(projectFolder, "https://github.com/org/repo", "deadbeef", "main");

        var content = Generate(builder, new FixedOriginProvider(origin), "ix-repo");

        Assert.Contains("[AXSharp.Connector.SourceFileAttribute(@\"src/sub/widget.st\")]", content);
        Assert.DoesNotContain("[AXSharp.Connector.SourceFileAttribute(@\"sub/widget.st\")]", content);
    }

    private string Generate(Type builder, ISourceOriginProvider provider, string outputSubFolder)
    {
        var projectFolder = Path.Combine(testFolder, "samples", "sourcefile");
        var options = new CompilerTestOptions
        {
            TargetPlatfromMoniker = "ax",
            OutputProjectFolder = Path.Combine("samples", "sourcefile", outputSubFolder, builder.Name)
        };

        var sourceFile = Path.Combine(projectFolder, "src", "sub", "widget.st");
        var project = new AXSharpProject(
            new AxProject(projectFolder, new[] { sourceFile }),
            new[] { builder },
            typeof(CsProject),
            options,
            sourceOriginProvider: provider);

        Policy
            .Handle<Exception>()
            .WaitAndRetry(10, a => TimeSpan.FromSeconds(2))
            .Execute(() =>
            {
                if (Directory.Exists(project.OutputFolder)) Directory.Delete(project.OutputFolder, true);
            });

        project.Generate();

        var generated = Directory
            .EnumerateFiles(project.OutputFolder, "widget.g.cs", SearchOption.AllDirectories)
            .Single();
        return File.ReadAllText(generated);
    }
}
