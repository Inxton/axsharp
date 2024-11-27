{S7.extern=ReadWrite}
CLASS stTest2
    VAR
        testVarString: STRING;
        testVarInt: INT;
        instanceOfstTest3: stTest3;
        arr1 : ARRAY [0..2] OF stTest := [(p1 := 445, p2 := 446),(p1 := 152, p2 := 100),(p1 := 121,p2 := 338)];	
    END_VAR
    
END_CLASS