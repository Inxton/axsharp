NAMESPACE makereadonce
    {S7.extern=ReadWrite}
    CLASS PUBLIC MembersWithMakeReadOnce
        VAR PUBLIC
            {#ix-attr:[ReadOnce()]}
            makeReadOnceMember : STRING;
            someOtherMember : STRING;

            {#ix-attr:[ReadOnce()]}
            makeReadComplexMember : ComplexMember;
            someotherComplexMember : ComplexMember;
        END_VAR    
    END_CLASS    
    {S7.extern=ReadWrite}
    CLASS PUBLIC ComplexMember
        VAR PUBLIC            
            someMember : STRING;
            someOtherMember : STRING;
        END_VAR    
    END_CLASS    
END_NAMESPACE
