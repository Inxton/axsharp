using System;
using Pocos.FileWithUsingsSimpleFirstLevelNamespace;
using Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified;
using Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo;

namespace Pocos
{
    namespace FileWithUsingsSimpleFirstLevelNamespace
    {
        public partial class Hello : AXSharp.Connector.IPlain
        {
        }
    }

    namespace FileWithUsingsSimpleQualifiedNamespace.Qualified
    {
        public partial class Hello : AXSharp.Connector.IPlain
        {
        }
    }

    namespace FileWithUsingsHelloLevelOne
    {
        namespace FileWithUsingsHelloLevelTwo
        {
            public partial class Hello : AXSharp.Connector.IPlain
            {
            }
        }
    }

    namespace ExampleNamespace
    {
        public partial class Hello : AXSharp.Connector.IPlain
        {
        }
    }
}