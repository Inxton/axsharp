using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;
using MonsterData.Pocos;

namespace Pocos
{
    public partial class ix_integration_plcTwinController
    {
        public global::Pocos.all_primitives all_primitives { get; set; } = new global::Pocos.all_primitives();
        public global::Pocos.weather weather { get; set; } = new global::Pocos.weather();
        public global::Pocos.weathers weathers { get; set; } = new global::Pocos.weathers();

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Weather in a stack pannel and grouped in group box")]
        public Layouts.Stacked.Pocos.weather weather_stacked { get; set; } = new Layouts.Stacked.Pocos.weather();

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Weather in a wrap pannel and grouped in group box")]
        public Layouts.Wrapped.Pocos.weather weather_wrapped { get; set; } = new Layouts.Wrapped.Pocos.weather();

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Weather in a tabs and grouped in group box")]
        public Layouts.Tabbed.Pocos.weather weather_tabbed { get; set; } = new Layouts.Tabbed.Pocos.weather();

        [ReadOnce()]
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Weather structure set to read once")]
        public Layouts.Stacked.Pocos.weather weather_readOnce { get; set; } = new Layouts.Stacked.Pocos.weather();

        [ReadOnly()]
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Weather structure set to read only")]
        public Layouts.Stacked.Pocos.weather weather_readOnly { get; set; } = new Layouts.Stacked.Pocos.weather();
        public global::Pocos.example test_example { get; set; } = new global::Pocos.example();
        public MeasurementExample.Pocos.Measurements measurements { get; set; } = new MeasurementExample.Pocos.Measurements();
        public global::Pocos.ixcomponent ixcomponent { get; set; } = new global::Pocos.ixcomponent();
        public MonsterData.Pocos.Monster monster { get; set; } = new MonsterData.Pocos.Monster();
    }
}