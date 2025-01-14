// AXSharp.Compiler.Cs
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using Irony.Ast;

namespace AXSharp.Compiler.Cs.Pragmas.PragmaParser;

internal static class AstNodeHelpers
{
    public static PragmaAstContext? GetContext(this AstContext context)
    {
        return context as PragmaAstContext;
    }

    public static PragmaGrammar? GetGrammar(this AstContext context)
    {
        return context.Language.Grammar as PragmaGrammar;
    }
}