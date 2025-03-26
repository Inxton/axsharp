{S7.extern=ReadWrite}
CLASS stTest
    VAR PUBLIC
        p1:INT;
        p2:INT;
        stTest3Struct: stTest3;
        DateVar2: DATE;
        complexInstanceNested : stComplex := (stComplexInteger := 13, stComplexString := 'hello nested'); 
    END_VAR
    
END_CLASS