NAMESPACE TypeWithNameAttributes    
    TYPE
        {#ix-attr:[Container(Layout.Wrap)]}
        {S7.extern=ReadWrite}
        TypesNameAttrMotor : STRUCT           
            isRunning : BOOL;
        END_STRUCT;
        {S7.extern=ReadWrite}
        TypesNameAttrVehicle : STRUCT
            m : TypesNameAttrMotor;
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