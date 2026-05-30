using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace ClassWithPragmasNamespace
    {
        [AXSharp.Connector.SourceFileAttribute(@"class_with_pragmas.st")]
        public partial class ClassWithPragmas : AXSharp.Connector.IPlain
        {
            public ClassWithPragmas()
            {
            }

            public ClassWithPragmasNamespace.ComplexType1 myComplexType { get; set; } = new ClassWithPragmasNamespace.ComplexType1();
        }

        [AXSharp.Connector.SourceFileAttribute(@"class_with_pragmas.st")]
        public partial class ComplexType1 : AXSharp.Connector.IPlain
        {
            public ComplexType1()
            {
            }
        }
    }
}