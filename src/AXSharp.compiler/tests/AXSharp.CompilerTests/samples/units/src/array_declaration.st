NAMESPACE ArrayDeclarationSimpleNamespace
    CLASS array_declaration_class
        VAR PUBLIC
            primitive : ARRAY[1..100] OF INT;
            complex : ARRAY[1..100] OF some_complex_type;
        END_VAR        
    END_CLASS

    CLASS some_complex_type
        VAR
            
        END_VAR
        
    END_CLASS

END_NAMESPACE