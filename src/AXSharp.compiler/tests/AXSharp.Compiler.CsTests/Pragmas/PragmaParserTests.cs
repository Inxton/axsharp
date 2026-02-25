// AXSharp.Compiler.CsTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System.Collections.ObjectModel;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Pragmas;
using AXSharp.Compiler.Cs.Pragmas.PragmaParser;
using NSubstitute;

namespace AXSharp.Compiler.CsTests.Pragmas;

public class PragmaParserTests
{
    private IPragma CreatePragma(string content)
    {
        return new PragmaMock(content);
    }

    private IDeclaration CreateDeclaration(string name = "TestMember")
    {
        var declaration = Substitute.For<IDeclaration>();
        declaration.Name.Returns(name);
        return declaration;
    }

    #region Added Property Declaration Tests

    [Fact]
    public void Should_Parse_Simple_Property_Declaration()
    {
        // Arrange
        var pragma = CreatePragma("#ix-prop:public int MyProperty");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("MyProperty", result.Product);
        Assert.Contains("public", result.Product);
        Assert.Contains("int", result.Product);
        Assert.Contains("get;", result.Product);
        Assert.Contains("set;", result.Product);
    }

    [Fact]
    public void Should_Parse_Property_Declaration_With_String_Type()
    {
        // Arrange
        var pragma = CreatePragma("#ix-prop:public string DisplayName");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        // String properties have special handling with interpolation and localization
        Assert.Contains("DisplayName", result.Product);
        Assert.Contains("_DisplayName", result.Product);
        Assert.Contains("Interpolate", result.Product);
        Assert.Contains("SymbolTail", result.Product);
        Assert.Contains("GetDisplayName", result.Product);
        Assert.Contains("DisplayName_raw", result.Product);
    }

    [Fact]
    public void Should_Parse_Property_Declaration_With_WString_Type()
    {
        // Arrange
        var pragma = CreatePragma("#ix-prop:public WSTRING Description");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        // WSTRING has the same special handling as STRING
        Assert.Contains("Description", result.Product);
        Assert.Contains("_Description", result.Product);
        Assert.Contains("Interpolate", result.Product);
        Assert.Contains("SymbolTail", result.Product);
    }

    [Fact]
    public void Should_Parse_Property_Declaration_With_Protected_Modifier()
    {
        // Arrange
        var pragma = CreatePragma("#ix-prop:protected bool IsEnabled");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("protected", result.Product);
        Assert.Contains("bool", result.Product);
        Assert.Contains("IsEnabled", result.Product);
    }

    [Fact]
    public void Should_Parse_Property_Declaration_With_Private_Modifier()
    {
        // Arrange
        var pragma = CreatePragma("#ix-prop:private decimal Price");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("private", result.Product);
        Assert.Contains("decimal", result.Product);
        Assert.Contains("Price", result.Product);
    }

    [Fact]
    public void Should_Parse_Property_Declaration_With_Complex_Type()
    {
        // Arrange
        var pragma = CreatePragma("#ix-prop:public Dictionary Tags");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("Dictionary", result.Product);
        Assert.Contains("Tags", result.Product);
    }

    #endregion

    #region Attribute Declaration Tests

    [Fact]
    public void Should_Parse_Attribute_Declaration_With_Simple_Attribute()
    {
        // Arrange
        var pragma = CreatePragma("#ix-attr:[ReadOnly()]");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Equal("[ReadOnly()]", result.Product);
    }

    [Fact]
    public void Should_Parse_Attribute_Declaration_With_Attribute_Parameters()
    {
        // Arrange
        var pragma = CreatePragma("#ix-attr:[Container(Layout.Wrap)]");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Equal("[Container(Layout.Wrap)]", result.Product);
    }

    [Fact]
    public void Should_Parse_Attribute_Declaration_With_Multiple_Parameters()
    {
        // Arrange
        var pragma = CreatePragma("#ix-attr:[Group(\"General\", Layout.GroupBox)]");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("[Group(", result.Product);
        Assert.Contains("General", result.Product);
        Assert.Contains("Layout.GroupBox", result.Product);
    }

    [Fact]
    public void Should_Parse_Attribute_Declaration_With_Numeric_Parameters()
    {
        // Arrange
        var pragma = CreatePragma("#ix-attr:[FieldSize(500)]");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Equal("[FieldSize(500)]", result.Product);
    }

    #endregion

    #region Property Setter Tests

    [Fact]
    public void Should_Parse_Property_Setter_With_String_Value()
    {
        // Arrange
        var pragma = CreatePragma("#ix-set:Title = \"My Title\"");
        var declaration = CreateDeclaration("myField");

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("Title", result.Product);
        Assert.Contains("@\"My Title\"", result.Product);
        Assert.Contains("myField.Title", result.Product);
        Assert.Equal(("Title", "@\"My Title\""), result.Property);
    }

