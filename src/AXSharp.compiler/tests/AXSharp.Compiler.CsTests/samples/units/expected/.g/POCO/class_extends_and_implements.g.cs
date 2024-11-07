using System;
using AXSharp.Abstractions.Presentation;

namespace Pocos
{
    public partial class ExtendsAndImplements : ExtendeeExtendsAndImplements, AXSharp.Connector.IPlain, IImplementation1, IImplementation2
    {
        public ExtendsAndImplements() : base()
        {
        }
    }

    public partial class ExtendeeExtendsAndImplements : AXSharp.Connector.IPlain
    {
        public ExtendeeExtendsAndImplements()
        {
        }
    }

    public partial interface IImplementation1
    {
    }

    public partial interface IImplementation2
    {
    }
}