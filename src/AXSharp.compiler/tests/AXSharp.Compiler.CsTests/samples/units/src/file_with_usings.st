USING FileWithUsingsSimpleFirstLevelNamespace;
USING FileWithUsingsSimpleQualifiedNamespace.Qualified;
USING FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo;

NAMESPACE FileWithUsingsSimpleFirstLevelNamespace
    CLASS Hello
        VAR 
            
        END_VAR    
    END_CLASS   
END_NAMESPACE

NAMESPACE FileWithUsingsSimpleQualifiedNamespace.Qualified
    CLASS Hello
        VAR 
            
        END_VAR    
    END_CLASS 
END_NAMESPACE

NAMESPACE FileWithUsingsHelloLevelOne
    NAMESPACE FileWithUsingsHelloLevelTwo
        CLASS Hello
            VAR 
            
            END_VAR    
        END_CLASS
    END_NAMESPACE    
END_NAMESPACE    

NAMESPACE ExampleNamespace
    CLASS Hello
        VAR 
            a : FileWithUsingsSimpleFirstLevelNamespace.Hello;
            b : FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello;
            c : FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello;
        END_VAR    
    END_CLASS    
END_NAMESPACE