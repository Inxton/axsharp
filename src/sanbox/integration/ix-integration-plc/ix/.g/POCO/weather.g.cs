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

        public global::Pocos.GeoLocation GeoLocation { get; set; } = new global::Pocos.GeoLocation();
        public Single Temperature { get; set; }

        public Single Humidity { get; set; }

        public string Location { get; set; } = string.Empty;
        public Single ChillFactor { get; set; }

        public global::Feeling Feeling { get; set; }
    }
}

namespace Pocos
{
    public partial class weathers : AXSharp.Connector.IPlain
    {
        public weathers()
        {
#pragma warning disable CS0612
            AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(i, () => new global::Pocos.weatherBase(), new[] { (0, 50) });
#pragma warning restore CS0612
        }

        public global::Pocos.weatherBase[] i { get; set; } = new global::Pocos.weatherBase[51];
    }
}