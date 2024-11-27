// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AX.ST.Semantic.Model.Declarations;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace AXSharp.Compiler.Cs.Pragmas.PragmaParser;

/// <summary>
/// Defines AST context.
/// </summary>
internal class PragmaAstContext : AstContext
{

    public IDeclaration Declaration { get; }

    public PragmaAstContext(LanguageData language, ParseTree rootTree, IDeclaration declaration)
        : base(language)
    {
        this.Declaration = declaration;
        this.DefaultIdentifierNodeType = typeof(AstNode);
        this.DefaultLiteralNodeType = typeof(AstNode);
        this.DefaultNodeType = typeof(AstNode);
    }
}