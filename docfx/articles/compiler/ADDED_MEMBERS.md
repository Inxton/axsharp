# Added members

The AXSharp Compiler allows the declaration of additional members for any TwinObject. These members will be added to the TwinObject but not accessible from the PLC. Added members are useful when we want to provide additional usually static information about the object (description, humanized annotation, etc), but we do not want to put additional load on the communication between the application and the PLC.
Added members are also a useful feature to provide additional context about the type or its particular instance.

## Added member declaration

Syntax

~~~ iecst
{#ix-prop:access_modifier type member_name}
~~~

Example

~~~iecst
{S7.extern=ReadWrite}
{#ix-prop:public string Description}
CLASS PUBLIC MyClass
    VAR PUBLIC
        Nested : MyNestedStructure;
    END_VAR
END_CLASS
~~~

This will translate into C# as

~~~C#
public MyClass 
{
    public string Description { get; set; }
    .
    .
    .
}
~~~

## Added member value setter

The value of any added property will be set in the type constructor.

## Setting value on type declaration (default value).

Syntax declaring at the type level
~~~ iecst
{#ix-set:member_name = value}
CLASS_DECLARATION | STRUCTURED_TYPE_DECLARATION
~~~

Example

~~~iecst
{S7.extern=ReadWrite}
{#ix-prop:public string Description}
{#ix-set:Description = "This is my classy description."}
CLASS PUBLIC MyClass
    VAR PUBLIC
        Nested : MyNestedStructure;
    END_VAR
END_CLASS
~~~

This will translate in the type constructor as

~~~C#
    public MyClass(....)
    {
        Description = "This is my classy decsription.";
        .
        .
        .
    }
~~~

## Setting value on member declaration


Syntax

~~~ iecst
{#ix-set:member_name = value}
FIELD_DECLARATION | VARIABLE_DECLARATION
~~~

~~~iecst
{S7.extern=ReadWrite}
CLASS PUBLIC MyClass
    VAR PUBLIC
        {#ix-set:Description = "This is my nested classy description."}
        Nested : MyNestedStructure;
    END_VAR
END_CLASS

{S7.extern=ReadWrite}
{#ix-prop:public string Description}
CLASS PUBLIC MyNestedStructure 
END_CLASS

~~~


> When declaring the value of an added member with pragma belonging to a member declaration the compiler will take care of assigning the value to the correct instance member.


## Default properties of Twins


## Common added members

**The following members are present by default in any type created by AX# compiler.**

## AttributeName

AttributeName is the default added member of all types within AXSharp. They are used to represent the name of the variable or object in a humanized way. These attributes can be later used to label the variables consistently in the UI.

~~~
{#ix-set: AttributeName = "Length"}
_length : REAL;
~~~

## Primitive added members

**The following members are present by default in any primitive type create by AXSharp compiler.**


## AttributeUnits

AttributeUnit is the default added member of all PrimitiveTwins within AXSharp. They are used to represent the unit measure of a variable.

~~~
{#ix-set: AttributeUnits = "mm"}
_length : REAL;
~~~

~~~ C#
// Writes unit of the '_lenght' variable to the console.
System.Console.WriteLine(MainPlc.MAIN._length.AttributeUnits);
~~~

## AttributeMinimum

AttributeMinimum is the default added member of all PrimitiveTwins within AXSharp. They are used to get or set the minimum value for the variable. By default, this attribute contains the minimal value of a given type.

~~~
{#ix-set: AttributeMinimum = 10.5f}
_length : REAL;
~~~

~~~ C#
// Writes min value of '_length' variable to the console.
System.Console.WriteLine(MainPlc.MAIN._length.AttributeMinimum);
~~~


## AttributeMaximum

AttributeMaximum is the default added member of all PrimitiveTwins within AXSharp. They are used to get or set the maximum value for the variable. By default, this attribute contains the maximum value of a given type.

~~~
{#ix-set: AttributeMaximum = 1525.5f}
_length : REAL;
~~~

~~~ C#
// Writes max. value of '_length' variable to the console.
System.Console.WriteLine(MainPlc.MAIN._length.AttributeMaximum);
~~~

## AttributeMinimum and AttributeMaximum at run time

NOT IMPLEMENTED FULLY!!!

When the application tries to write to the variable it first validates that the value to be written corresponds to the limit given by AttributeMinimum and AttributeMaximum. If the value to be written does not fall within the range this value is not written to the PLC and the writing operation is silently ignored.

## AttributeToolTip

AttributeToolTip allows you to describe the variable or an object. These can be then used to give short hints to the user in the application. This attribute can be localized.

