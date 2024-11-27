using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace TypesWithPropertyAttributes
{
    namespace Pocos
    {
        [AXSharp.Connector.AddedPropertiesAttribute("Description", "Some added property name value")]
        public partial class SomeAddedProperties : AXSharp.Connector.IPlain
        {
            public SomeAddedProperties()
            {
            }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Pocitadlo")]
            public Int16 Counter { get; set; }
        }
    }
}