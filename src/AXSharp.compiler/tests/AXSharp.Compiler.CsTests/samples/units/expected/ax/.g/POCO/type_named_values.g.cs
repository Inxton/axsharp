using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace NamedValuesNamespace
    {
        [AXSharp.Connector.SourceFileAttribute(@"type_named_values.st")]
        public partial class using_type_named_values : AXSharp.Connector.IPlain
        {
            public using_type_named_values()
            {
            }

            [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(global::NamedValuesNamespace.LightColors))]
            public Int16 LColors { get; set; }
        }
    }
}