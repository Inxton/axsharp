{S7.extern=ReadWrite}
CLASS stExample
    VAR PUBLIC
        {#ix-attr:[Container(Layout.Tabs)]}
        {#ix-attr:[Group(GroupLayout.GroupBox)]}
        {#ix-set:AttributeName = "Group name"}
        testInteger : INT;
        testEnum : stTestEnum;
        testString : STRING := 'Hello World';
        testBool : BOOL;
    END_VAR
    
END_CLASS