    [Fact]
    public void Should_Parse_Property_Setter_With_Numeric_Value()
    {
        // Arrange
        var pragma = CreatePragma("#ix-set:MinValue = 100");
        var declaration = CreateDeclaration("myField");

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("MinValue", result.Product);
        Assert.Contains("100", result.Product);
        Assert.Contains("myField.MinValue", result.Product);
        Assert.Equal(("MinValue", "100"), result.Property);
    }

    [Fact]
    public void Should_Parse_Property_Setter_With_Float_Value()
    {
        // Arrange
        var pragma = CreatePragma("#ix-set:Scale = 2.5");
        var declaration = CreateDeclaration("myField");

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("Scale", result.Product);
        Assert.Contains("2.5", result.Product);
        Assert.Equal(("Scale", "2.5"), result.Property);
    }

    [Fact]
    public void Should_Parse_Property_Setter_With_Boolean_Value()
    {
        // Arrange
        var pragma = CreatePragma("#ix-set:IsVisible = True");
        var declaration = CreateDeclaration("myField");

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("IsVisible", result.Product);
        Assert.Contains("True", result.Product);
        Assert.Equal(("IsVisible", "True"), result.Property);
    }

    [Fact]
    public void Should_Parse_Property_Setter_With_Enum_Value()
    {
        // Arrange
        var pragma = CreatePragma("#ix-set:Alignment = TextAlignment.Center");
        var declaration = CreateDeclaration("myField");

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("Alignment", result.Product);
        Assert.Contains("TextAlignment.Center", result.Product);
        Assert.Equal(("Alignment", "TextAlignment.Center"), result.Property);
    }

