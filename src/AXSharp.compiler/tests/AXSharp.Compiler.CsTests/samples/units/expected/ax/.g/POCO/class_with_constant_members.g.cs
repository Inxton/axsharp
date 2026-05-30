using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace ClassWithConstantMembersNamespace
    {
        [AXSharp.Connector.SourceFileAttribute(@"class_with_constant_members.st")]
        public partial class ClassWithConstantMembers : AXSharp.Connector.IPlain
        {
            public ClassWithConstantMembers()
            {
            }

            public Int16 myNonConstant { get; set; }
        }
    }
}