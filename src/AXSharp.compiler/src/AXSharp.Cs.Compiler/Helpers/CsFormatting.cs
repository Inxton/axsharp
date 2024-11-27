// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AXSharp.Compiler.Cs.Helpers;

internal static class CsFormatting
{
    public static string FormatCode(this string code)
    {
        return CSharpSyntaxTree.ParseText(code).GetRoot().NormalizeWhitespace().SyntaxTree.GetText().ToString();
    }
}