// AXSharp.CompilerTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AX.ST.Semantic;
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

    public class IxNodeVisitorTests
    {
        private readonly IxNodeVisitor _testClass;
        
        public IxNodeVisitorTests()
        {
            //_compilation = new Compilation();
#pragma warning disable CS0618
            _testClass = new IxNodeVisitor();
#pragma warning restore CS0618
        }

        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithPartialSemanticTreeAndData()
        {
            // Arrange
            var partialSemanticTree = new Mock<IPartialSemanticTree>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(partialSemanticTree, data));

            // Assert

        }

        [Fact]
        public void CannotCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithPartialSemanticTreeAndDataWithNullPartialSemanticTree()
        {
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(default(IPartialSemanticTree), new Mock<ICombinedThreeVisitor>().Object));
        }

        [Fact]
        public void CannotCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithPartialSemanticTreeAndDataWithNullData()
        {
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(new Mock<IPartialSemanticTree>().Object, default(ICombinedThreeVisitor)));
        }

        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithSymbolAndData()
        {
            // Arrange
            var symbol = new Mock<ISymbol>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(symbol, data));
        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithPragmaAndData()
        {
            // Arrange
            var pragma = new Mock<IPragma>().Object;
            var testClass = new Mock<IxNodeVisitor>();
            var data = testClass.As<ICombinedThreeVisitor>();

            // Act
            ((ISemanticNodeVisitor<ICombinedThreeVisitor>)testClass.Object).Visit(pragma, data.Object);

            // Assert
            data.Verify(p => p.CreatePragma(pragma, data.Object), Times.Once);
        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithConfigurationDeclarationAndData()
        {
            // Arrange
            var configurationDeclaration = new Mock<IConfigurationDeclaration>().Object;
            var testClass = new Mock<IxNodeVisitor>();
            var data = testClass.As<ICombinedThreeVisitor>();

            // Act
            ((ISemanticNodeVisitor<ICombinedThreeVisitor>)testClass.Object).Visit(configurationDeclaration, data.Object);

            // Assert
            data.Verify(p => p.CreateConfigDeclaration(configurationDeclaration, testClass.Object), Times.Once);
        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithTaskConfigurationDeclarationAndData()
        {
            // Arrange
            var taskConfigurationDeclaration = new Mock<ITaskConfigurationDeclaration>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(taskConfigurationDeclaration, data));



        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithTaskDeclarationAndData()
        {
            // Arrange
            var taskDeclaration = new Mock<ITaskDeclaration>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(taskDeclaration, data));

        }

        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithProgramConfigurationDeclarationAndData()
        {
            // Arrange
            var programConfigurationDeclaration = new Mock<IProgramConfigurationDeclaration>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(programConfigurationDeclaration, data));



        }

        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithNamespaceDeclarationAndData()
        {
            // Arrange
            var namespaceDeclaration = new Mock<INamespaceDeclaration>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(namespaceDeclaration, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithUsingDirectiveAndData()
        {
            // Arrange
            var usingDirective = new Mock<IUsingDirective>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(usingDirective, data));



        }

        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithProgramDeclarationAndData()
        {
            // Arrange
            var programDeclaration = new Mock<IProgramDeclaration>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(programDeclaration, data));



        }

        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithClassDeclarationAndData()
        {
            // Arrange
            var classDeclaration = new Mock<IClassDeclaration>();
            var testClass = new Mock<IxNodeVisitor>();
            var data = testClass.As<ICombinedThreeVisitor>();

            // Act
            ((ISemanticNodeVisitor<ICombinedThreeVisitor>)testClass.Object).Visit(classDeclaration.Object, data.Object);

            // Assert
            data.Verify(p => p.CreateClassDeclaration(classDeclaration.Object, testClass.Object), Times.Once);
        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithInterfaceDeclarationAndData()
        {
            // Arrange
            var interfaceDeclaration = new Mock<IInterfaceDeclaration>().Object;
            var testClass = new Mock<IxNodeVisitor>();
            var data = testClass.As<ICombinedThreeVisitor>();


            // Act
            ((ISemanticNodeVisitor<ICombinedThreeVisitor>)testClass.Object).Visit(interfaceDeclaration, data.Object);

            // Assert
            data.Verify(p => p.CreateInterfaceDeclaration(interfaceDeclaration, testClass.Object), Times.Once);
        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithFunctionDeclarationAndData()
        {
            // Arrange
            var functionDeclaration = new Mock<IFunctionDeclaration>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(functionDeclaration, data));



        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithFunctionBlockDeclarationAndData()
        {
            // Arrange
            var functionBlockDeclaration = new Mock<IFunctionBlockDeclaration>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(functionBlockDeclaration, data));



        }

        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithMethodDeclarationAndData()
        {
            // Arrange
            var methodDeclaration = new Mock<IMethodDeclaration>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(methodDeclaration, data));



        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithMethodPrototypeDeclarationAndData()
        {
            // Arrange
            var methodPrototypeDeclaration = new Mock<IMethodPrototypeDeclaration>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(methodPrototypeDeclaration, data));



        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithScalarTypeDeclarationAndData()
        {
            // Arrange
            var scalarTypeDeclaration = new Mock<IScalarTypeDeclaration>().Object;
            var testClass = new Mock<IxNodeVisitor>();
            var data = testClass.As<ICombinedThreeVisitor>();

            // Act
            ((ISemanticNodeVisitor<ICombinedThreeVisitor>)testClass.Object).Visit(scalarTypeDeclaration, data.Object);

            // Assert
            data.Verify(p => p.CreateScalarTypeDeclaration(scalarTypeDeclaration, testClass.Object), Times.Once);
        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithStructuredTypeDeclarationAndData()
        {
            // Arrange
            var structuredTypeDeclaration = new Mock<IStructuredTypeDeclaration>().Object;
            var testClass = new Mock<IxNodeVisitor>();
            var data = testClass.As<ICombinedThreeVisitor>();

            // Act
            ((ISemanticNodeVisitor<ICombinedThreeVisitor>)testClass.Object).Visit(structuredTypeDeclaration, data.Object);

            // Assert
            data.Verify(p => p.CreateStructuredType(structuredTypeDeclaration, testClass.Object), Times.Once);
        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithArrayTypeDeclarationAndData()
        {
            // Arrange
            var arrayTypeDeclaration = new Mock<IArrayTypeDeclaration>().Object;
            var testClass = new Mock<IxNodeVisitor>();
            var data = testClass.As<ICombinedThreeVisitor>(); // new Mock<ICombinedThreeVisitor>();

            // Act
            ((ISemanticNodeVisitor<ICombinedThreeVisitor>)testClass.Object).Visit(arrayTypeDeclaration, data.Object);


            // Assert
            data.Verify(p => p.CreateArrayTypeDeclaration(arrayTypeDeclaration, testClass.Object), Times.Once);
        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithEnumTypeDeclarationAndData()
        {
            // Arrange
            var enumTypeDeclaration = new Mock<IEnumTypeDeclaration>().Object;
            var testClass = new Mock<IxNodeVisitor>();
            var data = testClass.As<ICombinedThreeVisitor>();


            // Act
            ((ISemanticNodeVisitor<ICombinedThreeVisitor>)testClass.Object).Visit(enumTypeDeclaration, data.Object);

            // Assert
            data.Verify(p => p.CreateEnumTypeDeclaration(enumTypeDeclaration, testClass.Object), Times.Once);
        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithNamedValueTypeDeclarationAndData()
        {
            // Arrange
            var namedValueTypeDeclaration = new Mock<INamedValueTypeDeclaration>().Object;
            var testClass = new Mock<IxNodeVisitor>();
            var data = testClass.As<ICombinedThreeVisitor>();

            // Act
            ((ISemanticNodeVisitor<ICombinedThreeVisitor>)testClass.Object).Visit(namedValueTypeDeclaration, data.Object);

            // Assert
            data.Verify(p => p.CreateNamedValueTypeDeclaration(namedValueTypeDeclaration, testClass.Object), Times.Once);


        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithReferenceTypeDeclarationAndData()
        {
            // Arrange
            var referenceTypeDeclaration = new Mock<IReferenceTypeDeclaration>().Object;
            var testClass = new Mock<IxNodeVisitor>();
            var data = testClass.As<ICombinedThreeVisitor>();

            // Act
            ((ISemanticNodeVisitor<ICombinedThreeVisitor>)testClass.Object).Visit(referenceTypeDeclaration, data.Object);

            // Assert
            data.Verify(p => p.CreateReferenceToDeclaration(referenceTypeDeclaration, testClass.Object), Times.Once);
        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithStringTypeDeclarationAndData()
        {
            // Arrange
            var stringTypeDeclaration = new Mock<IStringTypeDeclaration>().Object;
            var testClass = new Mock<IxNodeVisitor>();
            var data = testClass.As<ICombinedThreeVisitor>();

            // Act
            ((ISemanticNodeVisitor<ICombinedThreeVisitor>)testClass.Object).Visit(stringTypeDeclaration, data.Object);

            // Assert
            data.Verify(p => p.CreateStringTypeDeclaration(stringTypeDeclaration, testClass.Object), Times.Once);
        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithDimensionAndData()
        {
            // Arrange
            var dimension = new Mock<IDimension>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(dimension, data));

            // Assert

        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithFieldDeclarationAndData()
        {
            // Arrange
            var fieldDeclaration = new Mock<IFieldDeclaration>().Object;
            var testClass = new Mock<IxNodeVisitor>();
            var data = testClass.As<ICombinedThreeVisitor>(); // new Mock<ICombinedThreeVisitor>();

            // Act
            ((ISemanticNodeVisitor<ICombinedThreeVisitor>)testClass.Object).Visit(fieldDeclaration, data.Object);

            // Assert
            data.Verify(p => p.CreateFieldDeclaration(fieldDeclaration, testClass.Object), Times.Once);
        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithVariableDeclarationAndData()
        {
            // Arrange
            var variableDeclaration = new Mock<IVariableDeclaration>().Object;
            var testClass = new Mock<IxNodeVisitor>();
            var data = testClass.As<ICombinedThreeVisitor>(); // new Mock<ICombinedThreeVisitor>();

            // Act
            ((ISemanticNodeVisitor<ICombinedThreeVisitor>)testClass.Object).Visit(variableDeclaration, data.Object);

            // Assert
            data.Verify(p => p.CreateVariableDeclaration(variableDeclaration, testClass.Object), Times.Once);
        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithEnumValueDeclarationAndData()
        {
            // Arrange
            var enumValueDeclaration = new Mock<IEnumValueDeclaration>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(enumValueDeclaration, data));

            // Assert

        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithNamedValueDeclarationAndData()
        {
            // Arrange
            var namedValueDeclaration = new Mock<INamedValueDeclaration>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(namedValueDeclaration, data));

            // Assert

        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithInitializerExpressionAndData()
        {
            // Arrange
            var initializerExpression = new Mock<ISemanticInitializerExpression>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(initializerExpression, data));

            // Assert

        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithArrayInitializerAndData()
        {
            // Arrange
            var arrayInitializer = new Mock<ISemanticArrayInitializer>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(arrayInitializer, data));

            // Assert

        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithStructureInitializerAndData()
        {
            // Arrange
            var structureInitializer = new Mock<ISemanticStructureInitializer>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(structureInitializer, data));

            // Assert

        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithMemberInitializerAndData()
        {
            // Arrange
            var memberInitializer = new Mock<ISemanticMemberInitializer>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Act
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(memberInitializer, data));

            // Assert

        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithSemanticTypeAccessAndData()
        {
            // Arrange
            var semanticTypeAccess = new Mock<ISemanticTypeAccess>().Object;
            var testClass = new Mock<IxNodeVisitor>();
            var data = testClass.As<ICombinedThreeVisitor>();

            // Act
            ((ISemanticNodeVisitor<ICombinedThreeVisitor>)testClass.Object).Visit(semanticTypeAccess, data.Object);

            // Assert
            data.Verify(p => p.CreateSemanticTypeAccess(semanticTypeAccess, testClass.Object), Times.Once);
        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithInstrListAndData()
        {
            // Arrange
            var instrList = new Mock<ISemanticInstructionList>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(instrList, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithISemanticAssignmentInstructionAndICombinedThreeVisitor()
        {
            // Arrange
            var assignment = new Mock<ISemanticAssignmentInstruction>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(assignment, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithISemanticUnsafeAssignmentInstructionAndICombinedThreeVisitor()
        {
            // Arrange
            var assignment = new Mock<ISemanticUnsafeAssignmentInstruction>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(assignment, data));



        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithAssignmentAttemptAndData()
        {
            // Arrange
            var assignmentAttempt = new Mock<ISemanticAssignmentAttemptInstruction>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(assignmentAttempt, data));



        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithExpressionAndData()
        {
            // Arrange
            var expression = new Mock<ISemanticExpressionInstruction>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(expression, data));



        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithCondStatementAndData()
        {
            // Arrange
            var condStatement = new Mock<ISemanticIfConditionalStatement>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(condStatement, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithCondInstrListAndData()
        {
            // Arrange
            var condInstrList = new Mock<IConditionalInstructionList>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(condInstrList, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithCaseStatementAndData()
        {
            // Arrange
            var caseStatement = new Mock<ISemanticCaseStatement>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(caseStatement, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithCaseSelectionAndData()
        {
            // Arrange
            var caseSelection = new Mock<ISemanticCaseSelection>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(caseSelection, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithSubrangeAndData()
        {
            // Arrange
            var subrange = new Mock<ISemanticSubrange>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(subrange, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithForStatementAndData()
        {
            // Arrange
            var forStatement = new Mock<ISemanticForStatement>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(forStatement, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithWhileStatementAndData()
        {
            // Arrange
            var whileStatement = new Mock<ISemanticWhileStatement>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(whileStatement, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithRepeatStatementAndData()
        {
            // Arrange
            var repeatStatement = new Mock<ISemanticRepeatStatement>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(repeatStatement, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithContinueInstructionAndData()
        {
            // Arrange
            var continueInstruction = new Mock<ISemanticContinueInstruction>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(continueInstruction, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithExitInstructionAndData()
        {
            // Arrange
            var exitInstruction = new Mock<ISemanticExitInstruction>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(exitInstruction, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithReturnStatementAndData()
        {
            // Arrange
            var returnStatement = new Mock<ISemanticReturnStatement>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(returnStatement, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithAsmStatementAndData()
        {
            // Arrange
            var asmStatement = new Mock<ISemanticAsmStatement>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(asmStatement, data));



        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithConstExprAndData()
        {
            // Arrange
            var constExpr = new Mock<ISemanticConstantExpression>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(constExpr, data));



        }


        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithIdentifierAccessAndData()
        {
            // Arrange
            var identifierAccess = new Mock<ISemanticIdentifierAccess>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(identifierAccess, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithBinExprAndData()
        {
            // Arrange
            var binExpr = new Mock<ISemanticBinaryExpression>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(binExpr, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithUnaryExpressionAndData()
        {
            // Arrange
            var unaryExpression = new Mock<ISemanticUnaryExpression>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(unaryExpression, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithQualifiedEnumAccessAndData()
        {
            // Arrange
            var qualifiedEnumAccess = new Mock<ISemanticQualifiedEnumAccess>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(qualifiedEnumAccess, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithMemberAccessExpressionAndData()
        {
            // Arrange
            var memberAccessExpression = new Mock<ISemanticMemberAccessExpression>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(memberAccessExpression, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithArrayAccessExpressionAndData()
        {
            // Arrange
            var arrayAccessExpression = new Mock<ISemanticArrayAccessExpression>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert

            Assert.Throws<System.NotImplementedException>(
                () => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(arrayAccessExpression, data));

        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithCallAndData()
        {
            // Arrange
            var call = new Mock<ISemanticCallExpression>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(call, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithParamListAndData()
        {
            // Arrange
            var paramList = new Mock<ISemanticParameterList>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(paramList, data));


        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithParamAssignmentAndData()
        {
            // Arrange
            var paramAssignment = new Mock<ISemanticParameterAssignment>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(paramAssignment, data));



        }



        [Fact]
        public void CanCallVisitForISemanticNodeVisitor_ICombinedThreeVisitor_WithPartialAccessExpressionAndData()
        {
            // Arrange
            var partialAccessExpression = new Mock<IPartialAccessExpression>().Object;
            var data = new Mock<ICombinedThreeVisitor>().Object;

            // Assert
            Assert.Throws<System.NotImplementedException>(() => ((ISemanticNodeVisitor<ICombinedThreeVisitor>)_testClass).Visit(partialAccessExpression, data));



        }
    }
}