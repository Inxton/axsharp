using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace Simatic.Ax.StateFramework
    {
        [AXSharp.Connector.SourceFileAttribute(@"type_with_enum.st")]
        public partial interface IGuard
        {
        }
    }

    namespace Simatic.Ax.StateFramework
    {
        [AXSharp.Connector.SourceFileAttribute(@"type_with_enum.st")]
        public partial class CompareGuardLint : AXSharp.Connector.IPlain, IGuard
        {
            public CompareGuardLint()
            {
            }

            public Int64 CompareToValue { get; set; }

            [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(global::Simatic.Ax.StateFramework.Condition))]
            public global::Simatic.Ax.StateFramework.Condition Condition { get; set; }
        }
    }
}