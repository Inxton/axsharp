using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace CompilerOmmits
    {
        [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
        public partial class ClassWithArrays : AXSharp.Connector.IPlain
        {
            public ClassWithArrays()
            {
            }

            public CompilerOmmits.Complex _must_be_omitted_in_onliner { get; set; } = new CompilerOmmits.Complex();
            public Byte[] _primitive { get; set; } = new Byte[11];
        }

        [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
        public partial class Complex : AXSharp.Connector.IPlain
        {
            public Complex()
            {
            }

            public string HelloString { get; set; } = string.Empty;
            public UInt64 Id { get; set; }
        }
    }

    namespace CompilerOmmitsEnums
    {
        [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
        public partial class ClassWithEnums : AXSharp.Connector.IPlain
        {
            public ClassWithEnums()
            {
            }

            [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(global::CompilerOmmitsEnums.Colors))]
            public global::CompilerOmmitsEnums.Colors colors { get; set; }

            [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(global::CompilerOmmitsEnums.NamedValuesColors))]
            public String NamedValuesColors { get; set; }
        }
    }

    namespace CompilerOmmitsMisc
    {
        [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
        public partial class VariousMembers : AXSharp.Connector.IPlain
        {
            public VariousMembers()
            {
            }

            public CompilerOmmitsMisc.SomeClass _SomeClass { get; set; } = new CompilerOmmitsMisc.SomeClass();
            public CompilerOmmitsMisc.ComersMotor _Motor { get; set; } = new CompilerOmmitsMisc.ComersMotor();
        }

        [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
        public partial class SomeClass : AXSharp.Connector.IPlain
        {
            public SomeClass()
            {
            }

            public string SomeClassVariable { get; set; } = string.Empty;
        }

        [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
        public partial class ComersMotor : AXSharp.Connector.IPlain
        {
            public ComersMotor()
            {
            }

            public Boolean isRunning { get; set; }
        }

        [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
        public partial class ComersVehicle : AXSharp.Connector.IPlain
        {
            public ComersVehicle()
            {
            }

            public CompilerOmmitsMisc.ComersMotor m { get; set; } = new CompilerOmmitsMisc.ComersMotor();
            public Int16 displacement { get; set; }
        }
    }

    namespace CompilerOmmitsUnknownArrays
    {
        [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
        public partial class ClassWithArrays : AXSharp.Connector.IPlain
        {
            public ClassWithArrays()
            {
#pragma warning disable CS0612
                AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(_complexKnown, () => new global::Pocos.CompilerOmmitsUnknownArrays.Complex(), new[] { (0, 10) });
#pragma warning restore CS0612
            }

            public CompilerOmmitsUnknownArrays.Complex[] _complexKnown { get; set; } = new CompilerOmmitsUnknownArrays.Complex[11];
            public Byte[] _primitive { get; set; } = new Byte[11];
        }

        [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
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