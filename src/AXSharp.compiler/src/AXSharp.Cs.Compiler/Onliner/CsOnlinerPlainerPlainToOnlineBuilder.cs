// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Text;
using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Pragmas;
using AXSharp.Compiler.Core;
using AXSharp.Compiler.Cs.Helpers;
using AXSharp.Compiler.Cs.Helpers.Onliners;
using AX.ST.Syntax.Tree;
using AXSharp.Compiler.Cs.Helpers.Plain;
using AXSharp.Connector;

namespace AXSharp.Compiler.Cs.Onliner;

internal class CsOnlinerPlainerPlainToOnlineBuilder : ICombinedThreeVisitor
{
    private readonly StringBuilder _memberDeclarations = new();

    protected CsOnlinerPlainerPlainToOnlineBuilder(ISourceBuilder sourceBuilder)
    {
        SourceBuilder = sourceBuilder;
    }

    private ISourceBuilder SourceBuilder { get; }

    public string Output => _memberDeclarations.ToString().FormatCode();

    
    public void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
    {
        if (fieldDeclaration.IsMemberEligibleForTranspile(SourceBuilder, "POCO"))
        {
            CreateAssignment(fieldDeclaration.Type, fieldDeclaration);
        }
    }

    public void CreateInterfaceDeclaration(IInterfaceDeclaration interfaceDeclaration, IxNodeVisitor visitor)
    {
        //
    }

    public void CreateVariableDeclaration(IVariableDeclaration variableDeclaration, IxNodeVisitor visitor)
    {
        if (variableDeclaration.IsMemberEligibleForTranspile(SourceBuilder, "POCO"))
        {
            CreateAssignment(variableDeclaration.Type, variableDeclaration);
        }
    }

    private void CreateAssignment(ITypeDeclaration typeDeclaration, IDeclaration declaration)
    {
        switch (typeDeclaration)
        {
            case IInterfaceDeclaration interfaceDeclaration:
                break;
            case IClassDeclaration classDeclaration:
            //case IAnonymousTypeDeclaration anonymousTypeDeclaration:
            case IStructuredTypeDeclaration structuredTypeDeclaration:
                AddToSource($"#pragma warning disable CS0612\n");
                AddToSource($" await this.{declaration.Name}.{MethodNameNoac}Async(plain.{declaration.Name});");
                AddToSource($"#pragma warning restore CS0612\n");
                break;
            case IArrayTypeDeclaration arrayTypeDeclaration:

                if (arrayTypeDeclaration.IsMemberEligibleForConstructor(SourceBuilder))
                {
                    switch (arrayTypeDeclaration.ElementTypeAccess.Type)
                    {
                        case IClassDeclaration classDeclaration:
                        case IStructuredTypeDeclaration structuredTypeDeclaration:
                            AddToSource($"var _{declaration.Name}_i_FE8484DAB3 = 0;");
                            AddToSource($"#pragma warning disable CS0612\n");
                            AddToSource(
                                $"{declaration.Name}.Select(p => p.{MethodNameNoac}Async(plain.{declaration.Name}[_{declaration.Name}_i_FE8484DAB3++])).ToArray();");
                            AddToSource($"#pragma warning restore CS0612\n");
                            break;
                        case IScalarTypeDeclaration scalarTypeDeclaration:
                        case IStringTypeDeclaration stringTypeDeclaration:
                            AddToSource($"var _{declaration.Name}_i_FE8484DAB3 = 0;");
                            AddToSource($"#pragma warning disable CS0612\n");
                            AddToSource(
                                $"{declaration.Name}.Select(p => p.LethargicWrite(plain.{declaration.Name}[_{declaration.Name}_i_FE8484DAB3++])).ToArray();");
                            AddToSource($"#pragma warning restore CS0612\n");
                            break;
                    }
                }

                break;
            case IReferenceTypeDeclaration referenceTypeDeclaration:
                break;
            case IEnumTypeDeclaration enumTypeDeclaration:
                AddToSource($"#pragma warning disable CS0612\n");
                AddToSource($" {declaration.Name}.LethargicWrite((short)plain.{declaration.Name});");
                AddToSource($"#pragma warning restore CS0612\n");
                break;
            case INamedValueTypeDeclaration namedValueTypeDeclaration:
                AddToSource($"#pragma warning disable CS0612\n");
                AddToSource($" {declaration.Name}.LethargicWrite(plain.{declaration.Name});");
                AddToSource($"#pragma warning restore CS0612\n");
                break;
            case IScalarTypeDeclaration scalarTypeDeclaration:
            case IStringTypeDeclaration stringTypeDeclaration:
                AddToSource($"#pragma warning disable CS0612\n");
                AddToSource($" {declaration.Name}.LethargicWrite(plain.{declaration.Name});");
                AddToSource($"#pragma warning restore CS0612\n");
                break;
        }
    }

