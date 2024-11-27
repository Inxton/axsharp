using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    public partial class weather : AXSharp.Connector.IPlain
    {
        public weather()
        {
        }

        public GeoLocation GeoLocation { get; set; } = new GeoLocation();
        public Single Temperature { get; set; }

        public Single Humidity { get; set; }

        public string Location { get; set; } = string.Empty;
        public Single ChillFactor { get; set; }

        public global::Feeling Feeling { get; set; }
    }

    public partial class weathers : AXSharp.Connector.IPlain
    {
        public weathers()
        {
#pragma warning disable CS0612
            AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(i, () => new weatherBase(), new[] { (0, 50) });
#pragma warning restore CS0612
        }

        public weatherBase[] i { get; set; } = new weatherBase[51];
    }
}