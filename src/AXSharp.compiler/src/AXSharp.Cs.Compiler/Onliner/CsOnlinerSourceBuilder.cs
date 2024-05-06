// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Security.Cryptography.X509Certificates;
using System.Text;
using AX.ST.Semantic;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Pragmas;
using AX.ST.Syntax.Tree;
using AXSharp.Compiler.Core;
using AXSharp.Compiler.Cs.Helpers;
using AXSharp.Compiler.Cs.Helpers.Plain;
using AXSharp.Connector;

namespace AXSharp.Compiler.Cs.Onliner;

/// <summary>
///     Source builder for <see cref="Onliner" /> twins.
/// </summary>
public class CsOnlinerSourceBuilder : ICombinedThreeVisitor, ISourceBuilder
{
    private readonly StringBuilder _sourceBuilder = new();

    /// <summary>
    ///     Creates new instance of <see cref="CsOnlinerSourceBuilder" />
    /// </summary>
    /// <param name="project">Ix project name.</param>
    /// <param name="compilation">AX compilation</param>
    public CsOnlinerSourceBuilder(AXSharpProject project,
        Compilation compilation)
    {
        Project = project;
        Compilation = compilation;
        CompilerOptions = project.CompilerOptions;
    }

    /// <inheritdoc />
    public Compilation Compilation { get; }

    public ICompilerOptions? CompilerOptions { get; }

    private AXSharpProject Project { get; }

    /// <inheritdoc />
    public void CreateFile(IFileSyntax fileSyntax, IxNodeVisitor visitor)
    {
        AddToSource("using System;");
        AddToSource("using AXSharp.Connector;");
        AddToSource("using AXSharp.Connector.ValueTypes;");
        AddToSource("using System.Collections.Generic;");
        AddToSource("using AXSharp.Connector.Localizations;");

        fileSyntax.Declarations.ToList().ForEach(p => p.Visit(visitor, this));
    }


    private string ReplaceGenericSignature(IClassDeclaration? classDeclaration)
    {
        if (classDeclaration == null)
            return string.Empty;

        var generics = new List<string>();
        var genericSignature = classDeclaration?.ExtendedType?.GetGenericAttributes()?.Product;
        if(string.IsNullOrEmpty(genericSignature))
        {
            return string.Empty;
        }

        foreach (var genericType in classDeclaration?.ExtendedType?.GetGenericAttributes().GenericTypes)
        {
            var fieldDeclaresGenericType = classDeclaration.Fields
                .FirstOrDefault(p => p.GetGenericAttributes().Any(a => a.GenericTypeAssignment.type == genericType));

            if (fieldDeclaresGenericType != null)
            {
                foreach (var attribute in fieldDeclaresGenericType?.GetGenericAttributes())
                {
                    if (attribute.GenericTypeAssignment.isPoco)
                    {
                       genericSignature = genericSignature.Replace(attribute.GenericTypeAssignment.type, $"Pocos.{fieldDeclaresGenericType?.Type.FullyQualifiedName}");
                    }
                    else
                    {
                       genericSignature = genericSignature.Replace(attribute.GenericTypeAssignment.type, fieldDeclaresGenericType?.Type.FullyQualifiedName);
                    }
                }
            }
        }

        return genericSignature;
    }

    public eCommAccessibility TypeCommAccessibility { get; private set; }

