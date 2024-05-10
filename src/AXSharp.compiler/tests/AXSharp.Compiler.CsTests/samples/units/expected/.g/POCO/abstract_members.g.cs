using System;

namespace Pocos
{
    public partial class AbstractMotor : AXSharp.Connector.IPlain
    {
        public AbstractMotor()
        {
        }

        public Boolean Run { get; set; }

        public Boolean ReverseDirection { get; set; }
    }
}