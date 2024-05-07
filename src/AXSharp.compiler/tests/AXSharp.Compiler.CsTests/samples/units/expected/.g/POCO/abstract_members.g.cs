using System;

namespace Pocos
{
    public partial class AbstractMotor : AXSharp.Connector.IPlain
    {
        public Boolean Run { get; set; }

        public Boolean ReverseDirection { get; set; }
    }
}