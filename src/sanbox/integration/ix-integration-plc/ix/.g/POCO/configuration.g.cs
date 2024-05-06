using System;

namespace Pocos
{
    using MonsterData;

    public partial class ix_integration_plcTwinController
    {
        public all_primitives all_primitives { get; set; } = new all_primitives();
        public weather weather { get; set; } = new weather();
        public weathers weathers { get; set; } = new weathers();
        public Layouts.Stacked.weather weather_stacked { get; set; } = new Layouts.Stacked.weather();
        public Layouts.Wrapped.weather weather_wrapped { get; set; } = new Layouts.Wrapped.weather();
        public Layouts.Tabbed.weather weather_tabbed { get; set; } = new Layouts.Tabbed.weather();
        public Layouts.Stacked.weather weather_readOnce { get; set; } = new Layouts.Stacked.weather();
        public Layouts.Stacked.weather weather_readOnly { get; set; } = new Layouts.Stacked.weather();
        public example test_example { get; set; } = new example();
        public MeasurementExample.Measurements measurements { get; set; } = new MeasurementExample.Measurements();
        public ixcomponent ixcomponent { get; set; } = new ixcomponent();
        public MonsterData.Monster monster { get; set; } = new MonsterData.Monster();
    }
}