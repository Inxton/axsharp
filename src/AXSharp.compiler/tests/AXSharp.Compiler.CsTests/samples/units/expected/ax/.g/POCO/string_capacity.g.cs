using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace StringCapacityNamespace
    {
        [AXSharp.Connector.SourceFileAttribute(@"string_capacity.st")]
        public partial class ClassWithDeclaredStringCapacities : AXSharp.Connector.IPlain
        {
            public ClassWithDeclaredStringCapacities()
            {
            }

            public string myString10 { get; set; } = string.Empty;
            public string myWString10 { get; set; } = string.Empty;
            public string myString { get; set; } = string.Empty;
            public string myWString { get; set; } = string.Empty;
            public string myWString1000 { get; set; } = string.Empty;
            public string[] myString10Array { get; set; } = new string[4];
        }
    }
}