// AXSharp.Compiler.CsTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Collections.ObjectModel;
using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Model.Init;
using AX.ST.Semantic.Pragmas;
using AX.ST.Semantic.Symbols;
using AX.ST.Semantic.Tree;
using AX.Text;
using AXSharp.Compiler.Cs;
using NSubstitute;
using Xunit.Abstractions;

namespace AXSharp.Compiler.CsTests;

public class PragmasExtensionsTests
{
    private readonly ITestOutputHelper output;

    public PragmasExtensionsTests(ITestOutputHelper output)
    {
        this.output = output;
    }


    [Fact]
    public void should_get_attribute_source()
    {
        var expected = "[Container(Layoyt.Wrap)]\r\n[Group(Layoyt.GroupBox)]";
        IEnumerable<IPragma> pragmas = new[]
        {
            new("#ix-attr:[Container(Layoyt.Wrap)]"),
            new PragmaMock("#ix-attr:[Group(Layoyt.GroupBox)]")
        };

        var actual = pragmas.AddAttributes();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void should_declare_property()
    {
        var expected = "private string _SomeField;\npublic string SomeField { get => string.IsNullOrEmpty(_SomeField) ? SymbolTail : _SomeField.Interpolate(this).CleanUpLocalizationTokens(); set => _SomeField = value; }public string GetSomeField(System.Globalization.CultureInfo culture){return this.Translate(_SomeField, culture).Interpolate(this);}";
        var field = NSubstitute.Substitute.For<ITypeDeclaration>();
        field.Name.Returns("someField");
        field.Pragmas.Returns(new ReadOnlyCollection<IPragma>(new IPragma[]
        {
            new PragmaMock("#ix-prop:public string SomeField")
        }));

        //new TypeMock("someField",
        //    new ReadOnlyCollection<IPragma>(new IPragma[]
        //    {
        //        new PragmaMock("#ix-prop:public string SomeField")
        //    }));

        var actual = field.DeclareProperties();

        Assert.Equal(expected, actual);
    }


    [Fact]
    public void should_set_property_string()
    {
        var expected = "someField.AttributeName = \"This is name\";";
        var field = NSubstitute.Substitute.For<IFieldDeclaration>();
        field.Name.Returns("someField");
        field.Pragmas.Returns(new ReadOnlyCollection<IPragma>(new IPragma[]
                {
                    new PragmaMock("#ix-set:AttributeName = \"This is name\"")
                }));

        //var field = new FieldMock("someField",
        //    new ReadOnlyCollection<IPragma>(new IPragma[]
        //    {
        //        new PragmaMock("#ix-set:AttributeName = \"This is name\"")
        //    }));

        var actual = field.SetProperties();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void should_set_property_number()
    {
        var expected = "someField.AttributeMinimum = 10.5f;";
        var field = NSubstitute.Substitute.For<IFieldDeclaration>();
        field.Name.Returns("someField");
        field.Pragmas.Returns(new ReadOnlyCollection<IPragma>(new IPragma[]
        {
            new PragmaMock("#ix-set:AttributeMinimum = 10.5f")
        }));

        //var field = new FieldMock("someField",
        //    new ReadOnlyCollection<IPragma>(new IPragma[]
        //    {
        //        new PragmaMock("#ix-set:AttributeMinimum = 10.5f")
        //    }));

        var actual = field.SetProperties();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void should_set_generic_multiple_attributes_no_constraints()
    {
        var expected = "<T,P>";
        var type = NSubstitute.Substitute.For<ITypeDeclaration>();
        type.Name.Returns("someField");
        type.Pragmas.Returns(new ReadOnlyCollection<IPragma>(new IPragma[]
        {
            new PragmaMock("#ix-generic:<T,P>")
        }));

        var actual = type.GetGenericAttributes();

        Assert.Equal(expected, actual.Product);
    }
    [Fact]
    public void should_set_generic_single_attributes_no_constraints()
    {
        var expected = "<T>";
        var type = NSubstitute.Substitute.For<ITypeDeclaration>();
        type.Name.Returns("someField");
        type.Pragmas.Returns(new ReadOnlyCollection<IPragma>(new IPragma[]
        {
            new PragmaMock("#ix-generic:<T>")
        }));

        var actual = type.GetGenericAttributes();

        Assert.Equal(expected, actual.Product);
    }


    [Fact]
    public void should_set_generic_single_attributes_with_constraints()
    {
        var expectedGenericDeclaration = "<T>";
        var expectedGenericConstraints = "where T : class";
        var type = NSubstitute.Substitute.For<ITypeDeclaration>();
        type.Name.Returns("someField");
        type.Pragmas.Returns(new ReadOnlyCollection<IPragma>(new IPragma[]
        {
            new PragmaMock("#ix-generic:<T> where T : class")
        }));

        var actual = type.GetGenericAttributes();

        Assert.Equal(expectedGenericDeclaration, actual.Product);
        Assert.Equal(expectedGenericConstraints, actual.GenericConstrains?.Trim());
        Assert.Equal("T", actual.GenericTypes.ToArray()[0]);
    }

    [Fact]
    public void should_set_generic_multiple_attributes_with_constraints()
    {
        var expectedGenericDeclaration = "<T,P>";
        var expectedGenericConstraints = "where T : class where P : struct";
        var type = NSubstitute.Substitute.For<ITypeDeclaration>();
        type.Name.Returns("someField");
        type.Pragmas.Returns(new ReadOnlyCollection<IPragma>(new IPragma[]
        {
            new PragmaMock("#ix-generic:<T,P> where T : class where P : struct")
        }));

        var actual = type.GetGenericAttributes();

        Assert.Equal(expectedGenericDeclaration, actual.Product);
        Assert.Equal(expectedGenericConstraints, actual.GenericConstrains?.Trim());
        Assert.Equal("T", actual.GenericTypes.ToArray()[0]);
        Assert.Equal("P", actual.GenericTypes.ToArray()[1]);
    }

    [Fact]
    public void should_assign_generic_type_to_extender()
    {
        var expected = "T";
        var type = NSubstitute.Substitute.For<ITypeDeclaration>();
        type.Name.Returns("someField");
        type.Pragmas.Returns(new ReadOnlyCollection<IPragma>(new IPragma[]
        {
            new PragmaMock("#ix-generic:T")
        }));

        var actual = type.GetGenericAttributes();

        Assert.Equal(expected, actual.GenericTypeAssignment.type);
    }

    [Fact]
    public void should_assign_generic_type_to_extender_as_plain()
    {
        var expected = "T";
        var type = NSubstitute.Substitute.For<ITypeDeclaration>();
        type.Name.Returns("someField");
        type.Pragmas.Returns(new ReadOnlyCollection<IPragma>(new IPragma[]
        {
            new PragmaMock("#ix-generic:T as POCO")
        }));

        var actual = type.GetGenericAttributes();

       Assert.Equal(expected, actual.GenericTypeAssignment.type);
       Assert.Equal(true, actual.GenericTypeAssignment.isPoco);
    }
}

public class PragmaMock : IPragma
{
    public PragmaMock(string content)
    {
        Content = content;
    }

    public string Content { get; }

    public Location Location => null;

    public IEnumerable<ISemanticNode> ChildNodes => throw new NotImplementedException();

    public void Accept<T>(ISemanticNodeVisitor<T> visitor, T data)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ISemanticNode> GetDescendentNodes()
    {
        throw new NotImplementedException();
    }
}

//public class FieldMock : IFieldDeclaration
//{
//    public FieldMock(string name, IReadOnlyCollection<IPragma> pragmas)
//    {
//        Name = name;
//        Pragmas = pragmas;
//    }

//    public AccessModifier AccessModifier => throw new NotImplementedException();

//    public Section Section => throw new NotImplementedException();

//    public ISemanticTypeAccess TypeAccess => throw new NotImplementedException();

//    public IDirectAccess Address => throw new NotImplementedException();

//    public ISemanticInitializer Initializer => throw new NotImplementedException();

//    public StorageModifier StorageModifier => throw new NotImplementedException();

//    public bool IsConstant => throw new NotImplementedException();

//    public bool IsDeclaredConstant => throw new NotImplementedException();

//    public bool IsDeclaredReadOnly => throw new NotImplementedException();

//    public ISymbol Symbol => throw new NotImplementedException();

//    public string Name { get; }

//    public string FullyQualifiedName => throw new NotImplementedException();

//    public DeclarationKind Kind => throw new NotImplementedException();

//    public IScope ContainingScope => throw new NotImplementedException();

//    public INamespaceDeclaration ContainingNamespace => throw new NotImplementedException();

//    public ITypeDeclaration Type => throw new NotImplementedException();
//    public IDocComment DocComment { get; }

//    public IReadOnlyCollection<IPragma> Pragmas { get; }

//    public Location Location => throw new NotImplementedException();

//    public IEnumerable<ISemanticNode> ChildNodes => throw new NotImplementedException();

//    public void Accept<T>(ISemanticNodeVisitor<T> visitor, T data)
//    {
//        throw new NotImplementedException();
//    }

//    public bool Equals(IDeclaration? other)
//    {
//        throw new NotImplementedException();
//    }

//    public IEnumerable<ISemanticNode> GetDescendentNodes()
//    {
//        throw new NotImplementedException();
//    }
//}

//public class TypeMock : ITypeDeclaration
//{
    
//    public TypeMock(string name, IReadOnlyCollection<IPragma> pragmas)
//    {
//        Name = name;
//        Pragmas = pragmas;
//    }

//    public IReadOnlyCollection<ISemanticTypeAccess> ExtendedTypeAccesses => throw new NotImplementedException();

//    public IReadOnlyCollection<ISemanticTypeAccess> ImplementedInterfaces => throw new NotImplementedException();

//    public GenericDataType GenericType => throw new NotImplementedException();

//    public IScope LocalScope => throw new NotImplementedException();

//    public ISemanticInitializer Initializer => throw new NotImplementedException();

//    public AccessModifier AccessModifier => throw new NotImplementedException();

//    public ulong? BitWidth => throw new NotImplementedException();

//    public ISymbol Symbol => throw new NotImplementedException();

//    public string Name { get; }

//    public string FullyQualifiedName => throw new NotImplementedException();

//    public DeclarationKind Kind => throw new NotImplementedException();

//    public IScope ContainingScope => throw new NotImplementedException();

//    public INamespaceDeclaration ContainingNamespace => throw new NotImplementedException();

//    public ITypeDeclaration Type => throw new NotImplementedException();

//    public IDocComment DocComment { get; }


//    public IReadOnlyCollection<IPragma> Pragmas { get; }

//    public Location Location => throw new NotImplementedException();

//    public IEnumerable<ISemanticNode> ChildNodes => throw new NotImplementedException();

//    public void Accept<T>(ISemanticNodeVisitor<T> visitor, T data)
//    {
//        throw new NotImplementedException();
//    }

//    public bool Equals(IDeclaration? other)
//    {
//        throw new NotImplementedException();
//    }

//    public bool Extends(ITypeDeclaration type)
//    {
//        throw new NotImplementedException();
//    }

//    public IReadOnlyCollection<ITypeDeclaration> GetAllExtendedTypes()
//    {
//        throw new NotImplementedException();
//    }

//    public IReadOnlyCollection<ITypeDeclaration> GetAllExtendedTypesUniquely()
//    {
//        throw new NotImplementedException();
//    }

//    public IReadOnlyCollection<IInterfaceDeclaration> GetAllImplementedInterfaces()
//    {
//        throw new NotImplementedException();
//    }

//    public IReadOnlyCollection<IInterfaceDeclaration> GetAllImplementedInterfacesUniquely()
//    {
//        throw new NotImplementedException();
//    }

//    public IEnumerable<ISemanticNode> GetDescendentNodes()
//    {
//        throw new NotImplementedException();
//    }

//    public T GetLiteralValue<T>(string literal)
//    {
//        throw new NotImplementedException();
//    }

//    public bool Implements(IInterfaceDeclaration @interface)
//    {
//        throw new NotImplementedException();
//    }

//    public bool IsAssignableTo(ITypeDeclaration other)
//    {
//        throw new NotImplementedException();
//    }

//    public bool IsEqualType(ITypeDeclaration other)
//    {
//        throw new NotImplementedException();
//    }

//    public bool IsOfGenericType(GenericDataType genericType)
//    {
//        throw new NotImplementedException();
//    }
//}