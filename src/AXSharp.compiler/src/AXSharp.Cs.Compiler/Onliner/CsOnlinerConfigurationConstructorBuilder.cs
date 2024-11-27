// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AX.ST.Semantic;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AXSharp.Compiler.Core;
using AXSharp.Compiler.Cs.Helpers;
using AXSharp.Connector;
using AXSharp.Connector.BuilderHelpers;
using AXSharp.Compiler.Cs;
using System;

namespace AXSharp.Compiler.Cs.Onliner;

internal class CsOnlinerConfigurationConstructorBuilder : CsOnlinerConstructorBuilder
{
    protected CsOnlinerConfigurationConstructorBuilder(ISourceBuilder sourceBuilder) : base(sourceBuilder)
    {
    }

    public new static CsOnlinerConfigurationConstructorBuilder Create(IxNodeVisitor visitor,
        IConfigurationDeclaration semantics, AXSharpProject project, ISourceBuilder sourceBuilder)
    {
        var builder = new CsOnlinerConfigurationConstructorBuilder(sourceBuilder);
        builder.AddToSource(
            $"public {project.TargetProject.ProjectRootNamespace}TwinController({typeof(ConnectorAdapter).n()} adapter, object[] parameters) {{");
        builder.AddToSource("this.Connector = adapter.GetConnector(parameters);");

        semantics.Variables.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource("}");


        builder.AddToSource(
            $"public {project.TargetProject.ProjectRootNamespace}TwinController({typeof(ConnectorAdapter).n()} adapter) {{");
        builder.AddToSource("this.Connector = adapter.GetConnector(adapter.Parameters);");

        semantics.Variables.ToList().ForEach(p => p.Accept(visitor, builder));
        builder.AddToSource("}");

        return builder;
    }

    public override void CreateVariableDeclaration(IVariableDeclaration semantics, IxNodeVisitor visitor)
    {
        if (semantics.IsMemberEligibleForConstructor(SourceBuilder))
        {
            switch (semantics.Type)
            {
                case IEnumTypeDeclaration @enum:
                    AddMemberInitialization(@enum, semantics, visitor);
                    break;
                case INamedValueTypeDeclaration namedValue:
                    AddMemberInitialization(namedValue, semantics, visitor);
                    break;
                case IArrayTypeDeclaration array:
                    AddArrayMemberInitialization(array, semantics, visitor);
                    break;
                case IScalarTypeDeclaration scalar:
                    AddMemberInitialization(scalar, semantics, visitor);
                    break;
                case IStringTypeDeclaration @string:
                    AddMemberInitialization(@string, semantics, visitor);
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

    private void AddArrayMemberInitialization(IArrayTypeDeclaration type, IVariableDeclaration field,
        IxNodeVisitor visitor)
    {
        if (!type.IsMemberEligibleForConstructor(this.SourceBuilder))
            return;

        AddToSource($"{field.Name}");
        AddToSource("= new");
        type.Accept(visitor, this);
        AddToSource(";");


        AddToSource($"{typeof(Arrays).n()}.InstantiateArray({field.Name}, " +
                    "this.Connector, " +
                    $"\"{field.GetAttributeNameValue(field.Name)}\", " +
                    $"\"{field.Name}\", " +
                    "(p, rt, st) => ");

        switch (type.ElementTypeAccess.Type)
        {

            case IClassDeclaration classDeclaration:
            case IStructuredTypeDeclaration structuredTypeDeclaration:
            case IEnumTypeDeclaration enumTypeDeclaration:
            case INamedValueTypeDeclaration namedValueTypeDeclaration:
                AddToSource("new");
                type.ElementTypeAccess.Type.Accept(visitor, this);
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

    private void AddMemberInitialization(IClassDeclaration type, IVariableDeclaration variable, IxNodeVisitor visitor)
    {
        var attributes = variable.Pragmas.AddAttributes();
        var isdb = attributes.Split('\n').Any(p => p == "[DBAttribute()]");
        AddToSource($"{variable.Name}");
        AddToSource("= new");
        type.Accept(visitor, this);
        var symbol = isdb ? $"\"\\\"{variable.Name}\\\"\"" : $"\"{variable.Name}\"";
        AddToSource($"(this.Connector, \"\", {symbol});");
    }

    private void AddMemberInitialization(IStructuredTypeDeclaration type, IVariableDeclaration variable, IxNodeVisitor visitor)
    {
        AddToSource($"{variable.Name}");
        AddToSource("= new");
        type.Accept(visitor, this);
        AddToSource($"(this.Connector, \"\", \"{variable.Name}\");");
    }

    private void AddMemberInitialization(IScalarTypeDeclaration type, IVariableDeclaration variable, IxNodeVisitor visitor)
    {
        AddToSource($"{variable.Name}");
        AddToSource($"= @Connector.ConnectorAdapter.AdapterFactory.Create{IecToAdapterExtensions.ToAdapterType(type)}");
        AddToSource($"(this.Connector, \"\", \"{variable.Name}\");");
    }

    private void AddMemberInitialization(IStringTypeDeclaration type, IVariableDeclaration variable, IxNodeVisitor visitor)
    {
        AddToSource($"{variable.Name}");
        AddToSource($"= @Connector.ConnectorAdapter.AdapterFactory.Create{IecToAdapterExtensions.ToAdapterType(type)}");
        AddToSource($"(this.Connector, \"\", \"{variable.Name}\");");
    }

    private void AddMemberInitialization(IEnumTypeDeclaration enumType, IVariableDeclaration variable, IxNodeVisitor visitor)
    {
        AddToSource($"{variable.Name}");
        AddToSource("= @Connector.ConnectorAdapter.AdapterFactory.CreateINT");
        AddToSource($"(this.Connector, \"\", \"{variable.Name}\");");
        AddToSource(variable.SetProperties());
    }

    private void AddMemberInitialization(INamedValueTypeDeclaration namedValueType, IVariableDeclaration variable, IxNodeVisitor visitor)
    {
        AddToSource($"{variable.Name}");
        AddToSource("= @Connector.ConnectorAdapter.AdapterFactory.Create", string.Empty);
        namedValueType.Type.Accept(visitor, this);
        AddToSource($"(this, \"{variable.GetAttributeNameValue(variable.Name)}\", \"{variable.Name}\");");
    }
}