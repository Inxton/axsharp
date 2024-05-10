using System;

namespace Pocos
{
    namespace makereadonce
    {
        public partial class MembersWithMakeReadOnce : AXSharp.Connector.IPlain
        {
            public MembersWithMakeReadOnce()
            {
            }

            public string makeReadOnceMember { get; set; } = string.Empty;
            public string someOtherMember { get; set; } = string.Empty;
            public makereadonce.ComplexMember makeReadComplexMember { get; set; } = new makereadonce.ComplexMember();
            public makereadonce.ComplexMember someotherComplexMember { get; set; } = new makereadonce.ComplexMember();
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