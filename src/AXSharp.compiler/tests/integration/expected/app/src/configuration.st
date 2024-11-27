CONFIGURATION MyConfiguration
    TASK Main(Interval := T#1000ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL
	{S7.extern=ReadWrite}
        lib1_MyClass : lib1.MyClass;
	{S7.extern=ReadWrite}
        lib2_MyClass : lib2.MyClass;
    END_VAR
END_CONFIGURATION
