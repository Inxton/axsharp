USING System.Strings;

{S7.extern=ReadWrite}


CLASS all_primitives
    VAR PUBLIC
        myBOOL	        :       BOOL	      ;
        myBYTE	        :       BYTE	      := BYTE#1;
        myWORD	        :       WORD	      := WORD#1;
        myDWORD	        :       DWORD	      := DWORD#1;
        myLWORD	        :       LWORD	      := LWORD#1;
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
        myLDATE	        :       LDATE	      ;
        myTIME_OF_DAY	:       TIME_OF_DAY	  ;
        myLTIME_OF_DAY	:       LTIME_OF_DAY  ;
        myDATE_AND_TIME  :      DATE_AND_TIME ;
        myLDATE_AND_TIME :      LDATE_AND_TIME;
        myCHAR	        :       CHAR	      ;
        myWCHAR	        :       WCHAR	      ;
        mySTRING	        :   STRING	      ;
        myWSTRING	    :       WSTRING	      ;

        testLWORD : LWORD;
        testULINT : ULINT;
        testULINTPlusOne : ULINT;
        testLWORDPlusOne : LWORD;

    END_VAR
    
    METHOD PUBLIC Simulate 
      
      myBOOL := NOT myBOOL;
      myBYTE := TO_BYTE(TO_SINT(myBYTE) + 1);
      myWORD := TO_WORD(TO_INT(myWORD) + 1);
      myDWORD := TO_DWORD(TO_DINT(myDWORD) + 1);
      myLWORD := TO_LWORD(TO_LINT(myLWORD) + 1);
      mySINT := mySINT + SINT#1;
      myINT := myINT + 1;
      myDINT := myDINT + DINT#1;
      myLINT := myLINT + LINT#1;
      myUSINT := myUSINT + USINT#1;
      myUINT := myUINT + UINT#1;
      myUDINT := myUDINT + UDINT#1;
      myULINT := myULINT + ULINT#1;
      myREAL := myREAL + REAL#1.0;
      myLREAL := myLREAL + 1.0;
      myTIME := myTIME + T#1s;
      myLTIME := myLTIME + LT#1s;
      myDATE := TO_DATE(To_LWORD(TO_ULINT(TO_LWORD(myDATE)) + ULINT#86400000000000));
      myLDATE := TO_DATE(To_LWORD(TO_ULINT(TO_LWORD(myLDATE)) + ULINT#86400000000000));
      myTIME_OF_DAY := myTIME_OF_DAY + T#1s;
      myLTIME_OF_DAY := myLTIME_OF_DAY + LT#1s;
      myDATE_AND_TIME := myDATE_AND_TIME + T#1s;
      myLDATE_AND_TIME := myLDATE_AND_TIME + LT#1s;
      myCHAR := myCHAR + CHAR#1;
      myWCHAR := myWCHAR + WCHAR#1;
      mySTRING := ToString(myINT);
      //myWSTRING := ToString(myINT);
  END_METHOD
END_CLASS