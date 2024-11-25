using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;
using Pocos.MonsterData;

namespace Pocos
{
    public partial class ix_integration_plcTwinController
    {
        public all_primitives all_primitives { get; set; } = new all_primitives();
        public weather weather { get; set; } = new weather();
        public weathers weathers { get; set; } = new weathers();
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Weather in a stack pannel and grouped in group box")]
        public Layouts.Stacked.weather weather_stacked { get; set; } = new Layouts.Stacked.weather();
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Weather in a wrap pannel and grouped in group box")]
        public Layouts.Wrapped.weather weather_wrapped { get; set; } = new Layouts.Wrapped.weather();
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Weather in a tabs and grouped in group box")]
        public Layouts.Tabbed.weather weather_tabbed { get; set; } = new Layouts.Tabbed.weather();
        [ReadOnce()]
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Weather structure set to read once")]
        public Layouts.Stacked.weather weather_readOnce { get; set; } = new Layouts.Stacked.weather();
        [ReadOnly()]
        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Weather structure set to read only")]
        public Layouts.Stacked.weather weather_readOnly { get; set; } = new Layouts.Stacked.weather();
        public example test_example { get; set; } = new example();
        public MeasurementExample.Measurements measurements { get; set; } = new MeasurementExample.Measurements();
        public ixcomponent ixcomponent { get; set; } = new ixcomponent();
        public MonsterData.Monster monster { get; set; } = new MonsterData.Monster();
    }
}