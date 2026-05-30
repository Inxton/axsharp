using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace Generics
    {
        [AXSharp.Connector.SourceFileAttribute(@"class_generic_extension.st")]
        public partial class Extender : AXSharp.Connector.IPlain
        {
            public Extender()
            {
            }
        }

        [AXSharp.Connector.SourceFileAttribute(@"class_generic_extension.st")]
        public partial class Extendee : Generics.Extender, AXSharp.Connector.IPlain
        {
            public Extendee() : base()
            {
            }

            public Generics.SomeType SomeType { get; set; } = new Generics.SomeType();
            public Generics.SomeType SomeTypeAsPoco { get; set; } = new Generics.SomeType();
        }

        [AXSharp.Connector.SourceFileAttribute(@"class_generic_extension.st")]
        public partial class Extendee2 : Generics.Extender, AXSharp.Connector.IPlain
        {
            public Extendee2() : base()
            {
            }

            public Generics.SomeType SomeType { get; set; } = new Generics.SomeType();
        }

        [AXSharp.Connector.SourceFileAttribute(@"class_generic_extension.st")]
        public partial class SomeType : AXSharp.Connector.IPlain
        {
            public SomeType()
            {
            }
        }
    }
}