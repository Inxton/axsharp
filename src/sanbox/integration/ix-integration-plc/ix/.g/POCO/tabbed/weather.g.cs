using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Layouts.Tabbed
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