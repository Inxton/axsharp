CLASS INTERNAL ClassWithUsingDirectives
    USING SimpleFirstLevelNamespace, SimpleQualifiedNamespace.Qualified;
    USING HelloLevelOne.HelloLevelTwo;
    USING InexistingNamespace;
END_CLASS

NAMESPACE SimpleFirstLevelNamespace
    
END_NAMESPACE

NAMESPACE SimpleQualifiedNamespace.Qualified
    
END_NAMESPACE

NAMESPACE HelloLevelOne
    NAMESPACE HelloLevelTwo
    END_NAMESPACE    
END_NAMESPACE    