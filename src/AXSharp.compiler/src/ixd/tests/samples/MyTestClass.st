/// <summary>
///  Hello class
/// </summary>
NAMESPACE MonsterNamespace
    CLASS MyTestClass
        VAR PUBLIC
    
            myBOOL	        :       BOOL	      ;
            myBYTE	        :       BYTE	      ;
            myWORD	        :       WORD	      ;
        END_VAR

        METHOD PUBLIC Start
                myBOOL := TRUE;
        END_METHOD

        METHOD PUBLIC Stop
                myBOOL := TRUE;
        END_METHOD
    
    END_CLASS
END_NAMESPACE