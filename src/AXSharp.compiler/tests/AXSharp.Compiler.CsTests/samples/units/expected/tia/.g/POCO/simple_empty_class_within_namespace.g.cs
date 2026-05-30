using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace sampleNamespace
    {
        [AXSharp.Connector.SourceFileAttribute(@"simple_empty_class_within_namespace.st")]
        public partial class simple_empty_class_within_namespace : AXSharp.Connector.IPlain
        {
            public simple_empty_class_within_namespace()
            {
            }
        }
    }
}