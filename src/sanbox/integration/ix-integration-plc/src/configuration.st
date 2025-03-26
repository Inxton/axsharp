USING MonsterData;
CONFIGURATION MyConfiguration
    TASK Main(Interval := T#10ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL
        {S7.extern=ReadWrite}
        all_primitives : all_primitives;
        {S7.extern=ReadWrite}
        weather : weather;
        {S7.extern=ReadWrite}
        weathers : weathers;
        {S7.extern=ReadWrite}
        {#ix-set:AttributeName = "Weather in a stack pannel and grouped in group box"}
        weather_stacked : Layouts.Stacked.weather;
        {S7.extern=ReadWrite}
        {#ix-set:AttributeName = "Weather in a wrap pannel and grouped in group box"}
        weather_wrapped : Layouts.Wrapped.weather;
        {S7.extern=ReadWrite}
        {#ix-set:AttributeName = "Weather in a tabs and grouped in group box"}
        weather_tabbed : Layouts.Tabbed.weather;
        {S7.extern=ReadWrite}
        {#ix-set:AttributeName = "Weather structure set to read once"}
        {#ix-attr:[ReadOnce()]}
        weather_readOnce : Layouts.Stacked.weather;
        {S7.extern=ReadWrite}
        {#ix-set:AttributeName = "Weather structure set to read only"}
        {#ix-attr:[ReadOnly()]}
        {S7.extern=ReadWrite}
        weather_readOnly : Layouts.Stacked.weather;

        {S7.extern=ReadWrite}
        test_example: example;
        {S7.extern=ReadWrite}
        measurements : MeasurementExample.Measurements;
        {S7.extern=ReadWrite}
        ixcomponent : ixcomponent;
        {S7.extern=ReadWrite}
        monster : Monster;
    END_VAR
END_CONFIGURATION
