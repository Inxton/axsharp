NAMESPACE MeasurementExample
    {#ix-attr:[Container(Layout.Wrap)]}
    {#ix-attr:[Group(GroupLayout.GroupBox)]}
    {S7.extern=ReadWrite}
    CLASS PUBLIC Measurement
        VAR PUBLIC
            {#ix-set:AttributeName = "Minimum"}
            Min : REAL;
            {#ix-set:AttributeName = "Measured"}
            {#ix-attr:[ReadOnly()]}
            Acquired : REAL;
            {#ix-set:AttributeName = "Maximum"}
            Max : REAL;
            {#ix-set:AttributeName = "Measurement Result"}
            Result : Result;        
        END_VAR   
        
        METHOD PUBLIC Simulate 
            Min := Min - REAL#1.0;
            Max := Max + REAL#1.0;
            Max := Max / REAL#2.0;            
        END_METHOD
    END_CLASS

    TYPE
        Result : INT (
            None := 0,
            Passed := 10,
            Failed := 20
        );
    END_TYPE

    {S7.extern=ReadWrite}
    CLASS PUBLIC Measurements
        VAR PUBLIC
            {#ix-attr:[Container(Layout.Stack)]}
            {#ix-attr:[Group(GroupLayout.GroupBox)]}
            {#ix-set:AttributeName = "Stack panel"}
            measurement_stack : Measurement;

            {#ix-attr:[Container(Layout.Wrap)]}
            {#ix-attr:[Group(GroupLayout.GroupBox)]}
            {#ix-set:AttributeName = "Wrap panel"}
            measurement_wrap : Measurement;
            
            {#ix-attr:[Container(Layout.UniformGrid)]}
            {#ix-attr:[Group(GroupLayout.GroupBox)]}
            {#ix-set:AttributeName = "Grid"}
            measurement_grid : Measurement;
            
            {#ix-attr:[Container(Layout.Tabs)]}
            {#ix-attr:[Group(GroupLayout.GroupBox)]}
            {#ix-set:AttributeName = "Tabs"}
            measurement_tabs : Measurement;
        END_VAR    

        METHOD PUBLIC Simulate
            measurement_stack.Simulate();
            measurement_wrap.Simulate();
            measurement_grid.Simulate();
            measurement_tabs.Simulate();            
        END_METHOD

    END_CLASS
END_NAMESPACE
