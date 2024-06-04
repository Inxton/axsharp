{S7.extern=ReadWrite}
CLASS testingProgram
    VAR PUBLIC
        {#ix-set:AttributeName = "instanceOfStPrimitive"}
        {#ix-attr:[Container(Layout.Wrap)]}
        testPrimitive: stTestPrimitive;
        {#ix-set:AttributeName = "instanceOfStPrimitive"}
        testComplex: stTestComplex; 
        {#ix-set:AttributeName = "instanceOfStEnum"}
        testEnum: stTestEnum;
        {#ix-attr:[Container(Layout.UniformGrid)]}
        testRenderIgnore : stTestRenderIgnore;
        
        testEmpty: TestEmpty;
        testLayoutOverwrite: TestLayoutOverwriting;
        testMixed : TestMixed;
        testMultipleLayouts: TestMultipleLayouts;
        testSimple: TestSimple;
        testWithoutLayouts: TestWithoutLayouts;
        testSimpleNested: TestSimpleNested;
        {#ix-attr:[Container(Layout.Tabs)]}
        testMultipleNested: stTestMultipleNested;

        testLayouts : stTestLayouts;

        {#ix-attr:[PresentationBaseAttribute("ixBlazor.App.Custom.MySimplePrimitiveStruct")]}
        testPresentationBaseStruct : stSimplePrimitive;

        {#ix-attr:[PresentationBaseAttribute("ixBlazor.App.Custom.MySimplePrimitiveTag")]}
        testPresentationBaseTag : WORD;
    END_VAR
    
END_CLASS