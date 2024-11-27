NAMESPACE makereadonly
    {S7.extern=ReadWrite}
    CLASS PUBLIC MembersWithMakeReadOnly
        VAR PUBLIC
            {#ix-attr:[ReadOnly()]}
            makeReadOnceMember : STRING;
            someOtherMember : STRING;

            {#ix-attr:[ReadOnly()]}
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
