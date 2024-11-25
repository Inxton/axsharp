using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

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
        [ReadOnly()]
        public string LongDescription { get; set; } = string.Empty;
        [ReadOnce()]
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "this has [ReadOnce()] attribute will be readon only once...")]
        public Int16 StartCounter { get; set; }

        [RenderIgnore()]
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "[RenderIgnore()] must not be displayed!")]
        public string RenderIgnoreAllToghether { get; set; } = string.Empty;
        [RenderIgnore("Control")]
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "[RenderIgnore(''Control'')]")]
        public string RenderIgnoreWhenControl { get; set; } = string.Empty;
        [RenderIgnore("Display")]
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "[RenderIgnore(''Display'')]")]
        public string RenderIgnoreWhenDisplay { get; set; } = string.Empty;
        [RenderIgnore("Control", "ShadowControl")]
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "[RenderIgnore(''Control'', ''ShadowControl'')]")]
        public string RenderIgnoreWhenControlAndShadow { get; set; } = string.Empty;
        [RenderIgnore("Display", "ShadowDisplay")]
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "[RenderIgnore(''Display'', ''ShadowDisplay'')]")]
        public string RenderIgnoreWhenDisplayAndShadow { get; set; } = string.Empty;
    }
}