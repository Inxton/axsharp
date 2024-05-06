using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Model.Init;
using AX.ST.Semantic.Pragmas;
using AX.ST.Semantic.Symbols;
using AX.ST.Semantic.Tree;
using AX.ST.Syntax.Tree;
using AX.Text;
using AXSharp.ixc_doc.Helpers;
using AXSharp.ixc_doc.Interfaces;
using AXSharp.ixc_doc.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.ixc_doc.Visitors
{

    //semantic
    public partial class MyNodeVisitor : ISemanticNodeVisitor<IYamlBuiderVisitor>
    {
        public YamlSerializerHelper YamlHelper { get; set; }
        //public Compiler.AxProject axProject { get; set; }

        public MyNodeVisitor(Compiler.AxProject? axProject = null)
        {
            //this.axProject = axProject;
            YamlHelper = new YamlSerializerHelper();
        }

        public void MapYamlHelperToSchema()
        {
            YamlHelper.Schema.Items = YamlHelper.Items.ToList();
            YamlHelper.Schema.References = YamlHelper.References.ToArray();
        }

        public void Visit(IPartialSemanticTree partialSemanticTree, IYamlBuiderVisitor data)
        {
            partialSemanticTree.ChildNodes.Where(p => p is INamespaceDeclaration).ToList().ForEach(p => p.Accept(this, data));
        }

        public void Visit(ISymbol symbol, IYamlBuiderVisitor data)
        {
         
        }

        public void Visit(IPragma pragma, IYamlBuiderVisitor data)
        {

        }

        public void Visit(IConfigurationDeclaration configurationDeclaration, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ITaskConfigurationDeclaration taskConfigurationDeclaration, IYamlBuiderVisitor data)
        {

        }

        public void Visit(ITaskDeclaration taskDeclaration, IYamlBuiderVisitor data)
        {

        }

        public void Visit(IProgramConfigurationDeclaration programConfigurationDeclaration, IYamlBuiderVisitor data)
        {

        }

        public void Visit(INamespaceDeclaration namespaceDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateNamespaceYaml(namespaceDeclaration, this);
        }

        public void Visit(IUsingDirective usingDirective, IYamlBuiderVisitor data)
        {

        }

        public void Visit(IProgramDeclaration programDeclaration, IYamlBuiderVisitor data)
        {
        }

        public void Visit(IClassDeclaration classDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateClassYaml(classDeclaration, this);
        }

        public void Visit(IInterfaceDeclaration interfaceDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateInterfaceYaml(interfaceDeclaration, this);
        }

        public void Visit(IFunctionDeclaration functionDeclaration, IYamlBuiderVisitor data)
        {
           data.CreateFunctionYaml(functionDeclaration, this);
        }

        public void Visit(IFunctionBlockDeclaration functionBlockDeclaration, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(IMethodDeclaration methodDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateMethodYaml(methodDeclaration, this);
        }

        public void Visit(IMethodPrototypeDeclaration methodPrototypeDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateMethodPrototypeYaml(methodPrototypeDeclaration, this);
        }

        public void Visit(IScalarTypeDeclaration scalarType, IYamlBuiderVisitor data)
        {
        }

        public void Visit(IStructuredTypeDeclaration structuredTypeDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateStructuredTypeYaml(structuredTypeDeclaration, this);
        }

        public void Visit(IArrayTypeDeclaration arrayTypeDeclaration, IYamlBuiderVisitor data)
        {
        }

        public void Visit(IEnumTypeDeclaration enumTypeDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateEnumTypeYaml(enumTypeDeclaration, this);
        }

        public void Visit(INamedValueTypeDeclaration namedValueTypeDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateNamedValueTypeYaml(namedValueTypeDeclaration, this);
        }

        public void Visit(IReferenceTypeDeclaration referenceTypeDeclaration, IYamlBuiderVisitor data)
        {
        }

        public void Visit(IStringTypeDeclaration stringTypeDeclaration, IYamlBuiderVisitor data)
        {
        }

        public void Visit(IDimension dimension, IYamlBuiderVisitor data)
        {
        }

        public void Visit(IFieldDeclaration fieldDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateFieldYaml(fieldDeclaration, this);
        }

        public void Visit(IVariableDeclaration variableDeclaration, IYamlBuiderVisitor data)
        {
        }

        public void Visit(IEnumValueDeclaration enumValueDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateEnumValueYaml(enumValueDeclaration, this);
        }

        public void Visit(INamedValueDeclaration namedValueDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateNamedValueYaml(namedValueDeclaration, this);
        }

        public void Visit(ISemanticInitializerExpression initializerExpression, IYamlBuiderVisitor data)
        {
        }

        public void Visit(ISemanticArrayInitializer arrayInitializer, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticStructureInitializer structureInitializer, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticMemberInitializer memberInitializer, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticTypeAccess semanticTypeAccess, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(IDocComment semanticTypeAccess, IYamlBuiderVisitor data)
        {
            throw new NotImplementedException();
        }

        public void Visit(ISemanticInstructionList instrList, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticAssignmentInstruction assignment, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticUnsafeAssignmentInstruction assignment, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticAssignmentAttemptInstruction assignmentAttempt, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticExpressionInstruction expression, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticIfConditionalStatement condStatement, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(IConditionalInstructionList condInstrList, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticCaseStatement caseStatement, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticCaseSelection caseSelection, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticSubrange subrange, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticForStatement forStatement, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticWhileStatement whileStatement, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticRepeatStatement repeatStatement, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticContinueInstruction continueInstruction, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticExitInstruction exitInstruction, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticReturnStatement returnStatement, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticAsmStatement asmStatement, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticConstantExpression constExpr, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticIdentifierAccess identifierAccess, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticBinaryExpression binExpr, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticUnaryExpression unaryExpression, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticQualifiedEnumAccess qualifiedEnumAccess, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticMemberAccessExpression memberAccessExpression, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticArrayAccessExpression arrayAccessExpression, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticCallExpression call, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticParameterList paramList, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(ISemanticParameterAssignment paramAssignment, IYamlBuiderVisitor data)
        {
            
        }

        public void Visit(IPartialAccessExpression partialAccessExpression, IYamlBuiderVisitor data)
        {
            
        }

    }

}