    [Fact]
    public void Should_Parse_Property_Setter_With_String_And_Maintain_Quotes()
    {
        // Arrange
        var pragma = CreatePragma("#ix-set:Name = \"Test Name With Spaces\"");
        var declaration = CreateDeclaration("myField");

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Property);
        var (propertyName, value) = result.Property;
        Assert.Equal("Name", propertyName);
        Assert.StartsWith("@\"", value);
        Assert.Contains("Test Name With Spaces", value);
    }

    #endregion

    #region Generic Declaration Tests

    [Fact]
    public void Should_Parse_Simple_Generic_Declaration()
    {
        // Arrange
        var pragma = CreatePragma("#ix-generic:<T>");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("<T>", result.Product);
        Assert.NotNull(result.GenericTypes);
        Assert.Contains("T", result.GenericTypes);
    }

    [Fact]
    public void Should_Parse_Multiple_Generic_Parameters()
    {
        // Arrange
        var pragma = CreatePragma("#ix-generic:<T,U,V>");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("T", result.Product);
        Assert.Contains("U", result.Product);
        Assert.Contains("V", result.Product);
        Assert.NotNull(result.GenericTypes);
        Assert.Contains("T", result.GenericTypes);
        Assert.Contains("U", result.GenericTypes);
        Assert.Contains("V", result.GenericTypes);
    }

    [Fact]
    public void Should_Parse_Generic_Declaration_With_Constraints()
    {
        // Arrange
        var pragma = CreatePragma("#ix-generic:<T> where T : IComparable");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("T", result.Product);
        Assert.Contains("where", result.GenericConstrains);
        Assert.Contains("IComparable", result.GenericConstrains);
    }

    [Fact]
    public void Should_Parse_Generic_Declaration_With_Multiple_Constraints()
    {
        // Arrange
        var pragma = CreatePragma("#ix-generic:<T,U> where T : class where U : struct");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.GenericConstrains);
        Assert.Contains("where T : class", result.GenericConstrains);
        Assert.Contains("where U : struct", result.GenericConstrains);
    }

    [Fact]
    public void Should_Parse_Generic_Type_Assignment_With_AsKeyword()
    {
        // Arrange
        var pragma = CreatePragma("#ix-generic:T as POCO");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert       
        Assert.NotNull(result.GenericTypeAssignment);
    }

    #endregion

    #region Error Handling Tests

    [Fact]
    public void Should_Throw_MalformedPragmaException_For_Invalid_Property_Pragma()
    {
        // Arrange
        var pragma = CreatePragma("#ix-prop:invalid syntax");
        var declaration = CreateDeclaration();

        // Act & Assert
        Assert.Throws<MalformedPragmaException>(() => PragmaCompiler.Compile(pragma, declaration));
    }

    [Fact]
    public void Should_Throw_MalformedPragmaException_For_Empty_Pragma()
    {
        // Arrange
        var pragma = CreatePragma("");
        var declaration = CreateDeclaration();

        // Act & Assert
        Assert.Throws<MalformedPragmaException>(() => PragmaCompiler.Compile(pragma, declaration));
    }

    [Fact]
    public void Should_Throw_MalformedPragmaException_For_Unknown_Pragma_Type()
    {
        // Arrange
        var pragma = CreatePragma("#ix-unknown:something");
        var declaration = CreateDeclaration();

        // Act & Assert
        Assert.Throws<MalformedPragmaException>(() => PragmaCompiler.Compile(pragma, declaration));
    }

    [Fact]
    public void Should_Throw_MalformedPragmaException_For_Malformed_Attribute()
    {
        // Arrange
        var pragma = CreatePragma("#ix-attr:[Incomplete(");
        var declaration = CreateDeclaration();

        // Act & Assert
        Assert.Throws<MalformedPragmaException>(() => PragmaCompiler.Compile(pragma, declaration));
    }

    [Fact]
    public void Should_Throw_MalformedPragmaException_For_Missing_Colon()
    {
        // Arrange
        var pragma = CreatePragma("#ix-prop public int MyProperty");
        var declaration = CreateDeclaration();

        // Act & Assert
        Assert.Throws<MalformedPragmaException>(() => PragmaCompiler.Compile(pragma, declaration));
    }

    #endregion

    #region Edge Cases Tests

    [Fact]
    public void Should_Parse_Property_Declaration_With_Underscores_In_Name()
    {
        // Arrange
        var pragma = CreatePragma("#ix-prop:public string My_Property_Name");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("My_Property_Name", result.Product);
    }

    [Fact]
    public void Should_Parse_Property_Declaration_With_Numbers_In_Name()
    {
        // Arrange
        var pragma = CreatePragma("#ix-prop:public int Property123");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("Property123", result.Product);
    }

    [Fact]
    public void Should_Parse_Attribute_With_Complex_Content()
    {
        // Arrange
        var pragma = CreatePragma("#ix-attr:[CustomAttribute(\"param1\", 123, true, SomeEnum.Value)]");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("[CustomAttribute(", result.Product);
        Assert.Contains("param1", result.Product);
        Assert.Contains("123", result.Product);
        Assert.Contains("true", result.Product);
        Assert.Contains("SomeEnum.Value", result.Product);
    }

    [Fact]
    public void Should_Compile_Pragma_Without_Declaration()
    {
        // Arrange
        var pragma = CreatePragma("#ix-prop:public string Title");

        // Act
        var result = PragmaCompiler.Compile(pragma);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("Title", result.Product);
    }

    [Fact]
    public void Should_Parse_Property_Setter_With_Empty_String()
    {
        // Arrange
        var pragma = CreatePragma("#ix-set:Value = \"\"");
        var declaration = CreateDeclaration("myField");

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("Value", result.Product);
        Assert.Contains("@\"\"", result.Product);
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void Should_Preserve_Case_Sensitivity_In_Property_Names()
    {
        // Arrange
        var pragma = CreatePragma("#ix-prop:public string MyPropName");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Product);
        Assert.Contains("MyPropName", result.Product);
        Assert.DoesNotContain("mypropname", result.Product);
    }

    [Fact]
    public void Should_Handle_String_Type_Case_Insensitive()
    {
        // Arrange - using lowercase "string"
        var pragma1 = CreatePragma("#ix-prop:public string Name");
        var pragma2 = CreatePragma("#ix-prop:public STRING Name");
        var declaration = CreateDeclaration();

        // Act
        var result1 = PragmaCompiler.Compile(pragma1, declaration);
        var result2 = PragmaCompiler.Compile(pragma2, declaration);

        // Assert - both should have the special string handling
        Assert.Contains("_Name", result1.Product);
        Assert.Contains("_Name", result2.Product);
        Assert.Contains("Interpolate", result1.Product);
        Assert.Contains("Interpolate", result2.Product);
    }

    [Fact]
    public void Should_Generate_Property_With_Getter_Setter_For_Non_String_Types()
    {
        // Arrange
        var pragma = CreatePragma("#ix-prop:public int Count");
        var declaration = CreateDeclaration();

        // Act
        var result = PragmaCompiler.Compile(pragma, declaration);

        // Assert
        Assert.NotNull(result.Product);
        // Should be a simple auto-property
        Assert.Matches(@"Count\s*\{\s*get;\s*set;\s*\}", result.Product);
        Assert.DoesNotContain("Interpolate", result.Product);
    }

    #endregion
}

/// <summary>
/// Mock implementation of IPragma for testing purposes.
/// </summary>
public class PragmaMock : IPragma
{
    public PragmaMock(string content)
    {
        Content = content;
    }

    public string Content { get; }

    public AX.Text.Location Location => null;

    public IEnumerable<AX.ST.Semantic.Tree.ISemanticNode> ChildNodes => throw new NotImplementedException();

    public TResult Accept<TResult, TContext>(AX.ST.Semantic.Tree.ISemanticNodeVisitor<TResult, TContext> visitor, TContext data)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<AX.ST.Semantic.Tree.ISemanticNode> GetDescendentNodes()
    {
        throw new NotImplementedException();
    }
}
