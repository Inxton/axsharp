// AXSharp.Compiler.CsTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Diagnostics;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Polly;
using Xunit.Abstractions;

namespace AXSharp.Compiler.CsTests;

public abstract class CsSourceBuilderTests
{
    private readonly ITestOutputHelper output;

    private readonly string testFolder;

    protected IEnumerable<Type> builders;

    protected string OutputSubFolder;

    protected CsSourceBuilderTests(ITestOutputHelper output)
    {
        this.output = output;

#pragma warning disable CS8604 // Possible null reference argument.
        var executingAssemblyFileInfo
            = new FileInfo(Assembly.GetExecutingAssembly().FullName);
#pragma warning restore CS8604 // Possible null reference argument.

        testFolder = executingAssemblyFileInfo.Directory!.FullName;
    }

    [Fact]
    public void simple_empty_class()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void simple_empty_class_within_namespace()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void class_with_primitive_members()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void class_with_complex_members()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void class_with_non_public_members()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void configuration()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void class_with_pragmas()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void enum_simple()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void type_named_values()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void class_internal()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }


    [Fact]
    public void class_with_using_directives()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void file_with_usings()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void class_no_access_modifier()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void class_implements()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }


    [Fact]
    public void class_implements_multiple()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void class_extends()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void class_extends_and_implements()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void struct_simple()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void class_extended_by_known_type()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }


    [Fact]
    public void type_with_enum()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void ref_to_simple()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }


    [Fact]
    public void type_named_values_literals()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void array_declaration()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void class_all_primitives()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void types_with_name_attributes()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void types_with_property_attributes()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void makereadonce()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    //makereadonly

    [Fact]
    public void makereadonly()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void file_with_unsupported()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }
    
    [Fact]
    public void misc()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void compileromitsattribute()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void class_generic_extension()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }

    [Fact]
    public void mixed_access()
    {
        var memberName = GetMethodName();
        CompareOutputs(memberName);
    }
    


    private void CompareOutputs(string memberName)
    {
        var sourceFile = Path.Combine(testFolder, $@"samples\units\src\{memberName}.st");
        var project = new AXSharpProject(new AxProject(Path.Combine(testFolder, @"samples\units\"),
                new[] { sourceFile }),
            builders, typeof(CsProject));

        var expectedSourceFile =
            Path.Combine(testFolder, @$"samples\units\expected\.g\{OutputSubFolder}\{memberName}.g.cs");
        var actualSourceFile = Path.Combine(project.OutputFolder, @$".g\{OutputSubFolder}\{memberName}.g.cs");

        Policy
            .Handle<Exception>()
            .WaitAndRetry(10, a => TimeSpan.FromSeconds(2))
            .Execute(() =>
            {
                if (File.Exists(actualSourceFile)) File.Delete(actualSourceFile);
            });

        project.Generate();


        var actualFileContent = File.ReadAllText(actualSourceFile);
        var expectedFileContent = File.Exists(expectedSourceFile) ? File.ReadAllText(expectedSourceFile) : string.Empty;

        var actualFileContentLines = actualFileContent.Split("\n").Select(a => a.Trim()).ToArray();
        var expectedFileContentLines = expectedFileContent.Split("\n").Select(a => a.Trim()).ToArray();

        for (int i = 0; i < expectedFileContentLines.Length; i++)
        {
            Assert.Equal(expectedFileContentLines[i], actualFileContentLines[i]);
        }


        output.WriteLine("------------------------ actual ------------------");
        output.WriteLine(actualSourceFile);
        output.WriteLine(actualFileContent);
        output.WriteLine("------------------------ expected ------------------");
        output.WriteLine(expectedSourceFile);
        output.WriteLine(expectedFileContent);

        
    }

    public string GetMethodName()
    {
        var frame = new StackFrame(1);
        var method = frame.GetMethod();
        var name = method.Name;
        return name;
    }
}