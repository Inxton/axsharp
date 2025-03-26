NAMESPACE NamedValuesNamespace
    TYPE
        LightColors : INT (
            LRED := 12,
            LGREEN := 14,
            LBLUE := 23
        );
    END_TYPE    
    
    {S7.extern=ReadWrite}
    CLASS PUBLIC using_type_named_values
        VAR PUBLIC
            LColors : LightColors;
        END_VAR        
    END_CLASS
END_NAMESPACE
