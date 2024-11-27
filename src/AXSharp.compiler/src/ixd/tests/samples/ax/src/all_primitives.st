/// <remarks>
/// Uses polar coordinates
/// </remarks>
CLASS all_primitives
    VAR PUBLIC
        myBOOL	        :       BOOL	      ;
        myBYTE	        :       BYTE	      ;
        myWORD	        :       WORD	      ;
        myDWORD	        :       DWORD	      ;
        myLWORD	        :       LWORD	      ;
        mySINT	        :       SINT	      ;
        myINT	        :       INT	          ;
        myDINT	        :       DINT	      ;
        myLINT	        :       LINT	      ;
        myUSINT	        :       USINT	      ;
        myUINT	        :       UINT	      ;
        myUDINT	        :       UDINT	      ;
        myULINT	        :       ULINT	      ;
        myREAL	        :       REAL	      ;
        myLREAL	        :       LREAL	      ;
        myTIME	        :       TIME	      ;
        myLTIME	        :       LTIME	      ;
        myDATE	        :       DATE	      ;
     //   myLDATE	        :       LDATE	      ;
        myTIME_OF_DAY	:       TIME_OF_DAY	  ;
     //   myLTIME_OF_DAY	:       LTIME_OF_DAY  ;
        myDATE_AND_TIME  :      DATE_AND_TIME ;
     //   myLDATE_AND_TIME :      LDATE_AND_TIME;
     //   myCHAR	        :       CHAR	      ;
     //   myWCHAR	        :       WCHAR	      ;
        mySTRING	        :   STRING	      ;
        myWSTRING	    :       WSTRING	      ;


       

    END_VAR
    
    ///<summary>
    /// Start method
    ///</summary>
    /// <param name="value">Describe parameter value.</param>
    /// <param name="value2">Describe parameter value2.</param>
    /// <returns>Return value of int.</returns>

    METHOD PUBLIC MyMethod : INT
        VAR_INPUT
            value : INT;
            value2 : INT;
        END_VAR

        myINT := value;

        // VAR_OUTPUT
        //     varout1 : INT;
        //     varout2 : INT;
        // END_VAR
    END_METHOD

    /// <summary>
    /// This method changes the point's location by the given x- and y-offsets.
    /// <example>
    /// For example:
    /// <code>
    /// Point p = new Point(3,5);
    /// p.Translate(-1,3);
    /// </code>
    /// results in <c>p</c>'s having the value (2,8).
    /// </example>
    /// </summary>
    METHOD PUBLIC Start : STRING
        VAR_INPUT
            firstName : STRING;
            lastName : STRING;
        END_VAR

        myString := firstName;

    END_METHOD

    METHOD PUBLIC Stop
        ;
    END_METHOD
END_CLASS