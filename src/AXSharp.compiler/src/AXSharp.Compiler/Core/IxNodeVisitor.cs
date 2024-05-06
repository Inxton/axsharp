// AXSharp.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

/*
 * NOTE we do use combined syntax semantics tree visitor. In short we start in syntactic node using root of `FileSyntax` then we cross the
 * existing namespaces and we search for structured types (CLASS, STRUCT, ENUM). Once we hit a structured type in the syntax tree we locate in
 * the Compilation's semantic three, from where we then run the semantic tree.
 */

using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Model.Init;
using AX.ST.Semantic.Pragmas;
using AX.ST.Semantic.Symbols;
using AX.ST.Semantic.Tree;
using AX.ST.Syntax.Tree;
using AXSharp.Compiler.Core;

namespace AXSharp.Compiler.Core;

/// <summary>
///     Provides implementation of combined syntax-semantic three.
///     Data object must implement <see cref="ICombinedThreeVisitor" />'s methods to provide source generation.
///     <remarks>
///         Not all Visit are implemented. We only visit nodes that have relevance for PLC's twin objects (Types,
///         Variables etc).
///     </remarks>
/// </summary>
public partial class IxNodeVisitor : ISemanticNodeVisitor<ICombinedThreeVisitor>
{
    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IPartialSemanticTree partialSemanticTree,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISymbol symbol, ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IPragma pragma, ICombinedThreeVisitor data)
    {
        data.CreatePragma(pragma, data);
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IConfigurationDeclaration configurationDeclaration,
        ICombinedThreeVisitor data)
    {
        data.CreateConfigDeclaration(configurationDeclaration, this);
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ITaskConfigurationDeclaration taskConfigurationDeclaration,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ITaskDeclaration taskDeclaration, ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(
        IProgramConfigurationDeclaration programConfigurationDeclaration, ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(INamespaceDeclaration namespaceDeclaration,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IUsingDirective usingDirective, ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IProgramDeclaration programDeclaration,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IClassDeclaration classDeclaration,
        ICombinedThreeVisitor data)
    {
        data.CreateClassDeclaration(classDeclaration, this);
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IInterfaceDeclaration interfaceDeclaration,
        ICombinedThreeVisitor data)
    {
        data.CreateInterfaceDeclaration(interfaceDeclaration, this);
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IFunctionDeclaration functionDeclaration,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IFunctionBlockDeclaration functionBlockDeclaration,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IMethodDeclaration methodDeclaration,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IMethodPrototypeDeclaration methodPrototypeDeclaration,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IScalarTypeDeclaration scalarTypeDeclaration,
        ICombinedThreeVisitor data)
    {
        data.CreateScalarTypeDeclaration(scalarTypeDeclaration, this);
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IStructuredTypeDeclaration structuredTypeDeclaration,
        ICombinedThreeVisitor data)
    {
        data.CreateStructuredType(structuredTypeDeclaration, this);
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IArrayTypeDeclaration arrayTypeDeclaration,
        ICombinedThreeVisitor data)
    {
        data.CreateArrayTypeDeclaration(arrayTypeDeclaration, this);
    }
    
    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IEnumTypeDeclaration enumTypeDeclaration,
        ICombinedThreeVisitor data)
    {
        data.CreateEnumTypeDeclaration(enumTypeDeclaration, this);
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(INamedValueTypeDeclaration namedValueTypeDeclaration,
        ICombinedThreeVisitor data)
    {
        data.CreateNamedValueTypeDeclaration(namedValueTypeDeclaration, this);
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IReferenceTypeDeclaration referenceTypeDeclaration,
        ICombinedThreeVisitor data)
    {
        data.CreateReferenceToDeclaration(referenceTypeDeclaration, this);
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IStringTypeDeclaration stringTypeDeclaration,
        ICombinedThreeVisitor data)
    {
        data.CreateStringTypeDeclaration(stringTypeDeclaration, this);
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IDimension dimension, ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IFieldDeclaration fieldDeclaration,
        ICombinedThreeVisitor data)
    {
        data.CreateFieldDeclaration(fieldDeclaration, this);
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IVariableDeclaration variableDeclaration,
        ICombinedThreeVisitor data)
    {
        data.CreateVariableDeclaration(variableDeclaration, this);
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IEnumValueDeclaration enumValueDeclaration,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(INamedValueDeclaration namedValueDeclaration,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticInitializerExpression initializerExpression,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticArrayInitializer arrayInitializer,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticStructureInitializer structureInitializer,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticMemberInitializer memberInitializer,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticTypeAccess semanticTypeAccess,
        ICombinedThreeVisitor data)
    {
        data.CreateSemanticTypeAccess(semanticTypeAccess, this);
    }

    public void Visit(IDocComment semanticTypeAccess, ICombinedThreeVisitor data)
    {
        data.CreateDocComment(semanticTypeAccess, data);
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticInstructionList instrList,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticAssignmentInstruction assignment,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticUnsafeAssignmentInstruction assignment,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticAssignmentAttemptInstruction assignmentAttempt,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticExpressionInstruction expression,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticIfConditionalStatement condStatement,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IConditionalInstructionList condInstrList,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticCaseStatement caseStatement,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticCaseSelection caseSelection,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticSubrange subrange, ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticForStatement forStatement,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticWhileStatement whileStatement,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticRepeatStatement repeatStatement,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticContinueInstruction continueInstruction,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticExitInstruction exitInstruction,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticReturnStatement returnStatement,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticAsmStatement asmStatement,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticConstantExpression constExpr,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticIdentifierAccess identifierAccess,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticBinaryExpression binExpr,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticUnaryExpression unaryExpression,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticQualifiedEnumAccess qualifiedEnumAccess,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticMemberAccessExpression memberAccessExpression,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticArrayAccessExpression arrayAccessExpression,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticCallExpression call, ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticParameterList paramList, ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(ISemanticParameterAssignment paramAssignment,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISemanticNodeVisitor<ICombinedThreeVisitor>.Visit(IPartialAccessExpression partialAccessExpression,
        ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }
}

public partial class IxNodeVisitor : ISyntaxNodeVisitor<ICombinedThreeVisitor>
{
    private string _containingNamespace = string.Empty;

    /// <summary>
    ///     Creates new instance fo <see cref="IxNodeVisitor" />
    /// </summary>
    /// <param name="compilation">Compilation object of this project.</param>
    public IxNodeVisitor(Compilation compilation)
    {
        Compilation = compilation;
    }

    /// <summary>
    /// Creates new instance of <see cref="IxNodeVisitor"/>
    /// </summary>
    [Obsolete("Use IxNodeVisitor(Compilation) instead.")]
    public IxNodeVisitor()
    {
        
    }

    /// <summary>
    /// Gets the compilation of given AX project.
    /// </summary>
    private Compilation Compilation { get; }


    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IFileSyntax fileSyntax, ICombinedThreeVisitor data)
    {
        data.CreateFile(fileSyntax, this);
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(INamespaceDeclarationSyntax namespaceDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        _containingNamespace = string.IsNullOrEmpty(_containingNamespace)
            ? namespaceDeclarationSyntax.Name.FullyQualifiedIdentifier
            : $"{_containingNamespace}|{namespaceDeclarationSyntax.Name.FullyQualifiedIdentifier}";

        data.CreateNamespaceDeclaration(namespaceDeclarationSyntax, this);

        var namespaceElements = _containingNamespace.Split('|');
        var elementsCount = namespaceElements.Length;
        _containingNamespace = string.Join('|', namespaceElements.Take(elementsCount - 1));
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IClassDeclarationSyntax classDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        var typeName = classDeclarationSyntax.Name.Text;
        var fullyQualifiedName = GetFullyQualifiedNameFromCurrentSyntaxTree(typeName);
        var semantics = Compilation.GetSemanticTree().Classes
            .FirstOrDefault(p => p.FullyQualifiedName == fullyQualifiedName);

        if (semantics == null) throw new TypeNotFoundInSemanticTreeException($"class '{fullyQualifiedName}'");

        data.CreateClassDeclaration(classDeclarationSyntax, semantics, this);
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IConfigDeclarationSyntax configDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        var semantics = Compilation.GetSemanticTree().Configurations.FirstOrDefault(p =>
            p.FullyQualifiedName == GetFullyQualifiedNameFromCurrentSyntaxTree(configDeclarationSyntax.Name.Text));

        if (semantics == null)
            throw new TypeNotFoundInSemanticTreeException(
                $"configuration '{GetFullyQualifiedNameFromCurrentSyntaxTree(configDeclarationSyntax.Name.Text)}'");

        data.CreateConfigDeclaration(configDeclarationSyntax, semantics, this);
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IEnumTypeDeclarationSyntax enumTypeDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        var semantics = Compilation.GetSemanticTree().Types.FirstOrDefault(p =>
            p.FullyQualifiedName == GetFullyQualifiedNameFromCurrentSyntaxTree(enumTypeDeclarationSyntax.Name.Text));

        if (semantics == null)
            throw new TypeNotFoundInSemanticTreeException(
                $"enum type '{GetFullyQualifiedNameFromCurrentSyntaxTree(enumTypeDeclarationSyntax.Name.Text)}'");

        data.CreateEnumTypeDeclaration(enumTypeDeclarationSyntax, semantics, this);
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(
        INamedValueTypeDeclarationSyntax namedValueTypeDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        var semantics = Compilation.GetSemanticTree()
                .Types.FirstOrDefault(p =>
                    p.FullyQualifiedName ==
                    GetFullyQualifiedNameFromCurrentSyntaxTree(namedValueTypeDeclarationSyntax.Name.Text)) as
            INamedValueTypeDeclaration;

        if (semantics == null)
            throw new TypeNotFoundInSemanticTreeException(
                $"named type '{GetFullyQualifiedNameFromCurrentSyntaxTree(namedValueTypeDeclarationSyntax.Name.Text)}'");

        data.CreateNamedValueTypeDeclaration(namedValueTypeDeclarationSyntax, semantics, this);
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IStructTypeDeclarationSyntax structTypeDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        var semantics = Compilation.GetSemanticTree().Types.FirstOrDefault(p =>
                p.FullyQualifiedName ==
                GetFullyQualifiedNameFromCurrentSyntaxTree(structTypeDeclarationSyntax.Name.Text)) as
            IStructuredTypeDeclaration;

        if (semantics == null)
            throw new TypeNotFoundInSemanticTreeException(
                $"interface '{GetFullyQualifiedNameFromCurrentSyntaxTree(structTypeDeclarationSyntax.Name.Text)}'");

        data.CreateStructuredType(structTypeDeclarationSyntax, semantics, this);
    }

    private string GetFullyQualifiedNameFromCurrentSyntaxTree(string typeName)
    {
        return string.IsNullOrEmpty(_containingNamespace.Replace('|', '.'))
            ? typeName
            : $"{_containingNamespace}.{typeName}";
    }

    #region NotImplemented

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IVarDeclarationSectionSyntax varDeclarationSectionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException("Using semantics here");
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IVariableDeclarationSyntax variableDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException("Using semantics here");
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IElementaryTypeSyntax elementaryTypeSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException("Using semantics here");
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IUsingDirectiveSyntax usingDirectiveSyntax,
        ICombinedThreeVisitor data)
    {
        data.CreateUsingDirective(usingDirectiveSyntax, data);
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IImplementsListSyntax implementsListSyntax,
        ICombinedThreeVisitor data)
    {
        data.CreateImplementsList(implementsListSyntax, data);
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IInterfaceDeclarationSyntax interfaceDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        var semantics = Compilation.GetSemanticTree().Interfaces.FirstOrDefault(p =>
            p.FullyQualifiedName == GetFullyQualifiedNameFromCurrentSyntaxTree(interfaceDeclarationSyntax.Name.Text));

        if (semantics == null)
            throw new TypeNotFoundInSemanticTreeException(
                $"interface '{GetFullyQualifiedNameFromCurrentSyntaxTree(interfaceDeclarationSyntax.Name.Text)}'");

        data.CreateInterfaceDeclaration(interfaceDeclarationSyntax, semantics, this);
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ITypeDeclarationSectionSyntax typeDeclarationSectionSyntax,
        ICombinedThreeVisitor data)
    {
        typeDeclarationSectionSyntax.TypeDeclarations.ToList().ForEach(p => p.Visit(this, data));
    }


    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IStringDeclarationSyntax stringDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException("Uses semantics");
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IUserDeclaredTypeSyntax userDeclaredTypeSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException("Uses semantics");
    }


    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IPragmaSyntax pragmaSyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException("Uses semantics");
    }


    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IExpressionInitializerSyntax expressionInitializerSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ILiteralSyntax literalSyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }


    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IExtendsListSyntax extendsListSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ITaskDeclarationSyntax taskDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ITaskConfigSyntax taskConfigSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(
        IAbstractMethodDeclarationSyntax abstractMethodDeclarationSyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IAccessModifierSyntax accessModifierSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IAdditionalVariableNameSyntax additionalVariableNameSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IAdditiveExpressionSyntax additiveExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IAndExpressionSyntax andExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IAnonymousArrayTypeSyntax anonymousArrayTypeSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IAnonymousReferenceTypeSyntax anonymousReferenceTypeSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IArgumentExpressionSyntax argumentExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IArrayAccessExpressionSyntax arrayAccessExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IArrayAccessIndexListSyntax arrayAccessIndexListSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IArrayAccessIndexSyntax arrayAccessIndexSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IArrayDeclarationSyntax arrayDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IArrayDimensionsSyntax arrayDimensionsSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IArrayDimensionSyntax arrayDimensionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IArrayElementInitializerSyntax arrayElementInitializerSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IArrayInitializerListSyntax arrayInitializerListSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IArrayInitializerSyntax arrayInitializerSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IArrayTypeDeclarationSyntax arrayTypeDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IAsmStatementSyntax asmStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(
        IAssignmentAttemptStatementSyntax assignmentAttemptStatementSyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IAssignStatementSyntax assignStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IBoundExpressionSyntax boundExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ICallExpressionSyntax callExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ICallParamAssignmentLhsSyntax callParamAssignmentLhsSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ICaseListElemSyntax caseListElemSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ICaseListSyntax caseListSyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ICaseSelectionSyntax caseSelectionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ICaseStatementSyntax caseStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IClassMemberDeclarationSyntax classMemberDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ICodeBodySyntax codeBodySyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ICommaCaseListElemSyntax commaCaseListElemSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    public void Accept(IConditionalAndExpressionSyntax conditionalAndExpressionSyntax, ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    public void Accept(IConditionalOrExpressionSyntax conditionalOrExpressionSyntax, ICombinedThreeVisitor data)
    {
        throw new NotImplementedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IConditionalStatementSyntax conditionalStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IConfigurationElementSyntax configurationElementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IContinueStatementSyntax continueStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IConvertExpressionSyntax convertExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IDeclarationSyntax declarationSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IDereferenceExpressionSyntax dereferenceExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IElseStatementSyntax elseStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IElsifStatementSyntax elsifStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IEmptyStatementSyntax emptyStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IEnumValueSyntax enumValueSyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IExitStatementSyntax exitStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(
        IExponentiationExpressionSyntax exponentiationExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IExpressionStatementSyntax expressionStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IExpressionSyntax expressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(
        IExternFunctionDeclarationSyntax externFunctionDeclarationSyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IForStatementSyntax forStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(
        IFunctionBlockDeclarationSyntax functionBlockDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IFunctionDeclarationSyntax functionDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        // Functions are unsupported.
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IIdentifierAccessSyntax identifierAccessSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IInitializerSyntax initializerSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IIntervalSpecSyntax intervalSpecSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IIterationStatementSyntax iterationStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IJumpStatementSyntax jumpStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IListElementSyntax listElementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IMemberAccessExpressionSyntax memberAccessExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IMemberInitializerSyntax memberInitializerSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IMethodDeclarationSyntax methodDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(
        IMethodPrototypeDeclarationSyntax methodPrototypeDeclarationSyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(
        IMultiplicativeExpressionSyntax multiplicativeExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(INamedParamListSyntax namedParamListSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(INamedTypeSyntax namedTypeSyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(INamedValueSyntax namedValueSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(INameOfExpressionSyntax nameOfExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(INamespaceElementSyntax namespaceElementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(INotExpressionSyntax notExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IOrExpressionSyntax orExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IParameterAssignmentSyntax parameterAssignmentSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IParameterListSyntax parameterListSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IParenthesesExpressionSyntax parenthesesExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IPartialAccessExpressionSyntax partialAccessExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IPouDeclarationSyntax pouDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IPrimaryExpressionSyntax primaryExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IPrioritySpecSyntax prioritySpecSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    public void Accept(IProgramConfigurationSyntax programConfigurationSyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IProgramDeclarationSyntax programDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        // We ignore PROGRAM declarations.
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IQualifiedEnumAccessSyntax qualifiedEnumAccessSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IQualifiedIdentifierListSyntax qualifiedIdentifierListSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IQualifiedIdentifierPartSyntax qualifiedIdentifierPartSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IQualifiedIdentifierSyntax qualifiedIdentifierSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IReferenceExpressionSyntax referenceExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(
        IReferenceTypeDeclarationSyntax referenceTypeDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IRelationalExpressionSyntax relationalExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IRepeatStatementSyntax repeatStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IReturnStatementSyntax returnStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IRotateShiftExpressionSyntax rotateShiftExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ISectionModifierSyntax sectionModifierSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IStatementListSyntax statementListSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IStatementPartSyntax statementPartSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IStatementSyntax statementSyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }


    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(
        IStructureInitializerListSyntax structureInitializerListSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IStructureInitializerSyntax structureInitializerSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ISuperExpressionSyntax superExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ITaskAssignmentSyntax taskAssignmentSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(
        ITaskConfigurationDeclarationSyntax taskConfigurationDeclarationSyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ITaskInitSyntax taskInitSyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IThisAccessSyntax thisAccessSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }


    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ITypeDeclarationSyntax typeDeclarationSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(ITypeSyntax typeSyntax, ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IUnnamedParamListSyntax unnamedParamListSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IUnsafeAssignStatementSyntax unsafeAssignStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IVariableListSyntax variableListSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IVariableNameAtSyntax variableNameAtSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IWhileStatementSyntax whileStatementSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    void ISyntaxNodeVisitor<ICombinedThreeVisitor>.Accept(IXOrExpressionSyntax xOrExpressionSyntax,
        ICombinedThreeVisitor data)
    {
        throw new NotSupportedException();
    }

    #endregion
}