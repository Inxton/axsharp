// AXSharp.CompilerTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AX.ST.Semantic;
using AX.Text;
using AXSharp.Compiler.Core;


namespace AXSharp.CompilerTests.Core
{
    using System;
    using Xunit;
    using Moq;
    using AX.ST.Semantic.Model;
    using AXSharp.Compiler.Core;
    using AX.ST.Semantic.Tree;
    using AX.ST.Semantic.Symbols;
    using AX.ST.Semantic.Pragmas;
    using AX.ST.Semantic.Model.Declarations;
    using AX.ST.Semantic.Model.Declarations.Types;
    using AX.ST.Semantic.Model.Init;
    using AX.ST.Syntax.Tree;
    using AX.ST.Syntax.Parser;
    using Xunit.Sdk;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class IxNodeVisitorTestsSemantics
    {
        private readonly IxNodeVisitor _testClass;
        private Compilation _compilation;

        public IxNodeVisitorTestsSemantics()
        {
            var sources = new StringText[]
            {
                new StringText("CLASS PUBLIC TestClass END_CLASS"),
                new StringText("CONFIGURATION MyConfiguration\r\n    TASK Main(Interval := T#1000ms, Priority := 1);\r\n    PROGRAM P1 WITH Main: MyProgram;\r\n\r\n    VAR_GLOBAL\r\n        lib1_MyClass : lib1.MyClass;\r\n        lib2_MyClass : lib2.MyClass;\r\n    END_VAR\r\nEND_CONFIGURATION"),
                new StringText("TYPE\r\n    TestEnum : (Red, Green, Blue) := Red;\r\nEND_TYPE"),
                new StringText("INTERFACE PUBLIC TestInterface\r\n    \r\nEND_INTERFACE"),
                new StringText("  TYPE\r\n        TestNamedValueType : INT (\r\n            LRED := 12,\r\n            LGREEN := 14,\r\n            LBLUE := 23\r\n        );\r\n    END_TYPE    \r\n"),
                new StringText("TYPE\r\n    TestStructureType : STRUCT\r\n        isRunning : BOOL;\r\n    END_STRUCT; END_TYPE")
            };

            _compilation = Compilation.Create(sources.Select(s => STParser.ParseTextAsync(s).Result), Compilation.Settings.Default).Result;
            _testClass = new IxNodeVisitor(_compilation);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new IxNodeVisitor(_compilation);

            // Assert
            Assert.NotNull(instance);

            // Act
#pragma warning disable CS0618
            instance = new IxNodeVisitor();
#pragma warning restore CS0618

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void CannotConstructWithNullCompilation()
        {
            // Assert.Throws<ArgumentNullException>(() => new IxNodeVisitor(default(Compilation)));
        }

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithFileSyntaxAndData()
        {
            // Arrange
            var fileSyntax = new Mock<IFileSyntax>();
            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;
            
            // Act
            ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)data.Object).Accept(fileSyntax.Object, asCombinedThreeVisitor);

            // Assert
            data.As<ICombinedThreeVisitor>()
                .Verify(p => p.CreateFile(fileSyntax.Object, data.Object),
                    Times.Once);

        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithNamespaceDeclarationSyntaxAndData()
        {
            // Arrange
            var namespaceDeclarationSyntax = new Mock<INamespaceDeclarationSyntax>();
            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;

            var name = new Mock<IQualifiedIdentifierSyntax>();
            name.Setup(n => n.FullyQualifiedIdentifier).Returns("test.ns.test");
            namespaceDeclarationSyntax.Setup(x => x.Name).Returns(name.Object);

            // Act
            ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(namespaceDeclarationSyntax.Object, asCombinedThreeVisitor);

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithClassDeclarationSyntaxAndData()
        {
            // Arrange
            var className = "TestClass";
            var classDeclarationSyntax = new Mock<IClassDeclarationSyntax>();
            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;


            var name = new Mock<ISyntaxToken>();
            name.Setup(n => n.Text).Returns(className);
            classDeclarationSyntax.Setup(x => x.Name).Returns(name.Object);

            var semantics = _compilation.GetSemanticTree().Classes.Where(p => p.Name == className)!
                .FirstOrDefault();

            // Act
            ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)data.Object).Accept(classDeclarationSyntax.Object,
                asCombinedThreeVisitor);


            // Assert
            data.As<ICombinedThreeVisitor>()
                .Verify(p => p.CreateClassDeclaration(classDeclarationSyntax.Object, semantics, data.Object),
                    Times.Once);
            
        }

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithClassDeclarationSyntaxAndData_and_non_existing_class()
        {
            // Arrange
            var classDeclarationSyntax = new Mock<IClassDeclarationSyntax>();
            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;


            var name = new Mock<ISyntaxToken>();
            name.Setup(n => n.Text).Returns("NotExistingClass");
            classDeclarationSyntax.Setup(x => x.Name).Returns(name.Object);

            // Assert
            Xunit.Assert.Throws<TypeNotFoundInSemanticTreeException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)data.Object).Accept(classDeclarationSyntax.Object,
                asCombinedThreeVisitor));


        }


        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithConfigDeclarationSyntaxAndData()
        {
            // Arrange
            var configDeclarationSyntax = new Mock<IConfigDeclarationSyntax>();

            var name = new Mock<ISyntaxToken>();
            name.Setup(n => n.Text).Returns("MyConfiguration");
            configDeclarationSyntax.Setup(p => p.Name).Returns(name.Object);


            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;

            var semantics = _compilation.GetSemanticTree().Configurations.FirstOrDefault();

            // Act
            ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)data.Object).Accept(configDeclarationSyntax.Object, asCombinedThreeVisitor);


            // Assert
            data.As<ICombinedThreeVisitor>()
                .Verify(p => p.CreateConfigDeclaration(configDeclarationSyntax.Object, semantics, data.Object),
                    Times.Once);
        }


        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithConfigDeclarationSyntaxAndData_non_existing_config()
        {
            // Arrange
            var configDeclarationSyntax = new Mock<IConfigDeclarationSyntax>();

            var name = new Mock<ISyntaxToken>();
            name.Setup(n => n.Text).Returns("NonExistingConfiguration");
            configDeclarationSyntax.Setup(p => p.Name).Returns(name.Object);


            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;

            var semantics = _compilation.GetSemanticTree().Configurations.FirstOrDefault();

            // Assert
            Xunit.Assert.Throws<TypeNotFoundInSemanticTreeException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)data.Object).Accept(configDeclarationSyntax.Object, asCombinedThreeVisitor));

        }


        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithEnumTypeDeclarationSyntaxAndData()
        {
            // Arrange
            var enumTypeDeclarationSyntax = new Mock<IEnumTypeDeclarationSyntax>();
            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;

            var name = new Mock<ISyntaxToken>();
            name.Setup(n => n.Text).Returns("TestEnum");
            enumTypeDeclarationSyntax.Setup(x => x.Name).Returns(name.Object);


            var semantics = _compilation.GetSemanticTree().Types.Where(p => p.Name == "TestEnum")!.FirstOrDefault();

            // Act
            ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)data.Object).Accept(enumTypeDeclarationSyntax.Object, asCombinedThreeVisitor);

            // Assert
            data.As<ICombinedThreeVisitor>()
                .Verify(p => p.CreateEnumTypeDeclaration(enumTypeDeclarationSyntax.Object, semantics!, data.Object),
                    Times.Once);
        }

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithEnumTypeDeclarationSyntaxAndData_enum_does_not_exist()
        {
            // Arrange
            var enumTypeDeclarationSyntax = new Mock<IEnumTypeDeclarationSyntax>();
            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;

            var name = new Mock<ISyntaxToken>();
            name.Setup(n => n.Text).Returns("InexistingEnum");
            enumTypeDeclarationSyntax.Setup(x => x.Name).Returns(name.Object);

            // Assert
            Xunit.Assert.Throws<TypeNotFoundInSemanticTreeException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)data.Object).Accept(enumTypeDeclarationSyntax.Object, asCombinedThreeVisitor));

            

        }

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithNamedValueTypeDeclarationSyntaxAndData()
        {
            // Arrange
            var namedValueTypeDeclarationSyntax = new Mock<INamedValueTypeDeclarationSyntax>();
            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;

            var name = new Mock<ISyntaxToken>();
            name.Setup(n => n.Text).Returns("TestNamedValueType");
            namedValueTypeDeclarationSyntax.Setup(x => x.Name).Returns(name.Object);


            var semantics = _compilation.GetSemanticTree()
                .Types
                .Where(p => p.Name == "TestNamedValueType")
                !.FirstOrDefault() as INamedValueTypeDeclaration;

            // Act
            ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)data.Object).Accept(namedValueTypeDeclarationSyntax.Object, asCombinedThreeVisitor);

            // Assert
            data.As<ICombinedThreeVisitor>()
                .Verify(p => p.CreateNamedValueTypeDeclaration(namedValueTypeDeclarationSyntax.Object, semantics, data.Object),
                    Times.Once);
        }


        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithNamedValueTypeDeclarationSyntaxAndData_non_existing_type()
        {
            // Arrange
            var namedValueTypeDeclarationSyntax = new Mock<INamedValueTypeDeclarationSyntax>();
            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;

            var name = new Mock<ISyntaxToken>();
            name.Setup(n => n.Text).Returns("NonExistingNamedValueType");
            namedValueTypeDeclarationSyntax.Setup(x => x.Name).Returns(name.Object);

            
            // Assert
            Xunit.Assert.Throws<TypeNotFoundInSemanticTreeException>(() =>((ISyntaxNodeVisitor<ICombinedThreeVisitor>)data.Object).Accept(namedValueTypeDeclarationSyntax.Object, asCombinedThreeVisitor));
        }

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithStructTypeDeclarationSyntaxAndData()
        {
            // Arrange
            var structTypeDeclarationSyntax = new Mock<IStructTypeDeclarationSyntax>();
            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;

            var name = new Mock<ISyntaxToken>();
            name.Setup(n => n.Text).Returns("TestStructureType");
            structTypeDeclarationSyntax.Setup(x => x.Name).Returns(name.Object);

            var semantics = _compilation.GetSemanticTree().Types.FirstOrDefault(p => p.Name == "TestStructureType") as IStructuredTypeDeclaration;

            // Act
            ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)data.Object).Accept(structTypeDeclarationSyntax.Object, asCombinedThreeVisitor);

            // Assert
            data.As<ICombinedThreeVisitor>()
                .Verify(p => p.CreateStructuredType(structTypeDeclarationSyntax.Object, semantics, data.Object),
                    Times.Once);
        }

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithStructTypeDeclarationSyntaxAndData_non_existing()
        {
            // Arrange
            var structTypeDeclarationSyntax = new Mock<IStructTypeDeclarationSyntax>();
            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;

            var name = new Mock<ISyntaxToken>();
            name.Setup(n => n.Text).Returns("NonExistingStructureType");
            structTypeDeclarationSyntax.Setup(x => x.Name).Returns(name.Object);

            

            // Act
            Xunit.Assert.Throws<TypeNotFoundInSemanticTreeException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)data.Object).Accept(structTypeDeclarationSyntax.Object, asCombinedThreeVisitor));

           
        }

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithVarDeclarationSectionSyntaxAndData()
        {
            // Arrange
            var varDeclarationSectionSyntax = new Mock<IVarDeclarationSectionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(varDeclarationSectionSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithVariableDeclarationSyntaxAndData()
        {
            // Arrange
            var variableDeclarationSyntax = new Mock<IVariableDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(variableDeclarationSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithElementaryTypeSyntaxAndData()
        {
            // Arrange
            var elementaryTypeSyntax = new Mock<IElementaryTypeSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Xunit.Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(elementaryTypeSyntax, data));

        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithUsingDirectiveSyntaxAndData()
        {
            // Arrange
            var usingDirectiveSyntax = new Mock<IUsingDirectiveSyntax>();
            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;

            // Act
            ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)data.Object).Accept(usingDirectiveSyntax.Object, asCombinedThreeVisitor);

            // Assert
            data.As<ICombinedThreeVisitor>()
                .Verify(p => p.CreateUsingDirective(usingDirectiveSyntax.Object, asCombinedThreeVisitor),
                    Times.Once);
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithImplementsListSyntaxAndData()
        {
            // Arrange
            var implementsListSyntax = new Mock<IImplementsListSyntax>();
            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;

            // Act
            ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(implementsListSyntax.Object, asCombinedThreeVisitor);

            // Assert
            data.As<ICombinedThreeVisitor>()
                .Verify(p => p.CreateImplementsList(implementsListSyntax.Object, asCombinedThreeVisitor),
                    Times.Once);
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithInterfaceDeclarationSyntaxAndData()
        {
            // Arrange
            var interfaceDeclarationSyntax = new Mock<IInterfaceDeclarationSyntax>();
            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;

            var name = new Mock<ISyntaxToken>();
            name.Setup(n => n.Text).Returns("TestInterface");
            interfaceDeclarationSyntax.Setup(x => x.Name).Returns(name.Object);

            var semantics = _compilation.GetSemanticTree().Interfaces.Where(p => p.Name == "TestInterface")!
                .FirstOrDefault();


            // Act
            ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)data.Object).Accept(interfaceDeclarationSyntax.Object, asCombinedThreeVisitor);

            // Assert
            data.As<ICombinedThreeVisitor>()
                .Verify(p => p.CreateInterfaceDeclaration(interfaceDeclarationSyntax.Object, semantics, data.Object),
                    Times.Once);
        }

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithInterfaceDeclarationSyntaxAndData_non_existing_interface()
        {
            // Arrange
            var interfaceDeclarationSyntax = new Mock<IInterfaceDeclarationSyntax>();
            var data = new Mock<IxNodeVisitor>(_compilation);
            var asCombinedThreeVisitor = data.As<ICombinedThreeVisitor>().Object;

            var name = new Mock<ISyntaxToken>();
            name.Setup(n => n.Text).Returns("NonExistingsInterface");
            interfaceDeclarationSyntax.Setup(x => x.Name).Returns(name.Object);




            // Act
            Assert.Throws<TypeNotFoundInSemanticTreeException>(() =>
                ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)data.Object).Accept(interfaceDeclarationSyntax.Object,
                    asCombinedThreeVisitor));



        }

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithTypeDeclarationSectionSyntaxAndData()
        {
            // Arrange
            var typeDeclarationSectionSyntax = new Mock<ITypeDeclarationSectionSyntax>();
            var data = new Mock<ICombinedThreeVisitor>();

            // Act
            ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(typeDeclarationSectionSyntax.Object, data.Object);


            typeDeclarationSectionSyntax.Verify(p => p.TypeDeclarations, Times.Once);
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithStringDeclarationSyntaxAndData()
        {
            // Arrange
            var stringDeclarationSyntax = new Mock<IStringDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(stringDeclarationSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithUserDeclaredTypeSyntaxAndData()
        {
            // Arrange
            var userDeclaredTypeSyntax = new Mock<IUserDeclaredTypeSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(userDeclaredTypeSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithPragmaSyntaxAndData()
        {
            // Arrange
            var pragmaSyntax = new Mock<IPragmaSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(pragmaSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithExpressionInitializerSyntaxAndData()
        {
            // Arrange
            var expressionInitializerSyntax = new Mock<IExpressionInitializerSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(expressionInitializerSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithLiteralSyntaxAndData()
        {
            // Arrange
            var literalSyntax = new Mock<ILiteralSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(literalSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithExtendsListSyntaxAndData()
        {
            // Arrange
            var extendsListSyntax = new Mock<IExtendsListSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(extendsListSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithTaskDeclarationSyntaxAndData()
        {
            // Arrange
            var taskDeclarationSyntax = new Mock<ITaskDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(taskDeclarationSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithTaskConfigSyntaxAndData()
        {
            // Arrange
            var taskConfigSyntax = new Mock<ITaskConfigSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(taskConfigSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithProgConfigSyntaxAndData()
        {
            // Arrange
            var progConfigSyntax = new Mock<IProgramConfigurationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(progConfigSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithAbstractMethodDeclarationSyntaxAndData()
        {
            // Arrange
            var abstractMethodDeclarationSyntax = new Mock<IAbstractMethodDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(abstractMethodDeclarationSyntax, data));

            // Assert
            //
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithAccessModifierSyntaxAndData()
        {
            // Arrange
            var accessModifierSyntax = new Mock<IAccessModifierSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(accessModifierSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithAdditionalVariableNameSyntaxAndData()
        {
            // Arrange
            var additionalVariableNameSyntax = new Mock<IAdditionalVariableNameSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(additionalVariableNameSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithAdditiveExpressionSyntaxAndData()
        {
            // Arrange
            var additiveExpressionSyntax = new Mock<IAdditiveExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(additiveExpressionSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithAndExpressionSyntaxAndData()
        {
            // Arrange
            var andExpressionSyntax = new Mock<IAndExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(andExpressionSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithAnonymousArrayTypeSyntaxAndData()
        {
            // Arrange
            var anonymousArrayTypeSyntax = new Mock<IAnonymousArrayTypeSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(anonymousArrayTypeSyntax, data));

            // Assert
            
        }


        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithAnonymousReferenceTypeSyntaxAndData()
        {
            // Arrange
            var anonymousReferenceTypeSyntax = new Mock<IAnonymousReferenceTypeSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(anonymousReferenceTypeSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithArgumentExpressionSyntaxAndData()
        {
            // Arrange
            var argumentExpressionSyntax = new Mock<IArgumentExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(argumentExpressionSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithArrayAccessExpressionSyntaxAndData()
        {
            // Arrange
            var arrayAccessExpressionSyntax = new Mock<IArrayAccessExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(arrayAccessExpressionSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithArrayAccessIndexListSyntaxAndData()
        {
            // Arrange
            var arrayAccessIndexListSyntax = new Mock<IArrayAccessIndexListSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(arrayAccessIndexListSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithArrayAccessIndexSyntaxAndData()
        {
            // Arrange
            var arrayAccessIndexSyntax = new Mock<IArrayAccessIndexSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(arrayAccessIndexSyntax, data));

            // Assert
            
        }

    

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithArrayDeclarationSyntaxAndData()
        {
            // Arrange
            var arrayDeclarationSyntax = new Mock<IArrayDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(arrayDeclarationSyntax, data));

            // Assert
            
        }

     

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithArrayDimensionsSyntaxAndData()
        {
            // Arrange
            var arrayDimensionsSyntax = new Mock<IArrayDimensionsSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(arrayDimensionsSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithArrayDimensionSyntaxAndData()
        {
            // Arrange
            var arrayDimensionSyntax = new Mock<IArrayDimensionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(arrayDimensionSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithArrayElementInitializerSyntaxAndData()
        {
            // Arrange
            var arrayElementInitializerSyntax = new Mock<IArrayElementInitializerSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(arrayElementInitializerSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithArrayInitializerListSyntaxAndData()
        {
            // Arrange
            var arrayInitializerListSyntax = new Mock<IArrayInitializerListSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(arrayInitializerListSyntax, data));

            // Assert
            
        }


        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithArrayInitializerSyntaxAndData()
        {
            // Arrange
            var arrayInitializerSyntax = new Mock<IArrayInitializerSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(arrayInitializerSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithArrayTypeDeclarationSyntaxAndData()
        {
            // Arrange
            var arrayTypeDeclarationSyntax = new Mock<IArrayTypeDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(arrayTypeDeclarationSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithAsmStatementSyntaxAndData()
        {
            // Arrange
            var asmStatementSyntax = new Mock<IAsmStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(asmStatementSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithAssignmentAttemptStatementSyntaxAndData()
        {
            // Arrange
            var assignmentAttemptStatementSyntax = new Mock<IAssignmentAttemptStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(assignmentAttemptStatementSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithAssignStatementSyntaxAndData()
        {
            // Arrange
            var assignStatementSyntax = new Mock<IAssignStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(assignStatementSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithBoundExpressionSyntaxAndData()
        {
            // Arrange
            var boundExpressionSyntax = new Mock<IBoundExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(boundExpressionSyntax, data));

            // Assert
            
        }

     

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithCallExpressionSyntaxAndData()
        {
            // Arrange
            var callExpressionSyntax = new Mock<ICallExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(callExpressionSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithCallParamAssignmentLhsSyntaxAndData()
        {
            // Arrange
            var callParamAssignmentLhsSyntax = new Mock<ICallParamAssignmentLhsSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(callParamAssignmentLhsSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithCaseListElemSyntaxAndData()
        {
            // Arrange
            var caseListElemSyntax = new Mock<ICaseListElemSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(caseListElemSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithCaseListSyntaxAndData()
        {
            // Arrange
            var caseListSyntax = new Mock<ICaseListSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(caseListSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithCaseSelectionSyntaxAndData()
        {
            // Arrange
            var caseSelectionSyntax = new Mock<ICaseSelectionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(caseSelectionSyntax, data));

            // Assert
            
        }

       
        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithCaseStatementSyntaxAndData()
        {
            // Arrange
            var caseStatementSyntax = new Mock<ICaseStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(caseStatementSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithClassMemberDeclarationSyntaxAndData()
        {
            // Arrange
            var classMemberDeclarationSyntax = new Mock<IClassMemberDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(classMemberDeclarationSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithCodeBodySyntaxAndData()
        {
            // Arrange
            var codeBodySyntax = new Mock<ICodeBodySyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(codeBodySyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithCommaCaseListElemSyntaxAndData()
        {
            // Arrange
            var commaCaseListElemSyntax = new Mock<ICommaCaseListElemSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(commaCaseListElemSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithConditionalStatementSyntaxAndData()
        {
            // Arrange
            var conditionalStatementSyntax = new Mock<IConditionalStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(conditionalStatementSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithConfigurationElementSyntaxAndData()
        {
            // Arrange
            var configurationElementSyntax = new Mock<IConfigurationElementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(configurationElementSyntax, data));

            // Assert
            
        }

     

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithContinueStatementSyntaxAndData()
        {
            // Arrange
            var continueStatementSyntax = new Mock<IContinueStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(continueStatementSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithConvertExpressionSyntaxAndData()
        {
            // Arrange
            var convertExpressionSyntax = new Mock<IConvertExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(convertExpressionSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithDeclarationSyntaxAndData()
        {
            // Arrange
            var declarationSyntax = new Mock<IDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(declarationSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithDereferenceExpressionSyntaxAndData()
        {
            // Arrange
            var dereferenceExpressionSyntax = new Mock<IDereferenceExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(dereferenceExpressionSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithElseStatementSyntaxAndData()
        {
            // Arrange
            var elseStatementSyntax = new Mock<IElseStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(elseStatementSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithElsifStatementSyntaxAndData()
        {
            // Arrange
            var elsifStatementSyntax = new Mock<IElsifStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(elsifStatementSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithEmptyStatementSyntaxAndData()
        {
            // Arrange
            var emptyStatementSyntax = new Mock<IEmptyStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(emptyStatementSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithEnumValueSyntaxAndData()
        {
            // Arrange
            var enumValueSyntax = new Mock<IEnumValueSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(enumValueSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithExitStatementSyntaxAndData()
        {
            // Arrange
            var exitStatementSyntax = new Mock<IExitStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(exitStatementSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithExponentiationExpressionSyntaxAndData()
        {
            // Arrange
            var exponentiationExpressionSyntax = new Mock<IExponentiationExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(exponentiationExpressionSyntax, data));

            // Assert
            
        }

     

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithExpressionStatementSyntaxAndData()
        {
            // Arrange
            var expressionStatementSyntax = new Mock<IExpressionStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(expressionStatementSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithExpressionSyntaxAndData()
        {
            // Arrange
            var expressionSyntax = new Mock<IExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(expressionSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithExternFunctionDeclarationSyntaxAndData()
        {
            // Arrange
            var externFunctionDeclarationSyntax = new Mock<IExternFunctionDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(externFunctionDeclarationSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithForStatementSyntaxAndData()
        {
            // Arrange
            var forStatementSyntax = new Mock<IForStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(forStatementSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithFunctionBlockDeclarationSyntaxAndData()
        {
            // Arrange
            var functionBlockDeclarationSyntax = new Mock<IFunctionBlockDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(functionBlockDeclarationSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithFunctionDeclarationSyntaxAndData()
        {
            // Arrange
            var functionDeclarationSyntax = new Mock<IFunctionDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Will not throw exception function is not implemented.
            //Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(functionDeclarationSyntax, data));

           
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithIdentifierAccessSyntaxAndData()
        {
            // Arrange
            var identifierAccessSyntax = new Mock<IIdentifierAccessSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(identifierAccessSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithInitializerSyntaxAndData()
        {
            // Arrange
            var initializerSyntax = new Mock<IInitializerSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(initializerSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithIntervalSpecSyntaxAndData()
        {
            // Arrange
            var intervalSpecSyntax = new Mock<IIntervalSpecSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(intervalSpecSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithIterationStatementSyntaxAndData()
        {
            // Arrange
            var iterationStatementSyntax = new Mock<IIterationStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(iterationStatementSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithJumpStatementSyntaxAndData()
        {
            // Arrange
            var jumpStatementSyntax = new Mock<IJumpStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(jumpStatementSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithListElementSyntaxAndData()
        {
            // Arrange
            var listElementSyntax = new Mock<IListElementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(listElementSyntax, data));

            // Assert
            
        }

        
        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithMemberAccessExpressionSyntaxAndData()
        {
            // Arrange
            var memberAccessExpressionSyntax = new Mock<IMemberAccessExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(memberAccessExpressionSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithMemberInitializerSyntaxAndData()
        {
            // Arrange
            var memberInitializerSyntax = new Mock<IMemberInitializerSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(memberInitializerSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithMethodDeclarationSyntaxAndData()
        {
            // Arrange
            var methodDeclarationSyntax = new Mock<IMethodDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(methodDeclarationSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithMethodPrototypeDeclarationSyntaxAndData()
        {
            // Arrange
            var methodPrototypeDeclarationSyntax = new Mock<IMethodPrototypeDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(methodPrototypeDeclarationSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithMultiplicativeExpressionSyntaxAndData()
        {
            // Arrange
            var multiplicativeExpressionSyntax = new Mock<IMultiplicativeExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(multiplicativeExpressionSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithNamedParamListSyntaxAndData()
        {
            // Arrange
            var namedParamListSyntax = new Mock<INamedParamListSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(namedParamListSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithNamedTypeSyntaxAndData()
        {
            // Arrange
            var namedTypeSyntax = new Mock<INamedTypeSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(namedTypeSyntax, data));

            // Assert
            
        }

     

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithNamedValueSyntaxAndData()
        {
            // Arrange
            var namedValueSyntax = new Mock<INamedValueSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(namedValueSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithNameOfExpressionSyntaxAndData()
        {
            // Arrange
            var nameOfExpressionSyntax = new Mock<INameOfExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(nameOfExpressionSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithNamespaceElementSyntaxAndData()
        {
            // Arrange
            var namespaceElementSyntax = new Mock<INamespaceElementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(namespaceElementSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithNotExpressionSyntaxAndData()
        {
            // Arrange
            var notExpressionSyntax = new Mock<INotExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(notExpressionSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithOrExpressionSyntaxAndData()
        {
            // Arrange
            var orExpressionSyntax = new Mock<IOrExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(orExpressionSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithParameterAssignmentSyntaxAndData()
        {
            // Arrange
            var parameterAssignmentSyntax = new Mock<IParameterAssignmentSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(parameterAssignmentSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithParameterListSyntaxAndData()
        {
            // Arrange
            var parameterListSyntax = new Mock<IParameterListSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(parameterListSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithParenthesesExpressionSyntaxAndData()
        {
            // Arrange
            var parenthesesExpressionSyntax = new Mock<IParenthesesExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(parenthesesExpressionSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithPartialAccessExpressionSyntaxAndData()
        {
            // Arrange
            var partialAccessExpressionSyntax = new Mock<IPartialAccessExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(partialAccessExpressionSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithPouDeclarationSyntaxAndData()
        {
            // Arrange
            var pouDeclarationSyntax = new Mock<IPouDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(pouDeclarationSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithPrimaryExpressionSyntaxAndData()
        {
            // Arrange
            var primaryExpressionSyntax = new Mock<IPrimaryExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(primaryExpressionSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithPrioritySpecSyntaxAndData()
        {
            // Arrange
            var prioritySpecSyntax = new Mock<IPrioritySpecSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(prioritySpecSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithProgramDeclarationSyntaxAndData()
        {
            // Arrange
            var programDeclarationSyntax = new Mock<IProgramDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Should be empty call we ignore PROGRAM declarations at this moment.;
            // Act
            ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(programDeclarationSyntax, data);
            
           
            
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithQualifiedEnumAccessSyntaxAndData()
        {
            // Arrange
            var qualifiedEnumAccessSyntax = new Mock<IQualifiedEnumAccessSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(qualifiedEnumAccessSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithQualifiedIdentifierListSyntaxAndData()
        {
            // Arrange
            var qualifiedIdentifierListSyntax = new Mock<IQualifiedIdentifierListSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(qualifiedIdentifierListSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithQualifiedIdentifierPartSyntaxAndData()
        {
            // Arrange
            var qualifiedIdentifierPartSyntax = new Mock<IQualifiedIdentifierPartSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(qualifiedIdentifierPartSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithQualifiedIdentifierSyntaxAndData()
        {
            // Arrange
            var qualifiedIdentifierSyntax = new Mock<IQualifiedIdentifierSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(qualifiedIdentifierSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithReferenceExpressionSyntaxAndData()
        {
            // Arrange
            var referenceExpressionSyntax = new Mock<IReferenceExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(referenceExpressionSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithReferenceTypeDeclarationSyntaxAndData()
        {
            // Arrange
            var referenceTypeDeclarationSyntax = new Mock<IReferenceTypeDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(referenceTypeDeclarationSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithRelationalExpressionSyntaxAndData()
        {
            // Arrange
            var relationalExpressionSyntax = new Mock<IRelationalExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(relationalExpressionSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithRepeatStatementSyntaxAndData()
        {
            // Arrange
            var repeatStatementSyntax = new Mock<IRepeatStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(repeatStatementSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithReturnStatementSyntaxAndData()
        {
            // Arrange
            var returnStatementSyntax = new Mock<IReturnStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(returnStatementSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithRotateShiftExpressionSyntaxAndData()
        {
            // Arrange
            var rotateShiftExpressionSyntax = new Mock<IRotateShiftExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(rotateShiftExpressionSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithSectionModifierSyntaxAndData()
        {
            // Arrange
            var sectionModifierSyntax = new Mock<ISectionModifierSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(sectionModifierSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithStatementListSyntaxAndData()
        {
            // Arrange
            var statementListSyntax = new Mock<IStatementListSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(statementListSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithStatementPartSyntaxAndData()
        {
            // Arrange
            var statementPartSyntax = new Mock<IStatementPartSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(statementPartSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithStatementSyntaxAndData()
        {
            // Arrange
            var statementSyntax = new Mock<IStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(statementSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithStructureInitializerListSyntaxAndData()
        {
            // Arrange
            var structureInitializerListSyntax = new Mock<IStructureInitializerListSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(structureInitializerListSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithStructureInitializerSyntaxAndData()
        {
            // Arrange
            var structureInitializerSyntax = new Mock<IStructureInitializerSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(structureInitializerSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithSuperExpressionSyntaxAndData()
        {
            // Arrange
            var superExpressionSyntax = new Mock<ISuperExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(superExpressionSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithTaskAssignmentSyntaxAndData()
        {
            // Arrange
            var taskAssignmentSyntax = new Mock<ITaskAssignmentSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(taskAssignmentSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithTaskConfigurationDeclarationSyntaxAndData()
        {
            // Arrange
            var taskConfigurationDeclarationSyntax = new Mock<ITaskConfigurationDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(taskConfigurationDeclarationSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithTaskInitSyntaxAndData()
        {
            // Arrange
            var taskInitSyntax = new Mock<ITaskInitSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(taskInitSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithThisAccessSyntaxAndData()
        {
            // Arrange
            var thisAccessSyntax = new Mock<IThisAccessSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(thisAccessSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithTypeDeclarationSyntaxAndData()
        {
            // Arrange
            var typeDeclarationSyntax = new Mock<ITypeDeclarationSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(typeDeclarationSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithTypeSyntaxAndData()
        {
            // Arrange
            var typeSyntax = new Mock<ITypeSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(typeSyntax, data));

            // Assert
            
        }

      

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithUnnamedParamListSyntaxAndData()
        {
            // Arrange
            var unnamedParamListSyntax = new Mock<IUnnamedParamListSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(unnamedParamListSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithUnsafeAssignStatementSyntaxAndData()
        {
            // Arrange
            var unsafeAssignStatementSyntax = new Mock<IUnsafeAssignStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(unsafeAssignStatementSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithVariableListSyntaxAndData()
        {
            // Arrange
            var variableListSyntax = new Mock<IVariableListSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(variableListSyntax, data));

            // Assert
            
        }

        

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithVariableNameAtSyntaxAndData()
        {
            // Arrange
            var variableNameAtSyntax = new Mock<IVariableNameAtSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(variableNameAtSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithWhileStatementSyntaxAndData()
        {
            // Arrange
            var whileStatementSyntax = new Mock<IWhileStatementSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(whileStatementSyntax, data));

            // Assert
            
        }

       

        [Fact]
        public void CanCallAcceptForISyntaxNodeVisitor_ICombinedThreeVisitor_WithXOrExpressionSyntaxAndData()
        {
            // Arrange
            var xOrExpressionSyntax = new Mock<IXOrExpressionSyntax>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotSupportedException>(() => ((ISyntaxNodeVisitor<ICombinedThreeVisitor>)_testClass).Accept(xOrExpressionSyntax, data));

            // Assert
            
        }

       
    }
}