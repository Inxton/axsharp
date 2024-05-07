using System;

namespace Pocos
{
    namespace GenericsTests
    {
        public partial class Extender : AXSharp.Connector.IPlain
        {
        }

        public partial class SomeTypeToBeGeneric : AXSharp.Connector.IPlain
        {
            public Boolean Boolean { get; set; }

            public Int16 Cele { get; set; }
        }

        public partial class Extendee2 : GenericsTests.Extender, AXSharp.Connector.IPlain
        {
            public GenericsTests.SomeTypeToBeGeneric SomeData { get; set; } = new GenericsTests.SomeTypeToBeGeneric();
        }
    }
}