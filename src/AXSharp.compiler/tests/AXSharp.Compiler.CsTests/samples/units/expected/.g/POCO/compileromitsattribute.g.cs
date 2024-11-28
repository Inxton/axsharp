using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace CompilerOmmits
{
    namespace Pocos
    {
        public partial class ClassWithArrays : AXSharp.Connector.IPlain
        {
            public ClassWithArrays()
            {
            }

            [CompilerOmitsAttribute("Onliner")]
            public CompilerOmmits.Pocos.Complex _must_be_omitted_in_onliner { get; set; } = new CompilerOmmits.Pocos.Complex();
            public Byte[] _primitive { get; set; } = new Byte[11];
        }
    }

    namespace Pocos
    {
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

namespace Enums
{
    namespace Pocos
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
}

namespace misc
{
    namespace Pocos
    {
        public partial class VariousMembers : AXSharp.Connector.IPlain
        {
            public VariousMembers()
            {
            }

            public misc.Pocos.SomeClass _SomeClass { get; set; } = new misc.Pocos.SomeClass();
            public misc.Pocos.Motor _Motor { get; set; } = new misc.Pocos.Motor();
        }
    }

    namespace Pocos
    {
        public partial class SomeClass : AXSharp.Connector.IPlain
        {
            public SomeClass()
            {
            }

            public string SomeClassVariable { get; set; } = string.Empty;
        }
    }

    namespace Pocos
    {
        public partial class Motor : AXSharp.Connector.IPlain
        {
            public Motor()
            {
            }

            public Boolean isRunning { get; set; }
        }
    }

    namespace Pocos
    {
        public partial class Vehicle : AXSharp.Connector.IPlain
        {
            public Vehicle()
            {
            }

            public misc.Pocos.Motor m { get; set; } = new misc.Pocos.Motor();
            public Int16 displacement { get; set; }
        }
    }
}

namespace UnknownArraysShouldNotBeTraspiled
{
    namespace Pocos
    {
        public partial class ClassWithArrays : AXSharp.Connector.IPlain
        {
            public ClassWithArrays()
            {
#pragma warning disable CS0612
                AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(_complexKnown, () => new UnknownArraysShouldNotBeTraspiled.Pocos.Complex(), new[] { (0, 10) });
#pragma warning restore CS0612
            }

            public UnknownArraysShouldNotBeTraspiled.Pocos.Complex[] _complexKnown { get; set; } = new UnknownArraysShouldNotBeTraspiled.Pocos.Complex[11];
            public Byte[] _primitive { get; set; } = new Byte[11];
        }
    }

    namespace Pocos
    {
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