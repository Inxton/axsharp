NAMESPACE TypeWithNameAttributes    
    TYPE
        {#ix-attr:[Container(Layout.Wrap)]}
        Motor : STRUCT           
            isRunning : BOOL;
        END_STRUCT;
        Vehicle : STRUCT
            m : Motor;
            displacement : INT;
        END_STRUCT;
    END_TYPE

    {#ix-prop:public string AttributeName}    
    CLASS NoAccessModifierClass
        VAR PUBLIC            
            SomeClassVariable : STRING;
        END_VAR
    END_CLASS
END_NAMESPACE