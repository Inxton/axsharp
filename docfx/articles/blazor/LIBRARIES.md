# External libraries

External libraries can store user defined views in different presentation types, which then can be dynamically rendered. 

This file describes how to create external library and connect it to the PLC project. Look at **integration-blazor** project example.

Make sure Blazor server project and PLC projects are created.

## Razor class library setup

### 1. Create library

Create Razor class library in your solution.


### 2. Add references 


- Install `AXSharp.Presentation.Blazor.Controls` nuget package or if you are using raw projects, add reference to `AXSharp.Presentation.Blazor.Controls` project.

- Add reference from Razor Class library to PLC project.

- In Blazor Server app, add reference to newly created Razor class library.

Dependency graph should look like this:

![alt text](~/images/blazor/dependency-graph.png "Dependency graph")


### 3. Add namespace

Add namespace of renderer to `_Imports.razor` in razor class library.
```
@using AXSharp.Presentation.Blazor.Controls.RenderableContent
```
### 4. Add renderable attribute

Add `RenderableBlazorAssemblyAttribute` to Razor class library.

1. Create folder named `Properties`
2. Inside folder create `AssemblyInfo.cs` class
3. Copy following code into `AssemblyInfo.cs` class:

```
using System.Reflection;
using AXSharp.Presentation.Blazor.Attributes;

[assembly: RenderableBlazorAssemblyAttribute()]

```

Thanks to this attribute, renderer will load assembly of created class library and it is able to look for custom defined views.

---
## Create custom view

### 1. Create PLC structure and its instance

```
CLASS ixcomponent
    VAR PUBLIC
        {#ix-set:AttributeName = "My integer"}
        my_int : INT;
        {#ix-set:AttributeName = "My string"}
        my_string : STRING;
        {#ix-set:AttributeName = "My bool"}	
        my_bool : BOOL;
    END_VAR
END_CLASS
```

```
ixcomponent_instance: ixcomponent;
```

Build plc project with `apax build` and compile it with `apax ixc` command.

### 2. Create IxComponentView in Razor class library

- Create folder with name `IxComponentView`.
- Create `IxComponentView.razor` class inside folder.
- Define your view.

```C#
@namespace AXSharp.Presentation.Blazor.Controls.Templates
@inherits RenderableComplexComponentBase<ixcomponent>

<h1>IxComponentView</h1>

<div class="card">
    <p>IxBool: @Component.my_bool.Cyclic</p>
    <p>IxInt: @Component.my_int.Cyclic</p>
    <p>IxString: @Component.my_string.Cyclic</p>
</div>


@code{
    protected override void OnInitialized()
    {
        UpdateValuesOnChange(Component);
    }
}

```

Note: If your plc variable is declared in global namespace, `AXSharp.Presentation.Blazor.Controls.Templates` namespace must be used to correctly locate view. 

If you plc variable is declared in your own namespace, make sure **namespace of custom view is the same as the namespace in plc file**.

At the end, structure of external library should look like this:

![alt text](~/images/blazor/project-structure.png "Structure")

### Render custom component in your application

```
<RenderableContentControl Context="@Entry.Plc.test_example.ixcomponent_instance"/>
```

If everything was done correctly, custom view defined in external library should be rendered.

![alt text](~/images/blazor/rendered-ui.png "Rendered UI")

---



## ViewModel approach

Renderer also supports injecting view-model classes into views. This enables to create custom components with MVVM pattern, where component logic can be placed into viewmodel class. Therefore, the code will be less coupled and more testable.


### 1. Create IxComponentServiceViewModel folder
Create `IxComponentServiceView.razor` file and `IxComponentViewModel.cs` file inside folder.


### 2. Define your viewmodel
Make sure, that viewmodel inherits from `RenderableViewModelBase`.
Copy following code into `IxComponentViewModel.cs` viewmodel class.
```C#
using AXSharp.Presentation;

namespace ix_integration_library.IxComponentServiceViewModel
{
    public class IxComponentServiceViewModel : RenderableViewModelBase
    {
        public IxComponentServiceViewModel()
        {
        }
        public ixcomponent Component { get; set; }
        public override object Model { get => this.Component; set { this.Component = value as ixcomponent; } }
    }
}
```
Note: Replace namespace with namespace of your plc library.

Copy following code into `IxComponentServiceView.razor` file. Make sure, that `RenderableViewModelComponentBase` class is inherited with generic type parameter of your viewmodel.

```C#
@namespace Ix.Presentation.Blazor.Controls.Templates
@using ix_integration_library.IxComponentServiceViewModel
@inherits RenderableViewModelComponentBase<IxComponentViewModel>

<h1>IxComponentView with ViewModel</h1>

<div class="card">
    <p>IxBool: @ViewModel.Component.my_bool.Cyclic</p>
    <p>IxInt: @ViewModel.Component.my_int.Cyclic</p>
    <p>IxString: @ViewModel.Component.my_string.Cyclic</p>
</div>


@code {
    protected override void OnInitialized()
    {
        UpdateValuesOnChange(ViewModel.Component);
    }
}
```
Note: viewmodel properties and variables can be accessed with inherited `ViewModel` variable.

## Optimizing PLC Data Polling

The `RenderableComponentBase` class contains an overridable method, `AddToPolling`, which is primarily tasked with adding elements to the polling queue. However, given the automatic subscription of all inner elements within a given object to the polling queue by default, it becomes imperative to manage this operation more precisely, especially when dealing with large objects.

Overriding this method in derived classes allows for the customization of how many and which elements are added to the polling queue. This capability is crucial for controlling resources and optimizing performance by preventing the automatic addition of potentially large numbers of elements to the polling queue.

Here is an example where the overridden method ensures that no elements are added to the polling queue:

```C#
public override void AddToPolling(ITwinElement element, int pollingInterval = 250)
{
    // Overriding with an empty method ensures no elements are added to the polling queue.
}
```

Contrastingly, in the following example, the overridden method only subscribes first-level primitive elements for polling, thus creating a more resource-efficient application:

```C#
public override void AddToPolling(ITwinElement element, int pollingInterval = 250)
{
    var sequencer = (AxoSequencer)element;
    var firstLevelPrimitives = sequencer.GetValueTags().ToList();

    firstLevelPrimitives.ForEach(p =>
    {
        p.StartPolling(pollingInterval, this);
        PolledElements.Add(p);
    });
}
```
This strategy allows for a more efficient use of resources by reducing the load of polled elements on the system.

For more details about polling [General Polling](../connectors/README.md#polling).

### 3. Render created component

```C#
<RenderableContentControl 
    Presentation="Service" 
    Context="@Entry.Plc.test_example.ixcomponent_instance" />
```

![alt text](~/images/blazor/viewmodel-service.png "UI with viewmodel")

