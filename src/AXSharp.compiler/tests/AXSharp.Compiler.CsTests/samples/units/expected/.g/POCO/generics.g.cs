using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace GenericsTests
{
    namespace Pocos
    {
        public partial class Extender : AXSharp.Connector.IPlain
        {
            public Extender()
            {
            }
        }
    }

    namespace Pocos
    {
        public partial class SomeTypeToBeGeneric : AXSharp.Connector.IPlain
        {
            public SomeTypeToBeGeneric()
            {
            }

            public Boolean Boolean { get; set; }
            public Int16 Cele { get; set; }
        }
    }

    namespace Pocos
    {
        public partial class Extendee2 : GenericsTests.Pocos.Extender, AXSharp.Connector.IPlain
        {
            public Extendee2() : base()
            {
            }

            [AXOpen.Data.AxoDataEntityAttribute]
            [Container(Layout.Stack)]
            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Shared Header")]
            public GenericsTests.Pocos.SomeTypeToBeGeneric SomeData { get; set; } = new GenericsTests.Pocos.SomeTypeToBeGeneric();
        }
    }
}