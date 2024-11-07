using System;
using AXSharp.Abstractions.Presentation;

namespace Pocos
{
    namespace Simatic.Ax.StateFramework
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