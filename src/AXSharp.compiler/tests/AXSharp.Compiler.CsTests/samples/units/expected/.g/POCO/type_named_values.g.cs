using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace NamedValuesNamespace
    {
        public partial class using_type_named_values : AXSharp.Connector.IPlain
        {
            public using_type_named_values()
            {
            }

            public Int16 LColors { get; set; }
        }
    }
}