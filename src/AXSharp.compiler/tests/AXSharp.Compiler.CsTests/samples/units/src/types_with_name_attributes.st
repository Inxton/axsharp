NAMESPACE TypeWithNameAttributes    
    TYPE
        {#ix-attr:[Container(Layout.Wrap)]}
        {S7.extern=ReadWrite}
        Motor : STRUCT           
            isRunning : BOOL;
        END_STRUCT;
        {S7.extern=ReadWrite}
        Vehicle : STRUCT
            m : Motor;
            displacement : INT;
        END_STRUCT;
    END_TYPE

    {#ix-prop:public string AttributeName}    
    {S7.extern=ReadWrite}
    CLASS NoAccessModifierClass
        VAR PUBLIC            
            SomeClassVariable : STRING;
        END_VAR
    END_CLASS
END_NAMESPACE