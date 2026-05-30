using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    [AXSharp.Connector.SourceFileAttribute(@"mixed_access.st")]
    public partial class MixedAccessMotor : AXSharp.Connector.IPlain
    {
        public MixedAccessMotor()
        {
        }

        public Boolean Run { get; set; }
    }

    [AXSharp.Connector.SourceFileAttribute(@"mixed_access.st")]
    public partial class struct1 : AXSharp.Connector.IPlain
    {
        public struct1()
        {
        }

        public struct2 s2 { get; set; } = new struct2();
    }

    [AXSharp.Connector.SourceFileAttribute(@"mixed_access.st")]
    public partial class struct2 : AXSharp.Connector.IPlain
    {
        public struct2()
        {
        }

        public struct3 s3 { get; set; } = new struct3();
    }

    [AXSharp.Connector.SourceFileAttribute(@"mixed_access.st")]
    public partial class struct3 : AXSharp.Connector.IPlain
    {
        public struct3()
        {
        }

        public struct4 s4 { get; set; } = new struct4();
    }

    [AXSharp.Connector.SourceFileAttribute(@"mixed_access.st")]
    public partial class struct4 : AXSharp.Connector.IPlain
    {
        public struct4()
        {
        }

        public Int16 s5 { get; set; }
    }

    [AXSharp.Connector.SourceFileAttribute(@"mixed_access.st")]
    public partial class AbstractMotor : AXSharp.Connector.IPlain
    {
        public AbstractMotor()
        {
        }

        public Boolean Run { get; set; }
        public Boolean ReverseDirection { get; set; }
    }

    [AXSharp.Connector.SourceFileAttribute(@"mixed_access.st")]
    public partial class GenericMotor : AbstractMotor, AXSharp.Connector.IPlain
    {
        public GenericMotor() : base()
        {
        }
    }

    [AXSharp.Connector.SourceFileAttribute(@"mixed_access.st")]
    public partial class SpecificMotorA : GenericMotor, AXSharp.Connector.IPlain
    {
        public SpecificMotorA() : base()
        {
        }
    }
}