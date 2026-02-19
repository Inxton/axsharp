using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    public partial class Motor : AXSharp.Connector.IPlain
    {
        public Motor()
        {
        }

        public Boolean isRunning { get; set; }
    }

    public partial class Vehicle : AXSharp.Connector.IPlain
    {
        public Vehicle()
        {
        }

        public Motor m { get; set; } = new Motor();
        public Int16 displacement { get; set; }
    }
}