// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Irony.Interpreter.Ast;

namespace AXSharp.Compiler.Cs.Pragmas.PragmaParser;

internal class PragmaVisitor : IAstVisitor
{

    public VisitorProduct Product { get; } = new();

    public void BeginVisit(IVisitableNode node)
    {
            
    }

    public void EndVisit(IVisitableNode node)
    {
            
    }
}

public class VisitorProduct
{
    public string? Product { get; set; }
    public string? GenericConstrains { get; set; }
    public IEnumerable<string> GenericTypes { get; set; }
    public (string type, bool isPoco) GenericTypeAssignment { get; set; }
    
    public (string PropertyName, string? InitValue) Property { get; set; }
}