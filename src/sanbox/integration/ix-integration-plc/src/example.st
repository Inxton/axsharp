{S7.extern=ReadWrite}
CLASS example
    VAR PUBLIC
        {#ix-attr:[Container(Layout.Stack)]}
        primitives_stack: test_primitive;
        {#ix-attr:[Container(Layout.Wrap)]}
        primitives_wrap: test_primitive;
        {#ix-attr:[Container(Layout.Tabs)]}
        primitives_tabs: test_primitive;
        {#ix-attr:[Container(Layout.UniformGrid)]}
        primitives_uniform: test_primitive;

        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-attr:[Group(GroupLayout.GroupBox)]}    
        test_groupbox: test_primitive;

        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-attr:[Group(GroupLayout.Border)]}    
        test_border: test_primitive;

        {#ix-attr:[Container(Layout.Tabs)]}
        {#ix-attr:[Group(GroupLayout.GroupBox)]}   
        testgroupbox: groupbox;
        testborder: border;

        ixcomponent_instance: ixcomponent;
        ixcomponent_instance2: MySecondNamespace.ixcomponent;
        ixcomponent_instance3: ThirdNamespace.ixcomponent;

        {#ix-attr:[Container(Layout.Stack)]}
        compositeStack :compositeLayout;
        {#ix-attr:[Container(Layout.Wrap)]}
        compositeWrap :compositeLayout;
        {#ix-attr:[Container(Layout.UniformGrid)]}
        compositeUniform :compositeLayout;
        {#ix-attr:[Container(Layout.Tabs)]}
        compositeTabs :compositeLayout;
        
        refIxComponent : REF_TO ixcomponent;
    END_VAR 
        
    METHOD PUBLIC ReferenceToComponent : Int
        refIxComponent^ := ixcomponent_instance;        
    END_METHOD

END_CLASS
