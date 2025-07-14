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

namespace AXSharp.Compiler.Cs.Onliner;

internal class CsOnlinerMemberBuilder : ICombinedThreeVisitor
{
    private readonly StringBuilder _memberDeclarations = new();

    protected CsOnlinerMemberBuilder(ISourceBuilder ISourceBuilder)
    {
        SourceBuilder = ISourceBuilder;
    }

    private ISourceBuilder SourceBuilder { get; }

    public string Output => _memberDeclarations.ToString().FormatCode();

    public void CreateArrayTypeDeclaration(IArrayTypeDeclaration arrayTypeDeclaration, IxNodeVisitor visitor)
    {
        var type = this.SourceBuilder.Compilation.FindTypeDeclaration(arrayTypeDeclaration.ElementTypeAccess, true);
        //arrayTypeDeclaration.ElementTypeAccess.Type.Accept(visitor, this);
        type.Accept(visitor, this);
        AddToSource("[]");
    }

    public void CreateClassDeclaration(IClassDeclaration classDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"{classDeclaration.GetQualifiedName()}");
    }

    public void CreateEnumTypeDeclaration(IEnumTypeDeclaration enumTypeDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"{enumTypeDeclaration.GetQualifiedName()}");
    }


    public void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
    {
        var eligibility = fieldDeclaration.IsMemberEligibleForTranspile(SourceBuilder, warnMissingOrInconsistent: true);
        if (eligibility.isEligible)
        {
            AddToSource(fieldDeclaration.Pragmas.AddAttributes());

            // TODO: This is not nice refactor, also we should embed the int wrapper into actual member of enum type!
            switch (fieldDeclaration.Type)
            {
                case IEnumTypeDeclaration @enum:
                    AddToSource($"[AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof({@enum.GetQualifiedName()}))]");
                    AddToSource($"{fieldDeclaration.AccessModifier.Transform()} ");
                    AddToSource("OnlinerInt");
                    AddToSource($" {fieldDeclaration.Name}");
                    AddToSource("{get;}");
                    break;
                case INamedValueTypeDeclaration namedValue:
                    AddToSource(
                        $"[AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof({namedValue.GetQualifiedName()}))]");
                    AddToSource($"{fieldDeclaration.AccessModifier.Transform()} ");
                    eligibility.eligibleType.Accept(visitor, this);
                    //fieldDeclaration.Type.Accept(visitor, this);
                    AddToSource($" {fieldDeclaration.Name}");
                    AddToSource("{get;}");
                    break;
                case IArrayTypeDeclaration array:
                    var arrayEligibility = array.IsEligibleForTranspile(SourceBuilder, true);
                    if (arrayEligibility.isEligibe)
                    {
                        AddToSource($"{fieldDeclaration.AccessModifier.Transform()} ");
                        //arrayEligibility.eligibleType.Accept(visitor, this);
                        fieldDeclaration.Type.Accept(visitor, this);
                        AddToSource($" {fieldDeclaration.Name}");
                        AddToSource("{get;}");
                    }
                    break;
                default:
                    AddToSource($"{fieldDeclaration.AccessModifier.Transform()} ");
                    eligibility.eligibleType.Accept(visitor, this);
                    //fieldDeclaration.Type.Accept(visitor, this);
                    AddToSource($" {fieldDeclaration.Name}");
                    AddToSource("{get;}");
                    break;
            }
        }
    }

    public virtual void CreateNamedValueTypeDeclaration(INamedValueTypeDeclaration namedValueTypeDeclaration,
        IxNodeVisitor visitor)
    {
        AddToSource(namedValueTypeDeclaration.ValueTypeAccess.Type.TransformType());
    }


    public void CreateInterfaceDeclaration(IInterfaceDeclaration interfaceDeclaration, IxNodeVisitor visitor)
    {
        AddToSource(interfaceDeclaration.Type.GetQualifiedName());
    }

    public void CreatePragma(IPragma pragma, ICombinedThreeVisitor visitor)
    {
        // This is handled by separate logic in PragmaExtensions.
    }

    public void CreateReferenceToDeclaration(IReferenceTypeDeclaration referenceTypeDeclaration, IxNodeVisitor visitor)
    {
        referenceTypeDeclaration.Pragmas.ToList().ForEach(p => p.Accept(visitor, this));
        referenceTypeDeclaration.ReferencedType.Accept(visitor, this);
    }

    public void CreateScalarTypeDeclaration(IScalarTypeDeclaration scalarTypeDeclaration, IxNodeVisitor visitor)
    {
        scalarTypeDeclaration.Pragmas.ToList().ForEach(p => p.Accept(visitor, this));
        AddToSource($"{scalarTypeDeclaration.TransformType()}");
    }

    public void CreateSemanticTypeAccess(ISemanticTypeAccess semanticTypeAccess, IxNodeVisitor visitor)
    {
        semanticTypeAccess.Type.Accept(visitor, this);
    }

    public void CreateStringTypeDeclaration(IStringTypeDeclaration stringTypeDeclaration, IxNodeVisitor visitor)
    {
        stringTypeDeclaration.Pragmas.ToList().ForEach(p => p.Accept(visitor, this));
        AddToSource($"{stringTypeDeclaration.TransformType()}");
    }

    public void CreateStructuredType(IStructuredTypeDeclaration structuredTypeDeclaration, IxNodeVisitor visitor)
    {
        structuredTypeDeclaration.Pragmas.ToList().ForEach(p => p.Accept(visitor, this));
        AddToSource($"{structuredTypeDeclaration.GetQualifiedName()}");
    }

    public void CreateVariableDeclaration(IVariableDeclaration semantics, IxNodeVisitor visitor)
    {
        var eligibility = semantics.IsMemberEligibleForTranspile(SourceBuilder, warnMissingOrInconsistent: true);
        if (eligibility.isEligibe)
        {
            AddToSource(semantics.Pragmas.AddAttributes());

            // TODO: This is not nice refactor, also we should embed the int wrapper into actual member of enum type!
            switch (eligibility.eligibleType)
            {
                case IEnumTypeDeclaration @enum:
                    AddToSource($"[AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof({@enum.GetQualifiedName()}))]");
                    AddToSource($"public");
                    AddToSource("OnlinerInt");
                    AddToSource($" {semantics.Name}");
                    AddToSource("{get;}");
                    break;
                case INamedValueTypeDeclaration namedValue:
                    AddToSource(
                        $"[AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof({namedValue.GetQualifiedName()}))]");
                    AddToSource($"public");
                    eligibility.eligibleType.Accept(visitor, this);
                    //semantics.Type.Accept(visitor, this);
                    AddToSource($" {semantics.Name}");
                    AddToSource("{get;}");
                    break;
                case IArrayTypeDeclaration array:
                    var arrayEligible = array.IsEligibleForTranspile(SourceBuilder, true);
                    if (arrayEligible.isEligibe)
                    {
                        AddToSource($"public");
                        eligibility.eligibleType.Accept(visitor, this);
                        //semantics.Type.Accept(visitor, this);
                        AddToSource($" {semantics.Name}");
                        AddToSource("{get;}");
                    }
                    break;
                default:
                    AddToSource($"public");
                    eligibility.eligibleType.Accept(visitor, this);
                    //semantics.Type.Accept(visitor, this);
                    AddToSource($" {semantics.Name}");
                    AddToSource("{get;}");
                    break;
            }
        }
    }


    protected void AddToSource(string token, string separator = " ")
    {
        _memberDeclarations.Append($"{token}{separator}");
    }

    public void AddTypeConstructionParameters(string parametersString)
    {
        AddToSource(parametersString);
    }

    public static CsOnlinerMemberBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
        ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerMemberBuilder(sourceBuilder);
        builder.AddToSource(semantics.DeclareProperties());
        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

        builder.AddToSource(@$"partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
            partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);");

        return builder;
    }

    public static CsOnlinerMemberBuilder Create(IxNodeVisitor visitor, IReadOnlyCollection<IConfigurationDeclaration> semantics,
        ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerMemberBuilder(sourceBuilder);

        foreach (var structuredTypeDeclaration in semantics)
        {
            builder.AddToSource(structuredTypeDeclaration.DeclareProperties());
            structuredTypeDeclaration.Variables.ToList().ForEach(p => p.Accept(visitor, builder));
        }
        
        builder.AddToSource(@$"partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
            partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);");

        return builder;
    }

    public static CsOnlinerMemberBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
        ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerMemberBuilder(sourceBuilder);
        builder.AddToSource(semantics.DeclareProperties());
        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

        builder.AddToSource(@$"partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
            partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);");

        return builder;
    }

    public static CsOnlinerMemberBuilder Create(IxNodeVisitor visitor, IConfigurationDeclaration semantics,
        ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerMemberBuilder(sourceBuilder);
        builder.AddToSource(semantics.DeclareProperties());
        semantics.Variables.ToList().ForEach(p => p.Accept(visitor, builder));
        return builder;
    }

    /// <inheritdoc />
    public void CreateDocComment(IDocComment semanticTypeAccess, ICombinedThreeVisitor data)
    {
        AddToSource(semanticTypeAccess.AddDocumentationComment(SourceBuilder));
    }
}