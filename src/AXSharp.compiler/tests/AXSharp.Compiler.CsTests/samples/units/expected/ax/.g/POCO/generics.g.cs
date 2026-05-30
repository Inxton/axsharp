using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace GenericsTests
    {
        [AXSharp.Connector.SourceFileAttribute(@"generics.st")]
        public partial class Extender : AXSharp.Connector.IPlain
        {
            public Extender()
            {
            }
        }

        [AXSharp.Connector.SourceFileAttribute(@"generics.st")]
        public partial class SomeTypeToBeGeneric : AXSharp.Connector.IPlain
        {
            public SomeTypeToBeGeneric()
            {
            }

            public Boolean Boolean { get; set; }
            public Int16 Cele { get; set; }
        }

        [AXSharp.Connector.SourceFileAttribute(@"generics.st")]
        public partial class Extendee2 : GenericsTests.Extender, AXSharp.Connector.IPlain
        {
            public Extendee2() : base()
            {
            }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"Shared Header")]
            public GenericsTests.SomeTypeToBeGeneric SomeData { get; set; } = new GenericsTests.SomeTypeToBeGeneric();
        }
    }
}