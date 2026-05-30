using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    [AXSharp.Connector.SourceFileAttribute(@"class_extends_and_implements.st")]
    public partial class ExtendsAndImplements : ExtendeeExtendsAndImplements, AXSharp.Connector.IPlain, IImplementation1, IImplementation2
    {
        public ExtendsAndImplements() : base()
        {
        }
    }

    [AXSharp.Connector.SourceFileAttribute(@"class_extends_and_implements.st")]
    public partial class ExtendeeExtendsAndImplements : AXSharp.Connector.IPlain
    {
        public ExtendeeExtendsAndImplements()
        {
        }
    }

    [AXSharp.Connector.SourceFileAttribute(@"class_extends_and_implements.st")]
    public partial interface IImplementation1
    {
    }

    [AXSharp.Connector.SourceFileAttribute(@"class_extends_and_implements.st")]
    public partial interface IImplementation2
    {
    }
}