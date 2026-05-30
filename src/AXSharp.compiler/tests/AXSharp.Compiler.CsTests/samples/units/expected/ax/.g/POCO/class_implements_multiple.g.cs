using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    [AXSharp.Connector.SourceFileAttribute(@"class_implements_multiple.st")]
    public partial class _NULL_CONTEXT_MULTIPLE : AXSharp.Connector.IPlain, IContext_Multiple, IObject_Multiple
    {
        public _NULL_CONTEXT_MULTIPLE()
        {
        }
    }

    [AXSharp.Connector.SourceFileAttribute(@"class_implements_multiple.st")]
    public partial interface IContext_Multiple
    {
    }

    [AXSharp.Connector.SourceFileAttribute(@"class_implements_multiple.st")]
    public partial interface IObject_Multiple
    {
    }
}