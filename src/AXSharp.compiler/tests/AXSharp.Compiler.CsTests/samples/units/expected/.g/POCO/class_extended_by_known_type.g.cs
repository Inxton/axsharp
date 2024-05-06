using System;

namespace Pocos
{
    namespace Simatic.Ax.StateFramework
    {
        public partial class State1Transition : Simatic.Ax.StateFramework.AbstractState, AXSharp.Connector.IPlain
        {
        }
    }

    namespace Simatic.Ax.StateFramework
    {
        public partial class AbstractState : AXSharp.Connector.IPlain, IState, IStateMuteable
        {
            public Int16 StateID { get; set; }

            public string StateName { get; set; } = string.Empty;
        }
    }
}