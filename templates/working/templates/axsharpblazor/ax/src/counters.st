
{S7.extern=ReadWrite}
CLASS Counters
    VAR PUBLIC
        {#ix-attr:[Container(Layout.Wrap)]}    
        wrap : Counter;
        {#ix-attr:[Container(Layout.Stack)]}    
        stack : Counter;
        {#ix-attr:[Container(Layout.UniformGrid)]}    
        grid : Counter;
        {#ix-attr:[Container(Layout.Tabs)]}    
        tabs : Counter;
    END_VAR    
END_CLASS

{S7.extern=ReadWrite}
CLASS PUBLIC Counter
    VAR PUBLIC
        {#ix-set:AttributeName = "Number of revolutions"}
        Counts : ULINT;
        {#ix-set:AttributeName = "Count revolutions"}
        AllowCounter : BOOL;
        {#ix-set:AttributeName = "Reset counts"}
        ResetCounter : BOOL;
    END_VAR

    METHOD PUBLIC RunCounter        
        IF (AllowCounter) THEN
            Counts := Counts + ULINT#1;    
        END_IF;  
        
        IF(ResetCounter) THEN
            Counts := ULINT#0;
            ResetCounter := FALSE;
        END_IF;    
    END_METHOD    
END_CLASS