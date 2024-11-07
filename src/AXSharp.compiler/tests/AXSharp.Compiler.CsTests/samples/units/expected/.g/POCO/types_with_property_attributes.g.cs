using System;
using AXSharp.Abstractions.Presentation;

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