using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace TypeWithNameAttributes
{
    namespace Pocos
    {
        public partial class Motor : AXSharp.Connector.IPlain
        {
            public Motor()
            {
            }

            public Boolean isRunning { get; set; }
        }
    }

    namespace Pocos
    {
        public partial class Vehicle : AXSharp.Connector.IPlain
        {
            public Vehicle()
            {
            }

            public TypeWithNameAttributes.Pocos.Motor m { get; set; } = new TypeWithNameAttributes.Pocos.Motor();
            public Int16 displacement { get; set; }
        }
    }

    namespace Pocos
    {
        public partial class NoAccessModifierClass : AXSharp.Connector.IPlain
        {
            public NoAccessModifierClass()
            {
            }

            public string SomeClassVariable { get; set; } = string.Empty;
        }
    }
}