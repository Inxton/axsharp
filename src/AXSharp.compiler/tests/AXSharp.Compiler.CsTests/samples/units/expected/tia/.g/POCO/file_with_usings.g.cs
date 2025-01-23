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
        public partial class Hello : AXSharp.Connector.IPlain
        {
            public Hello()
            {
            }
        }
    }

    namespace FileWithUsingsSimpleQualifiedNamespace.Qualified
    {
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
        public partial class Hello : AXSharp.Connector.IPlain
        {
            public Hello()
            {
            }
        }
    }
}