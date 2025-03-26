NAMESPACE TypesWithPropertyAttributes
    {#ix-prop:public string Description}
    {#ix-set:Description = "Some added property name value"}
    {S7.extern=ReadWrite}
    CLASS SomeAddedProperties
        VAR PUBLIC
            {#ix-set:AttributeName = "Pocitadlo"}
            Counter : INT;
        END_VAR    
    END_CLASS        
END_NAMESPACE