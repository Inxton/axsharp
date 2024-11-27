using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace makereadonly
{
    namespace Pocos
    {
        public partial class MembersWithMakeReadOnly : AXSharp.Connector.IPlain
        {
            public MembersWithMakeReadOnly()
            {
            }

            [ReadOnly()]
            public string makeReadOnceMember { get; set; } = string.Empty;
            public string someOtherMember { get; set; } = string.Empty;
            [ReadOnly()]
            public makereadonly.Pocos.ComplexMember makeReadComplexMember { get; set; } = new makereadonly.Pocos.ComplexMember();
            public makereadonly.Pocos.ComplexMember someotherComplexMember { get; set; } = new makereadonly.Pocos.ComplexMember();
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