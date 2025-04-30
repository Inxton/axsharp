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

        public test_primitive primitives_stack { get; set; } = new test_primitive();
        public test_primitive primitives_wrap { get; set; } = new test_primitive();
        public test_primitive primitives_tabs { get; set; } = new test_primitive();
        public test_primitive primitives_uniform { get; set; } = new test_primitive();
        public test_primitive test_groupbox { get; set; } = new test_primitive();
        public test_primitive test_border { get; set; } = new test_primitive();
        public groupbox testgroupbox { get; set; } = new groupbox();
        public border testborder { get; set; } = new border();
        public ixcomponent ixcomponent_instance { get; set; } = new ixcomponent();
        public MySecondNamespace.ixcomponent ixcomponent_instance2 { get; set; } = new MySecondNamespace.ixcomponent();
        public ThirdNamespace.ixcomponent ixcomponent_instance3 { get; set; } = new ThirdNamespace.ixcomponent();
        public compositeLayout compositeStack { get; set; } = new compositeLayout();
        public compositeLayout compositeWrap { get; set; } = new compositeLayout();
        public compositeLayout compositeUniform { get; set; } = new compositeLayout();
        public compositeLayout compositeTabs { get; set; } = new compositeLayout();
    }
}