    /// <inheritdoc />
    public void CreateClassDeclaration(IClassDeclarationSyntax classDeclarationSyntax,
        IClassDeclaration classDeclaration,
        IxNodeVisitor visitor)
    {
        TypeCommAccessibility = classDeclaration.GetCommAccessibility(this);
        
        classDeclarationSyntax.UsingDirectives.ToList().ForEach(p => p.Visit(visitor, this));
        var generic = classDeclaration.GetGenericAttributes();
        
        AddToSource(classDeclaration.Pragmas.AddAttributes());
        AddToSource($"{classDeclaration.AccessModifier.Transform()}partial class {classDeclaration.Name}{generic?.Product}");
        AddToSource(":");

        var isExtended = false;
        var extendedType = classDeclaration.ExtendedTypeAccesses.FirstOrDefault();
        if (Compilation.GetSemanticTree().Types
            .Any(p => p.FullyQualifiedName == extendedType?.Type.FullyQualifiedName))
        {
            AddToSource($"{extendedType.Type.FullyQualifiedName}{ReplaceGenericSignature(classDeclaration)}");
            isExtended = true;
        }
        else
        {
            AddToSource(typeof(ITwinObject).n()!);
        }

        AddToSource(classDeclarationSyntax.ImplementsList != null ? ", " : string.Empty);
        classDeclarationSyntax.ImplementsList?.Visit(visitor, this);

        AddToSource(generic?.GenericConstrains);

        AddToSource("\n{");

        AddToSource(CsOnlinerMemberBuilder.Create(visitor, classDeclaration, this).Output);

        AddToSource(CsOnlinerConstructorBuilder.Create(visitor, classDeclaration, this, isExtended, this.Project).Output);

        AddToSource(CsOnlinerPlainerOnlineToPlainBuilder.Create(visitor, classDeclaration, this, isExtended).Output);
        AddToSource(CsOnlinerPlainerOnlineToPlainProtectedBuilder.Create(visitor, classDeclaration, this, isExtended).Output);
        AddToSource(CsOnlinerPlainerPlainToOnlineBuilder.Create(visitor, classDeclaration, this, isExtended).Output);

        AddToSource(CsOnlinerPlainerShadowToPlainBuilder.Create(visitor, classDeclaration, this, isExtended).Output);
        AddToSource(CsOnlinerPlainerShadowToPlainProtectedBuilder.Create(visitor, classDeclaration, this, isExtended).Output);
        AddToSource(CsOnlinerPlainerPlainToShadowBuilder.Create(visitor, classDeclaration, this, isExtended).Output);
        
        AddToSource(CsOnlinerHasChangedBuilder.Create(visitor, classDeclaration, this, isExtended).Output);

        AddPollingMethod(isExtended);
        AddCreatePocoMethod(classDeclaration, isExtended);

        if (!isExtended) CreateITwinObjectImplementation();

        AddToSource("}");
    }

    private void AddPollingMethod(bool isExtended)
    {
        var qualifier = isExtended ? "new" : string.Empty;
        AddToSource($" public {qualifier} void Poll()\r\n    {{\r\n        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());\r\n    }}");
    }

    private void AddCreatePocoMethod(ITypeDeclaration typeDeclaration, bool isExtended)
    {
        var qualifier = isExtended ? "new" : string.Empty;
        AddToSource($"public {qualifier} Pocos.{typeDeclaration.FullyQualifiedName} CreateEmptyPoco(){{ return new Pocos.{typeDeclaration.FullyQualifiedName}();}}");
    }

