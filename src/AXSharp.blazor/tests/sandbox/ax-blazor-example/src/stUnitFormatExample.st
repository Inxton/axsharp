{S7.extern=ReadWrite}
CLASS stUnitFormatExample
    VAR PUBLIC
        {#ix-attr:[Container(Layout.Tabs)]}
        {#ix-attr:[Group(GroupLayout.GroupBox)]}
        {#ix-set:AttributeName = "Group name"}
        {#ix-attr:[Unit("m")]}
        testInteger : INT;

        {#ix-set:AttributeName = "Aaaaa"}
        {#ix-attr:[Unit("Nm")]}
        testREAL : REAL;

        {#ix-attr:[Format("yyyy.MM.dd")]}
        testDate : DATE;

        {#ix-attr:[Format("yyyy.MM.dd HH:mm:ss.fff")]}
        testDateAndTime : DATE_AND_TIME;

        {#ix-attr:[Format("#.##")]}
        testLREAL : LREAL;
    END_VAR
    
END_CLASS