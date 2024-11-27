// AXSharp.CompilerTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Xunit;
using AXSharp.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AX.ST.Semantic;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AXSharp.Compiler.Core;
using Moq;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Pragmas;
using AX.ST.Syntax.Tree;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AXSharp.Compiler.Tests
{
    public class IxProjectTests
    {
        public IxProjectTests()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var executingAssemblyFileInfo
                = new FileInfo(Assembly.GetExecutingAssembly().FullName);
#pragma warning restore CS8604 // Possible null reference argument.

            testFolder = Path.GetFullPath(executingAssemblyFileInfo.Directory!.FullName);
        }

        private readonly string testFolder;

        [Fact()]
        public void should_create_instance_of_IxProjectTest()
        {
            var builder = typeof(MockBuilder);
            var target = typeof(MockTargetProject);
            
            var axproject = new AxProject(Path.Combine(testFolder, "samples", "plt", "app"));

            var actual = new AXSharpProject(axproject, 
                new[] { builder }, 
                target);

            Assert.NotNull(actual);
            
        }

        [Fact()]
        public void should_generate_output_of_ix_project()
        {
            var builder = typeof(MockBuilder);
            var target = typeof(MockTargetProject);

            var axproject = new AxProject(Path.Combine(testFolder, "samples", "plt","app"));

            var actual = new AXSharpProject(axproject,
                new[] { builder },
                target);

            actual.Generate();

            var x = Path.Combine(testFolder, "samples", "plt", "app", "ix", ".meta", "meta.json");
            Assert.True(File.Exists(x));
            Assert.True(File.Exists(Path.Combine(testFolder, "samples", "plt", "app", "ix",".g","Mock", "configuration.py")));
            Assert.True(File.Exists(Path.Combine(testFolder, "samples", "plt", "app", "ix", ".g", "Mock", "program.py")));


        }

        public class MockBuilder : ISourceBuilder, ICombinedThreeVisitor
        {
            public MockBuilder(AXSharpProject project, Compilation compilation)
            {
                Compilation = compilation;
            }

            public string Output => "MockGroup";
            public string Group => "Mock";
            public string OutputFileSuffix => ".py";
            public string BuilderType => "Mock";
            public Compilation Compilation { get; }
            public ICompilerOptions? CompilerOptions { get; }
            public eCommAccessibility TypeCommAccessibility { get; } = eCommAccessibility.ReadWrite;

            #region ICombineThreeVisitor
            /// <summary>
            ///     Creates file declaration from <see cref="IFileSyntax" /> node of given syntax tree.
            /// </summary>
            /// <param name="fileSyntax">File syntax node.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateFile(IFileSyntax fileSyntax, IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates namespace declaration from <see cref="INamespaceDeclarationSyntax" /> node of given syntax tree.
            /// </summary>
            /// <param name="namespaceDeclarationSyntax">Namespace declaration syntax node.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateNamespaceDeclaration(INamespaceDeclarationSyntax namespaceDeclarationSyntax,
                IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates class declaration starting in <see cref="ClassDeclarationSyntax" /> node and continues in respective
            ///     semantic node of <see cref="IClassDeclaration" />.
            /// </summary>
            /// <param name="classDeclarationSyntax">Class declaration syntax node.</param>
            /// <param name="classDeclaration">Class declaration semantic node.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateClassDeclaration(IClassDeclarationSyntax classDeclarationSyntax,
                IClassDeclaration classDeclaration,
                IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates class declaration from semantic node <see cref="IClassDeclaration" />.
            /// </summary>
            /// <param name="classDeclaration">Class declaration semantics.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateClassDeclaration(IClassDeclaration classDeclaration, IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates field (type member) declaration from semantic node <see cref="IFieldDeclaration" />.
            /// </summary>
            /// <param name="fieldDeclaration">Field declaration semantics.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateFieldDeclaration(IFieldDeclaration fieldDeclaration, IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates configuration declaration from starting from syntax node and proceeds in respective semantic node.
            /// </summary>
            /// <param name="configDeclarationSyntax">Configuration declaration syntax node.</param>
            /// <param name="configurationDeclaration">Configuration declaration semantics.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateConfigDeclaration(IConfigDeclarationSyntax configDeclarationSyntax,
                IConfigurationDeclaration configurationDeclaration,
                IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates configuration declaration from semantic node of <see cref="IConfigurationDeclaration" />
            /// </summary>
            /// <param name="configurationDeclaration">Configuration declaration semantic node.</param>
            /// <param name="data">Associated visitor.</param>
            public virtual void CreateConfigDeclaration(IConfigurationDeclaration configurationDeclaration, IxNodeVisitor data)
            {
                
            }

            /// <summary>
            ///     Creates pragma declaration.
            /// </summary>
            /// <param name="pragma">Pragma declaration semantics</param>
            /// <param name="visitor">Associated visitor</param>
            public virtual void CreatePragma(IPragma pragma, ICombinedThreeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Created enum declaration starting from syntax node and continues in respective semantic node.
            /// </summary>
            /// <param name="enumTypeDeclarationSyntax">Enum type declaration syntax node.</param>
            /// <param name="typeDeclaration">Enum type declaration semantic node.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateEnumTypeDeclaration(IEnumTypeDeclarationSyntax enumTypeDeclarationSyntax,
                ITypeDeclaration typeDeclaration,
                IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates enum type.
            /// </summary>
            /// <param name="enumTypeDeclaration">Enum type semantics.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateEnumTypeDeclaration(IEnumTypeDeclaration enumTypeDeclaration, IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates Named Value Type declaration.
            /// </summary>
            /// <param name="namedValueTypeDeclarationSyntax">Named value type declaration syntax node.</param>
            /// <param name="namedValueTypeDeclaration">Named value type declaration semantics.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateNamedValueTypeDeclaration(
                INamedValueTypeDeclarationSyntax namedValueTypeDeclarationSyntax,
                INamedValueTypeDeclaration namedValueTypeDeclaration, IxNodeVisitor visitor)
            {
                /*Ignored in poco*/
            }

            /// <summary>
            ///     Creates named value type declaration.
            /// </summary>
            /// <param name="namedValueTypeDeclaration">Named value type declaration semantic node.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateNamedValueTypeDeclaration(INamedValueTypeDeclaration namedValueTypeDeclaration,
                IxNodeVisitor visitor)
            {
                /*Ignored in poco*/
            }

            /// <summary>
            ///     Creates using directive.
            /// </summary>
            /// <param name="usingDirectiveSyntax">Using directive syntax node.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateUsingDirective(IUsingDirectiveSyntax usingDirectiveSyntax, ICombinedThreeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates implements list.
            /// </summary>
            /// <param name="implementsListSyntax">Implements syntax node.</param>
            /// <param name="visitor">Associated visitor.</param>
            /// >
            public virtual void CreateImplementsList(IImplementsListSyntax implementsListSyntax, ICombinedThreeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates interface declaration syntax starting from syntax node and continues in respective semantic node.
            /// </summary>
            /// <param name="interfaceDeclarationSyntax">Interface declaration syntax node.</param>
            /// <param name="interfaceDeclaration">Interface declarations syntax semantic node.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateInterfaceDeclaration(IInterfaceDeclarationSyntax interfaceDeclarationSyntax,
                IInterfaceDeclaration interfaceDeclaration,
                IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates interface declaration from semantic node.
            /// </summary>
            /// <param name="interfaceDeclaration">Interface declaration semantics.</param>
            /// <param name="visitor">Associated visitor.</param>
            public void CreateInterfaceDeclaration(IInterfaceDeclaration interfaceDeclaration, IxNodeVisitor visitor)
            {
            }

            /// <summary>
            ///     Creates variable declaration.
            /// </summary>
            /// <param name="variableDeclaration">Variable declaration semantics.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateVariableDeclaration(IVariableDeclaration variableDeclaration, IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates structured type declaration.
            /// </summary>
            /// <param name="structTypeDeclarationSyntax">Structured type declaration syntax node.</param>
            /// <param name="structuredTypeDeclaration">Structured type semantics.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateStructuredType(IStructTypeDeclarationSyntax structTypeDeclarationSyntax,
                IStructuredTypeDeclaration structuredTypeDeclaration,
                IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates structured type.
            /// </summary>
            /// <param name="structuredTypeDeclaration">Structured type semantics.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateStructuredType(IStructuredTypeDeclaration structuredTypeDeclaration,
                IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates referenced type declaration.
            /// </summary>
            /// <param name="referenceTypeDeclaration">Reference type semantics.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateReferenceToDeclaration(IReferenceTypeDeclaration referenceTypeDeclaration,
                IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Creates semantic type access.
            /// </summary>
            /// <param name="semanticTypeAccess">Semantic type access semantics.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateSemanticTypeAccess(ISemanticTypeAccess semanticTypeAccess, IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Create scalar type declaration.
            /// </summary>
            /// <param name="scalarTypeDeclaration">Scalar type semantics.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateScalarTypeDeclaration(IScalarTypeDeclaration scalarTypeDeclaration, IxNodeVisitor visitor)
            {
                
            }


            /// <summary>
            ///     Creates string type declaration.
            /// </summary>
            /// <param name="stringTypeDeclaration">String type semantics.</param>
            /// <param name="visitor">Associated visitor.</param>
            public virtual void CreateStringTypeDeclaration(IStringTypeDeclaration stringTypeDeclaration, IxNodeVisitor visitor)
            {
                
            }

            /// <summary>
            ///     Created array type declaration.
            /// </summary>
            /// <param name="arrayTypeDeclaration">Array type semantics</param>
            /// <param name="visitor">Associated visitor.</param>
            public void CreateArrayTypeDeclaration(IArrayTypeDeclaration arrayTypeDeclaration, IxNodeVisitor visitor)
            {
            }

            public void CreateDocComment(IDocComment semanticTypeAccess, ICombinedThreeVisitor data)
            {
                
            }

            #endregion

        }

        public class MockTargetProject : ITargetProject
        {
            public MockTargetProject(AXSharpProject ixProject)
            {
                IxProject = ixProject;
            }

            private readonly AXSharpProject IxProject;

            public string GetMetaDataFolder => Path.Combine(IxProject.OutputFolder, ".meta");
            public string ProjectRootNamespace { get; }
            public void ProvisionProjectStructure()
            {
                
            }

            public void GenerateResources()
            {
                
            }

            public void GenerateCompanionData()
            {
               
            }

            public void InstallAXSharpDependencies(IEnumerable<object> dependencies)
            {
                
            }

            public IEnumerable<IReference> LoadReferences()
            {
                return new List<IReference>();
            }
        }
    }
}