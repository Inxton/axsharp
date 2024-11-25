using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace makereadonly
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
            public makereadonly.ComplexMember makeReadComplexMember { get; set; } = new makereadonly.ComplexMember();
            public makereadonly.ComplexMember someotherComplexMember { get; set; } = new makereadonly.ComplexMember();
        }

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