using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    public partial class appTwinController
    {
        public lib1.MyClass lib1_MyClass { get; set; } = new lib1.MyClass();
        public lib2.MyClass lib2_MyClass { get; set; } = new lib2.MyClass();
    }
}