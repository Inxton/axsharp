using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    public partial class _NULL_CONTEXT : AXSharp.Connector.IPlain, IContext
    {
        public _NULL_CONTEXT()
        {
        }
    }

    public partial interface IContext
    {
    }
}