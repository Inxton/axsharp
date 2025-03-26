// AXSharp.Compiler.Cs
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

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

namespace AXSharp.Compiler.Cs.Onliner;

internal class CsOnlinerConstructorBuilder : ICombinedThreeVisitor
{
    private readonly StringBuilder _constructorStatements = new();

    protected CsOnlinerConstructorBuilder(ISourceBuilder sourceBuilder)
    {
        SourceBuilder = sourceBuilder;
    }

    protected ISourceBuilder SourceBuilder { get; }

    public string Output => _constructorStatements.ToString().FormatCode();

    public void CreateClassDeclaration(IClassDeclaration classDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"{classDeclaration.GetQualifiedName()}");
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
        AddToSource($"{structuredTypeDeclaration.GetQualifiedName()}");
    }

    public void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
    {
        var eligibility = fieldDeclaration.IsMemberEligibleForConstructor(SourceBuilder);
        if (eligibility.isEligibe)
        {
            switch (eligibility.eligibleType)
            {
                case IArrayTypeDeclaration array:
                    AddArrayMemberInitialization(array, fieldDeclaration, visitor);
                    break;
                case IEnumTypeDeclaration @enum:
                    AddMemberInitialization(@enum, fieldDeclaration);
                    break;
                case INamedValueTypeDeclaration namedValue:
                    AddMemberInitialization(namedValue, fieldDeclaration, visitor);
                    break;
                case IScalarTypeDeclaration scalar:
                    AddMemberInitialization(scalar, fieldDeclaration);
                    break;
                case IStringTypeDeclaration @string:
                    AddMemberInitialization(@string, fieldDeclaration);
                    break;
                case IClassDeclaration @class:
                    AddMemberInitialization(@class, fieldDeclaration, visitor);
                    break;
                case IStructuredTypeDeclaration @struct:
                    AddMemberInitialization(@struct, fieldDeclaration, visitor);
                    break;
            }

            AddToSource(fieldDeclaration.SetProperties());
            AddToSource(fieldDeclaration.AddAnnotations(this.SourceBuilder));
        }
    }

    public virtual void CreateNamedValueTypeDeclaration(INamedValueTypeDeclaration namedValueTypeDeclaration,
        IxNodeVisitor visitor)
    {
        AddToSource(namedValueTypeDeclaration.ValueTypeAccess.Type.Name.ToUpperInvariant());
    }

    public virtual void CreateVariableDeclaration(IVariableDeclaration semantics, IxNodeVisitor visitor)
    {
        var elibility = semantics.IsMemberEligibleForConstructor(SourceBuilder);
        if (elibility.isEligibe)
        {
            switch (elibility.eligibleType)
            {
                case IArrayTypeDeclaration array:
                    AddArrayMemberInitialization(array, semantics, visitor);
                    break;
                case IEnumTypeDeclaration @enum:
                    AddMemberInitialization(@enum, semantics);
                    break;
                case INamedValueTypeDeclaration namedValue:
                    AddMemberInitialization(namedValue, semantics, visitor);
                    break;
                case IScalarTypeDeclaration scalar:
                    AddMemberInitialization(scalar, semantics);
                    break;
                case IStringTypeDeclaration @string:
                    AddMemberInitialization(@string, semantics);
                    break;
                case IClassDeclaration @class:
                    AddMemberInitialization(@class, semantics, visitor);
                    break;
                case IStructuredTypeDeclaration @struct:
                    AddMemberInitialization(@struct, semantics, visitor);
                    break;
            }

            AddToSource(semantics.SetProperties());
            AddToSource(semantics.AddAnnotations(this.SourceBuilder));
        }        
    }

    public void CreateEnumTypeDeclaration(IEnumTypeDeclaration enumTypeDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"{enumTypeDeclaration.GetQualifiedName()}");
    }

    public void CreateInterfaceDeclaration(IInterfaceDeclaration interfaceDeclaration, IxNodeVisitor visitor)
    {
        // No way to construct interfaces.
    }

    public void CreateArrayTypeDeclaration(IArrayTypeDeclaration arrayTypeDeclaration, IxNodeVisitor visitor)
    {
        var type = this.SourceBuilder.Compilation.FindTypeDeclaration(arrayTypeDeclaration.ElementTypeAccess);
        type.Accept(visitor, this);
        //arrayTypeDeclaration.ElementTypeAccess.Type.Accept(visitor, this);
        AddToSource("[");
        AddToSource(string.Join(",",
            arrayTypeDeclaration.Dimensions.Select(p => p.CountOfElements.ToString(CultureInfo.InvariantCulture))));
        AddToSource("]");
    }

    protected void AddToSource(string token, string separator = " ")
    {
        _constructorStatements.Append($"{token}{separator}");
    }

    public static CsOnlinerConstructorBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
        ISourceBuilder sourceBuilder, bool isExtended, AXSharpProject project)
    {
        var builder = new CsOnlinerConstructorBuilder(sourceBuilder);


        builder.AddToSource(
            $"public {semantics.Name}({typeof(ITwinObject).n()} parent, string readableTail, string symbolTail)");


        if (isExtended)
        {
            builder.AddToSource(project.UseBaseSymbol
                ? ": base(parent, readableTail, symbolTail + \".$base\") "
                : ": base(parent, readableTail, symbolTail)");
        }


        builder.AddToSource("{");

        builder.AddToSource(semantics.SetProperties());

        builder.AddToSource($"Symbol = {typeof(Connector.Connector).n()}.CreateSymbol(parent.Symbol, symbolTail);");

        if (!isExtended)
            builder.AddToSource(@$"this.@SymbolTail = symbolTail;
			    this.@Connector = parent.GetConnector();
			    this.@Parent = parent;
			    HumanReadable = {typeof(Connector.Connector).n()}.CreateHumanReadable(parent.HumanReadable, readableTail);");

        builder.AddToSource(@$"PreConstruct(parent, readableTail, symbolTail);");

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

        if (!isExtended)
        {
            builder.AddToSource("parent.AddChild(this);");
            builder.AddToSource("parent.AddKid(this);");
        }

        builder.AddToSource(@$"PostConstruct(parent, readableTail, symbolTail);");

        builder.AddToSource("}");
        return builder;
    }

    public static CsOnlinerConstructorBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
        ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerConstructorBuilder(sourceBuilder);


        builder.AddToSource(
            $"public {semantics.Name}({typeof(ITwinObject).n()} parent, string readableTail, string symbolTail)");


        builder.AddToSource("{");
        builder.AddToSource(@$"this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			HumanReadable = {typeof(Connector.Connector).n()}.CreateHumanReadable(parent.HumanReadable, readableTail);
            Symbol = {typeof(Connector.Connector).n()}.CreateSymbol(parent.Symbol, symbolTail);");

        builder.AddToSource(@$"PreConstruct(parent, readableTail, symbolTail);");

        semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource("parent.AddChild(this);");
        builder.AddToSource("parent.AddKid(this);");

        builder.AddToSource(@$"PostConstruct(parent, readableTail, symbolTail);");

        builder.AddToSource("}");

        return builder;
    }

    public static CsOnlinerConstructorBuilder Create(IxNodeVisitor visitor, IConfigurationDeclaration semantics,
        ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerConstructorBuilder(sourceBuilder);


        builder.AddToSource(
            $"public {semantics.Name}({typeof(ITwinObject).n()} parent, string readableTail, string symbolTail)");


        builder.AddToSource("{");
        builder.AddToSource(@$"this.@SymbolTail = symbolTail;
			this.@Connector = parent.GetConnector();
			this.@Parent = parent;
			HumanReadable = {typeof(Connector.Connector).n()}.CreateHumanReadable(parent.HumanReadable, readableTail);
            Symbol = {typeof(Connector.Connector).n()}.CreateSymbol(parent.Symbol, symbolTail);");

        builder.AddToSource(@$"PreConstruct(parent, readableTail, symbolTail);");

        semantics.Variables.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource("parent.AddChild(this);");
        builder.AddToSource("parent.AddKid(this);");

        builder.AddToSource(@$"PostConstruct(parent, readableTail, symbolTail);");

        builder.AddToSource("}");

        return builder;
    }

    public static CsOnlinerConstructorBuilder Create(IxNodeVisitor visitor, IConfigurationDeclaration semantics,
        AXSharpProject project, ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerConstructorBuilder(sourceBuilder);
        builder.AddToSource(
            $"public {project.TargetProject.ProjectRootNamespace}({typeof(ConnectorAdapter).n()} adapter, object[] parameters) {{");
        builder.AddToSource("this.Connector = adapter.GetConnector(parameters);");

        semantics.Variables.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource("}");
        return builder;
    }

    private void AddArrayMemberInitialization(IArrayTypeDeclaration type, IStorageDeclaration field,
        IxNodeVisitor visitor)
    {
        var eligibility = type.IsMemberEligibleForConstructor(this.SourceBuilder);
        if (!eligibility.isEligibe)
            return;

        AddToSource($"{field.Name}");
        AddToSource("= new");
        //eligibility.eligibleType.Accept(visitor, this);
        type.Accept(visitor, this);
        AddToSource(";");

        
        AddToSource($"{typeof(Arrays).n()}.InstantiateArray({field.Name}, " +
                    "this, " +
                    $"\"{field.GetAttributeNameValue(field.Name)}\", " +
                    $"\"{field.Name}\", " +
                    "(p, rt, st) => ");

        switch (eligibility.eligibleType)
        {
            
            case IClassDeclaration classDeclaration:
            case IStructuredTypeDeclaration structuredTypeDeclaration:
            case IEnumTypeDeclaration enumTypeDeclaration:
            case INamedValueTypeDeclaration namedValueTypeDeclaration:
                AddToSource("new");
                eligibility.eligibleType.Accept(visitor, this);
                //type.ElementTypeAccess.Type.Accept(visitor, this);
                break;
            case IScalarTypeDeclaration scalarTypeDeclaration:
                AddToSource($"@Connector.ConnectorAdapter.AdapterFactory.Create{IecToAdapterExtensions.ToAdapterType(scalarTypeDeclaration)}");
                break;
            case IStringTypeDeclaration stringTypeDeclaration:
                AddToSource($"@Connector.ConnectorAdapter.AdapterFactory.Create{IecToAdapterExtensions.ToAdapterType(stringTypeDeclaration)}");
                break;
        }

        var dimensions = "new[] {";
        foreach (var dimension in type.Dimensions)
        {
            dimensions = $"{dimensions}({dimension.LowerBoundValue}, {dimension.UpperBoundValue})";
        }

        dimensions = $"{dimensions}}}";

        AddToSource($"(p, rt, st), {dimensions});");

        

    }

    private void AddMemberInitialization(IClassDeclaration type, IStorageDeclaration field, IxNodeVisitor visitor)
    {
        AddToSource($"{field.Name}");
        AddToSource("= new");
        type.Accept(visitor, this);
        AddToSource($"(this, \"{field.GetAttributeNameValue(field.Name)}\", \"{field.Name}\");");
    }

    private void AddMemberInitialization(IStructuredTypeDeclaration type, IStorageDeclaration field, IxNodeVisitor visitor)
    {
        AddToSource($"{field.Name}");
        AddToSource("= new");
        type.Accept(visitor, this);
        AddToSource($"(this, \"{field.GetAttributeNameValue(field.Name)}\", \"{field.Name}\");");
    }

    private void AddMemberInitialization(IScalarTypeDeclaration type, IStorageDeclaration field)
    {
        AddToSource($"{field.Name}");
        AddToSource($"= @Connector.ConnectorAdapter.AdapterFactory.Create{IecToAdapterExtensions.ToAdapterType(type)}");
        AddToSource($"(this, \"{field.GetAttributeNameValue(field.Name)}\", \"{field.Name}\");");
    }

    private void AddMemberInitialization(IStringTypeDeclaration type, IStorageDeclaration field)
    {
        AddToSource($"{field.Name}");
        AddToSource($"= @Connector.ConnectorAdapter.AdapterFactory.Create{IecToAdapterExtensions.ToAdapterType(type)}");
        AddToSource($"(this, \"{field.GetAttributeNameValue(field.Name)}\", \"{field.Name}\");");
    }

    // We get warning here about unused method, it is false positive, but we will need to investigate further the object hierarchy.
    private void AddMemberInitialization(IEnumTypeDeclaration enumType, IStorageDeclaration field)
    {
        AddToSource($"{field.Name}");
        AddToSource("= @Connector.ConnectorAdapter.AdapterFactory.CreateINT");
        AddToSource($"(this, \"{field.GetAttributeNameValue(field.Name)}\", \"{field.Name}\");");
        AddToSource(field.SetProperties());
    }

    private void AddMemberInitialization(INamedValueTypeDeclaration namedValueType, IStorageDeclaration field,
        IxNodeVisitor visitor)
    {
        AddToSource($"{field.Name}");
        AddToSource("= @Connector.ConnectorAdapter.AdapterFactory.Create", string.Empty);
        namedValueType.Type.Accept(visitor, this);
        AddToSource($"(this, \"{field.GetAttributeNameValue(field.Name)}\", \"{field.Name}\");");
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