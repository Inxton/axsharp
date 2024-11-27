{S7.extern=ReadWrite}
CLASS stComplexUnknown
    VAR PUBLIC
        {#ix-set:AttributeName = "<#String unknown From PLC#>"}	
        stComplexUnknownString : STRING;
        {#ix-set:AttributeName = "<#BOOL unknown From PLC#>"}	
        testBool : BOOL;
        {#ix-set:AttributeName = "<#Integer unknown From PLC#>"}	
        stComplexUnknowInteger : INT;
        {#ix-set:AttributeName = "<#DATE unknown From PLC#>"}
        TestDate : DATE;	
    END_VAR    
END_CLASS