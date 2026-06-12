using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace Layouts.Stacked
    {
        [AXSharp.Connector.SourceFileAttribute(@"src/sanbox/integration/ix-integration-plc/src/stacked/weather.st")]
        public partial class weather : weatherBase, AXSharp.Connector.IPlain
        {
            public weather() : base()
            {
            }
        }
    }
}