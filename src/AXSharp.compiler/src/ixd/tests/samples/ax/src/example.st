///<summary>
/// Gets the state of this command.
///</summary>
CLASS example
    VAR PUBLIC
        ///<summary>
        /// Summary of property primitives_stack
        ///</summary>
        ///<returns>
        /// Some return comment for property primitives_stack
        ///</returns>
        {#ix-attr:[Container(Layout.Stack)]}
        primitives_stack: test_primitive;
        {#ix-attr:[Container(Layout.Wrap)]}
        primitives_wrap: test_primitive;
        {#ix-attr:[Container(Layout.Tabs)]}
        primitives_tabs: test_primitive;
        {#ix-attr:[Container(Layout.UniformGrid)]}
        primitives_uniform: test_primitive;

        /// <remarks>
        /// Uses polar coordinates
        /// </remarks>
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

        /// <summary>
        /// Uses <see cref="ixcomponent"/> to do some magic
        /// </summary>
        ixcomponent_instance: ixcomponent;
        /// <summary>
        /// Uses <see cref="plc.MySecondNamespace.ixcomponent"/> to do some magic
        /// </summary>
        ixcomponent_instance2: MySecondNamespace.ixcomponent;
        /// <summary>
        /// Uses <see cref="plc.example#plc_example_ReferenceToComponent__"/> to do some magic
        /// </summary>
        ixcomponent_instance3: ThirdNamespace.ixcomponent;

        refIxComponent : REF_TO ixcomponent;
    END_VAR 
        
    METHOD PUBLIC ReferenceToComponent : Int
        refIxComponent^ := ixcomponent_instance;        
    END_METHOD

END_CLASS
