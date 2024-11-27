PROGRAM MyProgram
    VAR_EXTERNAL

        //myArrayComplexNested: ARRAY[0..10] OF complexArrayItemNested; 

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
        myDATE_AND_TIME  :      DATE_AND_TIME ;
        myLDATE_AND_TIME :      LDATE_AND_TIME;
        myCHAR	        :       CHAR	      ;
        myWCHAR	        :       WCHAR	      ;
        mySTRING	        :   STRING	      ;
        myWSTRING	    :       WSTRING	      ;

        mins : full_of_primitives;
        maxs : full_of_primitives;

        minsmatch : full_of_primitives_match;
        maxsmatch : full_of_primitives_match;

        myColors : Colors;

        Hierarchy : hierarchy;


        myDATE_leap_febr	        :       DATE	      ;
        myLDATE_leap_febr	        :       LDATE	      ;
        myDATE_AND_TIME_leap_febr   :      DATE_AND_TIME ;
        myLDATE_AND_TIME_leap_febr  :      LDATE_AND_TIME;

        myDATE_leap_late	        :       DATE	      ;
        myLDATE_leap_late	        :       LDATE	      ;
        myDATE_AND_TIME_leap_late   :      DATE_AND_TIME ;
        myLDATE_AND_TIME_leap_late  :      LDATE_AND_TIME;
        //myArrayString: ARRAY[0..10] OF STRING ;
        //myArrayComplex: ARRAY[0..10] OF complexArrayItem; 

    END_VAR

    VAR
        _item : complexArrayItem;
    END_VAR

    VAR_TEMP

    END_VAR
   
    //myArrayComplex[1] :=  _item;
    //myArrayString[1] := 'ahoj2';

    

    //myDATE := DATE#1970-01-01;
    //myColors := Colors#GREEN;
    //mins.myBOOL	            :=  false    ;
    mins.myBYTE	            :=  BYTE#2#0    ;
    mins.myWORD	            :=  WORD#0    ;
    mins.myDWORD	        :=  DWORD#16#0    ;
    mins.myLWORD	        :=  LWORD#0    ;
    mins.mySINT	            :=  SINT#-128    ;
    mins.myINT	            :=  INT#-32768      ;
    mins.myDINT	            :=  DINT#-2147483648;
    mins.myLINT	            :=  LINT#-9223372036854775808    ;
    mins.myUSINT	        :=  USINT#0    ;
    mins.myUINT	            :=  UINT#0    ;
    mins.myUDINT	        :=  UDINT#0     ;
    mins.myULINT	        :=  ULINT#0    ;
    mins.myREAL	            :=  REAL#-3.402823e+38    ;
    mins.myLREAL	        :=  LREAL#-1.79769313486231e+308;
    mins.myTIME	            :=  TIME#-106751d23h47m16s854ms;
    mins.myLTIME	        :=  LTIME#-106751d23h47m16s854ms;
    mins.myDATE	            :=  DATE#1970-01-01    ;
    mins.myLDATE	        :=  LDATE#1970-01-01    ;
    mins.myTIME_OF_DAY	    :=  TOD#0:0:0.0   ;
    mins.myLTIME_OF_DAY	    :=  LTOD#0:0:0.0    ;
    mins.myDATE_AND_TIME    :=  DT#1970-01-01-0:0:0.0   ;
    mins.myLDATE_AND_TIME   :=  LDT#1970-01-01-0:0:0.0   ;
    mins.myCHAR	            :=  CHAR#' '    ;
    mins.myWCHAR	        :=  WCHAR#" "    ;
    mins.mySTRING	        :=  STRING#'';
    mins.myWSTRING	        :=  WSTRING#""    ;
   

    maxs.myBOOL	            :=  TRUE    ;
    maxs.myBYTE	            :=  BYTE#255    ;
    maxs.myWORD	            :=  WORD#065535    ;
    maxs.myDWORD	        :=  DWORD#4294967295    ;
    maxs.myLWORD	        :=  LWORD#18446744073709551615    ;
    maxs.mySINT	            :=  SINT#127    ;
    maxs.myINT	            :=  INT#32767      ;
    maxs.myDINT	            :=  DINT#2147483647;
    maxs.myLINT	            :=  LINT#9223372036854775807    ;
    maxs.myUSINT	        :=  USINT#255    ;
    maxs.myUINT	            :=  UINT#65535    ;
    maxs.myUDINT	        :=  UDINT#4294967295     ;
    maxs.myULINT	        :=  ULINT#18446744073709551615    ;
    maxs.myREAL	            :=  REAL#+3.402823e+38    ;
    maxs.myLREAL	        :=  LREAL#1.79769313486231e+308;
    maxs.myTIME	            :=  TIME#106751d23h47m16s854ms;
    maxs.myLTIME	        :=  LTIME#106751d23h47m16s854ms;    
    maxs.myLTIME	        :=  LTIME#106751d23h47m16s854ms;
    // maxs.myLTIME	        :=  LTIME#1067d12h28m40s368ms547us758ns;      
    maxs.myDATE	            :=  DATE#2262-04-11    ;
    maxs.myLDATE	        :=  LDATE#2262-04-11    ;
    maxs.myTIME_OF_DAY	    :=  TOD#23:59:59.999000000   ;
    maxs.myLTIME_OF_DAY	    :=  LTOD#23:59:59.999000000    ;
    maxs.myDATE_AND_TIME    :=  DT#2262-04-11-23:47:16.854   ;
    maxs.myLDATE_AND_TIME   :=  LDT#2262-04-11-23:47:16.854   ;
    maxs.myCHAR	            :=  CHAR#'$7F';
    maxs.myWCHAR	        :=  WCHAR#"$D7FE";
    maxs.mySTRING	        :=  STRING#'ABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMN';
    maxs.myWSTRING	        :=  WSTRING#"ABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMNOPRSTUABCDEFGHIJKLMN";



    minsmatch.myBOOL	        := mins.myBOOL	         =  myBOOL	        ;
    minsmatch.myBYTE	        := mins.myBYTE	         =  myBYTE	        ;
    minsmatch.myWORD	        := mins.myWORD	         =  myWORD	        ;
    minsmatch.myDWORD	        := mins.myDWORD	         =  myDWORD	        ;
    minsmatch.myLWORD	        := mins.myLWORD	         =  myLWORD	        ;
    minsmatch.mySINT	        := mins.mySINT	         =  mySINT	        ;
    minsmatch.myINT	            := mins.myINT	         =  myINT	        ;
    minsmatch.myDINT	        := mins.myDINT	         =  myDINT	        ;
    minsmatch.myLINT	        := mins.myLINT	         =  myLINT	        ;
    minsmatch.myUSINT	        := mins.myUSINT	         =  myUSINT	        ;
    minsmatch.myUINT	        := mins.myUINT	         =  myUINT	        ;
    minsmatch.myUDINT	        := mins.myUDINT	         =  myUDINT	        ;
    minsmatch.myULINT	        := mins.myULINT	         =  myULINT	        ;
    minsmatch.myREAL	        := mins.myREAL	         =  myREAL	        ;
    minsmatch.myLREAL	        := mins.myLREAL	         =  myLREAL	        ;
    minsmatch.myTIME	        := mins.myTIME	         =  myTIME	        ;
    minsmatch.myLTIME	        := mins.myLTIME	         =  myLTIME	        ;
    minsmatch.myDATE	        := mins.myDATE	         =  myDATE	        ;
    minsmatch.myLDATE	        := mins.myLDATE	         =  myLDATE	        ;
    minsmatch.myTIME_OF_DAY	    := mins.myTIME_OF_DAY	 =  myTIME_OF_DAY	;
    minsmatch.myLTIME_OF_DAY	:= mins.myLTIME_OF_DAY	 =  myLTIME_OF_DAY	;
    minsmatch.myDATE_AND_TIME   := mins.myDATE_AND_TIME  =  myDATE_AND_TIME ;
    minsmatch.myLDATE_AND_TIME  := mins.myLDATE_AND_TIME =  myLDATE_AND_TIME;
    minsmatch.myCHAR	        := mins.myCHAR	         =  myCHAR	        ;
    minsmatch.myWCHAR	        := mins.myWCHAR	         =  myWCHAR	        ;
    minsmatch.mySTRING	        := mins.mySTRING	     =  mySTRING	    ;   
    minsmatch.myWSTRING	        := mins.myWSTRING	     =  myWSTRING	    ;


    maxsmatch.myBOOL	        := maxs.myBOOL	         =  myBOOL	        ;
    maxsmatch.myBYTE	        := maxs.myBYTE	         =  myBYTE	        ;
    maxsmatch.myWORD	        := maxs.myWORD	         =  myWORD	        ;
    maxsmatch.myDWORD	        := maxs.myDWORD	         =  myDWORD	        ;
    maxsmatch.myLWORD	        := maxs.myLWORD	         =  myLWORD	        ;
    maxsmatch.mySINT	        := maxs.mySINT	         =  mySINT	        ;
    maxsmatch.myINT	            := maxs.myINT	         =  myINT	        ;
    maxsmatch.myDINT	        := maxs.myDINT	         =  myDINT	        ;
    maxsmatch.myLINT	        := maxs.myLINT	         =  myLINT	        ;
    maxsmatch.myUSINT	        := maxs.myUSINT	         =  myUSINT	        ;
    maxsmatch.myUINT	        := maxs.myUINT	         =  myUINT	        ;
    maxsmatch.myUDINT	        := maxs.myUDINT	         =  myUDINT	        ;
    maxsmatch.myULINT	        := maxs.myULINT	         =  myULINT	        ;
    maxsmatch.myREAL	        := maxs.myREAL	         =  myREAL	        ;
    maxsmatch.myLREAL	        := maxs.myLREAL	         =  myLREAL	        ;
    maxsmatch.myTIME	        := maxs.myTIME	         =  myTIME	        ;
    maxsmatch.myLTIME	        := maxs.myLTIME	         =  myLTIME	        ;
    maxsmatch.myDATE	        := maxs.myDATE	         =  myDATE	        ;
    maxsmatch.myLDATE	        := maxs.myLDATE	         =  myLDATE	        ;
    maxsmatch.myTIME_OF_DAY	    := maxs.myTIME_OF_DAY	 =  myTIME_OF_DAY	;
    maxsmatch.myLTIME_OF_DAY	:= maxs.myLTIME_OF_DAY	 =  myLTIME_OF_DAY	;
    maxsmatch.myDATE_AND_TIME   := maxs.myDATE_AND_TIME  =  myDATE_AND_TIME ;
    maxsmatch.myLDATE_AND_TIME  := maxs.myLDATE_AND_TIME =  myLDATE_AND_TIME;
    maxsmatch.myCHAR	        := maxs.myCHAR	         =  myCHAR	        ;
    maxsmatch.myWCHAR	        := maxs.myWCHAR	         =  myWCHAR	        ;
    maxsmatch.mySTRING	        := maxs.mySTRING	     =  mySTRING	    ;   
    maxsmatch.myWSTRING	        := maxs.myWSTRING	     =  myWSTRING	    ;


    myDATE_leap_febr	        :=  DATE#2024-02-29;
    myLDATE_leap_febr	        :=  LDATE#2024-02-29;
    myDATE_AND_TIME_leap_febr   :=  DATE_AND_TIME#2024-02-29-0:0:0.0;
    myLDATE_AND_TIME_leap_febr  :=  LDATE_AND_TIME#2024-02-29-0:0:0.0;

    myDATE_leap_late	        :=   DATE#2024-03-05;
    myLDATE_leap_late	        :=   LDATE#2024-03-05;
    myDATE_AND_TIME_leap_late   :=   DATE_AND_TIME#2024-03-05-0:0:0.0;
    myLDATE_AND_TIME_leap_late  :=   LDATE_AND_TIME#2024-03-05-0:0:0.0;
END_PROGRAM
