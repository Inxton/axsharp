using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace Enums
    {
        [AXSharp.Connector.SourceFileAttribute(@"misc.st")]
        public partial class ClassWithEnums : AXSharp.Connector.IPlain
        {
            public ClassWithEnums()
            {
            }

            [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(global::Enums.Colors))]
            public global::Enums.Colors colors { get; set; }

            [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(global::Enums.NamedValuesColors))]
            public String NamedValuesColors { get; set; }
        }
    }

    namespace misc
    {
        [AXSharp.Connector.SourceFileAttribute(@"misc.st")]
        public partial class VariousMembers : AXSharp.Connector.IPlain
        {
            public VariousMembers()
            {
            }

            public misc.SomeClass _SomeClass { get; set; } = new misc.SomeClass();
            public misc.MiscMotor _Motor { get; set; } = new misc.MiscMotor();
        }

        [AXSharp.Connector.SourceFileAttribute(@"misc.st")]
        public partial class SomeClass : AXSharp.Connector.IPlain
        {
            public SomeClass()
            {
            }

            public string SomeClassVariable { get; set; } = string.Empty;
        }

        [AXSharp.Connector.SourceFileAttribute(@"misc.st")]
        public partial class MiscMotor : AXSharp.Connector.IPlain
        {
            public MiscMotor()
            {
            }

            public Boolean isRunning { get; set; }
        }

        [AXSharp.Connector.SourceFileAttribute(@"misc.st")]
        public partial class MiscVehicle : AXSharp.Connector.IPlain
        {
            public MiscVehicle()
            {
            }

            public misc.MiscMotor m { get; set; } = new misc.MiscMotor();
            public Int16 displacement { get; set; }
        }
    }

    namespace UnknownArraysShouldNotBeTraspiled
    {
        [AXSharp.Connector.SourceFileAttribute(@"misc.st")]
        public partial class ClassWithArrays : AXSharp.Connector.IPlain
        {
            public ClassWithArrays()
            {
#pragma warning disable CS0612
                AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(_complexKnown, () => new global::Pocos.UnknownArraysShouldNotBeTraspiled.Complex(), new[] { (0, 10) });
#pragma warning restore CS0612
            }

            public UnknownArraysShouldNotBeTraspiled.Complex[] _complexKnown { get; set; } = new UnknownArraysShouldNotBeTraspiled.Complex[11];
            public Byte[] _primitive { get; set; } = new Byte[11];
        }

        [AXSharp.Connector.SourceFileAttribute(@"misc.st")]
        public partial class Complex : AXSharp.Connector.IPlain
        {
            public Complex()
            {
            }

            public string HelloString { get; set; } = string.Empty;
            public UInt64 Id { get; set; }
        }
    }
}