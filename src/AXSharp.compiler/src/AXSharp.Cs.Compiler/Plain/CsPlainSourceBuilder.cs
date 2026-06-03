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
using AX.ST.Syntax.Tree;
using AXSharp.Compiler.Core;
using AXSharp.Compiler.Cs.Helpers;
using AXSharp.Compiler.Cs.Helpers.Plain;
using AXSharp.Compiler.Cs.Onliner;

namespace AXSharp.Compiler.Cs.Plain;

/// <summary>
///     Provides builder for Plain twin objects.
/// </summary>
public class CsPlainSourceBuilder : ICombinedThreeVisitor, ISourceBuilder
{
    /// <summary>
    ///     Creates new instance of <see cref="CsPlainSourceBuilder" />
    /// </summary>
    /// <param name="project">Ix project</param>
    /// <param name="compilation">AX compilation</param>
    public CsPlainSourceBuilder(AXSharpProject project,
        Compilation compilation)
    {
        Project = project;
        Compilation = compilation;
        CompilerOptions = project.CompilerOptions;
    }

    private AXSharpProject Project { get; }

    /// <inheritdoc />
    public Compilation Compilation { get; }

    public ICompilerOptions? CompilerOptions { get; }

    public eCommAccessibility TypeCommAccessibility { get; private set; }


    private StringBuilder _sourceBuilder { get; } = new();

