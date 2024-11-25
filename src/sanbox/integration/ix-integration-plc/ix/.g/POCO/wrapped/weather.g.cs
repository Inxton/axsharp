using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace Layouts.Wrapped
    {
        public partial class weather : weatherBase, AXSharp.Connector.IPlain
        {
            public weather() : base()
            {
            }
        }
    }
}