{S7.extern=ReadWrite}
CLASS stMultipleLayouts
    VAR PUBLIC
        {#ix-attr:[Container(Layout.Tabs)]}
        {#ix-set:AttributeName = "With One Group Layout"}	
        Servo_S5: TestStructOneGroupWithLayout;	
    
        {#ix-attr:[Container(Layout.Tabs)]}
        {#ix-set:AttributeName = "Without Group Layout"}	
        Servo_S6: TestStruct;	
    
        {#ix-attr:[Container(Layout.Tabs)]}
        {#ix-set:AttributeName = "With Multiple Groups "}	
        Servo_S7: TestStructMultipleGroups;	
        
        {#ix-attr:[Container(Layout.Tabs)]}
        {#ix-set:AttributeName = "TestStruct With Main Layout  "}	
        Servo_S8: TestStructWithMainLayout;	
    
        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-set:AttributeName = "A1"}
        Piston_A1 : STRING;
        {#ix-set:AttributeName = "A2"}
        Piston_A2 : STRING;
        {#ix-set:AttributeName = "A3"}
        Piston_A3 : STRING;
        {#ix-set:AttributeName = "A4"}
        Piston_A4 : STRING;
        
        {#ix-attr:[Container(Layout.Wrap)]}
        {#ix-set:AttributeName = "A5"}
        Piston_A21 : INT;
        {#ix-set:AttributeName = "A6"}
        Piston_A22 : INT;
        {#ix-set:AttributeName = "A7"}
        Piston_A23 : INT;
        {#ix-set:AttributeName = "A8"}
        Piston_A24 : INT;
        
        {#ix-attr:[Container(Layout.Tabs)]}
        Piston_A31 : REAL;
        {#ix-set:AttributeName = "A8"}
        Piston_A32 : REAL;
        {#ix-set:AttributeName = "A9"}
        Piston_A33 : REAL;
        {#ix-set:AttributeName = "A10"}
        Piston_A34 : REAL;
        
        {#ix-attr:[Container(Layout.UniformGrid)]}
        Piston_A41 : BOOL;
        {#ix-set:AttributeName = "A11"}
        Piston_A42 : BOOL;
        {#ix-set:AttributeName = "A12"}
        Piston_A43 : BOOL;
        {#ix-set:AttributeName = "A13"}
        Piston_A44 : BOOL;
        
        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-set:AttributeName = "Servo S1"}	
        Servo_S1: STRING;
        
        {#ix-attr:[Container(Layout.Stack)]}	
        {#ix-set:AttributeName = "ABB S2"}	
        Servo_S2: STRING;	
        
        {#ix-attr:[Container(Layout.Tabs)]}
        {#ix-set:AttributeName = "My ix component"}	
        component: IxComponent;	
        {#ix-set:AttributeName = "Servo S3"}	
        Servo_S3: stTest3;
        {#ix-set:AttributeName = "Servo S4"}	
        Servo_S4: stTest3;	
        
    END_VAR
    
END_CLASS