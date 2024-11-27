using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Generics
{
    namespace Pocos
    {
        public partial class Extender : AXSharp.Connector.IPlain
        {
            public Extender()
            {
            }
        }
    }

    namespace Pocos
    {
        public partial class Extendee : Generics.Pocos.Extender, AXSharp.Connector.IPlain
        {
            public Extendee() : base()
            {
            }

            public Generics.Pocos.SomeType SomeType { get; set; } = new Generics.Pocos.SomeType();
            public Generics.Pocos.SomeType SomeTypeAsPoco { get; set; } = new Generics.Pocos.SomeType();
        }
    }

    namespace Pocos
    {
        public partial class Extendee2 : Generics.Pocos.Extender, AXSharp.Connector.IPlain
        {
            public Extendee2() : base()
            {
            }

            public Generics.Pocos.SomeType SomeType { get; set; } = new Generics.Pocos.SomeType();
        }
    }

    namespace Pocos
    {
        public partial class SomeType : AXSharp.Connector.IPlain
        {
            public SomeType()
            {
            }
        }
    }
}