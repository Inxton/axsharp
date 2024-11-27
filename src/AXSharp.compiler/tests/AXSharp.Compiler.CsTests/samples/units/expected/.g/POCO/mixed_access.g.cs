using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    public partial class unitsTwinController
    {
        public Boolean MotorOn { get; set; }

        public Int16 MotorState { get; set; }

        public global::Pocos.Motor Motor1 { get; set; } = new global::Pocos.Motor();
        public global::Pocos.Motor Motor2 { get; set; } = new global::Pocos.Motor();
        public global::Pocos.struct1 s1 { get; set; } = new global::Pocos.struct1();
        public global::Pocos.struct4 s4 { get; set; } = new global::Pocos.struct4();
        public global::Pocos.SpecificMotorA mot1 { get; set; } = new global::Pocos.SpecificMotorA();
    }
}

namespace Pocos
{
    public partial class Motor : AXSharp.Connector.IPlain
    {
        public Motor()
        {
        }

        public Boolean Run { get; set; }
    }
}

namespace Pocos
{
    public partial class struct1 : AXSharp.Connector.IPlain
    {
        public struct1()
        {
        }

        public global::Pocos.struct2 s2 { get; set; } = new global::Pocos.struct2();
    }
}

namespace Pocos
{
    public partial class struct2 : AXSharp.Connector.IPlain
    {
        public struct2()
        {
        }

        public global::Pocos.struct3 s3 { get; set; } = new global::Pocos.struct3();
    }
}

namespace Pocos
{
    public partial class struct3 : AXSharp.Connector.IPlain
    {
        public struct3()
        {
        }

        public global::Pocos.struct4 s4 { get; set; } = new global::Pocos.struct4();
    }
}

namespace Pocos
{
    public partial class struct4 : AXSharp.Connector.IPlain
    {
        public struct4()
        {
        }

        public Int16 s5 { get; set; }
    }
}

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

namespace Pocos
{
    public partial class GenericMotor : global::Pocos.AbstractMotor, AXSharp.Connector.IPlain
    {
        public GenericMotor() : base()
        {
        }
    }
}

namespace Pocos
{
    public partial class SpecificMotorA : global::Pocos.GenericMotor, AXSharp.Connector.IPlain
    {
        public SpecificMotorA() : base()
        {
        }
    }
}