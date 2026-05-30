using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace RefToSimple
    {
        [AXSharp.Connector.SourceFileAttribute(@"ref_to_simple.st")]
        public partial class ref_to_simple : AXSharp.Connector.IPlain
        {
            public ref_to_simple()
            {
            }
        }

        [AXSharp.Connector.SourceFileAttribute(@"ref_to_simple.st")]
        public partial class referenced : AXSharp.Connector.IPlain
        {
            public referenced()
            {
            }

            public Int16 b { get; set; }
        }
    }
}