PROGRAM MyProgram
    VAR_EXTERNAL
        Counter : ULINT;
        HelloWorld : STRING;
    END_VAR
    
    Counter := Counter + ULINT#1;
    IF(HelloWorld = 'RESET-COUNTER') THEN
        Counter := ULINT#0;
    END_IF;    
END_PROGRAM
