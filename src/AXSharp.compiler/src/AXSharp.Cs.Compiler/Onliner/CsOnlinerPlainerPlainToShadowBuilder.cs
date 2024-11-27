// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Model.Declarations;
using AXSharp.Compiler.Cs.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AX.ST.Semantic;
using AXSharp.Compiler.Core;
using AXSharp.Connector;

namespace AXSharp.Compiler.Cs.Onliner
{
    internal class CsOnlinerPlainerPlainToShadowBuilder : ICombinedThreeVisitor
    {
        private readonly StringBuilder _memberDeclarations = new();

        protected CsOnlinerPlainerPlainToShadowBuilder(ISourceBuilder sourceBuilder)
        {
            SourceBuilder = sourceBuilder;
        }

        private ISourceBuilder SourceBuilder { get; }

        public string Output => _memberDeclarations.ToString().FormatCode();


        public void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
        {
            if (fieldDeclaration.IsMemberEligibleForTranspile(SourceBuilder,"POCO"))
            {
                CreateAssignment(fieldDeclaration.Type, fieldDeclaration);
            }
        }

        public void CreateInterfaceDeclaration(IInterfaceDeclaration interfaceDeclaration, IxNodeVisitor visitor)
        {
            //
        }

        public void CreateVariableDeclaration(IVariableDeclaration variableDeclaration, IxNodeVisitor visitor)
        {
            if (variableDeclaration.IsMemberEligibleForTranspile(SourceBuilder, "POCO"))
            {
                CreateAssignment(variableDeclaration.Type, variableDeclaration);
            }
        }

        private void CreateAssignment(ITypeDeclaration typeDeclaration, IDeclaration declaration)
        {
            switch (typeDeclaration)
            {
                case IInterfaceDeclaration interfaceDeclaration:
                    break;
                case IClassDeclaration classDeclaration:
                //case IAnonymousTypeDeclaration anonymousTypeDeclaration:
                case IStructuredTypeDeclaration structuredTypeDeclaration:
                    AddToSource($" await this.{declaration.Name}.{MethodName}Async(plain.{declaration.Name});");
                    break;
                case IArrayTypeDeclaration arrayTypeDeclaration:

                    if (arrayTypeDeclaration.IsMemberEligibleForConstructor(SourceBuilder))
                    {
                        switch (arrayTypeDeclaration.ElementTypeAccess.Type)
                        {
                            case IClassDeclaration classDeclaration:
                            case IStructuredTypeDeclaration structuredTypeDeclaration:
                                AddToSource($"var _{declaration.Name}_i_FE8484DAB3 = 0;");
                                AddToSource(
                                    $"{declaration.Name}.Select(p => p.{MethodName}Async(plain.{declaration.Name}[_{declaration.Name}_i_FE8484DAB3++])).ToArray();");
                                break;
                            case IScalarTypeDeclaration scalarTypeDeclaration:
                            case IStringTypeDeclaration stringTypeDeclaration:
                                AddToSource($"var _{declaration.Name}_i_FE8484DAB3 = 0;");
                                AddToSource(
                                    $"{declaration.Name}.Select(p => p.Shadow = plain.{declaration.Name}[_{declaration.Name}_i_FE8484DAB3++]).ToArray();");
                                break;
                        }
                    }

                    break;
                case IReferenceTypeDeclaration referenceTypeDeclaration:
                    break;
                case IEnumTypeDeclaration enumTypeDeclaration:
                    AddToSource($" {declaration.Name}.Shadow = (short)plain.{declaration.Name};");
                    break;
                case INamedValueTypeDeclaration namedValueTypeDeclaration:
                    AddToSource($" {declaration.Name}.Shadow = plain.{declaration.Name};");
                    break;
                case IScalarTypeDeclaration scalarTypeDeclaration:
                case IStringTypeDeclaration stringTypeDeclaration:
                    AddToSource($" {declaration.Name}.Shadow = plain.{declaration.Name};");
                    break;
            }
        }

        public void CreateArrayTypeDeclaration(IArrayTypeDeclaration arrayTypeDeclaration, IxNodeVisitor visitor)
        {
            //
        }


        protected void AddToSource(string token, string separator = " ")
        {
            _memberDeclarations.Append($"{token}{separator}");
        }

        public void AddTypeConstructionParameters(string parametersString)
        {
            AddToSource(parametersString);
        }

        private static readonly string MethodName = TwinObjectExtensions.PlainToShadowMethodName;

        public static CsOnlinerPlainerPlainToShadowBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
            ISourceBuilder sourceBuilder)
        {
            var builder = new CsOnlinerPlainerPlainToShadowBuilder(sourceBuilder);

            builder.AddToSource(CsHelpers.CreateGenericSwapperMethodFromPlainer(MethodName, $"Pocos.{semantics.FullyQualifiedName}", false));

            builder.AddToSource($"public async Task<IEnumerable<ITwinPrimitive>> {MethodName}Async(Pocos.{semantics.FullyQualifiedName} plain){{\n");

            semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

            builder.AddToSource("return this.RetrievePrimitives();");
            builder.AddToSource($"}}");
            return builder;
        }

        public static CsOnlinerPlainerPlainToShadowBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
            ISourceBuilder sourceBuilder, bool isExtended)
        {
            var builder = new CsOnlinerPlainerPlainToShadowBuilder(sourceBuilder);

            builder.AddToSource(CsHelpers.CreateGenericSwapperMethodFromPlainer(MethodName, $"Pocos.{semantics.FullyQualifiedName}", isExtended));

            //var qualifier = isExtended ? "new" : string.Empty;
            var qualifier = string.Empty;
            builder.AddToSource($"public {qualifier} async Task<IEnumerable<ITwinPrimitive>> {MethodName}Async(Pocos.{semantics.FullyQualifiedName} plain){{\n");


            if (isExtended)
            {
                builder.AddToSource($"await base.{MethodName}Async(plain);");
            }

            semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
            builder.AddToSource("return this.RetrievePrimitives();");

            builder.AddToSource($"}}");
            return builder;
        }

        /// <inheritdoc />
        public void CreateDocComment(IDocComment semanticTypeAccess, ICombinedThreeVisitor data)
        {
            AddToSource(semanticTypeAccess.AddDocumentationComment(SourceBuilder));
        }
    }


}
