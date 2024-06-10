using System;

namespace Pocos
{
    public partial class weatherBase : AXSharp.Connector.IPlain
    {
        public weatherBase()
        {
        }

        public Single Latitude { get; set; }

        public Single Longitude { get; set; }

        public Single Altitude { get; set; }

        public string Description { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public Int16 StartCounter { get; set; }

        public string RenderIgnoreAllToghether { get; set; } = string.Empty;
        public string RenderIgnoreWhenControl { get; set; } = string.Empty;
        public string RenderIgnoreWhenDisplay { get; set; } = string.Empty;
        public string RenderIgnoreWhenControlAndShadow { get; set; } = string.Empty;
        public string RenderIgnoreWhenDisplayAndShadow { get; set; } = string.Empty;
    }
}