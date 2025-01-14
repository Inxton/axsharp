// AXSharp.Compiler.Cs
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace AXSharp.Compiler.Cs.Pragmas.PragmaParser;

internal class AttributeDeclarationAstNode : AstNode
{
    public string? AttributeLiteral { get; set; }

    public override void Init(AstContext context, ParseTreeNode treeNode)
    {
        AttributeLiteral = $"[{treeNode.ChildNodes[2].ChildNodes[1].FindTokenAndGetText()}]";
    }

    public override void AcceptVisitor(IAstVisitor visitor)
    {
        var v = visitor as PragmaVisitor;
        if (AttributeLiteral != null)
            if (v != null)
                v.Product.Product = AttributeLiteral;
    }
}