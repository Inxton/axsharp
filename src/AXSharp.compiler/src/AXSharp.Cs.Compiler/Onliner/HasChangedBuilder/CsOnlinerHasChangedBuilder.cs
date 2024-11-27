// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

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
    internal class CsOnlinerHasChangedBuilder : ICombinedThreeVisitor
    {
        private readonly StringBuilder _memberDeclarations = new();

        protected CsOnlinerHasChangedBuilder(ISourceBuilder sourceBuilder)
        {
            SourceBuilder = sourceBuilder;
        }

        private ISourceBuilder SourceBuilder { get; }

        public string Output => _memberDeclarations.ToString().FormatCode();


        public void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
        {
            if (fieldDeclaration.IsMemberEligibleForTranspile(SourceBuilder, "POCO"))
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

        internal void CreateAssignment(ITypeDeclaration typeDeclaration, IDeclaration declaration)
        {
            var index_var_name = "i760901_3001_mimi";
            switch (typeDeclaration)
            {
                case IInterfaceDeclaration interfaceDeclaration:
                    break;
                case IClassDeclaration classDeclaration:
                //case IAnonymousTypeDeclaration anonymousTypeDeclaration:
                case IStructuredTypeDeclaration structuredTypeDeclaration:
                    AddToSource($" if(await {declaration.Name}.{MethodName}(plain.{declaration.Name}, latest.{declaration.Name})) somethingChanged = true;");
                    break;
                case IArrayTypeDeclaration arrayTypeDeclaration:
                    if (arrayTypeDeclaration.IsMemberEligibleForConstructor(SourceBuilder))
                    {
                        switch (arrayTypeDeclaration.ElementTypeAccess.Type)
                        {
                            case IClassDeclaration classDeclaration:
                            case IStructuredTypeDeclaration structuredTypeDeclaration:
                                AddToSource(
                                    $"for (int {index_var_name} = 0; {index_var_name} < latest.{declaration.Name}.Length; {index_var_name}++)\r\n{{\r\n    if (await {declaration.Name}.ElementAt({index_var_name}).DetectsAnyChangeAsync(plain.{declaration.Name}[{index_var_name}], latest.{declaration.Name}[{index_var_name}]))\r\n        somethingChanged = true;\r\n}}");
                                break;
                            case IScalarTypeDeclaration scalarTypeDeclaration:
                            case IStringTypeDeclaration stringTypeDeclaration:
                                AddToSource(
                                    $"for (int {index_var_name} = 0; {index_var_name} < latest.{declaration.Name}.Length; {index_var_name}++)\r\n{{\r\n    if (latest.{declaration.Name}.ElementAt({index_var_name}) != plain.{declaration.Name}[{index_var_name}])\r\n        somethingChanged = true;\r\n}}");
                                break;
                        }
                    }

                    break;
                case IReferenceTypeDeclaration referenceTypeDeclaration:
                    break;
                case IEnumTypeDeclaration enumTypeDeclaration:
                    AddToSource($" if(plain.{declaration.Name} != ({declaration.Type.FullyQualifiedName})latest.{declaration.Name}) somethingChanged = true;");
                    break;
                case INamedValueTypeDeclaration namedValueTypeDeclaration:
                    AddToSource($" if(plain.{declaration.Name} != {declaration.Name}.LastValue) somethingChanged = true;");
                    break;
                case IScalarTypeDeclaration scalarTypeDeclaration:
                case IStringTypeDeclaration stringTypeDeclaration:
                    AddToSource($" if(plain.{declaration.Name} != {declaration.Name}.LastValue) somethingChanged = true;");
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

        protected static readonly string MethodName = $"Detects{TwinObjectExtensions.HasChangedMethodName}";

        public static CsOnlinerHasChangedBuilder Create(IxNodeVisitor visitor, IStructuredTypeDeclaration semantics,
            ISourceBuilder sourceBuilder)
        {
            var builder = new CsOnlinerHasChangedBuilder(sourceBuilder);

            builder.AddToSource(CsHelpers.CreateGenericHasChangedMethodMethod(MethodName, $"Pocos.{semantics.FullyQualifiedName}"));

            builder.AddToSource("///<summary>\n");
            builder.AddToSource("///Compares if the current plain object has changed from the previous object.This method is used by the framework to determine if the object has changed and needs to be updated.\n");
            builder.AddToSource("///[!NOTE] Any member in the hierarchy that is ignored by the compilers (e.g. when CompilerOmitAttribute is used) will not be compared, and therefore will not be detected as changed.\n");
            builder.AddToSource("///</summary>\n");


            builder.AddToSource($"public async Task<bool> {MethodName}(Pocos.{semantics.FullyQualifiedName} plain, Pocos.{semantics.FullyQualifiedName} latest = null){{\n");
            builder.AddToSource("var somethingChanged = false;");
            builder.AddToSource("if(latest == null) latest = await this._OnlineToPlainNoacAsync();");
            builder.AddToSource("return await Task.Run(async () => {\n");
            semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));
            builder.AddToSource($"plain = latest;");
            builder.AddToSource($"return somethingChanged;");
            builder.AddToSource($"}});");
            builder.AddToSource($"}}");
            return builder;
        }

        public static CsOnlinerHasChangedBuilder Create(IxNodeVisitor visitor, IClassDeclaration semantics,
            ISourceBuilder sourceBuilder, bool isExtended)
        {
            var builder = new CsOnlinerHasChangedBuilder(sourceBuilder);

            builder.AddToSource(CsHelpers.CreateGenericHasChangedMethodMethod(MethodName, $"Pocos.{semantics.FullyQualifiedName}", isExtended));

            var qualifier = isExtended ? "new" : string.Empty;

            builder.AddToSource("///<summary>\n");
            builder.AddToSource("///Compares if the current plain object has changed from the previous object.This method is used by the framework to determine if the object has changed and needs to be updated.\n");
            builder.AddToSource("///[!NOTE] Any member in the hierarchy that is ignored by the compilers (e.g. when CompilerOmitAttribute is used) will not be compared, and therefore will not be detected as changed.\n");
            builder.AddToSource("///</summary>\n");
            builder.AddToSource($"public {qualifier} async Task<bool> {MethodName}(Pocos.{semantics.FullyQualifiedName} plain, Pocos.{semantics.FullyQualifiedName} latest = null){{\n");

            builder.AddToSource("if(latest == null) latest = await this._OnlineToPlainNoacAsync();");
            builder.AddToSource("var somethingChanged = false;");
            
            builder.AddToSource("return await Task.Run(async () => {\n");
            if (isExtended)
            {
                builder.AddToSource($"if(await base.{MethodName}(plain)) return true;");
            }

            semantics.Fields.ToList().ForEach(p => p.Accept(visitor, builder));

            builder.AddToSource($"plain = latest;");
            builder.AddToSource($"return somethingChanged;");
            builder.AddToSource($"}});");
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
