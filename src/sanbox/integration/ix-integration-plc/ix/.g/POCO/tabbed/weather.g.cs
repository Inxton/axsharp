using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace Layouts.Tabbed
    {
        [AXSharp.Connector.SourceFileAttribute(@"src/sanbox/integration/ix-integration-plc/src/tabbed/weather.st")]
        public partial class weather : weatherBase, AXSharp.Connector.IPlain
        {
            public weather() : base()
            {
            }
        }
    }
}