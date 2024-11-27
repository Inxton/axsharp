PROGRAM MyProgram
    VAR_EXTERNAL
        all_primitives : all_primitives;        
        weather_stacked : Layouts.Stacked.weather;        
        weather_wrapped : Layouts.Wrapped.weather;        
        weather_tabbed : Layouts.Tabbed.weather;
        weather_readOnce : Layouts.Stacked.weather;
        test_example: example;
    END_VAR

    VAR

    END_VAR

    VAR_TEMP

    END_VAR    
    all_primitives.myINT := all_primitives.myINT + 1;    
    weather_stacked.Altitude := all_primitives.myINT;
    weather_wrapped.Altitude := all_primitives.myINT;    
    weather_tabbed.Altitude := all_primitives.myINT; 
    weather_readOnce.Altitude := all_primitives.myINT; 

    weather_stacked.StartCounter := all_primitives.myINT;
    weather_wrapped.StartCounter := all_primitives.myINT;
    weather_tabbed.StartCounter := all_primitives.myINT;    
    weather_readOnce.StartCounter := all_primitives.myINT; 

    test_example.ReferenceToComponent();
    
END_PROGRAM
