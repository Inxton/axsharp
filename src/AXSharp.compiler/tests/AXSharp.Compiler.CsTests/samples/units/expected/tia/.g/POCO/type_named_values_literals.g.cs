using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace Simatic.Ax.StateFramework
    {
        public partial class using_type_named_values : AXSharp.Connector.IPlain
        {
            public using_type_named_values()
            {
            }

            [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(global::Simatic.Ax.StateFramework.StateControllerStatus))]
            public UInt16 LColors { get; set; }
        }
    }
}