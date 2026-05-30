using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace TypesWithPropertyAttributes
    {
        [AXSharp.Connector.SourceFileAttribute(@"types_with_property_attributes.st")]
        [AXSharp.Connector.AddedPropertiesAttribute("Description", @"Some added property name value")]
        public partial class SomeAddedProperties : AXSharp.Connector.IPlain
        {
            public SomeAddedProperties()
            {
            }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"Pocitadlo")]
            public Int16 Counter { get; set; }
        }
    }
}