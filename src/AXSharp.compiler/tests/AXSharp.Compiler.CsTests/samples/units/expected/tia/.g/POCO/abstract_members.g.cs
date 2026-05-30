using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    [AXSharp.Connector.SourceFileAttribute(@"abstract_members.st")]
    public partial class AbstractMembersMotor : AXSharp.Connector.IPlain
    {
        public AbstractMembersMotor()
        {
        }

        public Boolean Run { get; set; }
        public Boolean ReverseDirection { get; set; }
    }
}