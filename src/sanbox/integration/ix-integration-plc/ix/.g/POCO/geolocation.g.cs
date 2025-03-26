using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Location")]
    public partial class GeoLocation : AXSharp.Connector.IPlain
    {
        public GeoLocation()
        {
        }

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Latitude [°]")]
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeMinimum", -90.0f)]
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeMaximum", 90.0f)]
        public Single Latitude { get; set; }

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Logitude [°]")]
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeMinimum", 0.0f)]
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeMaximum", 180.0f)]
        public Single Longitude { get; set; }

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Altitude [m]")]
        public Single Altitude { get; set; }

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Short descriptor")]
        public string Description { get; set; } = string.Empty;

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Long descriptor")]
        public string LongDescription { get; set; } = string.Empty;
    }
}