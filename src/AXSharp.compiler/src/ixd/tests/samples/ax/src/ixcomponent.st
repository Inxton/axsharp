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

NAMESPACE MySecondNamespace
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