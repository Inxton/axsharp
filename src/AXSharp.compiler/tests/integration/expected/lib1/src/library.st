NAMESPACE lib1
    {S7.extern=ReadWrite}
    CLASS PUBLIC MyClass
        VAR PUBLIC
            MyString : STRING;
            MyInt : INT;
        END_VAR

        VAR PRIVATE

        END_VAR

        METHOD PUBLIC MyMethod : BOOL
            MyMethod := FALSE;
        END_METHOD
    END_CLASS

END_NAMESPACE
