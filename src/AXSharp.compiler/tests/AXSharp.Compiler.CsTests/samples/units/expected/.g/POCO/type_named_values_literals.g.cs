using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Simatic.Ax.StateFramework
{
    namespace Pocos
    {
        public partial class using_type_named_values : AXSharp.Connector.IPlain
        {
            public using_type_named_values()
            {
            }

            public UInt16 LColors { get; set; }
        }
    }
}