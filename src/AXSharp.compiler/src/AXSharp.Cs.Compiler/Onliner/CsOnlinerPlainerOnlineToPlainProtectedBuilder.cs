// AXSharp.Compiler.Cs
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System.Text;
using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Pragmas;
using AXSharp.Compiler.Core;
using AXSharp.Compiler.Cs.Helpers;
using AXSharp.Compiler.Cs.Helpers.Onliners;
using AXSharp.Compiler.Cs.Helpers.Plain;
using AXSharp.Connector;

namespace AXSharp.Compiler.Cs.Onliner;

internal class CsOnlinerPlainerOnlineToPlainProtectedBuilder : CsOnlinerPlainerOnlineToPlainBuilder
{
    private readonly StringBuilder _memberDeclarations = new();

    protected CsOnlinerPlainerOnlineToPlainProtectedBuilder(ISourceBuilder sourceBuilder) : base(sourceBuilder)
    {
        
    }

    public new static CsOnlinerPlainerOnlineToPlainProtectedBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
        ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerPlainerOnlineToPlainProtectedBuilder(sourceBuilder);
        builder.AddToSource($"protected async Task<{semantics.GetFullyQualifiedPocoName()}> {MethodName}Async({semantics.GetFullyQualifiedPocoName()} plain){{\n");
        
        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource($"return plain;");
        builder.AddToSource($"}}");
        return builder;
    }

    public new static CsOnlinerPlainerOnlineToPlainProtectedBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
        ISourceBuilder sourceBuilder, bool isExtended)
    {
        var builder = new CsOnlinerPlainerOnlineToPlainProtectedBuilder(sourceBuilder);

        //var qualifier = isExtended ? "new" : string.Empty;
        var qualifier = string.Empty;
        builder.AddToSource($"[Obsolete(\"This method should not be used if you indent to access the controllers data. Use `{MethodName}` instead.\")]");
        builder.AddToSource("[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]");
        builder.AddToSource($"protected {qualifier} async Task<{semantics.GetFullyQualifiedPocoName()}> {MethodNameNoac}Async({semantics.GetFullyQualifiedPocoName()} plain){{\n");
        
        
        if (isExtended)
        {
            builder.AddToSource($"#pragma warning disable CS0612\n");
            builder.AddToSource($"await base.{MethodNameNoac}Async(plain);");
            builder.AddToSource($"#pragma warning restore CS0612\n");
        }

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource($"return plain;");
        builder.AddToSource($"}}");
        return builder;
    }
}