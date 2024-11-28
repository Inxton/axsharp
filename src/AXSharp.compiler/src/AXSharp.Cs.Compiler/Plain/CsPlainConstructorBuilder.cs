// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Globalization;
using System.Text;
using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Syntax.Parser;
using AXSharp.Compiler.Core;
using AXSharp.Compiler.Cs.Helpers;
using AXSharp.Compiler.Cs.Helpers.Onliners;
using AXSharp.Connector;
using AXSharp.Connector.BuilderHelpers;
using AXSharp.Compiler.Cs;

namespace AXSharp.Compiler.Cs.Plain;

internal class CsPlainConstructorBuilder : ICombinedThreeVisitor
{
    private readonly StringBuilder _constructorStatements = new();

    protected CsPlainConstructorBuilder(ISourceBuilder sourceBuilder)
    {
        SourceBuilder = sourceBuilder;
    }

    protected ISourceBuilder SourceBuilder { get; }

    public string Output => _constructorStatements.ToString().FormatCode();

    public void CreateClassDeclaration(IClassDeclaration classDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"{classDeclaration.GetFullyQualifiedPocoName()}");
    }

    public void CreateReferenceToDeclaration(IReferenceTypeDeclaration referenceTypeDeclaration, IxNodeVisitor visitor)
    {
        referenceTypeDeclaration.ReferencedType.Accept(visitor, this);
    }

    public void CreateScalarTypeDeclaration(IScalarTypeDeclaration scalarTypeDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"{scalarTypeDeclaration.TransformType()}");
    }

    public void CreateSemanticTypeAccess(ISemanticTypeAccess semanticTypeAccess, IxNodeVisitor visitor)
    {
        semanticTypeAccess.Type.Accept(visitor, this);
    }

    public void CreateStringTypeDeclaration(IStringTypeDeclaration stringTypeDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"{stringTypeDeclaration.TransformType()}");
    }

    public void CreateStructuredType(IStructuredTypeDeclaration structuredTypeDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"{structuredTypeDeclaration.GetFullyQualifiedPocoName()}");
    }

    public void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
    {
        if (fieldDeclaration.IsMemberEligibleForConstructor(SourceBuilder))
        {
            switch (fieldDeclaration.Type)
            {
                case IArrayTypeDeclaration array:
                    AddArrayMemberInitialization(array, fieldDeclaration, visitor);
                    break;
            }
        }
    }

    public virtual void CreateNamedValueTypeDeclaration(INamedValueTypeDeclaration namedValueTypeDeclaration,
        IxNodeVisitor visitor)
    {
        AddToSource(namedValueTypeDeclaration.ValueTypeAccess.Type.Name.ToUpperInvariant());
    }

    public void CreateInterfaceDeclaration(IInterfaceDeclaration interfaceDeclaration, IxNodeVisitor visitor)
    {
        // No way to construct interfaces.
    }

    public void CreateArrayTypeDeclaration(IArrayTypeDeclaration arrayTypeDeclaration, IxNodeVisitor visitor)
    {
       
    }

    protected void AddToSource(string token, string separator = " ")
    {
        _constructorStatements.Append($"{token}{separator}");
    }

    public static CsPlainConstructorBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
        ISourceBuilder sourceBuilder, bool isExtended, AXSharpProject project)
    {
        var builder = new CsPlainConstructorBuilder(sourceBuilder);


        builder.AddToSource(
            $"public {semantics.Name}()");


        if (isExtended)
        {
            builder.AddToSource(": base()");
        }

        builder.AddToSource("{");

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

        builder.AddToSource("}");
        return builder;
    }

    public static CsPlainConstructorBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
        ISourceBuilder sourceBuilder, bool isExtended, AXSharpProject project)
    {
        var builder = new CsPlainConstructorBuilder(sourceBuilder);


        builder.AddToSource(
            $"public {semantics.Name}()");


        if (isExtended)
        {
            builder.AddToSource(": base()");
        }

        builder.AddToSource("{");

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

        builder.AddToSource("}");
        return builder;
    }



    private void AddArrayMemberInitialization(IArrayTypeDeclaration type, IFieldDeclaration field,
        IxNodeVisitor visitor)
    {
        if(!type.IsMemberEligibleForConstructor(this.SourceBuilder))
            return;
        
        switch (type.ElementTypeAccess.Type)
        {
            case IClassDeclaration classDeclaration:
            case IStructuredTypeDeclaration structuredTypeDeclaration:
            case IEnumTypeDeclaration enumTypeDeclaration:
            case INamedValueTypeDeclaration namedValueTypeDeclaration:
                AddToSource("#pragma warning disable CS0612\n");
                AddToSource($"{typeof(Arrays).n()}.InstantiateArray({field.Name}, " +
                            "() => ");
                AddToSource("new");
                type.ElementTypeAccess.Type.Accept(visitor, this);
                var dimensions = "new[] {";
                foreach (var dimension in type.Dimensions)
                {
                    dimensions = $"{dimensions}({dimension.LowerBoundValue}, {dimension.UpperBoundValue})";
                }

                dimensions = $"{dimensions}}}";

                AddToSource($"(), {dimensions});");
                AddToSource("#pragma warning restore CS0612\n");
                break;
        }
    }

   

    public void AddTypeConstructionParameters(string parametersString)
    {
        AddToSource(parametersString);
    }

    /// <inheritdoc />
    public void CreateDocComment(IDocComment semanticTypeAccess, ICombinedThreeVisitor data)
    {
        AddToSource(semanticTypeAccess.AddDocumentationComment(SourceBuilder));
    }
}