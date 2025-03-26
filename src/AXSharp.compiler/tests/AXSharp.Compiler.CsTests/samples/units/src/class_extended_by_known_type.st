USING Simatic.Ax.Stateframework;
USING Simatic.Ax.StatePattern;


NAMESPACE Simatic.Ax.StateFramework
    {S7.extern=ReadWrite}
    CLASS State1Transition EXTENDS AbstractState

        METHOD PUBLIC OVERRIDE OnEntry
            ;
        END_METHOD

        METHOD PUBLIC OVERRIDE OnExit
            ;
        END_METHOD

        METHOD PUBLIC OVERRIDE Action
            ;
        END_METHOD

    END_CLASS 
    
    
END_NAMESPACE



NAMESPACE Simatic.Ax.StateFramework
    {S7.extern=ReadWrite}
    CLASS ABSTRACT AbstractState IMPLEMENTS IState, IStateMuteable

        VAR PUBLIC
            Transition1 : ITransition;
            StateID : INT;
            StateName : String;
        END_VAR

        VAR PROTECTED
             _actualStatus : StateStatus;
        END_VAR


        // METHOD PUBLIC ABSTRACT OnEntry
        // END_METHOD
        METHOD PUBLIC OnEntry
            _actualStatus := StateStatus#Active;
        END_METHOD

        METHOD PUBLIC ABSTRACT OnExit
        END_METHOD

        METHOD PUBLIC ABSTRACT Action
        END_METHOD

        METHOD PUBLIC GetTransition : ITransition
            VAR_INPUT 
                pos : UINT;
            END_VAR
            IF (THIS.TransitionCount() = pos + UINT#1) THEN
                GetTransition := transition1;
            ELSE
                GetTransition := NULL;
            END_IF;
        END_METHOD

        METHOD PUBLIC TransitionCount : UINT
            IF (transition1 <> NULL) THEN
                TransitionCount := UINT#1;
            ELSE
                TransitionCount := UINT#0;
            END_IF;
        END_METHOD

        METHOD PUBLIC GetStateID : INT
            GetStateID := StateID;
        END_METHOD
    
        METHOD PUBLIC FINAL GetStateName : STRING
            GetStateName := StateName;
        END_METHOD

        METHOD PUBLIC FINAL GetStatus : StateStatus
            GetStatus := _actualStatus;
        END_METHOD

        METHOD PUBLIC FINAL SetStatus
            VAR_INPUT
                status : StateStatus;
            END_VAR
            _actualStatus := status;
        END_METHOD
    END_CLASS
END_NAMESPACE