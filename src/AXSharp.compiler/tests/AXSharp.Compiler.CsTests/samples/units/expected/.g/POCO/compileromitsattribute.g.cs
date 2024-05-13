using System;

namespace Pocos
{
    namespace CompilerOmmits
    {
        public partial class ClassWithArrays : AXSharp.Connector.IPlain
        {
            public ClassWithArrays()
            {
            }

            public CompilerOmmits.Complex _must_be_omitted_in_onliner { get; set; } = new CompilerOmmits.Complex();
            public Byte[] _primitive { get; set; } = new Byte[11];
        }

        public partial class Complex : AXSharp.Connector.IPlain
        {
            public Complex()
            {
            }

            public string HelloString { get; set; } = string.Empty;
            public UInt64 Id { get; set; }
        }
    }

    namespace Enums
    {
        public partial class ClassWithEnums : AXSharp.Connector.IPlain
        {
            public ClassWithEnums()
            {
            }

            public global::Enums.Colors colors { get; set; }

            public String NamedValuesColors { get; set; }
        }
    }

    namespace misc
    {
        public partial class VariousMembers : AXSharp.Connector.IPlain
        {
            public VariousMembers()
            {
            }

            public misc.SomeClass _SomeClass { get; set; } = new misc.SomeClass();
            public misc.Motor _Motor { get; set; } = new misc.Motor();
        }

        public partial class SomeClass : AXSharp.Connector.IPlain
        {
            public SomeClass()
            {
            }

            public string SomeClassVariable { get; set; } = string.Empty;
        }

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

            public misc.Motor m { get; set; } = new misc.Motor();
            public Int16 displacement { get; set; }
        }
    }

    namespace UnknownArraysShouldNotBeTraspiled
    {
        public partial class ClassWithArrays : AXSharp.Connector.IPlain
        {
            public ClassWithArrays()
            {
#pragma warning disable CS0612
                AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(_complexKnown, () => new UnknownArraysShouldNotBeTraspiled.Complex(), new[] { (0, 10) });
#pragma warning restore CS0612
            }

            public UnknownArraysShouldNotBeTraspiled.Complex[] _complexKnown { get; set; } = new UnknownArraysShouldNotBeTraspiled.Complex[11];
            public Byte[] _primitive { get; set; } = new Byte[11];
        }

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