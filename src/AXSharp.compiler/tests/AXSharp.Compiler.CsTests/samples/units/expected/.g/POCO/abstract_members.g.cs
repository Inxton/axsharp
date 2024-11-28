using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

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