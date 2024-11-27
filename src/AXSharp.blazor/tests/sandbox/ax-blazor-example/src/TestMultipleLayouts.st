{S7.extern=ReadWrite}
CLASS TestMultipleLayouts
    VAR PUBLIC
        {#ix-set:AttributeName = "B1"}
        B1: INT;
        
        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-set:AttributeName = "B2"}
        B2: INT;

        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-set:AttributeName = "A1"}
        Piston_A1 : STRING;
        {#ix-set:AttributeName = "A2"}
        Piston_A2 : STRING;
        {#ix-set:AttributeName = "A3"}
        Piston_A3 : STRING;
        {#ix-set:AttributeName = "A4"}
        Piston_A4 : STRING;
        
        {#ix-attr:[Container(Layout.Tabs)]}
        Piston_A21 : INT;
        {#ix-set:AttributeName = "A5"}
        Piston_A22 : INT;
        {#ix-set:AttributeName = "A6"}
        Piston_A23 : INT;
        {#ix-set:AttributeName = "A7"}
        Piston_A24 : INT;
        
        {#ix-attr:[Container(Layout.UniformGrid)]}
        Piston_A31 : REAL;
        {#ix-set:AttributeName = "A8"}
        Piston_A32 : REAL;
        {#ix-set:AttributeName = "A9"}
        Piston_A33 : REAL;
        {#ix-set:AttributeName = "A10"}
        Piston_A34 : REAL;
        
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
    END_VAR
    
END_CLASS