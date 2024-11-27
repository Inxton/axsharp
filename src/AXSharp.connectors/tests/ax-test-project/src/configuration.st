CONFIGURATION MyConfiguration
    TASK Main(Interval := T#1ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL 

        //myArrayComplexNested: ARRAY[0..10] OF complexArrayItemNested; 
        
        //myArrayString: ARRAY[0..10] OF STRING; 

        // myArrayComplex: ARRAY[0..10] OF complexArrayItem; 
        {S7.extern=ReadWrite}
        mins : full_of_primitives;
        {S7.extern=ReadWrite}
        maxs : full_of_primitives;

        {S7.extern=ReadWrite}
        minsmatch : full_of_primitives_match;
        {S7.extern=ReadWrite}
        maxsmatch : full_of_primitives_match;
        {S7.extern=ReadWrite}
        myBOOL	        :       BOOL	 := TRUE     ;
        {S7.extern=ReadWrite}
        myBYTE	        :       BYTE	      ;
        {S7.extern=ReadWrite}
        myWORD	        :       WORD	      ;
        {S7.extern=ReadWrite}
        myDWORD	        :       DWORD	      ;
        {S7.extern=ReadWrite}
        myLWORD	        :       LWORD	      ;
        {S7.extern=ReadWrite}
        mySINT	        :       SINT	      ;
        {S7.extern=ReadWrite}
        myINT	        :       INT	          ;
        {S7.extern=ReadWrite}
        myDINT	        :       DINT	      ;
        {S7.extern=ReadWrite}
        myLINT	        :       LINT	      ;
        {S7.extern=ReadWrite}
        myUSINT	        :       USINT	      ;
        {S7.extern=ReadWrite}
        myUINT	        :       UINT	      ;
        {S7.extern=ReadWrite}
        myUDINT	        :       UDINT	      ;
        {S7.extern=ReadWrite}
        myULINT	        :       ULINT	      ;
        {S7.extern=ReadWrite}
        myREAL	        :       REAL	      ;
        {S7.extern=ReadWrite}
        myLREAL	        :       LREAL	      ;
        {S7.extern=ReadWrite}
        myTIME	        :       TIME	      ;
        {S7.extern=ReadWrite}
        myLTIME	        :       LTIME	      ;
        {S7.extern=ReadWrite}
        myDATE	        :       DATE	      ;
        {S7.extern=ReadWrite}
        myLDATE	        :       LDATE	      ;
        {S7.extern=ReadWrite}
        myTIME_OF_DAY	:       TIME_OF_DAY	  ;
        {S7.extern=ReadWrite}
        myLTIME_OF_DAY	:       LTIME_OF_DAY  ;
        {S7.extern=ReadWrite}
        myDATE_AND_TIME  :      DATE_AND_TIME ;
        {S7.extern=ReadWrite}
        myLDATE_AND_TIME :      LDATE_AND_TIME;
        {S7.extern=ReadWrite}
        myCHAR	        :       CHAR	      ;
        {S7.extern=ReadWrite}
        myWCHAR	        :       WCHAR	      ;
        {S7.extern=ReadWrite}
        mySTRING	        :   STRING	      ;
        {S7.extern=ReadWrite}
        myWSTRING	    :       WSTRING	      ;


        
        {S7.extern=ReadWrite}
        myColors : Colors;

        {S7.extern=ReadWrite}
        Hierarchy : hierarchy;
        // Issues  

        //GH_PKTu_ix_56_Base: GH.PKTu.ix_56.Base;

        // GH_PKTu_ix_56_FirstInheritance : GH.PKTu.ix_56.FirstInheritance;
        {S7.extern=ReadWrite}
        GH_PKTu_ix_56_SecondInheritance : GH.PKTu.ix_56.SecondInheritance;

        {S7.extern=ReadWrite}
        myDATE_leap_febr	        :       DATE	      ;        
        {S7.extern=ReadWrite}
        myLDATE_leap_febr	        :       LDATE	      ;
        {S7.extern=ReadWrite}
        myDATE_AND_TIME_leap_febr   :      DATE_AND_TIME ;
        {S7.extern=ReadWrite}
        myLDATE_AND_TIME_leap_febr  :      LDATE_AND_TIME;

        {S7.extern=ReadWrite}
        myDATE_leap_late	        :       DATE	      ;
        {S7.extern=ReadWrite}
        myLDATE_leap_late	        :       LDATE	      ;
        {S7.extern=ReadWrite}
        myDATE_AND_TIME_leap_late   :      DATE_AND_TIME ;
        {S7.extern=ReadWrite}
        myLDATE_AND_TIME_leap_late  :      LDATE_AND_TIME;
    END_VAR
END_CONFIGURATION
