using System;

namespace Pocos
{
    namespace TypesWithPropertyAttributes
    {
        public partial class SomeAddedProperties : AXSharp.Connector.IPlain
        {
            public SomeAddedProperties()
            {
            }

            public Int16 Counter { get; set; }
        }
    }
}