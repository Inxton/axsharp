using System;

namespace Pocos
{
    public partial class unitsTwinController
    {
        public Boolean MotorOn { get; set; }

        public Int16 MotorState { get; set; }

        public Motor Motor1 { get; set; } = new Motor();
        public Motor Motor2 { get; set; } = new Motor();
        public struct1 s1 { get; set; } = new struct1();
        public struct4 s4 { get; set; } = new struct4();
        public SpecificMotorA mot1 { get; set; } = new SpecificMotorA();
    }

    public partial class Motor : AXSharp.Connector.IPlain
    {
        public Motor()
        {
        }

        public Boolean Run { get; set; }
    }

    public partial class struct1
    {
        public struct2 s2 { get; set; } = new struct2();
    }

    public partial class struct2
    {
        public struct3 s3 { get; set; } = new struct3();
    }

    public partial class struct3
    {
        public struct4 s4 { get; set; } = new struct4();
    }

    public partial class struct4
    {
        public Int16 s5 { get; set; }
    }

    public partial class AbstractMotor : AXSharp.Connector.IPlain
    {
        public AbstractMotor()
        {
        }

        public Boolean Run { get; set; }

        public Boolean ReverseDirection { get; set; }
    }

    public partial class GenericMotor : AbstractMotor, AXSharp.Connector.IPlain
    {
        public GenericMotor() : base()
        {
        }
    }

    public partial class SpecificMotorA : GenericMotor, AXSharp.Connector.IPlain
    {
        public SpecificMotorA() : base()
        {
        }
    }
}