using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    public partial class Extended : global::Pocos.Extendee, AXSharp.Connector.IPlain
    {
        public Extended() : base()
        {
        }
    }
}

namespace Pocos
{
    public partial class Extendee : AXSharp.Connector.IPlain
    {
        public Extendee()
        {
        }
    }
}