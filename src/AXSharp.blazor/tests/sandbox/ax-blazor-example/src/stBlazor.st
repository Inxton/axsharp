{S7.extern=ReadWrite}
CLASS stBlazor
    VAR PUBLIC
        {#ix-set:AttributeName = "<#Integer From PLC#>"}	
        testInteger : INT;
        {#ix-set:AttributeName = "testEnum"}
        testEnum : stTestEnum;
        {#ix-set:AttributeName = "<#String From PLC#>"}	
        testString : STRING;
        testReal : REAL := REAL#592.25;
        {#ix-attr:[RenderIgnore("Display")]}
        testLReal : LREAL := 42.2;
        testBool : BOOL;
        {#ix-set:AttributeName = "Complex"}
        {#ix-attr:[RenderIgnore()]}
        complexInstance : stComplex := (stComplexInteger := 33, stComplexString := 'hello world');
        {#ix-set:AttributeName = "testInstance1"}
        testInstance : stTest3;
        {#ix-set:AttributeName = "testInstance2"}
        testInstance2 : stTest3;
        {#ix-set:AttributeName = "testInstance3"}
        testInstance3 : stTest;
    END_VAR
    
END_CLASS