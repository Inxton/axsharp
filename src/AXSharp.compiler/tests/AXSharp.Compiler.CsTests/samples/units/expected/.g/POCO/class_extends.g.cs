using System;

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