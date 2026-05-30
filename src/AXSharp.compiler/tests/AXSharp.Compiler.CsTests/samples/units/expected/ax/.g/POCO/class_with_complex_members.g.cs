using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace ClassWithComplexTypesNamespace
    {
        [AXSharp.Connector.SourceFileAttribute(@"class_with_complex_members.st")]
        public partial class ClassWithComplexTypes : AXSharp.Connector.IPlain
        {
            public ClassWithComplexTypes()
            {
            }

            public ClassWithComplexTypesNamespace.ComplexType1 myComplexType { get; set; } = new ClassWithComplexTypesNamespace.ComplexType1();
        }

        [AXSharp.Connector.SourceFileAttribute(@"class_with_complex_members.st")]
        public partial class ComplexType1 : AXSharp.Connector.IPlain
        {
            public ComplexType1()
            {
            }
        }
    }
}