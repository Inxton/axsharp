using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace ClassWithPragmasNamespace
{
    namespace Pocos
    {
        public partial class ClassWithPragmas : AXSharp.Connector.IPlain
        {
            public ClassWithPragmas()
            {
            }

            [Container(Layout.Wrap)]
            public ClassWithPragmasNamespace.Pocos.ComplexType1 myComplexType { get; set; } = new ClassWithPragmasNamespace.Pocos.ComplexType1();
        }
    }

    namespace Pocos
    {
        public partial class ComplexType1 : AXSharp.Connector.IPlain
        {
            public ComplexType1()
            {
            }
        }
    }
}