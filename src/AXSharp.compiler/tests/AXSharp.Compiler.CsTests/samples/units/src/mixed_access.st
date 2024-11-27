CONFIGURATION config
    VAR_GLOBAL
        {S7.extern=ReadWrite}
        MotorOn : BOOL;             // variable can be read and written
		
        {S7.extern=ReadOnly}
        MotorState: INT;            // variable can only be read
		
        MotorFlags: UINT;           // variable is not accessible by an external system (default: hidden)
		
        {S7.extern=ReadWrite}
        Motor1 : Motor;        // the variables in Motor1 be read and written if enabled

        {S7.extern=ReadOnly}
        Motor2 : Motor;        // the variables in Motor2 be only read if enabled

        {S7.extern=ReadWrite}
        s1 : struct1;
        {S7.extern=ReadWrite}
        s4 : struct4;

        {S7.extern=ReadWrite}
        mot1 : SpecificMotorA;
    END_VAR
END_CONFIGURATION

CLASS Motor
    VAR PUBLIC
        {S7.extern=ReadWrite}
        Run               : BOOL;
        ReverseDirection  : BOOL;
    END_VAR
    VAR INTERNAL
        {S7.extern=ReadOnly}
        ActualVelocity    : LReal;
    END_VAR
    // ...
END_CLASS

TYPE 
    struct1 : STRUCT  {S7.extern=ReadWrite}  s2 : struct2;   END_STRUCT;
    struct2 : STRUCT  {S7.extern=ReadOnly}   s3 : struct3;   END_STRUCT;
    struct3 : STRUCT  {S7.extern=ReadWrite}  s4 : struct4;   END_STRUCT;
    struct4 : STRUCT  {S7.extern=ReadWrite}  s5 : INT;       END_STRUCT;
END_TYPE

{S7.extern=ReadWrite}
CLASS ABSTRACT AbstractMotor
    VAR PUBLIC
        Run               : BOOL;
        ReverseDirection  : BOOL;
    END_VAR
    ...
END_CLASS

{S7.extern=ReadWrite}
CLASS GenericMotor  EXTENDS AbstractMotor
    VAR INTERNAL
        {S7.extern=ReadOnly}
        ActualVelocity    : LReal;
    END_VAR
    ...
END_CLASS

{S7.extern=ReadWrite}
CLASS SpecificMotorA  EXTENDS GenericMotor
    VAR INTERNAL
        {S7.extern=ReadWrite}
        MaxAcceleration    : LReal;
    END_VAR
    ...
END_CLASS