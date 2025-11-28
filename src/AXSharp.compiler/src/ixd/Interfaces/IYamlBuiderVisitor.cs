using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Pragmas;
using AX.ST.Syntax.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.ixc_doc.Visitors;

namespace AXSharp.ixc_doc.Interfaces
{
    public interface IYamlBuiderVisitor
    {
        /// <summary>
        ///     Creates file declaration from <see cref="IFileSyntax" /> node of given syntax tree.
        /// </summary>
        /// <param name="fileSyntax">File syntax node.</param>
        /// <param name="visitor">Associated visitor.</param>
        public virtual void CreateFile(IFileSyntax fileSyntax, DocNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateNamespaceYaml(
          INamespaceDeclaration namespaceDeclaration,
          DocNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateClassYaml(
          IClassDeclaration classDeclaration,
          DocNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateFieldYaml(
          IFieldDeclaration fieldDeclaration,
          DocNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateMethodYaml(
         IMethodDeclaration methodDeclaration,
         DocNodeVisitor visitor)
            {
            throw new NotImplementedException();
        }

        public virtual void CreateNamedValueTypeYaml(
          INamedValueTypeDeclaration namedValueTypeDeclaration,
          DocNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateEnumTypeYaml(
            IEnumTypeDeclaration enumTypeDeclaration, 
            DocNodeVisitor myNodeVisitor)
        {
            throw new NotImplementedException();
        }

        void CreateNamedValueYaml(
            INamedValueDeclaration namedValueDeclaration, 
            DocNodeVisitor myNodeVisitor)
        {
            throw new NotImplementedException();
        }

        void CreateEnumValueYaml(
            IEnumValueDeclaration enumValueDeclaration, 
            DocNodeVisitor myNodeVisitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateInterfaceYaml(
          IInterfaceDeclaration InterfaceDeclaration,
          DocNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateMethodPrototypeYaml(
          IMethodPrototypeDeclaration methodPrototypeDeclaration,
          DocNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateFunctionYaml(
          IFunctionDeclaration functionDeclaration,
          DocNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        void CreateStructuredTypeYaml(
            IStructuredTypeDeclaration structuredTypeDeclaration, 
            DocNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
