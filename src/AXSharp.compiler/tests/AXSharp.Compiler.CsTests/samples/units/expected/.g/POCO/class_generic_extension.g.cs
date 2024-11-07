using System;
using AXSharp.Abstractions.Presentation;

namespace Pocos
{
    namespace Generics
    {
        public partial class Extender : AXSharp.Connector.IPlain
        {
            public Extender()
            {
            }
        }

        public partial class Extendee : Generics.Extender, AXSharp.Connector.IPlain
        {
            public Extendee() : base()
            {
            }

            public Generics.SomeType SomeType { get; set; } = new Generics.SomeType();
            public Generics.SomeType SomeTypeAsPoco { get; set; } = new Generics.SomeType();
        }

        public partial class Extendee2 : Generics.Extender, AXSharp.Connector.IPlain
        {
            public Extendee2() : base()
            {
            }

            public Generics.SomeType SomeType { get; set; } = new Generics.SomeType();
        }

        public partial class SomeType : AXSharp.Connector.IPlain
        {
            public SomeType()
            {
            }
        }
    }
}