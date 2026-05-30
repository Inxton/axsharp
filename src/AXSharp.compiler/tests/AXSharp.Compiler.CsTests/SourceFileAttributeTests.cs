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

public class SourceFileAttributeTests
{
    // sample under samples/sourcefile/src/sub/widget.st declares a CLASS, a STRUCT, an ENUM and an INTERFACE.
    private const string ExpectedAttribute = "[AXSharp.Connector.SourceFileAttribute(@\"sub/widget.st\")]";

    private readonly string testFolder;

    public SourceFileAttributeTests()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        var executingAssemblyFileInfo = new FileInfo(Assembly.GetExecutingAssembly().FullName);
#pragma warning restore CS8604 // Possible null reference argument.
        testFolder = executingAssemblyFileInfo.Directory!.FullName;
    }

    // The source file lives in a sub-folder of `src`, so the emitted path must be relativized
    // against the project `src` folder AND forward-slash normalized (a flat sample would not
    // catch the separator normalization).
    [Theory]
    [InlineData(typeof(CsOnlinerSourceBuilder))]
    [InlineData(typeof(CsPlainSourceBuilder))]
    public void emits_forward_slash_relative_source_path(Type builder)
    {
        var content = Generate(builder);

        Assert.Contains(ExpectedAttribute, content);
        Assert.DoesNotContain("sub\\widget.st", content);
    }

    // Placement invariant: every emitted source-file attribute is immediately followed by a
    // type declaration — never stranded on a using/namespace/member line. (The reverse is not
    // asserted: builders also emit nested helper partial classes that are intentionally
    // undecorated.) This is robust to how many types a given builder emits.
    [Theory]
    [InlineData(typeof(CsOnlinerSourceBuilder))]
    [InlineData(typeof(CsPlainSourceBuilder))]
    public void every_attribute_directly_precedes_a_type_declaration(Type builder)
    {
        var lines = Generate(builder)
            .Replace("\r", string.Empty)
            .Split('\n')
            .Select(l => l.Trim())
            .ToArray();

        var attributeLineIndexes = Enumerable.Range(0, lines.Length)
            .Where(i => lines[i] == ExpectedAttribute)
            .ToArray();

        Assert.NotEmpty(attributeLineIndexes);

        foreach (var i in attributeLineIndexes)
        {
            var next = NextNonEmpty(lines, i + 1);
            Assert.True(next != null && IsTypeDeclaration(next),
                $"SourceFileAttribute at line {i} is not directly followed by a type declaration but by: '{next}'");
        }
    }

    private static bool IsTypeDeclaration(string line) =>
        line.Contains("partial class ") ||
        line.Contains("partial interface ") ||
        line.StartsWith("public enum ");

    private static string? NextNonEmpty(string[] lines, int start)
    {
        for (var i = start; i < lines.Length; i++)
            if (lines[i].Length > 0) return lines[i];
        return null;
    }

    private string Generate(Type builder)
    {
        var projectFolder = Path.Combine(testFolder, "samples", "sourcefile");
        var options = new CompilerTestOptions
        {
            TargetPlatfromMoniker = "ax",
            OutputProjectFolder = Path.Combine("samples", "sourcefile", "ix", builder.Name)
        };

        var sourceFile = Path.Combine(projectFolder, "src", "sub", "widget.st");
        var project = new AXSharpProject(new AxProject(projectFolder, new[] { sourceFile }), new[] { builder }, typeof(CsProject), options);

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
