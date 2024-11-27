using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Syntax.Tree;
using CommandLine;
using AXSharp.ixc_doc.Helpers;
using AXSharp.ixc_doc.Interfaces;
using AXSharp.ixc_doc.Mapper;
using AXSharp.ixc_doc.Schemas;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NuGet.Packaging;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using AXSharp.ixc_doc.Models;

namespace AXSharp.ixc_doc.Visitors
{
    public class YamlBuilder : IYamlBuiderVisitor
    {
        private YamlSerializer _s { get; set; }
        private YamlHelpers _yh { get; set; }
        private CodeToYamlMapper _mp { get; set; }
        private List<NamespaceWrapper> NamespaceWrappers {get; set; } = new List<NamespaceWrapper>();
        internal YamlBuilder(YamlSerializer serializer)
        {
            _yh = new YamlHelpers();
            _mp = new CodeToYamlMapper(_yh);
            _s = serializer;
        }

        public virtual void CreateNamespaceYaml(INamespaceDeclaration namespaceDeclaration, MyNodeVisitor v)
        {
            // create toc item
            var tocSchemaItem = new TocSchema.Item(namespaceDeclaration, namespaceDeclaration.Name);
           
            var hasTypes = namespaceDeclaration.Declarations.Any(p => p is IClassDeclaration || p is IStructuredTypeDeclaration || p is IInterfaceDeclaration );

            // add to namespace group if is not global
            if (namespaceDeclaration.FullyQualifiedName != "$GLOBAL" && hasTypes)
                if (_yh.FindTocGroup(v.YamlHelper.TocSchema.Items, Helpers.Helpers.GetBaseUid(namespaceDeclaration)) == null)
                    _yh.AddToTocSchema(v, tocSchemaItem, Helpers.Helpers.GetBaseUid(namespaceDeclaration));

            // iterate through children
            namespaceDeclaration.Declarations.ToList().ForEach(p =>{p.Accept(v, this);});


            // if is not global namespace and contains types
            if (namespaceDeclaration.FullyQualifiedName != "$GLOBAL" && hasTypes)
            {
                
                var uid = Helpers.Helpers.GetBaseUid(namespaceDeclaration);
                //check whether namespace has occured previously
                var wrapper = NamespaceWrappers.FirstOrDefault(p=> p?.NamespaceItem.Id == uid);

                if(wrapper != null)
                {
                    //if yes, acquire temporay children and add them to the namespace wrapper
                    wrapper.NamespaceTemporaryChildren.Clear();
                    UpdateNamespaceChildrenAndReferences(namespaceDeclaration, wrapper);
                }
                else
                { 
                    //we have new namespace, create new namespace wrapper
                    wrapper = new NamespaceWrapper(_mp.PopulateItem(namespaceDeclaration));
                    UpdateNamespaceChildrenAndReferences(namespaceDeclaration, wrapper);
                    NamespaceWrappers.Add(wrapper);
                }


                v.YamlHelper.Schema.Items.Add(wrapper.NamespaceItem);
                v.YamlHelper.Schema.References = wrapper.NamespaceReferences.ToArray();
                _s.SchemaToYaml(v.YamlHelper.Schema, Helpers.Helpers.GetBaseUid(namespaceDeclaration));
                v.YamlHelper.Schema = new YamlSchema();
                v.YamlHelper.NamespaceReferences.Clear();

            }
        }

        //operation on semantic tree
        public virtual void CreateClassYaml(IClassDeclaration classDeclaration, MyNodeVisitor v)
        {
            var item = _mp.PopulateItem(classDeclaration);
            
            //add to items
            v.YamlHelper.Items.Add(item);
            _yh.AddReferences(item.InheritedMembers, v);
            _yh.AddReferences(item.Implements, v);

            var tocSchemaItem = new TocSchema.Item(item.Uid, item.FullName);

            if (item.Namespace != "$GLOBAL")
            {
                //class is grouped in namespace
                _yh.AddToTocSchema(v, tocSchemaItem, item.Namespace);
            }
            else
            {
                // if global, class is not grouped
                _yh.AddToTocSchema(v, tocSchemaItem, null);
            }

            classDeclaration.ChildNodes.ToList().ForEach(p => p.Accept(v, this));

            //map helpers list to schema lists
            v.MapYamlHelperToSchema();

            //serialize schema to yaml
            _s.SchemaToYaml(v.YamlHelper.Schema, Helpers.Helpers.GetBaseUid(classDeclaration));
            //clear schema for next use
            v.YamlHelper.Schema = new YamlSchema();
            v.YamlHelper.Items.Clear();
            v.YamlHelper.References.Clear();
        }

