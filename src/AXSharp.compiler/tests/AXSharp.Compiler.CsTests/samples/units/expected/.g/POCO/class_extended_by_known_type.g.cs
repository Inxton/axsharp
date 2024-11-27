using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Simatic.Ax.StateFramework
{
    namespace Pocos
    {
        public partial class State1Transition : Simatic.Ax.StateFramework.Pocos.AbstractState, AXSharp.Connector.IPlain
        {
            public State1Transition() : base()
            {
            }
        }
    }
}

namespace Simatic.Ax.StateFramework
{
    namespace Pocos
    {
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