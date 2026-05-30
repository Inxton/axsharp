using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace Layouts.Tabbed
    {
        [AXSharp.Connector.SourceFileAttribute(@"tabbed/weather.st")]
        public partial class weather : weatherBase, AXSharp.Connector.IPlain
        {
            public weather() : base()
            {
            }
        }
    }
}