{S7.extern=ReadWrite}
CLASS stTestRenderIgnore
    VAR PUBLIC
        {#ix-attr:[RenderIgnore()]}
        {#ix-set:AttributeName = "<#Integer From PLC#>"}	
        testInteger : INT;
        {#ix-set:AttributeName = "<#UInteger From PLC#>"}	
        testUInteger : INT;
        {#ix-set:AttributeName = "<#STRING From PLC#>"}	
        testString : STRING;
        {#ix-set:AttributeName = "<#WORD From PLC#>"}	
        testWord : WORD;
        {#ix-set:AttributeName = "<#BYTE From PLC#>"}	
        testByte : BYTE;
        {#ix-set:AttributeName = "<#REAL From PLC#>"}	
        testReal : REAL;
        {#ix-set:AttributeName = "<#LREAL From PLC#>"}	
        testLReal : LREAL;
        {#ix-attr:[RenderIgnore("Display","ShadowDisplay")]}
        {#ix-set:AttributeName = "<#BOOL From PLC#>"}	
        testBool : BOOL;
        {#ix-attr:[RenderIgnore("Control","ShadowControl")]}
        {#ix-set:AttributeName = "<#DATE From PLC#>"}
        TestDate : DATE;	
        {#ix-set:AttributeName = "<#DATE_AND_TIME From PLC#>"}	
        TestDateTime : DATE_AND_TIME;	
    END_VAR
    
END_CLASS