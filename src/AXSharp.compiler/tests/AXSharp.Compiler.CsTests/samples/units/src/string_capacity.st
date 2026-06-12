NAMESPACE StringCapacityNamespace
    {S7.extern=ReadWrite}
    CLASS PUBLIC ClassWithDeclaredStringCapacities
        VAR PUBLIC
            myString10      :   STRING[10];
            myWString10     :   WSTRING[10];
            myString        :   STRING;
            myWString       :   WSTRING;
            myWString1000   :   WSTRING[1000];
            myString10Array :   ARRAY[0..3] OF STRING[10];
        END_VAR
    END_CLASS
END_NAMESPACE
