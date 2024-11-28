using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Layouts.Stacked
{
    namespace Pocos
    {
        public partial class weather : global::Pocos.weatherBase, AXSharp.Connector.IPlain
        {
            public weather() : base()
            {
            }
        }
    }
}