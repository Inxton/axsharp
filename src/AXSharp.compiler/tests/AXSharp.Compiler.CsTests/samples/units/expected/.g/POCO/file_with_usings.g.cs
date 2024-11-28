using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;
using FileWithUsingsSimpleFirstLevelNamespace.Pocos;
using FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos;
using FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos;

namespace FileWithUsingsSimpleFirstLevelNamespace
{
    namespace Pocos
    {
        public partial class Hello : AXSharp.Connector.IPlain
        {
            public Hello()
            {
            }
        }
    }
}

namespace FileWithUsingsSimpleQualifiedNamespace.Qualified
{
    namespace Pocos
    {
        public partial class Hello : AXSharp.Connector.IPlain
        {
            public Hello()
            {
            }
        }
    }
}

namespace FileWithUsingsHelloLevelOne
{
    namespace FileWithUsingsHelloLevelTwo
    {
        namespace Pocos
        {
            public partial class Hello : AXSharp.Connector.IPlain
            {
                public Hello()
                {
                }
            }
        }
    }
}

namespace ExampleNamespace
{
    namespace Pocos
    {
        public partial class Hello : AXSharp.Connector.IPlain
        {
            public Hello()
            {
            }
        }
    }
}