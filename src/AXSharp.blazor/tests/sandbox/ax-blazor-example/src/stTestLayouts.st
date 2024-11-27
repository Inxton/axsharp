{S7.extern=ReadWrite}
CLASS stTestLayouts
    VAR PUBLIC
        {#ix-attr:[Container(Layout.Stack)]}
        test_stack: stSimplePrimitive;
        {#ix-attr:[Container(Layout.Wrap)]}
        test_wrap: stSimplePrimitive;
        {#ix-attr:[Container(Layout.Tabs)]}
        test_tabs: stSimplePrimitive;
        {#ix-attr:[Container(Layout.UniformGrid)]}
        test_uniform: stSimplePrimitive;

        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-attr:[Group(GroupLayout.GroupBox)]}
        test_groupbox_stack: stSimplePrimitive;

        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-attr:[Group(GroupLayout.Border)]}
        test_border_stack: stSimplePrimitive;

        {#ix-attr:[Container(Layout.Wrap)]}
        {#ix-attr:[Group(GroupLayout.GroupBox)]}
        test_groupbox_wrap: stSimplePrimitive;

        {#ix-attr:[Container(Layout.Wrap)]}
        {#ix-attr:[Group(GroupLayout.Border)]}
        test_border_wrap: stSimplePrimitive;

        {#ix-attr:[Container(Layout.Tabs)]}
        {#ix-attr:[Group(GroupLayout.GroupBox)]}
        test_groupbox_tabs: stSimplePrimitive;

        {#ix-attr:[Container(Layout.Tabs)]}
        {#ix-attr:[Group(GroupLayout.Border)]}
        test_border_tabs: stSimplePrimitive;

        {#ix-attr:[Container(Layout.UniformGrid)]}
        {#ix-attr:[Group(GroupLayout.GroupBox)]}
        test_groupbox_uniformGrid: stSimplePrimitive;

        {#ix-attr:[Container(Layout.UniformGrid)]}
        {#ix-attr:[Group(GroupLayout.Border)]}
        test_border_uniformGrid: stSimplePrimitive;
    END_VAR    
END_CLASS