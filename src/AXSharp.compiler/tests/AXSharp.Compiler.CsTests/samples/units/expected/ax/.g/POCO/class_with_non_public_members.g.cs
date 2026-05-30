using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace ClassWithNonTraspilableMemberssNamespace
    {
        [AXSharp.Connector.SourceFileAttribute(@"class_with_non_public_members.st")]
        public partial class ClassWithNonTraspilableMembers : AXSharp.Connector.IPlain
        {
            public ClassWithNonTraspilableMembers()
            {
            }

            public ClassWithNonTraspilableMemberssNamespace.ComplexType1 myComplexType { get; set; } = new ClassWithNonTraspilableMemberssNamespace.ComplexType1();
        }

        [AXSharp.Connector.SourceFileAttribute(@"class_with_non_public_members.st")]
        public partial class ComplexType1 : AXSharp.Connector.IPlain
        {
            public ComplexType1()
            {
            }
        }
    }
}