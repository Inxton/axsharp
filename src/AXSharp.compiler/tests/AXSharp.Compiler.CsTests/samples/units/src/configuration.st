CONFIGURATION MyConfiguration
    TASK Main(Interval := T#1000ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL
        {S7.extern=ReadWrite}
        Complex : ComplexForConfig;
        {S7.extern=ReadWrite}
        myBOOL	        :       BOOL	      ;
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
        myDATE_AND_TIME  :       DATE_AND_TIME ;
        {S7.extern=ReadWrite}
        myLDATE_AND_TIME :       LDATE_AND_TIME;
        {S7.extern=ReadWrite}
        myCHAR	        :       CHAR	      ;
        {S7.extern=ReadWrite}
        myWCHAR	        :       WCHAR	      ;
        {S7.extern=ReadWrite}
        mySTRING	        :       STRING	      ;
        {S7.extern=ReadWrite}
        myWSTRING	    :       WSTRING	      ;

        {S7.extern=ReadWrite}
        {#ix-attr:[ReadOnce()]}
        myWSTRING_readOnce	    :       WSTRING	      ;
        {#ix-attr:[ReadOnly()]}
        {S7.extern=ReadWrite}
        myWSTRING_readOnly	    :       WSTRING	      ;
        {#ix-attr:[ReadOnce()]}
        {S7.extern=ReadWrite}
        cReadOnce           : ComplexForConfig;
        {#ix-attr:[ReadOnly()]}
        {S7.extern=ReadWrite}
        cReadOnly           : ComplexForConfig;
        {S7.extern=ReadWrite}
        Colorss : Colorss;
        {S7.extern=ReadWrite}
        Colorsss : Colorsss;

        {S7.extern=ReadWrite}
        {#ix-attr:[CompilerOmitsAttribute()]}
        _must_be_omitted_everywhere : BOOL;
        {S7.extern=ReadWrite}
        {#ix-attr:[CompilerOmitsAttribute("Onliner")]}
        _must_be_omitted_in_onliner : BOOL;
        {S7.extern=ReadWrite}
        {#ix-attr:[CompilerOmitsAttribute("POCO")]}
         _must_be_omitted_in_poco : BOOL;
    END_VAR
END_CONFIGURATION
{S7.extern=ReadWrite}
CLASS PUBLIC ComplexForConfig
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
        myLDATE	        :       LDATE	      ;
        myTIME_OF_DAY	:       TIME_OF_DAY	  ;
        myLTIME_OF_DAY	:       LTIME_OF_DAY  ;
        myDATE_AND_TIME  :       DATE_AND_TIME ;
        myLDATE_AND_TIME :       LDATE_AND_TIME;
        myCHAR	        :       CHAR	      ;
        myWCHAR	        :       WCHAR	      ;
        mySTRING	        :       STRING	      ;
        myWSTRING	    :       WSTRING	      ;
        myMotor         : Motor;
    END_VAR    
END_CLASS   

TYPE
    Colorss : (Red, Green, Blue) := Red;
END_TYPE

TYPE
    Colorsss : ULINT (Red := ULINT#1, Green := ULINT#2, Blue := ULINT#3) := Red;
END_TYPE

 TYPE
        {S7.extern=ReadWrite}
        Motor : STRUCT
            isRunning : BOOL;
        END_STRUCT;
        {S7.extern=ReadWrite}
        Vehicle : STRUCT
            m : Motor;
            displacement : INT;
        END_STRUCT;
    END_TYPE