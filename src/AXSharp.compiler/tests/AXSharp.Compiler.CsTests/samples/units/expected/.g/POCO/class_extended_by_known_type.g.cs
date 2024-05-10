using System;

namespace Pocos
{
    namespace Simatic.Ax.StateFramework
    {
        public partial class State1Transition : Simatic.Ax.StateFramework.AbstractState, AXSharp.Connector.IPlain
        {
            public State1Transition() : base()
            {
            }
        }
    }

    namespace Simatic.Ax.StateFramework
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