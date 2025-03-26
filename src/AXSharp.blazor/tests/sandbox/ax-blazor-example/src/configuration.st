CONFIGURATION MyConfiguration
    TASK Main(Interval := T#1000ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL
        {S7.extern=ReadWrite}
        testingProgram : testingProgram; 
        {S7.extern=ReadWrite}
        prgWeatherStations : prgWeatherStations;
        {S7.extern=ReadWrite}
        MAINC : MAIN;       
    END_VAR
END_CONFIGURATION
