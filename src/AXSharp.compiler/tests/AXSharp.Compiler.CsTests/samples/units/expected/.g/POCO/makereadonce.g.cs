using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace makereadonce
{
    namespace Pocos
    {
        public partial class MembersWithMakeReadOnce : AXSharp.Connector.IPlain
        {
            public MembersWithMakeReadOnce()
            {
            }

            [ReadOnce()]
            public string makeReadOnceMember { get; set; } = string.Empty;
            public string someOtherMember { get; set; } = string.Empty;

            [ReadOnce()]
            public makereadonce.Pocos.ComplexMember makeReadComplexMember { get; set; } = new makereadonce.Pocos.ComplexMember();
            public makereadonce.Pocos.ComplexMember someotherComplexMember { get; set; } = new makereadonce.Pocos.ComplexMember();
        }
    }

    namespace Pocos
    {
        public partial class ComplexMember : AXSharp.Connector.IPlain
        {
            public ComplexMember()
            {
            }

            public string someMember { get; set; } = string.Empty;
            public string someOtherMember { get; set; } = string.Empty;
        }
    }
}