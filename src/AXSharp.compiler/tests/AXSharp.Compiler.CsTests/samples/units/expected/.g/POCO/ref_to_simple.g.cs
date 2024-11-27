using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace RefToSimple
{
    namespace Pocos
    {
        public partial class ref_to_simple : AXSharp.Connector.IPlain
        {
            public ref_to_simple()
            {
            }
        }
    }

    namespace Pocos
    {
        public partial class referenced : AXSharp.Connector.IPlain
        {
            public referenced()
            {
            }

            public Int16 b { get; set; }
        }
    }
}