using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace ClassWithComplexTypesNamespace
{
    namespace Pocos
    {
        public partial class ClassWithComplexTypes : AXSharp.Connector.IPlain
        {
            public ClassWithComplexTypes()
            {
            }

            public ClassWithComplexTypesNamespace.Pocos.ComplexType1 myComplexType { get; set; } = new ClassWithComplexTypesNamespace.Pocos.ComplexType1();
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