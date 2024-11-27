using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace ClassWithNonTraspilableMemberssNamespace
{
    namespace Pocos
    {
        public partial class ClassWithNonTraspilableMembers : AXSharp.Connector.IPlain
        {
            public ClassWithNonTraspilableMembers()
            {
            }

            public ClassWithNonTraspilableMemberssNamespace.Pocos.ComplexType1 myComplexType { get; set; } = new ClassWithNonTraspilableMemberssNamespace.Pocos.ComplexType1();
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