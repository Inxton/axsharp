// AXSharp.Compiler.CsTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.IO;
using System.Reflection;
using AXSharp.Compiler;
using AXSharp.Compiler.Cs;
using AXSharp.Compiler.Cs.Onliner;
using Polly;
using Xunit;

namespace AXSharp.Compiler.CsTests;

public class AssemblyAttributesEmissionTests
{
    private readonly string testFolder;

    public AssemblyAttributesEmissionTests()
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

    [Fact]
    public void Library_mode_emits_SourceLibrary_assembly_attribute()
    {
        // default provider => apax/library mode; sourcefile sample is "@ax/sourcefile" @ 0.0.0
        var content = GenerateAssemblyAttributes(provider: null, outputSubFolder: "ix-asm-lib");

        Assert.NotNull(content);
        Assert.Contains("[assembly: AXSharp.Connector.SourceLibrary(@\"@ax/sourcefile\", @\"0.0.0\")]", content);
    }

    [Fact]
    public void Repository_mode_emits_SourceRepository_assembly_attribute()
    {
        var projectFolder = Path.Combine(testFolder, "samples", "sourcefile");
        var origin = SourceOrigin.Repository(projectFolder, "https://github.com/org/repo", "deadbeef", "main");

        var content = GenerateAssemblyAttributes(new FixedOriginProvider(origin), "ix-asm-repo");

        Assert.NotNull(content);
        Assert.Contains(
            "[assembly: AXSharp.Connector.SourceRepository(@\"https://github.com/org/repo\", @\"deadbeef\", @\"main\")]",
            content);
    }

    [Fact]
    public void None_mode_emits_no_assembly_attributes_file()
    {
        var content = GenerateAssemblyAttributes(new NoSourceOriginProvider(), "ix-asm-none");

        Assert.Null(content);
    }

    private string? GenerateAssemblyAttributes(ISourceOriginProvider? provider, string outputSubFolder)
    {
        var projectFolder = Path.Combine(testFolder, "samples", "sourcefile");
        var options = new CompilerTestOptions
        {
            TargetPlatfromMoniker = "ax",
            OutputProjectFolder = Path.Combine("samples", "sourcefile", outputSubFolder)
        };

        var sourceFile = Path.Combine(projectFolder, "src", "sub", "widget.st");
        var project = new AXSharpProject(
            new AxProject(projectFolder, new[] { sourceFile }),
            new[] { typeof(CsOnlinerSourceBuilder) },
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

        var asmPath = Path.Combine(project.OutputFolder, ".g", "AssemblyAttributes.g.cs");
        return File.Exists(asmPath) ? File.ReadAllText(asmPath) : null;
    }
}
