// AXSharp.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Pragmas;
using AX.ST.Syntax.Tree;
using AXSharp.Compiler.Core;

namespace AXSharp.Compiler.Core;

/// <summary>
///     Provides implementation abstraction for syntax-semantic tree visitor.
/// </summary>
public interface ICombinedThreeVisitor
{
    
    /// <summary>
    ///     Creates file declaration from <see cref="IFileSyntax" /> node of given syntax tree.
    /// </summary>
    /// <param name="fileSyntax">File syntax node.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateFile(IFileSyntax fileSyntax, IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates namespace declaration from <see cref="INamespaceDeclarationSyntax" /> node of given syntax tree.
    /// </summary>
    /// <param name="namespaceDeclarationSyntax">Namespace declaration syntax node.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateNamespaceDeclaration(INamespaceDeclarationSyntax namespaceDeclarationSyntax,
        IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates class declaration starting in <see cref="ClassDeclarationSyntax" /> node and continues in respective
    ///     semantic node of <see cref="IClassDeclaration" />.
    /// </summary>
    /// <param name="classDeclarationSyntax">Class declaration syntax node.</param>
    /// <param name="classDeclaration">Class declaration semantic node.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateClassDeclaration(IClassDeclarationSyntax classDeclarationSyntax,
        IClassDeclaration classDeclaration,
        IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates class declaration from semantic node <see cref="IClassDeclaration" />.
    /// </summary>
    /// <param name="classDeclaration">Class declaration semantics.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateClassDeclaration(IClassDeclaration classDeclaration, IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates field (type member) declaration from semantic node <see cref="IFieldDeclaration" />.
    /// </summary>
    /// <param name="fieldDeclaration">Field declaration semantics.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates configuration declaration from starting from syntax node and proceeds in respective semantic node.
    /// </summary>
    /// <param name="configDeclarationSyntax">Configuration declaration syntax node.</param>
    /// <param name="configurationDeclaration">Configuration declaration semantics.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateConfigDeclaration(IConfigDeclarationSyntax configDeclarationSyntax,
        IConfigurationDeclaration configurationDeclaration,
        IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates configuration declaration from semantic node of <see cref="IConfigurationDeclaration" />
    /// </summary>
    /// <param name="configurationDeclaration">Configuration declaration semantic node.</param>
    /// <param name="data">Associated visitor.</param>
    public virtual void CreateConfigDeclaration(IConfigurationDeclaration configurationDeclaration, IxNodeVisitor data)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates pragma declaration.
    /// </summary>
    /// <param name="pragma">Pragma declaration semantics</param>
    /// <param name="visitor">Associated visitor</param>
    public virtual void CreatePragma(IPragma pragma, ICombinedThreeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Created enum declaration starting from syntax node and continues in respective semantic node.
    /// </summary>
    /// <param name="enumTypeDeclarationSyntax">Enum type declaration syntax node.</param>
    /// <param name="typeDeclaration">Enum type declaration semantic node.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateEnumTypeDeclaration(IEnumTypeDeclarationSyntax enumTypeDeclarationSyntax,
        ITypeDeclaration typeDeclaration,
        IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates enum type.
    /// </summary>
    /// <param name="enumTypeDeclaration">Enum type semantics.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateEnumTypeDeclaration(IEnumTypeDeclaration enumTypeDeclaration, IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates Named Value Type declaration.
    /// </summary>
    /// <param name="namedValueTypeDeclarationSyntax">Named value type declaration syntax node.</param>
    /// <param name="namedValueTypeDeclaration">Named value type declaration semantics.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateNamedValueTypeDeclaration(
        INamedValueTypeDeclarationSyntax namedValueTypeDeclarationSyntax,
        INamedValueTypeDeclaration namedValueTypeDeclaration, IxNodeVisitor visitor)
    {
        /*Ignored in poco*/
    }

