using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    [AXSharp.Connector.SourceFileAttribute(@"class_extends.st")]
    public partial class Extended : Extendee, AXSharp.Connector.IPlain
    {
        public Extended() : base()
        {
        }
    }

    [AXSharp.Connector.SourceFileAttribute(@"class_extends.st")]
    public partial class Extendee : AXSharp.Connector.IPlain
    {
        public Extendee()
        {
        }
    }
}