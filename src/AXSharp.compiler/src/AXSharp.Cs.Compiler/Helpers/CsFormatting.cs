// AXSharp.Compiler.Cs
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AXSharp.Compiler.Cs.Helpers;

internal static class CsFormatting
{
    public static string FormatCode(this string code)
    {
        return CSharpSyntaxTree.ParseText(code).GetRoot().NormalizeWhitespace(eol:"\n").SyntaxTree.GetText().ToString();
    }
}