using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace ArrayDeclarationSimpleNamespace
{
    namespace Pocos
    {
        public partial class array_declaration_class : AXSharp.Connector.IPlain
        {
            public array_declaration_class()
            {
#pragma warning disable CS0612
                AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(complex, () => new ArrayDeclarationSimpleNamespace.Pocos.some_complex_type(), new[] { (1, 100) });
#pragma warning restore CS0612
            }

            public Int16[] primitive { get; set; } = new Int16[100];
            public ArrayDeclarationSimpleNamespace.Pocos.some_complex_type[] complex { get; set; } = new ArrayDeclarationSimpleNamespace.Pocos.some_complex_type[100];
        }
    }

    namespace Pocos
    {
        public partial class some_complex_type : AXSharp.Connector.IPlain
        {
            public some_complex_type()
            {
            }
        }
    }
}