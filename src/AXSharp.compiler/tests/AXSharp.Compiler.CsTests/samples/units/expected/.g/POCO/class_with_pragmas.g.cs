using System;

namespace Pocos
{
    namespace ClassWithPragmasNamespace
    {
        public partial class ClassWithPragmas : AXSharp.Connector.IPlain
        {
            public ClassWithPragmas()
            {
            }

            public ClassWithPragmasNamespace.ComplexType1 myComplexType { get; set; } = new ClassWithPragmasNamespace.ComplexType1();
        }

        public partial class ComplexType1 : AXSharp.Connector.IPlain
        {
            public ComplexType1()
            {
            }
        }
    }
}