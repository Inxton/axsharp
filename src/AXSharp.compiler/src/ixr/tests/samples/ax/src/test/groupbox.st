{#ix-attr:[Container(Layout.Stack)]}
{#ix-attr:[Group(GroupLayout.GroupBox)]}   
CLASS groupbox
    VAR PUBLIC
        {#ix-set:AttributeName = "<#Integer From PLC#>"}	
        testInteger : INT;
        {#ix-set:AttributeName = "<#UInteger From PLC#>"}	
        testUInteger : INT;
        {#ix-set:AttributeName = "<#STRING From PLC#>"}	
        testString : STRING;
        {#ix-set:AttributeName = "<#WORD From PLC#>"}	
        testWord : WORD;
        {#ix-set:AttributeName = "<#BYTE From PLC#>"}	
        testByte : BYTE;
        {#ix-set:AttributeName = "<#REAL From PLC#>"}	
        testReal : REAL;
        {#ix-set:AttributeName = "<#LREAL From PLC#>"}	
        testLReal : LREAL;
        {#ix-set:AttributeName = "<#BOOL From PLC#>"}	
        testBool : BOOL;
        {#ix-set:AttributeName = "<#DATE From PLC#>"}
        TestDate : DATE;	
        {#ix-set:AttributeName = "<#DATE_AND_TIME From PLC#>"}	
        TestDateTime : DATE_AND_TIME;	
        {#ix-set:AttributeName = "<#TIME_OF_DAY From PLC#>"}	
        TestTimeOfDay : TIME_OF_DAY;	
        {#ix-set:AttributeName = "<#ENUM Station status#>"}	
        Status : enumStationStatus;	
    END_VAR
    
END_CLASS