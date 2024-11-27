![](docfx/images/banner_wider.png)

[![dev](https://github.com/ix-ax/axsharp/actions/workflows/dev.yml/badge.svg?branch=dev)](https://github.com/ix-ax/axsharp/actions/workflows/dev.yml)
[![preview](https://github.com/ix-ax/axsharp/actions/workflows/release.yml/badge.svg?branch=releases%2Fv0)](https://github.com/ix-ax/axsharp/actions/workflows/release.yml)
[![master](https://github.com/ix-ax/axsharp/actions/workflows/master.yml/badge.svg?branch=master)](https://github.com/ix-ax/axsharp/actions/workflows/master.yml)
![semver](https://img.shields.io/badge/semver-0.10.0-blue)
[![GitHub license](https://badgen.net/github/license/Naereen/Strapdown.js)](https://github.com/ix-ax/axsharp/blob/dev/LICENSE)

> **NOTE**
> **We have just gone through project-wide refactoring changing the name from **IX** to **AX#** (AXSharp). Some links and documentation may still be using the IX and some links are still broken at this point. We are working hard on fixing those issues. Should any of these block you please feel free to reach out to the team.**


# AX#

The **AX#** is a series of tools that extend SIMATIC AX (Automation Xpansion) with a flexible and powerful connection with .NET ecosystem. AX# includes a compiler (`ixc`) that translates PLC data structures into C# (PLC .NET Twin), which makes the PLC data available in a structured way for any .NET application. Furthermore, presentation libraries provide additional features for the automated rendering of PLC data in the UI/HMI.

## State of the project

This project is under development; however, we are [releasing versions](https://github.com/ix-ax/axsharp/releases) that you can play with before the release of the full version. This project follows [semantic versioning](https://semver.org/).

All versions released with a major version number `0` (e.g. 0.10.0) can have breaking changes to the previous version at any moment. Stable versions will be released with a major version number greater than `0` (e.g. 1.2.1).

We plan to have production-ready libraries and tools in early spring 2024, that will cover:

- compiler for building .NET twin object (ix compiler)
- communication layer between .NET twin objects and S71500 series PLCs
- user interface generator for Blazor application
- application templates for quick development and deployment.


**Documentation** is a work in progress should you find missing, unclear, or misleading content please feel free to [add an issue](https://github.com/ix-ax/axsharp/issues/new/choose) or to create a pull request with the fix you find appropriate.

There are some **known issues** that we are looking into in the development process. The list of known issues is [here](https://github.com/ix-ax/axsharp/issues?q=is%3Aissue+is%3Aopen+label%3Aknown-issue).



**AX#** is the underlying technology for the [AXOpen](https://github.com/ix-ax/AXOpen) that will be rendered public in this organization and will provide a series of libraries and components for building automation projects.

## Disclaimer

**It is necessary to have a valid license for SIMATIC AX in order to use AX#!**  
SIMATIC AX is currently in a limited sales release in selected European countries only. You will need to request access from the AX team which will check if your use case is suitable for the current state of the product. The first step to getting the approval is contacting your local SIEMENS sales representative or writing an email to [simatic-ax@siemens.com](mailto:simatic-ax@siemens.com?subject=Request%20for%20access%20|%20SIMATIC%20AX%20for%20IX).

## How it works

In simple terms, the AX# takes the PLC program and translates the data structured into .NET classes.

### Write PLC code

~~~iecst
{S7.extern=ReadWrite}
{#ix-attr:[Container(Layout.Stack)]}
{#ix-attr:[Group(Layout.GroupBox)]}
{#ix-set:AttributeName = "Location"}
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

### Automatically render in HMI

![](assets/pics/2022-12-20-06-54-08.png)


## Getting started

To get started, visit the documentation [here](docfx/index.md#getting-started).


## Examples 

Examples can be found [here](src/AXSharp.examples)


## Contributing

Contributions are welcomed. Have a look at [contributing document](CONTRIBUTING.md).
