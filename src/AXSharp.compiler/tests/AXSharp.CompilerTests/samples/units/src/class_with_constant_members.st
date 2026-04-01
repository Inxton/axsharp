NAMESPACE ClassWithConstantMembersNamespace
    {S7.extern=ReadWrite}
    CLASS PUBLIC ClassWithConstantMembers
        VAR PUBLIC
           myNonConstant : INT;
        END_VAR
        VAR CONSTANT
           myConstant : INT := 10;
           myConstantString : STRING := 'hello';
        END_VAR
    END_CLASS
END_NAMESPACE