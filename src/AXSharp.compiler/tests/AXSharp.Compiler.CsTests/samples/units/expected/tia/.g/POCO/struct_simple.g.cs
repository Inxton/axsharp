using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    [AXSharp.Connector.SourceFileAttribute(@"struct_simple.st")]
    public partial class StructSimpleMotor : AXSharp.Connector.IPlain
    {
        public StructSimpleMotor()
        {
        }

        public Boolean isRunning { get; set; }
    }

    [AXSharp.Connector.SourceFileAttribute(@"struct_simple.st")]
    public partial class StructSimpleVehicle : AXSharp.Connector.IPlain
    {
        public StructSimpleVehicle()
        {
        }

        public StructSimpleMotor m { get; set; } = new StructSimpleMotor();
        public Int16 displacement { get; set; }
    }
}