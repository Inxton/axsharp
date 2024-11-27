{#ix-attr:[Container(Layout.Stack)]}
{#ix-attr:[Group(GroupLayout.GroupBox)]}
{#ix-set:AttributeName = "Location"}
{S7.extern=ReadWrite}
CLASS  GeoLocation
    VAR PUBLIC
        {#ix-set:AttributeName = "Latitude [°]"}
        {#ix-set:AttributeMinimum = -90.0f}
        {#ix-set:AttributeMaximum = 90.0f}
        Latitude : REAL;
        {#ix-set:AttributeName = "Logitude [°]"}
        {#ix-set:AttributeMinimum = 0.0f}
        {#ix-set:AttributeMaximum = 180.0f}
        Longitude : REAL;
        {#ix-set:AttributeName = "Altitude [m]"}
        Altitude : REAL;
        {#ix-set:AttributeName = "Short descriptor"}
        Description : STRING[10];
        {#ix-set:AttributeName = "Long descriptor"}
        LongDescription : STRING[254];
    END_VAR    
END_CLASS