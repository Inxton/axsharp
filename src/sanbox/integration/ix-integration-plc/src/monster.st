NAMESPACE MonsterData
    {S7.extern=ReadWrite}
    CLASS MonsterBase
        VAR PUBLIC
            ArrayOfBytes : ARRAY[0..3] OF BYTE;
            ArrayOfDrives : ARRAY[0..3] OF DriveBase;
            ArrayOfIxComponent : ARRAY[0..3] OF ixcomponent;
        END_VAR        
    END_CLASS

    {S7.extern=ReadWrite}
    CLASS Monster EXTENDS MonsterBase
        VAR PUBLIC
            DriveA : DriveBase;
        END_VAR        
    END_CLASS
    
    {S7.extern=ReadWrite}
    CLASS DriveBase
        VAR PUBLIC
            Position : LREAL;
            Velo : LREAL;
            Acc : LREAL;
            Dcc : LREAL;            
        END_VAR        
    END_CLASS
END_NAMESPACE