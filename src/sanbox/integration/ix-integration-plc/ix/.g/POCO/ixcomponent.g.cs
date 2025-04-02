using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    public partial class ixcomponent : AXSharp.Connector.IPlain
    {
        public ixcomponent()
        {
        }

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"My integer")]
        public Int16 my_int { get; set; }

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"My string")]
        public string my_string { get; set; } = string.Empty;

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"My bool")]
        public Boolean my_bool { get; set; }
    }

    namespace MySecondNamespace
    {
        public partial class ixcomponent : AXSharp.Connector.IPlain
        {
            public ixcomponent()
            {
            }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"My integer")]
            public Int16 my_int { get; set; }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"My string")]
            public string my_string { get; set; } = string.Empty;

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"My bool")]
            public Boolean my_bool { get; set; }
        }
    }

    namespace ThirdNamespace
    {
        public partial class ixcomponent : AXSharp.Connector.IPlain
        {
            public ixcomponent()
            {
            }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"My integer")]
            public Int16 my_int { get; set; }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"My string")]
            public string my_string { get; set; } = string.Empty;

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"My bool")]
            public Boolean my_bool { get; set; }
        }
    }
}