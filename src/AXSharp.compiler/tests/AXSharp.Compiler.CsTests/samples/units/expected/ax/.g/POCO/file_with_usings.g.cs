using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;
using Pocos.FileWithUsingsSimpleFirstLevelNamespace;
using Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified;
using Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo;

namespace Pocos
{
    namespace FileWithUsingsSimpleFirstLevelNamespace
    {
        [AXSharp.Connector.SourceFileAttribute(@"file_with_usings.st")]
        public partial class Hello : AXSharp.Connector.IPlain
        {
            public Hello()
            {
            }
        }
    }

    namespace FileWithUsingsSimpleQualifiedNamespace.Qualified
    {
        [AXSharp.Connector.SourceFileAttribute(@"file_with_usings.st")]
        public partial class Hello : AXSharp.Connector.IPlain
        {
            public Hello()
            {
            }
        }
    }

    namespace FileWithUsingsHelloLevelOne
    {
        namespace FileWithUsingsHelloLevelTwo
        {
            [AXSharp.Connector.SourceFileAttribute(@"file_with_usings.st")]
            public partial class Hello : AXSharp.Connector.IPlain
            {
                public Hello()
                {
                }
            }
        }
    }

    namespace ExampleNamespace
    {
        [AXSharp.Connector.SourceFileAttribute(@"file_with_usings.st")]
        public partial class Hello : AXSharp.Connector.IPlain
        {
            public Hello()
            {
            }
        }
    }
}