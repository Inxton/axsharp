
    {#ix-attr:[Container(Layout.Stack)]}
    {#ix-attr:[Group(GroupLayout.GroupBox)]}    
    CLASS PUBLIC weatherBase
      VAR PUBLIC
        Latitude : REAL;       
        Longitude : REAL;       
        Altitude : REAL;                
        Description : STRING;    
        {#ix-attr:[ReadOnly()]}   
        LongDescription : STRING;
        {#ix-attr:[ReadOnce()]} 
        {#ix-set:AttributeName = "this has [ReadOnce()] attribute will be readon only once..."} 
        StartCounter : INT;

        {#ix-attr:[RenderIgnore()]} 
        {#ix-set:AttributeName = "[RenderIgnore()] must not be displayed!"} 
        RenderIgnoreAllToghether : STRING;
        {#ix-set:AttributeName = "[RenderIgnore(''Control'')]"} 
        {#ix-attr:[RenderIgnore("Control")]} 
        RenderIgnoreWhenControl : STRING;
        {#ix-set:AttributeName = "[RenderIgnore(''Display'')]"} 
        {#ix-attr:[RenderIgnore("Display")]} 
        RenderIgnoreWhenDisplay : STRING;
        {#ix-set:AttributeName = "[RenderIgnore(''Control'', ''ShadowControl'')]"} 
        {#ix-attr:[RenderIgnore("Control", "ShadowControl")]} 
        RenderIgnoreWhenControlAndShadow : STRING;
        {#ix-set:AttributeName = "[RenderIgnore(''Display'', ''ShadowDisplay'')]"} 
        {#ix-attr:[RenderIgnore("Display", "ShadowDisplay")]} 
        RenderIgnoreWhenDisplayAndShadow : STRING;
      END_VAR  
    END_CLASS    
