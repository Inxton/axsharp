using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace lib2
    {
        public partial class MyClass : AXSharp.Connector.IPlain
        {
            public MyClass()
            {
            }

            public string MyString { get; set; } = string.Empty;
            public Int16 MyInt { get; set; }
        }
    }
}