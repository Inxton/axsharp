using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace Simatic.Ax.StateFramework
    {
        [AXSharp.Connector.SourceFileAttribute(@"class_extended_by_known_type.st")]
        public partial class State1Transition : Simatic.Ax.StateFramework.AbstractState, AXSharp.Connector.IPlain
        {
            public State1Transition() : base()
            {
            }
        }
    }

    namespace Simatic.Ax.StateFramework
    {
        [AXSharp.Connector.SourceFileAttribute(@"class_extended_by_known_type.st")]
        public partial class AbstractState : AXSharp.Connector.IPlain, IState, IStateMuteable
        {
            public AbstractState()
            {
            }

            public Int16 StateID { get; set; }
            public string StateName { get; set; } = string.Empty;
        }
    }
}