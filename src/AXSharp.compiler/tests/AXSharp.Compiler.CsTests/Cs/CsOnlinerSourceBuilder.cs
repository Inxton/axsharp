// AXSharp.Compiler.CsTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AXSharp.Compiler.Cs.Onliner;
using Xunit.Abstractions;

namespace AXSharp.Compiler.CsTests;

public class CsOnlinerSourceBuilderTests : CsSourceBuilderTests
{
    public CsOnlinerSourceBuilderTests(ITestOutputHelper output) : base(output)
    {
        OutputSubFolder = "Onliners";
        builders = new[] { typeof(CsOnlinerSourceBuilder) };
    }
}