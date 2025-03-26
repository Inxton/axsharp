NAMESPACE Enums
{S7.extern=ReadWrite}
    CLASS ClassWithEnums
        VAR PUBLIC              
            colors : Colors;            
            NamedValuesColors : NamedValuesColors;           
        END_VAR
        
    END_CLASS

    TYPE
        Colors : (Red, Green, Blue) := Red;
    END_TYPE

    TYPE
        NamedValuesColors : STRING (Red := '1', Green := '2', Blue := '3') := Red;
    END_TYPE
      
END_NAMESPACE

NAMESPACE misc
{S7.extern=ReadWrite}
    CLASS PUBLIC VariousMembers
        VAR PUBLIC
            _SomeClass : SomeClass;  
            _Motor : Motor;  
        END_VAR    
    END_CLASS    
    
    {S7.extern=ReadWrite}  
    CLASS SomeClass
        VAR PUBLIC            
            SomeClassVariable : STRING;
        END_VAR
    END_CLASS

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
END_NAMESPACE

NAMESPACE UnknownArraysShouldNotBeTraspiled
    {S7.extern=ReadWrite}
    CLASS ClassWithArrays
        VAR PUBLIC
            _complexKnown : ARRAY[0..10] OF Complex;
            _complexUnknown : ARRAY [0..10] OF ComplexUnknown;
            _primitive  : ARRAY[0..10] OF BYTE;
        END_VAR
        
    END_CLASS

    {S7.extern=ReadWrite}
    CLASS PUBLIC Complex 
        VAR PUBLIC
            HelloString : STRING;
            Id : ULINT;
        END_VAR        
    END_CLASS    
END_NAMESPACE