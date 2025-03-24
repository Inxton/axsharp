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

internal class AddedPropertySetterAstNode : AstNode
{
    public string? MemberName { get; set; }
    public string? PropertyName { get; set; }
    public string? InitValue { get; set; }

    public override void Init(AstContext context, ParseTreeNode treeNode)
    {
        var grammar = context.GetGrammar();
        if (grammar != null)
        {
            PropertyName = treeNode.GetTheOnlyNode(grammar.AddedPropertyIdentifier.Name).FindTokenAndGetText();            
            var asFreeTextLiteral = InitValue = treeNode.GetTheOnlyNode(grammar.AddedPropertyInitializer.Name).FindTokenAndGetText();

            if (asFreeTextLiteral.StartsWith("\""))
            {
                InitValue = $"@{asFreeTextLiteral}";
            }
            else
            {
                InitValue = asFreeTextLiteral;
            }
                        
            MemberName = context.GetContext()?.Declaration?.Name;
        }
    }

    public override void AcceptVisitor(IAstVisitor visitor)
    {

        if (visitor is PragmaVisitor v)
        {
            v.Product.Product = MemberName != null
                ? $"{MemberName}.{PropertyName} = {InitValue};"
                : $"{PropertyName} = {InitValue};";

            if (PropertyName != null) v.Product.Property = (PropertyName, InitValue);
        }
    }
}