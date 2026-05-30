TYPE
    {S7.extern=ReadWrite}
    StructSimpleMotor : STRUCT
        isRunning : BOOL;
    END_STRUCT;
    {S7.extern=ReadWrite}
    StructSimpleVehicle : STRUCT
        m : StructSimpleMotor;
        displacement : INT;
    END_STRUCT;
END_TYPE