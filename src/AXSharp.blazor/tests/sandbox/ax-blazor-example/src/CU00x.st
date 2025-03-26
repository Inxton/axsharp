{S7.extern=ReadWrite}
CLASS CU00x EXTENDS CUBase
    VAR PUBLIC
        _cuName: STRING;
    END_VAR    
END_CLASS

{S7.extern=ReadWrite}
CLASS CUBase
    VAR PUBLIC
        _baseName : STRING;
    END_VAR       
END_CLASS    