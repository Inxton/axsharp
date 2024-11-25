using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    public partial class Extended : Extendee, AXSharp.Connector.IPlain
    {
        public Extended() : base()
        {
        }
    }

    public partial class Extendee : AXSharp.Connector.IPlain
    {
        public Extendee()
        {
        }
    }
}