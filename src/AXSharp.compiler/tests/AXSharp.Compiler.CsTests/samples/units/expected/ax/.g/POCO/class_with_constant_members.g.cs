using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace ClassWithConstantMembersNamespace
    {
        public partial class ClassWithConstantMembers : AXSharp.Connector.IPlain
        {
            public ClassWithConstantMembers()
            {
            }

            public Int16 myNonConstant { get; set; }
        }
    }
}