{S7.extern=ReadWrite}
{#ix-attr:[Container(Layout.Stack)]}
CLASS prgWeatherStations
    VAR PUBLIC
        {#ix-attr:[Container(Layout.Tabs)]}
        {#ix-set:AttributeName = "Weather Cyclic"}
        _weatherStations : fbWorldWeatherWatch;	
        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-attr:[Group(GroupLayout.GroupBox)]}
        {#ix-set:AttributeName = "What plc says"}	
        PlcCommentOnCurrentWeather : STRING[254] := 'TEST';	
    END_VAR    
END_CLASS