{S7.extern=ReadWrite}
CLASS ixcomponent
    VAR PUBLIC
        {#ix-set:AttributeName = "My integer"}
        my_int : INT;
        {#ix-set:AttributeName = "My string"}
        my_string : STRING;
        {#ix-set:AttributeName = "My bool"}	
        my_bool : BOOL;
    END_VAR

    METHOD PUBLIC Simulate 
        my_int := my_int + INT#1;        
    END_METHOD
END_CLASS

NAMESPACE MySecondNamespace
    {S7.extern=ReadWrite}
    CLASS ixcomponent
        VAR PUBLIC
            {#ix-set:AttributeName = "My integer"}
            my_int : INT;
            {#ix-set:AttributeName = "My string"}
            my_string : STRING;
            {#ix-set:AttributeName = "My bool"}	
            my_bool : BOOL;
        END_VAR
    END_CLASS
END_NAMESPACE

NAMESPACE ThirdNamespace
    {S7.extern=ReadWrite}
    CLASS ixcomponent
        VAR PUBLIC
            {#ix-set:AttributeName = "My integer"}
            my_int : INT;
            {#ix-set:AttributeName = "My string"}
            my_string : STRING;
            {#ix-set:AttributeName = "My bool"}	
            my_bool : BOOL;
        END_VAR
    END_CLASS
END_NAMESPACE