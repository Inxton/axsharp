USING MonsterData;
CONFIGURATION MyConfiguration
    TASK Main(Interval := T#10ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL
        all_primitives : all_primitives;
        weather : weather;
        weathers : weathers;
        {#ix-set:AttributeName = "Weather in a stack pannel and grouped in group box"}
        weather_stacked : Layouts.Stacked.weather;
        {#ix-set:AttributeName = "Weather in a wrap pannel and grouped in group box"}
        weather_wrapped : Layouts.Wrapped.weather;
        {#ix-set:AttributeName = "Weather in a tabs and grouped in group box"}
        weather_tabbed : Layouts.Tabbed.weather;
        {#ix-set:AttributeName = "Weather structure set to read once"}
        {#ix-attr:[ReadOnce()]}
        weather_readOnce : Layouts.Stacked.weather;
        {#ix-set:AttributeName = "Weather structure set to read only"}
        {#ix-attr:[ReadOnly()]}
        weather_readOnly : Layouts.Stacked.weather;

        test_example: example;
        measurements : MeasurementExample.Measurements;
        monster : Monster;
    END_VAR
END_CONFIGURATION

