// AXSharp.Compiler.CsTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AXSharp.Compiler.Cs.Plain;
using Xunit.Abstractions;

namespace AXSharp.Compiler.CsTests.Cs.tia;

public class CsPlainSourceBuilderTests : CsSourceBuilderTests
{
    public CsPlainSourceBuilderTests(ITestOutputHelper output) : base(output)
    {
        OutputSubFolder = "POCO";
        builders = new[] { typeof(CsPlainSourceBuilder) };
    }

    protected override ICompilerOptions CompilerOptions => new CompilerTestOptions() { TargetPlatfromMoniker = "tia" };

    protected override string ExpectedFolder => @"samples\units\expected\tia\.g\";
}