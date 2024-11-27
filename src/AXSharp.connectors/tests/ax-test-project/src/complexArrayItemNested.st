{S7.extern=ReadWrite}
CLASS complexArrayItemNested
    VAR PUBLIC
        nesteditemBOOL	        :       BOOL	      ;
        nesteditemSTRING	        :   STRING	      ;
        nesteditemINT	    :       INT	      ;
        nestedComplex: complexArrayItem;
    END_VAR    
END_CLASS