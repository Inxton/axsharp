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
using AXSharp.Compiler.Core;
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
    public partial class DocNodeVisitor : ISemanticNodeVisitor<Unit, IYamlBuiderVisitor>
    {
        public YamlSerializerHelper YamlHelper { get; set; }
        //public Compiler.AxProject axProject { get; set; }

        public DocNodeVisitor(Compiler.AxProject? axProject = null)
        {
            //this.axProject = axProject;
            YamlHelper = new YamlSerializerHelper();
        }

        public void MapYamlHelperToSchema()
        {
            YamlHelper.Schema.Items = YamlHelper.Items.ToList();
            YamlHelper.Schema.References = YamlHelper.References.ToArray();
        }

        public Unit Visit(IPartialSemanticTree partialSemanticTree, IYamlBuiderVisitor data)
        {
            partialSemanticTree.ChildNodes.Where(p => p is INamespaceDeclaration).ToList().ForEach(p => p.Accept(this, data));
            return default;
        }

        public Unit Visit(ISymbol symbol, IYamlBuiderVisitor data)
        {
         
                    return default;
        }

        public Unit Visit(IPragma pragma, IYamlBuiderVisitor data)
        {

                    return default;
        }

        public Unit Visit(IConfigurationDeclaration configurationDeclaration, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ITaskConfigurationDeclaration taskConfigurationDeclaration, IYamlBuiderVisitor data)
        {

                    return default;
        }

        public Unit Visit(ITaskDeclaration taskDeclaration, IYamlBuiderVisitor data)
        {

                    return default;
        }

        public Unit Visit(IProgramConfigurationDeclaration programConfigurationDeclaration, IYamlBuiderVisitor data)
        {

                    return default;
        }

        public Unit Visit(INamespaceDeclaration namespaceDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateNamespaceYaml(namespaceDeclaration, this);
            return default;
        }

        public Unit Visit(IUsingDirective usingDirective, IYamlBuiderVisitor data)
        {

                    return default;
        }

        public Unit Visit(IProgramDeclaration programDeclaration, IYamlBuiderVisitor data)
        {
                    return default;
        }

        public Unit Visit(IClassDeclaration classDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateClassYaml(classDeclaration, this);
            return default;
        }

        public Unit Visit(IInterfaceDeclaration interfaceDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateInterfaceYaml(interfaceDeclaration, this);
            return default;
        }

        public Unit Visit(IFunctionDeclaration functionDeclaration, IYamlBuiderVisitor data)
        {
           data.CreateFunctionYaml(functionDeclaration, this);
            return default;
        }

        public Unit Visit(IFunctionBlockDeclaration functionBlockDeclaration, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(IMethodDeclaration methodDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateMethodYaml(methodDeclaration, this);
            return default;
        }

        public Unit Visit(IClassMethodDeclaration methodDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateMethodYaml(methodDeclaration, this);
            return default;
        }

        public Unit Visit(IMethodPrototypeDeclaration methodPrototypeDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateMethodPrototypeYaml(methodPrototypeDeclaration, this);
            return default;
        }

        public Unit Visit(IScalarTypeDeclaration scalarType, IYamlBuiderVisitor data)
        {
                    return default;
        }

        public Unit Visit(IStructuredTypeDeclaration structuredTypeDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateStructuredTypeYaml(structuredTypeDeclaration, this);
            return default;
        }

        public Unit Visit(IArrayTypeDeclaration arrayTypeDeclaration, IYamlBuiderVisitor data)
        {
                    return default;
        }

        public Unit Visit(IEnumTypeDeclaration enumTypeDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateEnumTypeYaml(enumTypeDeclaration, this);
            return default;
        }

        public Unit Visit(INamedValueTypeDeclaration namedValueTypeDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateNamedValueTypeYaml(namedValueTypeDeclaration, this);
            return default;
        }

        public Unit Visit(IReferenceTypeDeclaration referenceTypeDeclaration, IYamlBuiderVisitor data)
        {
                    return default;
        }

        public Unit Visit(IStringTypeDeclaration stringTypeDeclaration, IYamlBuiderVisitor data)
        {
                    return default;
        }

        public Unit Visit(IDimension dimension, IYamlBuiderVisitor data)
        {
                    return default;
        }

        public Unit Visit(IFieldDeclaration fieldDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateFieldYaml(fieldDeclaration, this);
            return default;
        }

        public Unit Visit(IVariableDeclaration variableDeclaration, IYamlBuiderVisitor data)
        {
                    return default;
        }

        public Unit Visit(IEnumValueDeclaration enumValueDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateEnumValueYaml(enumValueDeclaration, this);
            return default;
        }

        public Unit Visit(INamedValueDeclaration namedValueDeclaration, IYamlBuiderVisitor data)
        {
            data.CreateNamedValueYaml(namedValueDeclaration, this);
            return default;
        }

        public Unit Visit(ISemanticInitializerExpression initializerExpression, IYamlBuiderVisitor data)
        {
                    return default;
        }

        public Unit Visit(ISemanticArrayInitializer arrayInitializer, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticStructureInitializer structureInitializer, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticMemberInitializer memberInitializer, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticTypeAccess semanticTypeAccess, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(IDocComment semanticTypeAccess, IYamlBuiderVisitor data)
        {
            // throw new NotImplementedException();
            return default;
        
            return default;
        }

        public Unit Visit(ISemanticInstructionList instrList, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticEmptyInstruction emptyInstruction, IYamlBuiderVisitor data)
        {
           // throw new NotImplementedException();
            return default;
        
            return default;
        }

        public Unit Visit(ISemanticAssignmentInstruction assignment, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticUnsafeAssignmentInstruction assignment, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticAssignmentAttemptInstruction assignmentAttempt, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticExpressionInstruction expression, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticIfConditionalStatement condStatement, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(IConditionalInstructionList condInstrList, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticCaseStatement caseStatement, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticCaseSelectionStatement caseSelectionStatement, IYamlBuiderVisitor data)
        {
            throw new NotImplementedException();
        }

        //public Unit Visit(ISemanticCaseSelection caseSelection, IYamlBuiderVisitor data)
        //{
            
        //}

        public Unit Visit(ISemanticSubrange subrange, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticForStatement forStatement, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticWhileStatement whileStatement, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticRepeatStatement repeatStatement, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticContinueInstruction continueInstruction, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticExitInstruction exitInstruction, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticReturnStatement returnStatement, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticAsmStatement asmStatement, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticConstantExpression constExpr, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticIdentifierAccess identifierAccess, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticBinaryExpression binExpr, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticUnaryExpression unaryExpression, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticQualifiedEnumAccess qualifiedEnumAccess, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticMemberAccessExpression memberAccessExpression, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticArrayAccessExpression arrayAccessExpression, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticCallExpression call, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticParameterList paramList, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(ISemanticParameterAssignment paramAssignment, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

        public Unit Visit(IPartialAccessExpression partialAccessExpression, IYamlBuiderVisitor data)
        {
            
                    return default;
        }

    }

}
