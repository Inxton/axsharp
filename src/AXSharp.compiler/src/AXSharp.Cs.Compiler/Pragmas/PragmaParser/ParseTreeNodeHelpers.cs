// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Irony.Parsing;

namespace AXSharp.Compiler.Cs.Pragmas.PragmaParser;

internal static class ParseTreeNodeHelpers
{
    public static List<ParseTreeNode> GetNodes(this ParseTreeNode node, string termName, List<ParseTreeNode>? nodes = null)
    {
        if (nodes == null)
        {
            nodes = new List<ParseTreeNode>();
        }

        foreach (ParseTreeNode childNode in node.ChildNodes)
        {
            string term = childNode.Term.Name;

            if (term == termName)
            {
                nodes.Add(childNode);
            }
            else
            {
                GetNodes(childNode, termName, nodes);
            }
        }

        return nodes;
    }

    public static ParseTreeNode GetTheOnlyNode(this ParseTreeNode node, string termName, List<ParseTreeNode>? nodes = null)
    {
        if (nodes == null)
        {
            nodes = new List<ParseTreeNode>();
        }

        foreach (ParseTreeNode childNode in node.ChildNodes)
        {
            string term = childNode.Term.Name;

            if (term == termName)
            {
                nodes.Add(childNode);
            }
            else
            {
                GetNodes(childNode, termName, nodes);
            }
        }

        if (nodes.Count > 1)
            throw new Exception($"Internal error. Only one node {termName} is required.");

        if (nodes.Count == 0)
            throw new Exception($"Internal error. The node {termName} is required but was not found at expected position in the AstTree.");

        return nodes[0];
    }
}