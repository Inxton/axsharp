using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    public partial class appTwinController
    {
        public lib1.Pocos.MyClass lib1_MyClass { get; set; } = new lib1.Pocos.MyClass();
        public lib2.Pocos.MyClass lib2_MyClass { get; set; } = new lib2.Pocos.MyClass();
    }
}