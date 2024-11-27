{#ix[Container(Layout.Tabs)]}
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

CLASS weathers
    VAR PUBLIC
        i : ARRAY[0..50] OF weatherBase;
    END_VAR    
END_CLASS

TYPE
    Feeling : (Freezing, Cold, Lookworm, Warm, Hot);
END_TYPE

TYPE
    Colors : INT (
        RED := 12,
        GREEN := 14,
        BLUE := 23
    );
END_TYPE