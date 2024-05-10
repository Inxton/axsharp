using System;

namespace Pocos
{
    namespace TypeWithNameAttributes
    {
        public partial class Motor
        {
            public Boolean isRunning { get; set; }
        }

        public partial class Vehicle
        {
            public TypeWithNameAttributes.Motor m { get; set; } = new TypeWithNameAttributes.Motor();
            public Int16 displacement { get; set; }
        }

        public partial class NoAccessModifierClass : AXSharp.Connector.IPlain
        {
            public NoAccessModifierClass()
            {
            }

            public string SomeClassVariable { get; set; } = string.Empty;
        }
    }
}