{#ix[Container(Layout.Tabs)]}
{S7.extern=ReadWrite}
CLASS weather
    VAR PUBLIC                        
        GeoLocation : GeoLocation;               
        {#ix[Container(Layout.Wrap)]}
        Temperature : REAL;
        Humidity : REAL;
        Location : STRING;  
        ChillFactor : REAL;
        Feeling : Feeling;
    END_VAR    
END_CLASS

{S7.extern=ReadWrite}
CLASS weathers
    VAR PUBLIC
        i : ARRAY[0..50] OF weatherBase;
    END_VAR    
END_CLASS

TYPE
    Feeling : (Freezing, Cold, Lookworm, Warm, Hot);
END_TYPE

TYPE
    {S7.extern=ReadWrite}
    Colors : INT (
        RED := 12,
        GREEN := 14,
        BLUE := 23
    );
END_TYPE