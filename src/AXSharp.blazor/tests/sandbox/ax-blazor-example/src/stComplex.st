{S7.extern=ReadWrite}
CLASS stComplex
    VAR PUBLIC
        {#ix-set:AttributeName = "<#Integer From PLC#>"}	
        stComplexInteger : INT;
        {#ix-set:AttributeName = "<#String From PLC#>"}	
        stComplexString : STRING;
    END_VAR
    
END_CLASS