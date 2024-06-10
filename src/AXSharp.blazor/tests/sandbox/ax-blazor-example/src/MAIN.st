{S7.extern=ReadWrite}
CLASS MAIN
    VAR PUBLIC
        {#ix-set:AttributeName = "<#Hello#> <#World#>"}
        Hello_World : STRING := 'Hello world!';
        {#ix-set:AttributeName = "cislo"}
        cislo : INT;
        {#ix-set:AttributeName = "bool_val"}
        boolValue : BOOL;
        {#ix-set:AttributeName = "Position"}
        Position : LREAL;
        {#ix-set:AttributeName = "Struct"}
        instanceOfstTest: stTest;
        {#ix-set:AttributeName = "Struct2"}
        instanceOfstTest2: stTest2 ;
        {#ix-set:AttributeName = "instanceOfStTest3"}
        instanceOfstTest3: stTest3 ;
        {#ix-set:AttributeName = "BlazorStruct"}
        instanceOfstBlazor: stBlazor := (testInteger := 10, testString := 'ahoj');
        arr1 : ARRAY [0..2] OF stTest := [(p1 := 6, p2 := 3),(p1 := 9, p2 := 10),(p1 := 12,p2 := 33)];	
        arr2 :ARRAY [0..5] OF INT;
        dateVar : DATE;
        
        {#ix-set:AttributeName = "instanceOfComplex"}
        instanceOfstComplex: stComplex ;
        {#ix-set:AttributeName = "instanceOfStPrimitive"}
        instanceOfstPrimitive: stTestPrimitive ;
        
    //	{#ix [Container(Layout.Stack)]}
        instanceOfstMultipleLayouts: stMultipleLayouts ;
        instanceOfstMultipleLayouts2: stLayouts ;	
        
        {#ix-set:AttributeName = "instanceOfIxComponent"}
        instanceOfIxComponent: IxComponent ;	
        
        groupBox_test: GroupBox_other;
        cu00: CU00x;
        cuBase: CUBase;

        {#ix-attr:[RenderTemplateOverrideAttribute("ixBlazor.App.Custom.MySimplePrimitiveStruct")]}
        OverridedSimpleStruct : stSimplePrimitive;

        {#ix-attr:[RenderTemplateOverrideAttribute("ixBlazor.App.Custom.MySimplePrimitiveTag")]}
        OverridedSimpleTag : WORD;

    END_VAR
    
END_CLASS