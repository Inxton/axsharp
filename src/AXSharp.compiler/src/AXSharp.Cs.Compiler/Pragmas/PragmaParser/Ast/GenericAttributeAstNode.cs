using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace AXSharp.Compiler.Cs.Pragmas.PragmaParser;

public class GenericAttributeAstNode : AstNode
{
    public override void Init(AstContext context, ParseTreeNode treeNode)
    {
        Children = treeNode.ChildNodes.Select(p => p.AstNode as AstNode).ToList();
    }

    public List<AstNode?> Children { get; set; }

    public override void AcceptVisitor(IAstVisitor visitor)
    {
        this.Children.ForEach(p =>
        {
            if (p != null)
            {
                p.AcceptVisitor(visitor);
            }
        });
    }
}