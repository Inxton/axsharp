{S7.extern=ReadWrite}
CLASS stTestComplex
    VAR PUBLIC
        {#ix-set:AttributeName = "<#Integer From PLC#>"}	
        testInteger : INT;
        {#ix-set:AttributeName = "<#Complex Instance#>"}	
        testComplexInstance : stComplex := (stComplexInteger := 33, stComplexString := 'hello world');
        {#ix-set:AttributeName = "<#String From PLC#>"}	
        testString : STRING;
        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-set:AttributeName = "<#Complex Instance#>"}	
        testComplexUnknownInstance : stComplexUnknown; 
    END_VAR
    
END_CLASS