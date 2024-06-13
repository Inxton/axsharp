{S7.extern=ReadWrite}
CLASS stRenderTemplateOverride
    VAR PUBLIC
        {#ix-set:AttributeName = "<#Test Integer#>"}
        testInteger : INT;
        {#ix-attr:[RenderTemplateOverrideAttribute("ixBlazor.App.Custom.MySimplePrimitiveTag")]}
        {#ix-set:AttributeName = "<#Test String#>"}
        testString : STRING;
    END_VAR
END_CLASS