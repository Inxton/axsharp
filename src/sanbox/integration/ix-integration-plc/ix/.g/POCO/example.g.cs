using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    public partial class example : AXSharp.Connector.IPlain
    {
        public example()
        {
        }

        [Container(Layout.Stack)]
        public global::Pocos.test_primitive primitives_stack { get; set; } = new global::Pocos.test_primitive();

        [Container(Layout.Wrap)]
        public global::Pocos.test_primitive primitives_wrap { get; set; } = new global::Pocos.test_primitive();

        [Container(Layout.Tabs)]
        public global::Pocos.test_primitive primitives_tabs { get; set; } = new global::Pocos.test_primitive();

        [Container(Layout.UniformGrid)]
        public global::Pocos.test_primitive primitives_uniform { get; set; } = new global::Pocos.test_primitive();

        [Container(Layout.Stack)]
        [Group(GroupLayout.GroupBox)]
        public global::Pocos.test_primitive test_groupbox { get; set; } = new global::Pocos.test_primitive();

        [Container(Layout.Stack)]
        [Group(GroupLayout.Border)]
        public global::Pocos.test_primitive test_border { get; set; } = new global::Pocos.test_primitive();

        [Container(Layout.Tabs)]
        [Group(GroupLayout.GroupBox)]
        public global::Pocos.groupbox testgroupbox { get; set; } = new global::Pocos.groupbox();
        public global::Pocos.border testborder { get; set; } = new global::Pocos.border();
        public global::Pocos.ixcomponent ixcomponent_instance { get; set; } = new global::Pocos.ixcomponent();
        public MySecondNamespace.Pocos.ixcomponent ixcomponent_instance2 { get; set; } = new MySecondNamespace.Pocos.ixcomponent();
        public ThirdNamespace.Pocos.ixcomponent ixcomponent_instance3 { get; set; } = new ThirdNamespace.Pocos.ixcomponent();

        [Container(Layout.Stack)]
        public global::Pocos.compositeLayout compositeStack { get; set; } = new global::Pocos.compositeLayout();

        [Container(Layout.Wrap)]
        public global::Pocos.compositeLayout compositeWrap { get; set; } = new global::Pocos.compositeLayout();

        [Container(Layout.UniformGrid)]
        public global::Pocos.compositeLayout compositeUniform { get; set; } = new global::Pocos.compositeLayout();

        [Container(Layout.Tabs)]
        public global::Pocos.compositeLayout compositeTabs { get; set; } = new global::Pocos.compositeLayout();
    }
}