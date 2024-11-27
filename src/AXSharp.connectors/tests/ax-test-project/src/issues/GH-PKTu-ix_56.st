// https://github.com/ix-ax/axsharp/issues/56
NAMESPACE GH.PKTu.ix_56
    {S7.extern=ReadWrite}
    CLASS ComplexMember
        VAR PUBLIC
            Counter : INT;
        END_VAR
        
    END_CLASS
    {S7.extern=ReadWrite}
    CLASS Base
        VAR PUBLIC
            baseMember : STRING;
            baseComplexMember : ComplexMember;
            BaseDavid : DavidBase;
        END_VAR        
    END_CLASS
    {S7.extern=ReadWrite}
    CLASS FirstInheritance EXTENDS Base
        VAR PUBLIC
            FirstInheritanceMember : STRING;
            FirstInheritanceComplexMember : ComplexMember;
            FirstDavid : DavidBase;
        END_VAR        
    END_CLASS
    {S7.extern=ReadWrite}
    CLASS SecondInheritance EXTENDS FirstInheritance
        VAR PUBLIC
            SecondInheritanceMember : STRING;
            SecondInheritanceComplexMember : ComplexMember;
            SecodnDavid : DavidBase;
        END_VAR        
    END_CLASS
    {S7.extern=ReadWrite}
    CLASS PedroBase
        VAR PUBLIC
            p : STRING;
        END_VAR        
    END_CLASS
    {S7.extern=ReadWrite}
    CLASS DavidBase EXTENDS PedroBase
        VAR PUBLIC
            d : STRING;
        END_VAR        
    END_CLASS
END_NAMESPACE