// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AX.ST.Semantic.Model.Declarations;
using Irony.Ast;
using Irony.Parsing;
using Polly;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AXSharp.Compiler.Cs.Pragmas.PragmaParser;

/// <summary>
/// Describes formal grammar for pragma expressions. 
/// </summary>
internal class PragmaGrammar : Grammar
{
    public readonly IdentifierTerminal AccessQualifierTerminal =
        new(nameof(AccessQualifierTerminal));

    public readonly NonTerminal AddedPropertyDeclaration =
        new(nameof(AddedPropertyDeclaration), typeof(AddedPropertyDeclarationAstNode));

    public readonly IdentifierTerminal AddedPropertyIdentifier = new(nameof(AddedPropertyIdentifier));

    public readonly NonTerminal AddedPropertyInitializer = new (nameof(AddedPropertyInitializer));

    public readonly FreeTextLiteral AddedPropertyFreeTextLiteral =
        new FreeTextLiteral(nameof(AddedPropertyFreeTextLiteral), FreeTextOptions.AllowEmpty | FreeTextOptions.IncludeTerminator | FreeTextOptions.AllowEof);

    public readonly NonTerminal AddedPropertySetter = new(nameof(AddedPropertySetter), typeof(AddedPropertySetterAstNode));
    public readonly Terminal assing = new(nameof(assing));

    public readonly NonTerminal DeclarationAttribute = new(nameof(DeclarationAttribute), typeof(AttributeDeclarationAstNode));
    public readonly NonTerminal ClrAttribute = new(nameof(ClrAttribute));

    public readonly NonTerminal GenericDeclarationAttribute =
        new(nameof(GenericDeclarationAttribute), typeof(GenericDeclarationAstNode));


    public readonly NonTerminal GenericAttribute = new (nameof(GenericAttribute), typeof(GenericAttributeAstNode));
    public readonly NonTerminal GenericTypeIdentifiers = new(nameof(GenericTypeIdentifiers));
    public readonly IdentifierTerminal GenericTypeIdentifier = new(nameof(GenericTypeIdentifier));
    public readonly NonTerminal GenericConstraint = new (nameof(GenericConstraint));
    public readonly NonTerminal GenericConstraints = new (nameof(GenericConstraints));
    public readonly IdentifierTerminal GenericConstrainTypeIdentifier = new (nameof(GenericConstrainTypeIdentifier));

    public readonly NonTerminal GenericTypeAssignment = new(nameof(GenericTypeAssignment), typeof(GenericTypeAssignmentAstNode));


    public readonly FreeTextLiteral ClrAttributeContent =
        new(nameof(ClrAttributeContent), FreeTextOptions.IncludeTerminator, "]");

    
    public readonly NonTerminal Pragmas = new(nameof(Pragmas), typeof(PragmaAstNode));
    public readonly IdentifierTerminal PropertyIdentifierTerminal = new(nameof(PropertyIdentifierTerminal), IdOptions.None);
    public readonly IdentifierTerminal TypeIdentifierTerminal = new(nameof(TypeIdentifierTerminal), IdOptions.None);


    public Terminal ix_prop = new(nameof(ix_prop));
    public Terminal ix_set = new(nameof(ix_set));
    public Terminal colon = new(nameof(colon));
    public Terminal ix_attr = new(nameof(ix_attr));
    public Terminal squareClose = new(nameof(squareClose));
    public Terminal squareOpen = new(nameof(squareOpen));
    public Terminal ix_generic = new(nameof(ix_generic));
    public Terminal @where = new (nameof(@where));
    public Terminal @as = new(nameof(@as));
    public Terminal less = new(nameof(less));
    public Terminal more = new (nameof(more));
    public Terminal poco = new (nameof(poco));

    public PragmaGrammar(IDeclaration declaration) : this()
    {
        Declaration = declaration;
    }


    /// <summary>
    /// Creates new instance of <see cref="PragmaGrammar"/>
    /// </summary>
    public PragmaGrammar()
    {
        colon = ToTerm(":", nameof(colon));
        ix_prop = ToTerm("#ix-prop", nameof(ix_prop));
        ix_attr = ToTerm("#ix-attr", nameof(ix_attr));
        ix_set = ToTerm("#ix-set", nameof(ix_set));
        ix_generic = ToTerm("#ix-generic", nameof(ix_generic));
        squareOpen = ToTerm("[", nameof(squareOpen));
        squareClose = ToTerm("]", nameof(squareOpen));
        assing = ToTerm("=", nameof(assing));
        @where = ToTerm("where", nameof(where));
        @as = ToTerm("as", nameof(@as));
        less = ToTerm("<", "<");
        more = ToTerm(">", "<");
        poco = ToTerm("POCO", nameof(poco));

        //{#ix-generic:<TP, TO> : where TP : something where TO : somethingElse}
        GenericConstraint.Rule = @where + GenericTypeIdentifier + colon + GenericConstrainTypeIdentifier;
        GenericTypeAssignment.Rule = GenericTypeIdentifier + ((@as  + poco) | this.Empty);
        MakeStarRule(GenericConstraints, GenericConstraint);
        MakeStarRule(GenericTypeIdentifiers, ToTerm(","), GenericTypeIdentifier);
        
        GenericDeclarationAttribute.Rule = less + GenericTypeIdentifiers + more
                                           + GenericConstraints;


        // GenericAttribute.Rule = ix_generic + colon + (GenericTypeAssignment | GenericDeclarationAttribute);

        GenericAttribute.Rule = (ix_generic + colon +  GenericDeclarationAttribute)
                                | (ix_generic + colon + GenericTypeAssignment);

        //{#ix-prop:public string SomeProperty}
        AddedPropertyDeclaration.Rule = ix_prop + ToTerm(":") +
                                        AccessQualifierTerminal + TypeIdentifierTerminal + PropertyIdentifierTerminal;


        //{#ix-attr:[ReadOnly()]}
        ClrAttribute.Rule = squareOpen + ClrAttributeContent + squareClose;

        DeclarationAttribute.Rule = ix_attr + colon + ClrAttribute;

        // {#ix-set:SomeProperty = "hello"}
        AddedPropertyInitializer.Rule = AddedPropertyFreeTextLiteral; 

        AddedPropertySetter.Rule =
            ix_set + colon + AddedPropertyIdentifier + assing + AddedPropertyInitializer;

        Pragmas.Rule = AddedPropertyDeclaration | DeclarationAttribute | AddedPropertySetter | GenericAttribute;

        Root = Pragmas;

        LanguageFlags = LanguageFlags.CreateAst;
    }

    private IDeclaration Declaration { get; }

    public override void BuildAst(LanguageData language, ParseTree parseTree)
    {
        var astContext = new PragmaAstContext(language, parseTree, Declaration);
        var astBuilder = new AstBuilder(astContext);
        astBuilder.BuildAst(parseTree);
    }
}