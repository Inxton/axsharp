// AXSharp.Compiler.Cs
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AX.ST.Semantic;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AXSharp.Compiler.Core;
using AXSharp.Compiler.Cs.Helpers;
using AXSharp.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.Compiler.Cs.Onliner
{
    internal class CsOnlinerPlainerShadowToPlainBuilder : ICombinedThreeVisitor
    {
        private readonly StringBuilder _memberDeclarations = new();

        protected CsOnlinerPlainerShadowToPlainBuilder(ISourceBuilder sourceBuilder)
        {
            SourceBuilder = sourceBuilder;
        }

        private ISourceBuilder SourceBuilder { get; }

        public string Output => _memberDeclarations.ToString().FormatCode();


        public void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
        {
            var eligibility = fieldDeclaration.IsMemberEligibleForTranspile(SourceBuilder, "POCO");
            if (eligibility.isEligible)
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
            var eligibility = variableDeclaration.IsMemberEligibleForTranspile(SourceBuilder, "POCO");
            if (eligibility.isEligibe)
            {
                CreateAssignment(variableDeclaration.Type, variableDeclaration);
            }
        }

        internal void CreateAssignment(ITypeDeclaration typeDeclaration, IDeclaration declaration)
        {
            switch (typeDeclaration)
            {
                case IInterfaceDeclaration interfaceDeclaration:
                    break;
                case IClassDeclaration classDeclaration:
                //case IAnonymousTypeDeclaration anonymousTypeDeclaration:
                case IStructuredTypeDeclaration structuredTypeDeclaration:
                    AddToSource($" plain.{declaration.Name} = await {declaration.Name}.{MethodName}Async();");
                    break;
                case IArrayTypeDeclaration arrayTypeDeclaration:
                    var arrayEligibility = arrayTypeDeclaration.IsMemberEligibleForConstructor(SourceBuilder);
                    if (arrayEligibility.isEligibe)
                    {
                        switch (arrayTypeDeclaration.ElementTypeAccess.Type)
                        {
                            case IClassDeclaration classDeclaration:
                            case IStructuredTypeDeclaration structuredTypeDeclaration:
                                AddToSource(
                                    $"plain.{declaration.Name} = {declaration.Name}.Select(async p => await p.{MethodName}Async()).Select(p => p.Result).ToArray();");
                                break;
                            case IScalarTypeDeclaration scalarTypeDeclaration:
                            case IStringTypeDeclaration stringTypeDeclaration:

                                AddToSource(
                                    $"plain.{declaration.Name} = {declaration.Name}.Select(p => p.Shadow).ToArray();");
                                break;
                        }
                    }

                    break;
                case IReferenceTypeDeclaration referenceTypeDeclaration:
                    break;
                case IEnumTypeDeclaration enumTypeDeclaration:
                    AddToSource($" plain.{declaration.Name} = ({declaration.Type.FullyQualifiedName}){declaration.Name}.Shadow;");
                    break;
                case INamedValueTypeDeclaration namedValueTypeDeclaration:
                    AddToSource($" plain.{declaration.Name} = {declaration.Name}.Shadow;");
                    break;
                case IScalarTypeDeclaration scalarTypeDeclaration:
                case IStringTypeDeclaration stringTypeDeclaration:
                    AddToSource($" plain.{declaration.Name} = {declaration.Name}.Shadow;");
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

        protected static readonly string MethodName = TwinObjectExtensions.ShadowToPlainMethodName;

        public static CsOnlinerPlainerShadowToPlainBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
            ISourceBuilder sourceBuilder)
        {
            var builder = new CsOnlinerPlainerShadowToPlainBuilder(sourceBuilder);

            builder.AddToSource(CsHelpers.CreateGenericSwapperMethodToPlainer(MethodName, $"{semantics.GetFullyQualifiedPocoName()}", false));

            builder.AddToSource($"public async Task<{semantics.GetFullyQualifiedPocoName()}> {MethodName}Async(){{\n");
            builder.AddToSource($"{semantics.GetFullyQualifiedPocoName()} plain = new {semantics.GetFullyQualifiedPocoName()}();");

            semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

            builder.AddToSource($"return plain;");
            builder.AddToSource($"}}");
            return builder;
        }

        public static CsOnlinerPlainerShadowToPlainBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
            ISourceBuilder sourceBuilder, bool isExtended)
        {
            var builder = new CsOnlinerPlainerShadowToPlainBuilder(sourceBuilder);

            builder.AddToSource(CsHelpers.CreateGenericSwapperMethodToPlainer(MethodName, $"{semantics.GetFullyQualifiedPocoName()}", isExtended));

            var qualifier = isExtended ? "new" : string.Empty;

            builder.AddToSource($"public {qualifier} async Task<{semantics.GetFullyQualifiedPocoName()}> {MethodName}Async(){{\n");
            builder.AddToSource($"{semantics.GetFullyQualifiedPocoName()} plain = new {semantics.GetFullyQualifiedPocoName()}();");

            if (isExtended)
            {
                builder.AddToSource($"await base.{MethodName}Async(plain);");
            }

            semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
            builder.AddToSource($"return plain;");
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
