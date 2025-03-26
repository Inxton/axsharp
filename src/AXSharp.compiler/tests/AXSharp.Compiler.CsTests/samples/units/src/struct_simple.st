TYPE
    {S7.extern=ReadWrite}
    Motor : STRUCT
        isRunning : BOOL;
    END_STRUCT;
    {S7.extern=ReadWrite}
    Vehicle : STRUCT
        m : Motor;
        displacement : INT;
    END_STRUCT;
END_TYPE