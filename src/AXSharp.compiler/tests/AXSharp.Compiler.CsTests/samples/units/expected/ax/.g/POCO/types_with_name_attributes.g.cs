using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace TypeWithNameAttributes
    {
        [AXSharp.Connector.SourceFileAttribute(@"types_with_name_attributes.st")]
        public partial class TypesNameAttrMotor : AXSharp.Connector.IPlain
        {
            public TypesNameAttrMotor()
            {
            }

            public Boolean isRunning { get; set; }
        }

        [AXSharp.Connector.SourceFileAttribute(@"types_with_name_attributes.st")]
        public partial class TypesNameAttrVehicle : AXSharp.Connector.IPlain
        {
            public TypesNameAttrVehicle()
            {
            }

            public TypeWithNameAttributes.TypesNameAttrMotor m { get; set; } = new TypeWithNameAttributes.TypesNameAttrMotor();
            public Int16 displacement { get; set; }
        }

        [AXSharp.Connector.SourceFileAttribute(@"types_with_name_attributes.st")]
        public partial class NoAccessModifierClass : AXSharp.Connector.IPlain
        {
            public NoAccessModifierClass()
            {
            }

            public string SomeClassVariable { get; set; } = string.Empty;
        }
    }
}