    /// <inheritdoc />
    public void CreateConfigDeclaration(IConfigDeclarationSyntax configDeclarationSyntax,
        IConfigurationDeclaration configurationDeclaration,
        IxNodeVisitor visitor)
    {
        TypeCommAccessibility = eCommAccessibility.None;
        
        AddToSource(
            $"public partial class {Project.TargetProject.ProjectRootNamespace}TwinController : ITwinController {{");
        AddToSource($"public {typeof(Connector.Connector).n()} Connector {{ get; }}");
        AddToSource(CsOnlinerMemberBuilder.Create(visitor, configurationDeclaration, this).Output);
        AddToSource(CsOnlinerConfigurationConstructorBuilder
            .Create(visitor, configurationDeclaration, Project, this).Output);
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateConfigDeclaration(IConfigurationDeclaration configurationDeclaration, IxNodeVisitor visitor)
    {
        TypeCommAccessibility = eCommAccessibility.None;

        AddToSource($"public partial class {Project.TargetProject.ProjectRootNamespace} : ITwinController {{");
        AddToSource(@$"public {typeof(Connector.Connector).n()} Connector {{ get; }}");
        AddToSource(CsOnlinerConstructorBuilder.Create(visitor, configurationDeclaration, Project, this).Output);
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateEnumTypeDeclaration(IEnumTypeDeclarationSyntax enumTypeDeclarationSyntax,
        ITypeDeclaration typeDeclaration,
        IxNodeVisitor visitor)
    {
        TypeCommAccessibility = eCommAccessibility.None;
        
        AddToSource($"public enum {enumTypeDeclarationSyntax.Name.Text} {{");
        AddToSource(string.Join("\n,", enumTypeDeclarationSyntax.EnumValues.Select(p => p.Name.Text)));
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateNamedValueTypeDeclaration(INamedValueTypeDeclarationSyntax namedValueTypeDeclarationSyntax,
        INamedValueTypeDeclaration namedValueTypeDeclaration, IxNodeVisitor visitor)
    {
        TypeCommAccessibility = eCommAccessibility.None;
        
        AddToSource(
            $"public enum {namedValueTypeDeclarationSyntax.Name.Text} : {namedValueTypeDeclarationSyntax.Type.TransformType()} {{");

        // TODO: Value re-interpretation should be done according to the type.

        AddToSource(string.Join(",\n",
            namedValueTypeDeclaration.Values.Select(p => $"{p.Name} = {p.Value.ReinterpretValue<int>()}")));

        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateNamespaceDeclaration(INamespaceDeclarationSyntax namespaceDeclarationSyntax,
        IxNodeVisitor visitor)
    {
        AddToSource($"namespace {namespaceDeclarationSyntax.Name.FullyQualifiedIdentifier} {{");
        namespaceDeclarationSyntax.NamespaceElements.ToList().ForEach(p => p.Visit(visitor, this));
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreatePragma(IPragma pragma, ICombinedThreeVisitor visitor)
    {
        if (pragma.Content.StartsWith("#ix:")) AddToSource(pragma.Content.Remove(0, 4));
    }

    /// <inheritdoc />
    public void CreateUsingDirective(IUsingDirectiveSyntax usingDirectiveSyntax, ICombinedThreeVisitor visitor)
    {
        usingDirectiveSyntax.QualifiedIdentifierList.ListElements.ToList().ForEach(p =>
        {
            if (Compilation.GetSemanticTree().Namespaces
                .Any(n => n.FullyQualifiedName == p.Name.FullyQualifiedIdentifier))
                AddToSource($"using {p.Name.FullyQualifiedIdentifier};\n");
        });
    }

    /// <inheritdoc />
    public void CreateImplementsList(IImplementsListSyntax implementsListSyntax, ICombinedThreeVisitor visitor)
    {
        AddToSource(string.Join(", ",
            implementsListSyntax.QualifiedIdentifierList.ListElements.Select(p => p.Name.FullyQualifiedIdentifier)));
    }

    /// <inheritdoc />
    public void CreateInterfaceDeclaration(IInterfaceDeclarationSyntax interfaceDeclarationSyntax,
        IInterfaceDeclaration interfaceDeclaration,
        IxNodeVisitor visitor)
    {
        TypeCommAccessibility = eCommAccessibility.None;
        
        AddToSource($"{interfaceDeclaration.AccessModifier.Transform()} partial interface {interfaceDeclaration.Name} {{}}");
    }

    /// <inheritdoc />
    public void CreateInterfaceDeclaration(IInterfaceDeclaration interfaceDeclaration, IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void CreateStructuredType(IStructTypeDeclarationSyntax structTypeDeclarationSyntax,
        IStructuredTypeDeclaration structuredTypeDeclaration,
        IxNodeVisitor visitor)
    {
        TypeCommAccessibility = structuredTypeDeclaration.GetCommAccessibility(this);

        AddToSource(structuredTypeDeclaration.Pragmas.AddAttributes());
        AddToSource(
            $"{structuredTypeDeclaration.AccessModifier.Transform()}partial class {structTypeDeclarationSyntax.Name.Text}");
        AddToSource(":");

        AddToSource(typeof(ITwinObject).n()!);

        AddToSource("\n{");

        AddToSource(CsOnlinerMemberBuilder.Create(visitor, structuredTypeDeclaration, this).Output);

        AddToSource(CsOnlinerConstructorBuilder.Create(visitor, structuredTypeDeclaration, this).Output);

        AddToSource(CsOnlinerPlainerOnlineToPlainBuilder.Create(visitor, structuredTypeDeclaration, this).Output);
        AddToSource(CsOnlinerPlainerOnlineToPlainProtectedBuilder.Create(visitor, structuredTypeDeclaration, this).Output);
        AddToSource(CsOnlinerPlainerPlainToOnlineBuilder.Create(visitor, structuredTypeDeclaration, this).Output);

        AddToSource(CsOnlinerPlainerShadowToPlainBuilder.Create(visitor, structuredTypeDeclaration, this).Output);
        AddToSource(CsOnlinerPlainerShadowToPlainProtectedBuilder.Create(visitor, structuredTypeDeclaration, this).Output);
        AddToSource(CsOnlinerPlainerPlainToShadowBuilder.Create(visitor, structuredTypeDeclaration, this).Output);

        AddToSource(CsOnlinerHasChangedBuilder.Create(visitor, structuredTypeDeclaration, this).Output);
        AddPollingMethod(false);

        AddCreatePocoMethod(structuredTypeDeclaration, false);

        CreateITwinObjectImplementation();

        AddToSource("}");
    }


    /// <inheritdoc />
    public void CreateArrayTypeDeclaration(IArrayTypeDeclaration arrayTypeDeclaration, IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public string Group => "Onliners";

    /// <inheritdoc />
    public string Output => _sourceBuilder.ToString().FormatCode();

    /// <inheritdoc />
    public string OutputFileSuffix => ".g.cs";

    /// <inheritdoc />
    public string BuilderType => "Onliner";


    private void CreateITwinObjectImplementation()
    {
        AddToSource(
            $"private IList<{typeof(ITwinObject).n()}> Children {{ get; }} = new List<{typeof(ITwinObject).n()}>();" +
            $"public IEnumerable<{typeof(ITwinObject).n()}> GetChildren() {{ return Children;}}" +
            $"private IList<{typeof(ITwinElement).n()}> Kids {{get;}} = new List<{typeof(ITwinElement).n()}>();" +
            $"public IEnumerable<{typeof(ITwinElement).n()}> GetKids() {{ return Kids;}}" +
            $"private IList<{typeof(ITwinPrimitive).n()}> ValueTags {{get;}} = new List<{typeof(ITwinPrimitive).n()}>();" +
            $"public IEnumerable<{typeof(ITwinPrimitive).n()}> GetValueTags() {{ return ValueTags;}}" +
            $"public void AddValueTag({typeof(ITwinPrimitive).n()} valueTag) {{ ValueTags.Add(valueTag); }}" +
            $"public void AddKid({typeof(ITwinElement).n()} kid) {{ Kids.Add(kid); }}" +
            $"public void AddChild({typeof(ITwinObject).n()} twinObject) {{ Children.Add(twinObject); }}" +
            $"protected {typeof(Connector.Connector).n()} @Connector{{ get; }}" +
            $"public {typeof(Connector.Connector).n()} GetConnector() {{ return this.@Connector; }}" +
            "public string GetSymbolTail() { return this.SymbolTail; }" +
            $"public {typeof(ITwinObject).n()} GetParent() {{ return this.@Parent; }}" +
            "public string Symbol { get; protected set; }" +
            "private string _attributeName;" +
            "public System.String AttributeName {  get => string.IsNullOrEmpty(_attributeName) ? SymbolTail : _attributeName.Interpolate(this).CleanUpLocalizationTokens(); set => _attributeName = value; }" +
            "public System.String GetAttributeName(System.Globalization.CultureInfo culture) {  return this.Translate(_attributeName, culture).Interpolate(this); }" +
            "private string _humanReadable;" +
            "public string HumanReadable {  get => string.IsNullOrEmpty(_humanReadable) ? SymbolTail : _humanReadable.Interpolate(this).CleanUpLocalizationTokens(); set => _humanReadable = value; }" +
            "public System.String GetHumanReadable(System.Globalization.CultureInfo culture) {  return this.Translate(_humanReadable, culture); }" +
            "protected System.String @SymbolTail { get; set;}" +
            $"protected {typeof(ITwinObject).n()} @Parent {{ get; set; }}"+
            $"public AXSharp.Connector.Localizations.Translator Interpreter => global::{Project.TargetProject.ProjectRootNamespace}.PlcTranslator.Instance;"
        );
    }

    private void AddToSource(string token, string separator = " ")
    {
        _sourceBuilder.Append($"{token}{separator}");
    }

    /// <inheritdoc />
    public void CreateDocComment(IDocComment semanticTypeAccess, ICombinedThreeVisitor data)
    {
        AddToSource(semanticTypeAccess.AddDocumentationComment(this));
    }
}