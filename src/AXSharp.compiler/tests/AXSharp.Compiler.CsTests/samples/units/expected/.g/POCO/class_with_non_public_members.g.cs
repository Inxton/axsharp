using System;

namespace Pocos
{
    namespace ClassWithNonTraspilableMemberssNamespace
    {
        public partial class ClassWithNonTraspilableMembers : AXSharp.Connector.IPlain
        {
            public ClassWithNonTraspilableMembers()
            {
            }

            public ClassWithNonTraspilableMemberssNamespace.ComplexType1 myComplexType { get; set; } = new ClassWithNonTraspilableMemberssNamespace.ComplexType1();
        }

        public partial class ComplexType1 : AXSharp.Connector.IPlain
        {
            public ComplexType1()
            {
            }
        }
    }
}