    public void CreateArrayTypeDeclaration(IArrayTypeDeclaration arrayTypeDeclaration, IxNodeVisitor visitor)
    {
        //
    }


    protected void AddToSource(string token, string separator = " ")
    {
        _memberDeclarations.Append($"{token}{separator}");
    }

    public void AddTypeConstructionParameters(string parametersString)
    {
        AddToSource(parametersString);
    }

    private static readonly string MethodName = TwinObjectExtensions.PlainToOnlineMethodName;
    private static readonly string MethodNameNoac = $"_{TwinObjectExtensions.PlainToOnlineMethodName}Noac";


    public static CsOnlinerPlainerPlainToOnlineBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
        ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerPlainerPlainToOnlineBuilder(sourceBuilder);

        builder.AddToSource(CsHelpers.CreateGenericSwapperMethodFromPlainer(MethodName, $"Pocos.{semantics.FullyQualifiedName}", false));

        builder.AddToSource($"public async Task<IEnumerable<ITwinPrimitive>> {MethodName}Async(Pocos.{semantics.FullyQualifiedName} plain){{\n");

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

        builder.AddToSource("return await this.WriteAsync<IgnoreOnPocoOperation>();");

        builder.AddToSource($"}}");

        // Noac method
        builder.AddToSource($"[Obsolete(\"This method should not be used if you indent to access the controllers data. Use `{MethodName}` instead.\")]");
        builder.AddToSource("[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]");
        builder.AddToSource($"public async Task {MethodNameNoac}Async(Pocos.{semantics.FullyQualifiedName} plain){{\n");

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

        builder.AddToSource($"}}");

        return builder;
    }

    public static CsOnlinerPlainerPlainToOnlineBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
        ISourceBuilder sourceBuilder, bool isExtended)
    {
        var builder = new CsOnlinerPlainerPlainToOnlineBuilder(sourceBuilder);

        builder.AddToSource(CsHelpers.CreateGenericSwapperMethodFromPlainer(MethodName, $"Pocos.{semantics.FullyQualifiedName}", isExtended));

        //var qualifier = isExtended ? "new" : string.Empty;
        var qualifier = string.Empty;

        builder.AddToSource($"public {qualifier} async Task<IEnumerable<ITwinPrimitive>> {MethodName}Async(Pocos.{semantics.FullyQualifiedName} plain){{\n");
       

        if (isExtended)
        {
            builder.AddToSource($"await base.{MethodNameNoac}Async(plain);");
        }

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

        builder.AddToSource("return await this.WriteAsync<IgnoreOnPocoOperation>();");

        builder.AddToSource($"}}");

        // Noac method
        builder.AddToSource($"[Obsolete(\"This method should not be used if you indent to access the controllers data. Use `{MethodName}` instead.\")]");
        builder.AddToSource("[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]");
        builder.AddToSource($"public async Task {MethodNameNoac}Async(Pocos.{semantics.FullyQualifiedName} plain){{\n");
        
        if (isExtended)
        {
            builder.AddToSource($"await base.{MethodNameNoac}Async(plain);");
        }

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

        builder.AddToSource($"}}");

        return builder;
    }

    /// <inheritdoc />
    public void CreateDocComment(IDocComment semanticTypeAccess, ICombinedThreeVisitor data)
    {
        AddToSource(semanticTypeAccess.AddDocumentationComment(SourceBuilder));
    }
}