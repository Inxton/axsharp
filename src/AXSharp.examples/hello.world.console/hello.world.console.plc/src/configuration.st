CONFIGURATION MyConfiguration
    TASK Main(Interval := T#1000ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL
        {S7.extern=ReadWrite}
        Counter : ULINT;
        {S7.extern=ReadWrite}
        HelloWorld : STRING := 'Hello world';
    END_VAR
END_CONFIGURATION
