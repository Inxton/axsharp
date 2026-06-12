using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace Layouts.Wrapped
    {
        [AXSharp.Connector.SourceFileAttribute(@"src/sanbox/integration/ix-integration-plc/src/wrapped/weather.st")]
        public partial class weather : weatherBase, AXSharp.Connector.IPlain
        {
            public weather() : base()
            {
            }
        }
    }
}