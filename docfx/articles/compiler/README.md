# AXSharp Compiler

**AXSharp Compiler (`ixc`) translates PLC data structures into C# (PLC .NET Twin), which makes the PLC data available in a structured way for any .NET application.**

### Adding types and members to the communication over WebAPI

Starting from the version v2.0.0+ of `sld`, to make member or type accessible over the communication there is a need to add pragma `{S7.extern=ReadWrite}` or `{S7.extern=ReadOnly}` in the appropriate place in the code.

For details see [here](https://console.simatic-ax.siemens.io/docs/sld/extern-accessibility)


### Write PLC code

~~~iecst
{#ix-attr:[Container(Layout.Stack)]}
{#ix-attr:[Group(Layout.GroupBox)]}
{#ix-set:AttributeName = "Location"}
{S7.extern=ReadWrite}
CLASS  GeoLocation
    VAR PUBLIC
        {#ix-set:AttributeName = "Latitude [°]"}
        {#ix-set:AttributeMinimum = -90.0f}
        {#ix-set:AttributeMaximum = 90.0f}
        Latitude : REAL;
        {#ix-set:AttributeName = "Logitude [°]"}
        {#ix-set:AttributeMinimum = 0.0f}
        {#ix-set:AttributeMaximum = 180.0f}
        Longitude : REAL;
        {#ix-set:AttributeName = "Altitude [m]"}
        Altitude : REAL;
        {#ix-set:AttributeName = "Short descriptor"}
        Description : STRING[10];
        {#ix-set:AttributeName = "Long descriptor"}
        LongDescription : STRING[254];
    END_VAR    
END_CLASS
~~~

### Compile to get .NET twin

~~~ C#
using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;

[Container(Layout.Stack)]
[Group(Layout.GroupBox)]
public partial class GeoLocation : AXSharp.Connector.ITwinObject
{
    public OnlinerReal Latitude { get; }

    public OnlinerReal Longitude { get; }

    public OnlinerReal Altitude { get; }

    public OnlinerString Description { get; }

    public OnlinerString LongDescription { get; }

    --- truncated ----
~~~

You can then access the data in your .NET application

### Use in any .NET application

~~~C#
// Write
await Entry.Plc.weather.GeoLocation.Longitude.SetAsync(10);

// Read
var longitude = await Entry.Plc.weather.GeoLocation.Longitude.GetAsync();

// Bulk read
Entry.Plc.weather.GeoLocation.Read();

// Bulk write
Entry.Plc.weather.GeoLocation.Write();
~~~

- [Attributes](ATTRIBUTES.md)
- [Added members](ADDED_MEMBERS.md)
- [Config file](CONFIG_FILE.md)
- [Packaging and dependency management](PACKAGING.md)

AXSharp compiles transpiles the following project blocks:

- [Configuration's global variables](https://console.simatic-ax.siemens.io/docs/st/language/program-structure/configuration#global-variables)
- [Elementary data types](https://console.simatic-ax.siemens.io/docs/st/language/types-and-variables#elementary-data-types)
- [Class](https://console.simatic-ax.siemens.io/docs/st/language/program-structure/program-organization-unit#class-declaration)
- [User defined data types](https://console.simatic-ax.siemens.io/docs/st/language/types-and-variables#user-defined-data-types)
- [Strucured types](https://console.simatic-ax.siemens.io/docs/st/language/types-and-variables#structured-type-without-relative-addressing)
- [Data type with named values as enums](https://console.simatic-ax.siemens.io/docs/st/language/types-and-variables#data-type-with-named-values)
- [Enumerations](https://console.simatic-ax.siemens.io/docs/st/language/types-and-variables#enumeration)





