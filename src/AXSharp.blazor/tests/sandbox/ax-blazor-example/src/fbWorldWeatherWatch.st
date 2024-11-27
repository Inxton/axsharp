{S7.extern=ReadWrite}
CLASS fbWorldWeatherWatch
    VAR PUBLIC
        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-set:AttributeName = "North pole station"}
        //{#ix-set:OpenMap = "https://www.openstreetmap.org/?mlat=74.652&mlon=-95.556#map=6/74.652/-95.556"}
        NorthPole : structWeatherStation := (StationICAO := 'CYRB');
        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-set:AttributeName = "South pole station"}
        //{#ix-set:OpenMap = "https://www.openstreetmap.org/?mlat=-85.05113&mlon=-16.80701#map=1/-85.05113/-16.80701"}
        SouthPole : structWeatherStation := (StationICAO := 'NZSP');
        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-set:AttributeName = "Verl, Germany"}
        //{#ix-set:OpenMap = "https://www.openstreetmap.org/?mlat=51.5799&mlon=8.415#map=6/51.5799/8.4155"}
        Verl : structWeatherStation := (StationICAO := 'EDLP');
        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-set:AttributeName = "Kriva, Slovakia"}
        //{#ix-set:OpenMap = "https://www.openstreetmap.org/?mlat=48.1723&mlon=17.2158#map=6/48.1723/17.2158"}
        Kriva : structWeatherStation := (StationICAO := 'LZIB');
    END_VAR
    
END_CLASS