    /// <summary>
    ///     Creates named value type declaration.
    /// </summary>
    /// <param name="namedValueTypeDeclaration">Named value type declaration semantic node.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateNamedValueTypeDeclaration(INamedValueTypeDeclaration namedValueTypeDeclaration,
        IxNodeVisitor visitor)
    {
        /*Ignored in poco*/
    }

    /// <summary>
    ///     Creates using directive.
    /// </summary>
    /// <param name="usingDirectiveSyntax">Using directive syntax node.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateUsingDirective(IUsingDirectiveSyntax usingDirectiveSyntax, ICombinedThreeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates implements list.
    /// </summary>
    /// <param name="implementsListSyntax">Implements syntax node.</param>
    /// <param name="visitor">Associated visitor.</param>
    /// >
    public virtual void CreateImplementsList(IImplementsListSyntax implementsListSyntax, ICombinedThreeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates interface declaration syntax starting from syntax node and continues in respective semantic node.
    /// </summary>
    /// <param name="interfaceDeclarationSyntax">Interface declaration syntax node.</param>
    /// <param name="interfaceDeclaration">Interface declarations syntax semantic node.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateInterfaceDeclaration(IInterfaceDeclarationSyntax interfaceDeclarationSyntax,
        IInterfaceDeclaration interfaceDeclaration,
        IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates interface declaration from semantic node.
    /// </summary>
    /// <param name="interfaceDeclaration">Interface declaration semantics.</param>
    /// <param name="visitor">Associated visitor.</param>
    void CreateInterfaceDeclaration(IInterfaceDeclaration interfaceDeclaration, IxNodeVisitor visitor);

    /// <summary>
    ///     Creates variable declaration.
    /// </summary>
    /// <param name="variableDeclaration">Variable declaration semantics.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateVariableDeclaration(IVariableDeclaration variableDeclaration, IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates structured type declaration.
    /// </summary>
    /// <param name="structTypeDeclarationSyntax">Structured type declaration syntax node.</param>
    /// <param name="structuredTypeDeclaration">Structured type semantics.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateStructuredType(IStructTypeDeclarationSyntax structTypeDeclarationSyntax,
        IStructuredTypeDeclaration structuredTypeDeclaration,
        IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates structured type.
    /// </summary>
    /// <param name="structuredTypeDeclaration">Structured type semantics.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateStructuredType(IStructuredTypeDeclaration structuredTypeDeclaration,
        IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates referenced type declaration.
    /// </summary>
    /// <param name="referenceTypeDeclaration">Reference type semantics.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateReferenceToDeclaration(IReferenceTypeDeclaration referenceTypeDeclaration,
        IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates semantic type access.
    /// </summary>
    /// <param name="semanticTypeAccess">Semantic type access semantics.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateSemanticTypeAccess(ISemanticTypeAccess semanticTypeAccess, IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Create scalar type declaration.
    /// </summary>
    /// <param name="scalarTypeDeclaration">Scalar type semantics.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateScalarTypeDeclaration(IScalarTypeDeclaration scalarTypeDeclaration, IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates string type declaration.
    /// </summary>
    /// <param name="stringTypeDeclaration">String type semantics.</param>
    /// <param name="visitor">Associated visitor.</param>
    public virtual void CreateStringTypeDeclaration(IStringTypeDeclaration stringTypeDeclaration, IxNodeVisitor visitor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates array type declaration.
    /// </summary>
    /// <param name="arrayTypeDeclaration">Array type semantics</param>
    /// <param name="visitor">Associated visitor.</param>
    void CreateArrayTypeDeclaration(IArrayTypeDeclaration arrayTypeDeclaration, IxNodeVisitor visitor);

    /// <summary>
    ///     Creates documentation for 
    /// </summary>
    /// <param name="docComment">Documentation comment</param>
    /// <param name="data">Associated visitor</param>
    void CreateDocComment(IDocComment docComment, ICombinedThreeVisitor data);
}