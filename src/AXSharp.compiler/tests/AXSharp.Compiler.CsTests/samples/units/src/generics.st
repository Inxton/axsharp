// GENERIC
NAMESPACE Generics
    {#ix-generic:<TOnline, TPlain> where TOnline : ITwinObject}
    CLASS PUBLIC Extender
    
    END_CLASS

	CLASS SomeType
	     VAR PUBLIC
		    Boolean : BOOL;
			Cele : INT;
		 END_VAR
	END_CLASS

   CLASS PUBLIC Extendee2 EXTENDS Extender
        VAR PUBLIC
            {#ix-generic:TOnline}
            {#ix-generic:TPlain as POCO}
            {#ix-attr:[AXOpen.Data.AxoDataEntityAttribute]}
            {#ix-attr:[Container(Layout.Stack)]} 
            {#ix-set:AttributeName = "Shared Header"}
            SomeType : SomeType;          
        END_VAR
   END_CLASS
END_NAMESPACE    