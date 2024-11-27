// AXSharp.Compiler.CsTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

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