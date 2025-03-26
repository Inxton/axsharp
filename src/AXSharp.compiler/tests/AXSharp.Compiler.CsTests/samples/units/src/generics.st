// GENERIC
NAMESPACE GenericsTests
    {#ix-generic:<TOnline, TPlain> where TOnline : ITwinObject}
	{S7.extern=ReadWrite}
    CLASS PUBLIC Extender
    
    END_CLASS

    {S7.extern=ReadWrite}
	CLASS PUBLIC SomeTypeToBeGeneric
	     VAR PUBLIC
		    Boolean : BOOL;
			Cele : INT;
		 END_VAR
	END_CLASS

	{S7.extern=ReadWrite}
   CLASS PUBLIC Extendee2 EXTENDS Extender
        VAR PUBLIC
            {#ix-generic:TOnline}
            {#ix-generic:TPlain as POCO}
            {#ix-attr:[AXOpen.Data.AxoDataEntityAttribute]}
            {#ix-attr:[Container(Layout.Stack)]} 
            {#ix-set:AttributeName = "Shared Header"}
            SomeData : SomeTypeToBeGeneric;          
        END_VAR
   END_CLASS
END_NAMESPACE    