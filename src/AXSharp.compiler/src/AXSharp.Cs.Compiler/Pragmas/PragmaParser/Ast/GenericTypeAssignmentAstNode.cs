using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace AXSharp.Compiler.Cs.Pragmas.PragmaParser;

internal class GenericTypeAssignmentAstNode : AstNode
{
    public (string type, bool isPoco) GenericTypeDeclaration { get; private set; }
    public string? GenericConstraints { get; private set; }
    public IEnumerable<string> GenericTypes { get; private set; }

    public override void Init(AstContext context, ParseTreeNode treeNode)
    {
        var isPoco = false;
        if (treeNode.ChildNodes.Count > 1 && treeNode.ChildNodes[1].ChildNodes.Count > 1)
        {
            isPoco = treeNode.ChildNodes[1].ChildNodes[1]?.Token?.Text.Trim() == "POCO";
        }
            
        GenericTypeDeclaration = (treeNode.ChildNodes[0].Token.Text.Trim(), isPoco);
    }

    public override void AcceptVisitor(IAstVisitor visitor)
    {
        if (visitor is PragmaVisitor v)
        {
            v.Product.GenericTypeAssignment = GenericTypeDeclaration;
        }
    }
}