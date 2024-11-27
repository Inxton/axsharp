# AXSharp.Presentation.Blazor

AXSharp.Presentation.Blazor is set of libraries, which provides automatic generation of UI and custom styles.  


---

## Prerequisites

[Checkout you have installed all prerequisites](~/index.md#prerequisites)

[Install package source](~/index.md#add-package-source)

 ---
## Installing


### **Install latest NuGet package AXSharp.Presentation.Blazor.Controls**



~~~
$ dotnet add package AXSharp.Presentation.Blazor.Controls 
~~~

---
### **Add AXSharp namespace to Blazor application.**


Add the following line to`_Imports.razor` file:

```
@using AXSharp.Presentation.Blazor.Controls.RenderableContent
```
- - -
**Register AXSharp.Framework.Blazor services in DI container and build PLC connector.**

Add AXSharp services to container located in `Program.cs` file and build PLC connector:

```
builder.Services.AddIxBlazorServices();
Entry.Plc.Connector.BuildAndStart();
```

Notes: 
- Replace `Plc` with the name of your PLC instance.
- AXSharp.Framework.Blazor services are located in *AXSharp.Presentation.Blazor.Services* namespace.

---
## How to access PLC variables and automatically update UI

To access PLC variables and notify UI on value change, `RenderableComponentBase` class must be inherited and `UpdateValuesOnChange` must be invoked. Otherwise UI won't be updated on PLC value change. 


```C#
@page "/"
@inherits RenderableComponentBase

<p>@Entry.Plc.Counter.Cyclic</p>

@code
{       
    protected override void OnInitialized()
    {
        UpdateValuesOnChange(Entry.Plc.Counter);
    }
}
```
 

---

## Automatic renderer of UI

**RenderableContentControl** is Blazor component, which can automatically render UI from PLC structures. The changes of PLC values are automatically updated in UI.
The following line demonstrates the basic usage of the RenderableContentControl component:

```
<RenderableContentControl Presentation="Control"
                          Context="@Entry.Plc.prgWeatherStations"/>
```

Documentation for RenderableContentControl component can be found in **[RENDERABLECONTENT](RENDERABLECONTENT.md)** file.


