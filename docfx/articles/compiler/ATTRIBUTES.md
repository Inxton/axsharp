# Attributes

Any type or type member (CLASS, STRUCT...) can have declared attributes from within `PLC` code using pragmas.

Syntax

~~~ iecst
{#ix-attr:[TypeAttribute()]}
~~~

Example

~~~iecst
{S7.extern=ReadWrite}
{#ix-attr:[Container(Layout.Wrap)]} (* Example of an attribute declared at type level. *)
CLASS PUBLIC MyClass
    VAR PUBLIC
        {#ix-attr:[Container(Layout.Tabs)]} (* Example of an attribute declared at member level.*)
        Nested : MyNestedStructure;
    END_VAR
END_CLASS
~~~

## Special attributes

### ReadOnce attribute

ReadOnce attribute instructs the communication layer to read a member (variable) only once during the application's lifetime.
This feature aims at reducing the load on the communication layer. You would typically use this attribute when you have immutable data
such as description strings needing to be accessed only once.
ReadOnce attribute will only limit reading the members during periodic (cyclic) access. It will not affect the direct reading of a variable 
or explicit call of batch reading on a complex structure.
ReadOnce attribute can be only applied to members (property, field), not type (CLASS, STRUCT) declaration.


Example

~~~iecst
{S7.extern=ReadWrite}
CLASS PUBLIC MyClass
    VAR PUBLIC
        {#ix-attr:[ReadOnce()]} // this structure will be read only once
        Nested : MyNestedStructure;
        {#ix-attr:[ReadOnce()]} // this variable will be read only once
        MyString : STRING;
    END_VAR
END_CLASS
~~~

### ReadOnly attribute

ReadOnly attribute render the member (variable) inaccessible for write operation from the AXSharp application. It does not prevent the variable from writing in the PLC or by other means of connection.


~~~iecst
{S7.extern=ReadWrite}
CLASS PUBLIC MyClass
    VAR PUBLIC
        {#ix-attr:[ReadOnly()]} // this structure will be read only
        Nested : MyNestedStructure;
        {#ix-attr:[ReadOnly()]} // this variable will be read only
        MyString : STRING;
    END_VAR
END_CLASS
~~~

### CompilerOmits attribute

CompilerOmits attribute instructs the compiler to skip the compilation of a member for specific output. `IXC` supports these omissions:

|   Target    |                      Description                       |
| ----------- | ------------------------------------------------------ |
| Onliner     | Will skip the emission of the member for onliner twins |
| POCO        | Will skip the emission of the member for POCO twins    |
| -No params- | Will skip the emission for all output types            |


~~~iecst
{S7.extern=ReadWrite}
CLASS PUBLIC MyClass
    VAR PUBLIC
        {#ix-attr:[CompilerOmits()]} 
        MyStringIgnoredForAllOutputs : STRING;
        {#ix-attr:[CompilerOmits("Onliner")]} 
        MyStringIgnoredInOnliner : STRING;
        {#ix-attr:[CompilerOmits("POCO")]} 
        MyStringIgnoredInPocos : STRING;
        {#ix-attr:[CompilerOmits("POCO", "Onliner")]} 
        MyStringIgnoredInPocosAndOnliners : STRING;
    END_VAR
END_CLASS
~~~

### Ignoring Communication with the Controller for POCO Operations

To minimize the communication overhead between the application and the controller during POCO operations, you can annotate specific members of a type to bypass data exchange. This attribute should be applied when the compiler omits the compilation of particular members in the POCO object.

```iecst
{S7.extern=ReadWrite}
CLASS PUBLIC MyClass
    VAR PUBLIC        
        {#ix-attr:[CompilerOmits("POCO")]} 
        {#ix-attr:[IgnoreOnPocoOperation()]} 
        MyStringIgnoredInPocos : STRING;        
    END_VAR
END_CLASS
```

### Generic extension attributes

ixc allows to declare generic attributes in ST that will add a genetic notation to transpiled types.
The use of generics is an advanced scenario aimed at simplifying some tasks where templating is needed. 
This feature was explicitly crafted for data exchange scenarios, and it does not support the entire range of use of generics in C#.


#### Use

Any class can be annotated with the following attribute:

~~~iecst
   {#ix-generic:<TOnline, TPlain> where TOnline : ITwinObject}
    CLASS PUBLIC Extender
    
    END_CLASS
~~~

That will create the following type declaration in twin type:

~~~C#
public partial class Extender<TOnline, TPlain> : AXSharp.Connector.ITwinObject where TOnline : ITwinObject
~~~


When deriving from a class with generic annotation following additional annotation should be used for a generic member of the class:

~~~iecst
    CLASS PUBLIC Extendee2 EXTENDS Extender
        VAR PUBLIC
            {#ix-generic:TOnline}
            {#ix-generic:TPlain as POCO}
            SomeType : SomeType;          
        END_VAR
    END_CLASS
~~~

Where TOnline generic type will be substituted with `SomeType`. The `as POCO` will transpale `TPlain` generic attribute as the corresponding plain (aka POCO) type.

The previous example will transpile as follows.
~~~C#
public partial class Extendee : Generics.Extender<Generics.SomeType, Pocos.Generics.SomeType>
~~~

### See also 

#### [RenderIgnore](../blazor/RENDERABLECONTENT.md#renderignore-and-custom-labels)
