using System;

namespace Pocos
{
    public partial class ixcomponent : AXSharp.Connector.IPlain
    {
        public ixcomponent()
        {
        }

        public Int16 my_int { get; set; }

        public string my_string { get; set; } = string.Empty;
        public Boolean my_bool { get; set; }
    }

    namespace MySecondNamespace
    {
        public partial class ixcomponent : AXSharp.Connector.IPlain
        {
            public ixcomponent()
            {
            }

            public Int16 my_int { get; set; }

            public string my_string { get; set; } = string.Empty;
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

            public Int16 my_int { get; set; }

            public string my_string { get; set; } = string.Empty;
            public Boolean my_bool { get; set; }
        }
    }
}