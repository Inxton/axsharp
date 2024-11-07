using System;
using AXSharp.Abstractions.Presentation;

namespace Pocos
{
    public partial class GeoLocation : AXSharp.Connector.IPlain
    {
        public GeoLocation()
        {
        }

        public Single Latitude { get; set; }

        public Single Longitude { get; set; }

        public Single Altitude { get; set; }

        public string Description { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
    }
}