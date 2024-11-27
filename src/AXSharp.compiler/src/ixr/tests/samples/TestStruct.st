///<summary>
/// Gets the state of this command.
///</summary>

CONFIGURATION Default
    PROGRAM P1 : PRG;
    VAR_GLOBAL
        s AT %MB0 : IoStruct;
        s2 AT %MB100 : IoStruct;
        s3 AT %MB200: IoStruct;
        norm : ARRAY [0..1] OF INT := [123, 456];
        test: TestObj;

        s_0, s_1 : INT;
        s2_0, s2_1 : INT;
        s3_0, s3_1 : INT;
        norm_0, norm_1 : INT;
    END_VAR
END_CONFIGURATION
    
TYPE
    IoStruct : STRUCT
        ioArr AT %B0 : ARRAY [0..1] OF INT;
    END_STRUCT;

    s : STRUCT
        ddsd : INT;
    END_STRUCT; 
END_TYPE

/// <summary>
///  Hello class
/// </summary>
// <params>

CLASS TestObj 
    VAR PUBLIC
    
        myBOOL	        :       BOOL	      ;
        myBYTE	        :       BYTE	      ;
        myWORD	        :       WORD	      ;
    END_VAR
    
END_CLASS

PROGRAM PRG
    VAR_EXTERNAL
        s : IoStruct;
        s2 : IoStruct;
        s3 : IoStruct;
        norm : ARRAY [0..1] OF INT;

        test: TestObj;

        s_0, s_1 : INT;
        s2_0, s2_1 : INT;
        s3_0, s3_1 : INT;
        norm_0, norm_1 : INT;
    END_VAR
    VAR
        done : BOOL;
    END_VAR

    IF done THEN RETURN; END_IF;

    done := TRUE;

    s2.ioArr[0] := INT#16#ABCD;
    s2.ioArr[1] := INT#16#EF01;

    s3.ioArr := s2.ioArr;
    s.ioArr := norm;
    norm := s2.ioArr;

    test.myBOOL := true;
    s_0 := s.ioArr[0];
    s_1 := s.ioArr[1];
    s2_0 := s2.ioArr[0];
    s2_1 := s2.ioArr[1];
    s3_0 := s3.ioArr[0];
    s3_1 := s3.ioArr[1];
    norm_0 := norm[0];
    norm_1 := norm[1];

END_PROGRAM
