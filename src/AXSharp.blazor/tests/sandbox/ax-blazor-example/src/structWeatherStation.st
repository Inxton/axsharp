{S7.extern=ReadWrite}
CLASS structWeatherStation
    VAR PUBLIC
        {#ix-set:AttributeName = "Station name (ICAO)"}	
        StationICAO : STRING[4];
        {#ix-set:AttributeName = "Station status"}
        StationStatus : enumStationStatus; 
        {#ix-set:AttributeName = "Dew Point"}	
        {#ix-set:AttributeUnits = "°C"}
        {#ix-attr:[RenderIgnore()]}
        DewPoint : REAL;
        {#ix-set:AttributeName = "Pressure"}
        {#ix-set:AttributeUnits = "Torr"}	
        Pressure : REAL;
        {#ix-set:AttributeName = "Temperature"}
        {#ix-set:AttributeUnits = "°C"}
        Temp : REAL;
        {#ix-set:AttributeName = "Visibility"}
        {#ix-set:AttributeUnits = "km"}	
        Visibility : REAL;			
        {#ix-set:AttributeName = "Wind heading"}
        {#ix-set:AttributeUnits = "Azimuth"}	
        WindHeading : UINT;	
        {#ix-set:AttributeName = "Wind speed"}
        {#ix-set:AttributeUnits = "m/s"}	
        WindSpeed : REAL;	
        TestDate : DATE;	
        TestDateTime : DATE_AND_TIME;	
        TestTimeOfDay : TIME_OF_DAY;	
    END_VAR
    
END_CLASS