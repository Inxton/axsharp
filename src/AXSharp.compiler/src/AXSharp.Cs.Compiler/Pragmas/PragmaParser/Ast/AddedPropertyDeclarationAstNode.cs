// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace AXSharp.Compiler.Cs.Pragmas.PragmaParser;

internal class AddedPropertyDeclarationAstNode : AstNode
{
    public string? AccessQualifier { get; set; }
    public string? Type { get; set; }
    public string? Identifier { get; set; }

    public override void Init(AstContext context, ParseTreeNode treeNode)
    {
        AccessQualifier = treeNode.ChildNodes[2].FindTokenAndGetText();
        Type = treeNode.ChildNodes[3].FindTokenAndGetText();
        Identifier = treeNode.ChildNodes[4].FindTokenAndGetText();
    }

    public override void AcceptVisitor(IAstVisitor visitor)
    {
        if (visitor is PragmaVisitor v)
        {
            if (Type.ToUpperInvariant() == "STRING" || Type.ToUpperInvariant() == "WSTRING")
            {
                v.Product.Product = $"private {Type} _{Identifier};" +
                            $"\n{AccessQualifier} {Type} {Identifier} " +
                            $"{{ " +
                            $"get => string.IsNullOrEmpty(_{Identifier}) ? SymbolTail : _{Identifier}.Interpolate(this).CleanUpLocalizationTokens(); set => _{Identifier} = value; " +
                            $"}}"+
                            $"{AccessQualifier} {Type} Get{Identifier}(System.Globalization.CultureInfo culture)" +
                            $"{{"+
                            $"return this.Translate(_{Identifier}, culture).Interpolate(this);" +
                            $"}}";
            }
            else
            {
                v.Product.Product = $"{AccessQualifier} {Type} {Identifier} {{ get; set; }}";
            }
           
        }
    }
}