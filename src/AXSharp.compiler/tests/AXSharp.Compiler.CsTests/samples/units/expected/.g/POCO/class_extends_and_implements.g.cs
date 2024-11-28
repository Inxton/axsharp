using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    public partial class ExtendsAndImplements : global::Pocos.ExtendeeExtendsAndImplements, AXSharp.Connector.IPlain, IImplementation1, IImplementation2
    {
        public ExtendsAndImplements() : base()
        {
        }
    }
}

namespace Pocos
{
    public partial class ExtendeeExtendsAndImplements : AXSharp.Connector.IPlain
    {
        public ExtendeeExtendsAndImplements()
        {
        }
    }
}

public partial interface IImplementation1
{
}

public partial interface IImplementation2
{
}