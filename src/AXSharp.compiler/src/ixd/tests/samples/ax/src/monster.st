NAMESPACE MonsterData
    CLASS MonsterBase
        VAR PUBLIC
            ArrayOfBytes : ARRAY[0..3] OF BYTE;
            ArrayOfDrives : ARRAY[0..3] OF DriveBase;
            ArrayOfIxComponent : ARRAY[0..3] OF ixcomponent;
            BaseInt : INT;
        END_VAR      
        
        METHOD PUBLIC Start : INT
            VAR_INPUT
                value : INT;
                value2 : INT;
            END_VAR
    
            BaseInt := value;
        END_METHOD
    END_CLASS
    CLASS Monster EXTENDS MonsterBase
        VAR PUBLIC
            DriveA : DriveBase;
        END_VAR        
    END_CLASS

    CLASS DriveBase
        VAR PUBLIC
            Position : LREAL;
            Velo : LREAL;
            Acc : LREAL;
            Dcc : LREAL;            
        END_VAR        
    END_CLASS
END_NAMESPACE