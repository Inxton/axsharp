{S7.extern=ReadWrite}
{#ix-attr:[Container(Layout.UniformGrid)]}
CLASS TestLayoutOverwriting
    VAR PUBLIC
        ix_bool : BOOL;
        ix_int : INT;
        ix_string : STRING;

        {#ix-attr:[Container(Layout.Wrap)]}
        simple: TestSimple;

        {#ix-attr:[Container(Layout.Wrap)]}
        weather: prgWeatherStations;
    END_VAR
    
END_CLASS