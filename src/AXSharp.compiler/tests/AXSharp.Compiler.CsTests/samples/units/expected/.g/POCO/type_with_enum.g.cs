using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Simatic.Ax.StateFramework
{
    public partial interface IGuard
    {
    }
}

namespace Simatic.Ax.StateFramework
{
    namespace Pocos
    {
        public partial class CompareGuardLint : AXSharp.Connector.IPlain, IGuard
        {
            public CompareGuardLint()
            {
            }

            public Int64 CompareToValue { get; set; }

            public global::Simatic.Ax.StateFramework.Condition Condition { get; set; }
        }
    }
}