        //operation on semantic tree
        public virtual void CreateFieldYaml(IFieldDeclaration fieldDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(fieldDeclaration);
            visitor.YamlHelper.Items.Add(item);
            _yh.AddReference(fieldDeclaration.Type, visitor);
        }

        public virtual void CreateMethodYaml(IMethodDeclaration methodDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(methodDeclaration);
            visitor.YamlHelper.Items.Add(item);

            var returnType = methodDeclaration.Variables.Where(v => v.Section == Section.Return).FirstOrDefault();
            if (returnType != null)
                _yh.AddReference(returnType.Type, visitor);

            var inputVars = methodDeclaration.Variables.Where(v => v.Section == Section.Input);
            if (inputVars != null)
            foreach (var inputVar in inputVars)
            {
                _yh.AddReference(inputVar.Type, visitor);
            }
        }

        public virtual void CreateNamedValueTypeYaml(INamedValueTypeDeclaration namedValueTypeDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(namedValueTypeDeclaration);
            visitor.YamlHelper.Items.Add(item);

            var tocSchemaItem = new TocSchema.Item(item.Uid, item.FullName);

            _yh.AddToTocSchema(visitor, tocSchemaItem, item.Namespace);

            namedValueTypeDeclaration.ChildNodes.ToList().ForEach(p => p.Accept(visitor, this));

            //map helpers list to schema lists
            visitor.MapYamlHelperToSchema();

            //serialize schema to yaml
            _s.SchemaToYaml(visitor.YamlHelper.Schema, Helpers.Helpers.GetBaseUid(namedValueTypeDeclaration));
            //clear schema for next use
            visitor.YamlHelper.Schema = new YamlSchema();
            visitor.YamlHelper.Items.Clear();
            visitor.YamlHelper.References.Clear();
        }

        public virtual void CreateEnumTypeYaml(IEnumTypeDeclaration enumTypeDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(enumTypeDeclaration);
            visitor.YamlHelper.Items.Add(item);

            var tocSchemaItem = new TocSchema.Item(item.Uid, item.FullName);

            _yh.AddToTocSchema(visitor, tocSchemaItem, item.Namespace);

            enumTypeDeclaration.ChildNodes.ToList().ForEach(p => p.Accept(visitor, this));

            //map helpers list to schema lists
            visitor.MapYamlHelperToSchema();

            //serialize schema to yaml
            _s.SchemaToYaml(visitor.YamlHelper.Schema, Helpers.Helpers.GetBaseUid(enumTypeDeclaration));
            //clear schema for next use
            visitor.YamlHelper.Schema = new YamlSchema();
            visitor.YamlHelper.Items.Clear();
            visitor.YamlHelper.References.Clear();
        }

        public virtual void CreateNamedValueYaml(INamedValueDeclaration namedValueDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(namedValueDeclaration);
            visitor.YamlHelper.Items.Add(item);
            _yh.AddReference(namedValueDeclaration.Type, visitor);
        }

        public virtual void CreateEnumValueYaml(IEnumValueDeclaration enumValueDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(enumValueDeclaration);
            visitor.YamlHelper.Items.Add(item);
            _yh.AddReference(enumValueDeclaration.Type, visitor);
        }

        public virtual void CreateInterfaceYaml(IInterfaceDeclaration interfaceDeclaration, MyNodeVisitor v)
        {
            var item = _mp.PopulateItem(interfaceDeclaration);
            v.YamlHelper.Items.Add(item);

            var tocSchemaItem = new TocSchema.Item(item.Uid, item.FullName);

            if (item.Namespace != "$GLOBAL")
            {
                //interface is grouped in namespace
                _yh.AddToTocSchema(v, tocSchemaItem, item.Namespace);
            }
            else
            {
                // if global, interface is not grouped
                _yh.AddToTocSchema(v, tocSchemaItem, null);
            }

            interfaceDeclaration.ChildNodes.ToList().ForEach(p => p.Accept(v, this));

            //map helpers list to schema lists
            v.MapYamlHelperToSchema();

            //serialize schema to yaml
            _s.SchemaToYaml(v.YamlHelper.Schema, Helpers.Helpers.GetBaseUid(interfaceDeclaration));
            //clear schema for next use
            v.YamlHelper.Schema = new YamlSchema();
            v.YamlHelper.Items.Clear();
            v.YamlHelper.References.Clear();
        }

