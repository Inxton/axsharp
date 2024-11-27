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
        public virtual void CreateFile(IFileSyntax fileSyntax, MyNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateNamespaceYaml(
          INamespaceDeclaration namespaceDeclaration,
          MyNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateClassYaml(
          IClassDeclaration classDeclaration,
          MyNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateFieldYaml(
          IFieldDeclaration fieldDeclaration,
          MyNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateMethodYaml(
         IMethodDeclaration methodDeclaration,
         MyNodeVisitor visitor)
            {
            throw new NotImplementedException();
        }

        public virtual void CreateNamedValueTypeYaml(
          INamedValueTypeDeclaration namedValueTypeDeclaration,
          MyNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateEnumTypeYaml(
            IEnumTypeDeclaration enumTypeDeclaration, 
            MyNodeVisitor myNodeVisitor)
        {
            throw new NotImplementedException();
        }

        void CreateNamedValueYaml(
            INamedValueDeclaration namedValueDeclaration, 
            MyNodeVisitor myNodeVisitor)
        {
            throw new NotImplementedException();
        }

        void CreateEnumValueYaml(
            IEnumValueDeclaration enumValueDeclaration, 
            MyNodeVisitor myNodeVisitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateInterfaceYaml(
          IInterfaceDeclaration InterfaceDeclaration,
          MyNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateMethodPrototypeYaml(
          IMethodPrototypeDeclaration methodPrototypeDeclaration,
          MyNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public virtual void CreateFunctionYaml(
          IFunctionDeclaration functionDeclaration,
          MyNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }

        void CreateStructuredTypeYaml(
            IStructuredTypeDeclaration structuredTypeDeclaration, 
            MyNodeVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
