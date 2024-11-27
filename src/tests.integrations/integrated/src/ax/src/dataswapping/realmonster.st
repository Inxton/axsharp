NAMESPACE RealMonsterData
    {S7.extern=ReadWrite}
    CLASS RealMonsterBase
        VAR PUBLIC
            Description : STRING;
            Id : ULINT;
            TestDate : DATE;
            TestDateTime: DATE_AND_TIME;
            TestTimeSpan: TIME_OF_DAY;
            ArrayOfBytes : ARRAY[0..3] OF BYTE;
            ArrayOfDrives : ARRAY[0..3] OF DriveBaseNested;
        END_VAR        
    END_CLASS

    {S7.extern=ReadWrite}
    CLASS RealMonster EXTENDS RealMonsterBase
        VAR PUBLIC
            DriveA : DriveBaseNested;
        END_VAR        
    END_CLASS

    {S7.extern=ReadWrite}
    CLASS DriveBaseNested
        VAR PUBLIC
            Position : LREAL;
            Velo : LREAL;
            Acc : LREAL;
            Dcc : LREAL;
            NestedLevelOne : NestedLevelOne;            
        END_VAR        
    END_CLASS

    {S7.extern=ReadWrite}
    CLASS NestedLevelOne
        VAR PUBLIC
            Position : LREAL;
            Velo : LREAL;
            Acc : LREAL;
            Dcc : LREAL;     
            NestedLevelTwo: NestedLevelTwo;       
        END_VAR        
    END_CLASS

    {S7.extern=ReadWrite}
    CLASS NestedLevelTwo
        VAR PUBLIC
            Position : LREAL;
            Velo : LREAL;
            Acc : LREAL;
            Dcc : LREAL;   
            NestedLevelThree: NestedLevelThree;         
        END_VAR        
    END_CLASS

    {S7.extern=ReadWrite}
    CLASS NestedLevelThree
        VAR PUBLIC
            Position : LREAL;
            Velo : LREAL;
            Acc : LREAL;
            Dcc : LREAL;            
        END_VAR        
    END_CLASS
    
END_NAMESPACE