using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace AXSharp.Compiler.Cs.Pragmas.PragmaParser;

internal class GenericDeclarationAstNode : AstNode
{
    public string GenericTypeDeclaration { get; private set; }
    public string? GenericConstraints { get; private set; }
    public IEnumerable<string> GenericTypes { get; private set; }

    public override void Init(AstContext context, ParseTreeNode treeNode)
    {
        GenericTypeDeclaration = string.Join(",",treeNode.ChildNodes[1].ChildNodes.Select(p => p.Token.Text));

        GenericTypes = treeNode.ChildNodes[1].ChildNodes.Select(p => p.Token.Text);

        if (treeNode.ChildNodes.Count >= 3)
        {
            
            foreach (var node in treeNode.ChildNodes[3].ChildNodes)
            {
                GenericConstraints = $"{GenericConstraints} where {node.ChildNodes[1].Token.Text} : {node.ChildNodes[3].Token.Text}";
            }
        }
    }

    public override void AcceptVisitor(IAstVisitor visitor)
    {
        if (visitor is PragmaVisitor v)
        {
            v.Product.Product = $"<{GenericTypeDeclaration}>";
            v.Product.GenericConstrains = GenericConstraints;
            v.Product.GenericTypes = GenericTypes;
        }
    }
}