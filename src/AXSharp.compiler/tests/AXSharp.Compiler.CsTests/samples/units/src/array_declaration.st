NAMESPACE ArrayDeclarationSimpleNamespace
    {S7.extern=ReadWrite}
    CLASS array_declaration_class
        VAR PUBLIC
            primitive : ARRAY[1..100] OF INT;
            complex : ARRAY[1..100] OF some_complex_type;
            // multidimensional arrays should not be transpiled
            complex_multidim : ARRAY[1..100, 0..30] OF some_complex_type;
            primitive_multidim : ARRAY[1..100, 0..100, 0..300] OF INT;
        END_VAR        
    END_CLASS
    {S7.extern=ReadWrite}
    CLASS some_complex_type
        VAR
            
        END_VAR
        
    END_CLASS

END_NAMESPACE