        public virtual void CreateMethodPrototypeYaml(IMethodPrototypeDeclaration methodPrototypeDeclaration, MyNodeVisitor visitor)
        {
            var item = _mp.PopulateItem(methodPrototypeDeclaration);
            visitor.YamlHelper.Items.Add(item);

            var returnType = methodPrototypeDeclaration.Variables.Where(v => v.Section == Section.Return).FirstOrDefault();
            if (returnType != null)
                _yh.AddReference(returnType.Type, visitor);

            //add references for return type and for input types
            var inputVars = methodPrototypeDeclaration.Variables.Where(v => v.Section == Section.Input);
            if (inputVars != null)
            foreach (var inputVar in inputVars)
            {
                _yh.AddReference(inputVar.Type, visitor);
            }
        }

        public virtual void CreateFunctionYaml(IFunctionDeclaration functionDeclaration, MyNodeVisitor v)
        {
            var item = _mp.PopulateItem(functionDeclaration);
            v.YamlHelper.Items.Add(item);
            var tocSchemaItem = new TocSchema.Item(item.Uid, item.FullName);

            if (item.Namespace != "$GLOBAL")
            {
                //class is grouped in namespace
                _yh.AddToTocSchema(v, tocSchemaItem, item.Namespace);
            }
            else
            {
                // if global, class is not grouped
                _yh.AddToTocSchema(v, tocSchemaItem, null);
            }


            //add references for return type and for input types
            var returnType = functionDeclaration.Variables.Where(v => v.Section == Section.Return).FirstOrDefault();
            if (returnType != null)
                _yh.AddReference(returnType.Type, v);

            var inputVars = functionDeclaration.Variables.Where(v => v.Section == Section.Input);
            if (inputVars != null)
            foreach (var inputVar in inputVars)
            {
                _yh.AddReference(inputVar.Type, v);
            }

            //map helpers list to schema lists
            v.MapYamlHelperToSchema();

            //serialize schema to yaml
            _s.SchemaToYaml(v.YamlHelper.Schema, item.Uid);
            //clear schema for next use
            v.YamlHelper.Schema = new YamlSchema();
            v.YamlHelper.Items.Clear();
            v.YamlHelper.References.Clear();
        }


        private void UpdateNamespaceChildrenAndReferences(INamespaceDeclaration namespaceDeclaration, NamespaceWrapper wrapper)
        { 
            foreach (var declaration in namespaceDeclaration.Declarations)
            {
                if(declaration is IFunctionDeclaration functionDeclaration)
                {
                    wrapper.NamespaceTemporaryChildren.Add(Helpers.Helpers.GetBaseUid(functionDeclaration));
                      wrapper.NamespaceReferences.Add(_yh.CreateNamespaceReference(functionDeclaration));
                }
                else
                { 
                    wrapper.NamespaceTemporaryChildren.Add(Helpers.Helpers.GetBaseUid(declaration));
                    wrapper.NamespaceReferences.Add(_yh.CreateNamespaceReference(declaration));
                }
              
            }
            //add new children to the namespaceitem
            wrapper.NamespaceItem.Children.AddRange(wrapper.NamespaceTemporaryChildren);
        }

        public virtual void CreateStructuredTypeYaml(IStructuredTypeDeclaration structuredTypeDeclaration, MyNodeVisitor v)
        {
            var item = _mp.PopulateItem(structuredTypeDeclaration);

            //add to items
            v.YamlHelper.Items.Add(item);

            var tocSchemaItem = new TocSchema.Item(item.Uid, item.FullName);

            if (item.Namespace != "$GLOBAL")
            {
                //class is grouped in namespace
                _yh.AddToTocSchema(v, tocSchemaItem, item.Namespace);
            }
            else
            {
                // if global, class is not grouped
                _yh.AddToTocSchema(v, tocSchemaItem, null);
            }

            structuredTypeDeclaration.ChildNodes.ToList().ForEach(p => p.Accept(v, this));

            //map helpers list to schema lists
            v.MapYamlHelperToSchema();

            //serialize schema to yaml
            _s.SchemaToYaml(v.YamlHelper.Schema, Helpers.Helpers.GetBaseUid(structuredTypeDeclaration));
            //clear schema for next use
            v.YamlHelper.Schema = new YamlSchema();
            v.YamlHelper.Items.Clear();
            v.YamlHelper.References.Clear();
        }


    }
}
