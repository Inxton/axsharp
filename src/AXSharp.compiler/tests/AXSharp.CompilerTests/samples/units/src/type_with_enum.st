NAMESPACE Simatic.Ax.StateFramework
    INTERFACE IGuard 

        METHOD Check : BOOL
        END_METHOD

        METHOD Reset
        END_METHOD
        
    END_INTERFACE
END_NAMESPACE


NAMESPACE Simatic.Ax.StateFramework

    /// Condition
    /// GT: Greater than compareValue
    /// EQ: Equal to compareValue
    /// LT: Less than compareValue
    /// NE: Is not equal to compareValue
    /// GE: Greater or equal to compareValue
    /// LE: Less or equal to compareValue
    TYPE 
        Condition : (GT, EQ, LT, NE, GE, LE);
    END_TYPE

    CLASS CompareGuardLint IMPLEMENTS IGuard
        VAR PUBLIC
            Value : REF_TO LINT;
            CompareToValue : LINT;
            Condition : Condition;
        END_VAR
        
        METHOD PUBLIC Config
            VAR_INPUT
                countValue : REF_TO LINT;
                compareValue : LINT;
                compareCondition : Condition;                
            END_VAR
            Value := countValue;
            CompareToValue := compareValue;
            Condition := compareCondition;
        END_METHOD
        
        // ---------------- IMPLEMENT IGuard---------------------------
        METHOD PUBLIC Check : BOOL

            CASE Condition OF
                Condition#GT : Check := Value^ > CompareToValue;
                Condition#EQ : Check := Value^ = CompareToValue;
                Condition#LT : Check := Value^ < CompareToValue;
                Condition#GE : Check := Value^ >= CompareToValue;
                Condition#LE : Check := Value^ <= CompareToValue;
            END_CASE
            ;

        END_METHOD

        METHOD PUBLIC Reset
            ;
        END_METHOD
        // ---------------- END IMPLEMENT IGuard---------------------------


    END_CLASS
END_NAMESPACE

