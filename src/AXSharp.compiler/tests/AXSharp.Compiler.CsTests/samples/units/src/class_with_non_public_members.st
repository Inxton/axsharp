NAMESPACE ClassWithNonTraspilableMemberssNamespace
   {S7.extern=ReadWrite}
    CLASS PUBLIC ClassWithNonTraspilableMembers
        VAR PUBLIC            
           myComplexType : ComplexType1;
        END_VAR   
        
        VAR            
           myComplexType1 : ComplexType1;
           myBooool : BOOL;
        END_VAR   

        VAR            
           myComplexType2 : ComplexType1;
           myBooool1 : BOOL;
        END_VAR   
    END_CLASS
    {S7.extern=ReadWrite}
    CLASS PUBLIC ComplexType1
       
    END_CLASS
END_NAMESPACE    