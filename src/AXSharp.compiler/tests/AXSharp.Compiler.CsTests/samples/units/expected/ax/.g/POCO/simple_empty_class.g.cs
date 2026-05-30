using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    [AXSharp.Connector.SourceFileAttribute(@"simple_empty_class.st")]
    public partial class simple_class : AXSharp.Connector.IPlain
    {
        public simple_class()
        {
        }
    }
}