    /// <inheritdoc />
    public void CreateClassDeclaration(IClassDeclarationSyntax classDeclarationSyntax,
        IClassDeclaration classDeclaration,
        IxNodeVisitor visitor)
    {
        TypeCommAccessibility = classDeclaration.GetCommAccessibility(this);

        // This is a workaround for abstract classes where semantic model does not contain pragmas even when declared in the source.
        if (classDeclarationSyntax.ClassKeyword.FullText.Trim().ToLower().StartsWith("{S7.extern=ReadWrite}".ToLower()))
        {
            TypeCommAccessibility = eCommAccessibility.ReadWrite;
        }

        if (classDeclarationSyntax.ClassKeyword.FullText.Trim().ToLower().StartsWith("{S7.extern=Read}".ToLower()))
        {
            TypeCommAccessibility = eCommAccessibility.ReadOnly;
        }
        
        classDeclarationSyntax.UsingDirectives.ToList().ForEach(p => p.Visit(visitor, this));

        var classDeclarations = this.Compilation.GetSemanticTree().Classes
            .Where(p => p.FullyQualifiedName == classDeclaration.GetQualifiedName());
        AddToSource(classDeclaration.GetSourceFileAttribute(Project.SourceOrigin));
        AddToSource(classDeclaration.Pragmas.AddedPropertiesAsAttributes());

        AddToSource($"{classDeclaration.AccessModifier.Transform()}partial class {classDeclaration.Name}");
       
        var isExtended = false;
        AX.ST.Semantic.Model.ISemanticTypeAccess? extendedType = classDeclaration.ExtendedTypeAccesses.FirstOrDefault();

        //TODO: Workaround for not fully qualified declarations. To be addressed with proper dependency handling in stc.
        var extend = Compilation.FindTypeDeclaration(extendedType);
        if (extend != null)
        {
            AddToSource($" : {extend.FullyQualifiedName}");
            isExtended = true;
        }

        AddToSource(isExtended ? ", AXSharp.Connector.IPlain" : ": AXSharp.Connector.IPlain");

        AddToSource(classDeclarationSyntax.ImplementsList != null
            ? ", "
            : "");

        classDeclarationSyntax.ImplementsList?.Visit(visitor, this);



        AddToSource("{");
        AddToSource(CsPlainConstructorBuilder.Create(visitor, classDeclaration, this, isExtended, Project).Output);
        classDeclarationSyntax.UsingDirectives.ToList().ForEach(p => p.Visit(visitor, this));
        classDeclaration.Fields.ToList().ForEach(p => p.Accept(visitor, this));
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
    public void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
    {
        var eligibility = fieldDeclaration.IsMemberEligibleForDataExchange(this);
        if (eligibility.isEligible)
        {
            AddToSource(fieldDeclaration.Pragmas.AddedPropertiesAsAttributes());
            switch (eligibility.eligibleType)
            {
                case IArrayTypeDeclaration arrayType:
                    var arrayEligibility = arrayType.IsEligibleForTranspile(this);
                    if (arrayEligibility.isEligibe)
                    {
                        fieldDeclaration.Pragmas.AddAttributes();
                        AddToSource($"{fieldDeclaration.AccessModifier.Transform()}");
                        arrayEligibility.eligibleType.Accept(visitor, this);
                        //arrayType.ElementTypeAccess.Type.Accept(visitor, this);
                        AddToSource("[]");
                        AddToSource($" {fieldDeclaration.Name}");
                        AddToSource("{get; set;}");

                        AddToSource($"= new");
                        arrayEligibility.eligibleType.Accept(visitor, this);
                        //arrayType.ElementTypeAccess.Type.Accept(visitor, this);
                        AddToSource($"[");
                        AddToSource(string.Join(",", arrayType.Dimensions.Select(p => p.CountOfElements)));
                        AddToSource($"];");
                    }
                    break;
                case IStringTypeDeclaration:
                    AddPropertyDeclaration(fieldDeclaration, fieldDeclaration, visitor);
                    AddToSource(" = string.Empty;");
                    break;
                case IEnumTypeDeclaration @enum:
                    AddToSource($"[AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(global::{@enum.GetQualifiedName()}))]");
                    AddPropertyDeclaration(fieldDeclaration, fieldDeclaration, visitor);
                    break;
                case INamedValueTypeDeclaration namedValueType:
                    AddToSource($"[AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(global::{namedValueType.GetQualifiedName()}))]");
                    AddPropertyDeclaration(fieldDeclaration, fieldDeclaration, visitor);                    
                    break;
                case IScalarTypeDeclaration scalar:
                    AddPropertyDeclaration(fieldDeclaration, fieldDeclaration, visitor);
                    AddToSource(scalar.CreateScalarInitializer(this.Project?.CompilerOptions?.TargetPlatfromMoniker));                                        
                    break;
                case IReferenceTypeDeclaration d:
                case IStructuredTypeDeclaration s:
                    AddPropertyDeclaration(fieldDeclaration, eligibility.eligibleType, visitor);
                    AddToSource(" = new ");
                    eligibility.eligibleType.Accept(visitor, this);
                    //fieldDeclaration.Type.Accept(visitor, this);
                    AddToSource("();");
                    break;
            }
        }
    }

    private void AddPropertyDeclaration(IDeclaration fieldDeclaration, IDeclaration eligibleType,  IxNodeVisitor visitor)
    {
        fieldDeclaration.Pragmas.AddAttributes();
        switch (fieldDeclaration)
        {
            case IFieldDeclaration f:
                AddToSource($"{f.AccessModifier.Transform()}");
                break;
            case IVariableDeclaration v:
                AddToSource($"public");
                break;
        }
        eligibleType.Type.Accept(visitor, this);
        AddToSource($" {fieldDeclaration.Name}");
        AddToSource("{get; set;}");
    }

    /// <inheritdoc />
    public virtual void CreateNamedValueTypeDeclaration(INamedValueTypeDeclaration namedValueTypeDeclaration,
        IxNodeVisitor visitor)
    {
        AddToSource(namedValueTypeDeclaration.ValueTypeAccess.Type.TransformType());
    }

    /// <inheritdoc />
    public void CreateFile(IFileSyntax fileSyntax, IxNodeVisitor visitor)
    {
        AddToSource("using System;");
        AddToSource("using AXSharp.Abstractions.Presentation;");
        AddToSource("using AXSharp.Connector;");
        
        foreach (var fileSyntaxUsingDirective in
                 fileSyntax.UsingDirectives
                     .Where(p => this.Compilation.GetSemanticTree().Namespaces.Select(p => p.FullyQualifiedName).Contains(p.QualifiedIdentifierList.GetText())))
        {
            //AddToSource($"using {fileSyntaxUsingDirective.QualifiedIdentifierList.GetText()};");
            AddToSource($"using Pocos.{fileSyntaxUsingDirective.QualifiedIdentifierList.GetText()};");           
        }

        AddToSource("namespace Pocos {");
        fileSyntax.Declarations.ToList().ForEach(p => p.Visit(visitor, this));
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateConfigDeclaration(IConfigDeclarationSyntax configDeclarationSyntax,
        IConfigurationDeclaration configurationDeclaration,
        IxNodeVisitor visitor)
    {
        /// In order to align with stc v7 where multiple configurations are allowed that are merged at
        /// compile time, we need to create a merged configuration class that contains all the configurations.
        /// We merge the configuration in <see>CreateMergedConfigurations</see> the entry is called outside visitor in
        /// Generate method of the <see>AXSharpProject</see>.
        
        return;
        TypeCommAccessibility = eCommAccessibility.None;

        AddToSource($"public partial class {Project.TargetProject.ProjectRootNamespace}TwinController{{");
        configurationDeclaration.Variables.ToList().ForEach(p => p.Accept(visitor, this));
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreatePragma(IPragma pragma, ICombinedThreeVisitor visitor)
    {
        // if (semantics.Content.StartsWith("#ix")) OutputBuilder.AppendLine(semantics.Content.Remove(0, 3));
    }

    /// <inheritdoc />
    public void CreateEnumTypeDeclaration(IEnumTypeDeclarationSyntax enumTypeDeclarationSyntax,
        ITypeDeclaration typeDeclaration,
        IxNodeVisitor visitor)
    {
        // We do not compile enums in Plains, only in Onliners.
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
        AddToSource(interfaceDeclaration.GetSourceFileAttribute(Project.SourceOrigin));
        AddToSource($"{interfaceDeclaration.AccessModifier.Transform()} partial interface {interfaceDeclaration.Name} {{}}");
    }

    /// <inheritdoc />
    public void CreateConfigDeclaration(IConfigurationDeclaration configurationDeclaration, IxNodeVisitor visitor)
    {
        /// In order to align with stc v7 where multiple configurations are allowed that are merged at
        /// compile time, we need to create a merged configuration class that contains all the configurations.
        /// We merge the configuration in <see>CreateMergedConfigurations</see> the entry is called outside visitor in
        /// Generate method of the <see>AXSharpProject</see>.
        return;
        AddToSource($"public partial class {Project.TargetProject.ProjectRootNamespace}{{");
        configurationDeclaration.Variables.ToList().ForEach(p => p.Accept(visitor, this));
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateVariableDeclaration(IVariableDeclaration fieldDeclaration, IxNodeVisitor visitor)
    {
        var eligibility = fieldDeclaration.IsMemberEligibleForTranspile(this);
        if (eligibility.isEligibe)
        {            
            AddToSource(fieldDeclaration.Pragmas.AddedPropertiesAsAttributes());
            switch (fieldDeclaration.Type)
            {
                case IArrayTypeDeclaration arrayType:
                    var arrayEligibility = arrayType.IsEligibleForTranspile(this);
                    if (arrayEligibility.isEligibe)
                    {
                        fieldDeclaration.Pragmas.AddAttributes();
                        AddToSource($"public");
                        arrayEligibility.eligibleType.Accept(visitor, this);
                        //arrayType.ElementTypeAccess.Type.Accept(visitor, this);
                        AddToSource("[]");
                        AddToSource($" {fieldDeclaration.Name}");
                        AddToSource("{get; set;}");

                        AddToSource($"= new");
                        arrayEligibility.eligibleType.Accept(visitor, this);
                        //arrayType.ElementTypeAccess.Type.Accept(visitor, this);
                        AddToSource($"[");
                        AddToSource(string.Join(",", arrayType.Dimensions.Select(p => p.CountOfElements)));
                        AddToSource($"];");
                    }
                    break;
                case IStringTypeDeclaration:
                    AddPropertyDeclaration(fieldDeclaration, fieldDeclaration, visitor);
                    AddToSource(" = string.Empty;");
                    break;
                case IEnumTypeDeclaration @enum:
                    AddToSource($"[AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof({@enum.GetQualifiedName()}))]");
                    AddPropertyDeclaration(fieldDeclaration, fieldDeclaration, visitor);
                    break;
                case INamedValueTypeDeclaration namedValueType:
                    AddToSource($"[AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(global::{namedValueType.GetQualifiedName()}))]");
                    AddPropertyDeclaration(fieldDeclaration, fieldDeclaration.Type, visitor);
                    break;
                case IScalarTypeDeclaration scalar:
                    AddPropertyDeclaration(fieldDeclaration, fieldDeclaration, visitor);
                    AddToSource(scalar.CreateScalarInitializer(this.Project?.CompilerOptions?.TargetPlatfromMoniker));
                    break;
                case IReferenceTypeDeclaration d:
                case IStructuredTypeDeclaration s:
                    AddPropertyDeclaration(fieldDeclaration, fieldDeclaration.Type, visitor);                    
                    AddToSource(" = new ");
                    eligibility.eligibleType.Accept(visitor, this);
                    //fieldDeclaration.Type.Accept(visitor, this);
                    AddToSource("();");
                    break;
            }
        }

    }

    /// <inheritdoc />
    public void CreateStructuredType(IStructTypeDeclarationSyntax structTypeDeclarationSyntax,
        IStructuredTypeDeclaration structuredTypeDeclaration,
        IxNodeVisitor visitor)
    {
        TypeCommAccessibility = structuredTypeDeclaration.GetCommAccessibility(this);

        AddToSource(structuredTypeDeclaration.GetSourceFileAttribute(Project.SourceOrigin));
        AddToSource(
            $"{structuredTypeDeclaration.AccessModifier.Transform()}partial class {structTypeDeclarationSyntax.Name.Text} : AXSharp.Connector.IPlain");
        AddToSource("{");

        AddToSource(CsPlainConstructorBuilder.Create(visitor, structuredTypeDeclaration, this, false, Project).Output);

        structuredTypeDeclaration.Fields.ToList().ForEach(p => p.Accept(visitor, this));
        AddToSource("}");
    }

    /// <inheritdoc />
    public void CreateReferenceToDeclaration(IReferenceTypeDeclaration referenceTypeDeclaration, IxNodeVisitor visitor)
    {
        referenceTypeDeclaration.ReferencedType.Accept(visitor, this);
    }

    /// <inheritdoc />
    public void CreateSemanticTypeAccess(ISemanticTypeAccess semanticTypeAccess, IxNodeVisitor visitor)
    {
        semanticTypeAccess.Type.Accept(visitor, this);
    }

    /// <inheritdoc />
    public void CreateScalarTypeDeclaration(IScalarTypeDeclaration scalarTypeDeclaration, IxNodeVisitor visitor)
    {
        AddToSource(scalarTypeDeclaration.TransformType());
    }

    /// <inheritdoc />
    public void CreateClassDeclaration(IClassDeclaration classDeclaration, IxNodeVisitor data)
    {
        AddToSource(classDeclaration.GetQualifiedName());
    }

    /// <inheritdoc />
    public void CreateInterfaceDeclaration(IInterfaceDeclaration interfaceDeclaration, IxNodeVisitor visitor)
    {
        AddToSource(interfaceDeclaration.GetQualifiedName());
    }

    /// <inheritdoc />
    public void CreateArrayTypeDeclaration(IArrayTypeDeclaration arrayTypeDeclaration, IxNodeVisitor visitor)
    {
        // WATCH!
        var eligibility = arrayTypeDeclaration.IsEligibleForTranspile(this);
        if (!eligibility.isEligibe) return;

        eligibility.eligibleType.Accept(visitor, this);
        //arrayTypeDeclaration.ElementTypeAccess.Type.Accept(visitor, this);
        AddToSource("[]");
    }

    /// <inheritdoc />
    public void CreateDocComment(IDocComment semanticTypeAccess, ICombinedThreeVisitor data)
    {
        AddToSource(semanticTypeAccess.AddDocumentationComment(this));
    }

    /// <inheritdoc />
    public void CreateStringTypeDeclaration(IStringTypeDeclaration stringTypeDeclaration, IxNodeVisitor visitor)
    {
        stringTypeDeclaration.Pragmas.ToList().ForEach(p => p.Accept(visitor, this));
        AddToSource($"{stringTypeDeclaration.TransformType()}");
    }

    /// <inheritdoc />
    public void CreateStructuredType(IStructuredTypeDeclaration structuredTypeDeclaration, IxNodeVisitor visitor)
    {
        structuredTypeDeclaration.Pragmas.ToList().ForEach(p => p.Accept(visitor, this));
        AddToSource($"{structuredTypeDeclaration.GetQualifiedName()}");
    }

    /// <inheritdoc />
    public void CreateEnumTypeDeclaration(IEnumTypeDeclaration enumTypeDeclaration, IxNodeVisitor visitor)
    {
        AddToSource($"global::{enumTypeDeclaration.Type.FullyQualifiedName}");
    }

    /// <inheritdoc />
    public string Output => _sourceBuilder.ToString().FormatCode();

    /// <inheritdoc />
    public string Group => "POCO";

    /// <inheritdoc />
    public string OutputFileSuffix => ".g.cs";

    public string BuilderType => "POCO";

    private void AddToSource(string token, string separator = " ")
    {
        _sourceBuilder.Append($"{token}{separator}");
    }

    private static string ShortedQualifiedIfPossible(IDeclaration semantics)
    {
        return semantics.Type.FullyQualifiedName.StartsWith($"{semantics.ContainingNamespace.FullyQualifiedName}.")
            ? semantics.Type.FullyQualifiedName.Remove(0, semantics.ContainingNamespace.FullyQualifiedName.Length + 1)
            : semantics.Type.FullyQualifiedName;
    }
}
