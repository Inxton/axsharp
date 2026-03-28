# Creating NuGet package from Twin library

NuGet is the preferred way to manage dependencies in an AXSharp project. For creating NuGet packages you would follow the same procedure as with ordinary NuGet packages.

## Additional configuration

To properly create and consume a package you'd need to add the PLC project's metadata to your NuGet package.

You can do it by adding the following in the respective `csproj` file of your twin project.

~~~ XML
	<ItemGroup>
		<Folder Include=".meta\" />
		<Content Include=".meta\**"/>
	</ItemGroup>
~~~

or 

Set the files in the `.meta` folder to build action to `Content`.

![](~/images/compiler/2023-02-18-09-32-22.png)


## Versioning

> **Important**
> The APAX package and respective Twin NuGet package must be released with the same version number. APAX package and NuGet package with the same version number are considered aligned.

## UI companion packages

A PLC library can ship a UI companion package alongside the twin connector — for example a set of auto-generated Blazor components. `ixc` handles the bookkeeping automatically via `axsharp.companion.json`.

### Library author

Set `UiHostProject` in your library's `AXSharp.config.json` to the path of the UI `.csproj` within your repository:

~~~json
{
    "OutputProjectFolder": "../ix",
    "UiHostProject": "../MyLib.UI/MyLib.UI.csproj"
}
~~~

When `ixc` compiles the library it:

1. Reads the `<PackageId>` from the UI project file (falls back to `<apax-name>.UI` if the file is absent).
2. Writes `UiId` and `UiVersion` into `axsharp.companion.json` next to the existing twin connector fields.

~~~json
{
  "Id": "MyLib.Twin",
  "Version": "1.2.3",
  "UiId": "MyLib.UI",
  "UiVersion": "1.2.3"
}
~~~

> The UI package version always matches the library version — publish both together.

### Consumer

Set `UiHostProject` in your application's `AXSharp.config.json` to the Blazor or UI host project that should receive the dependency:

~~~json
{
    "OutputProjectFolder": "ix",
    "UiHostProject": "../MyBlazorApp/MyBlazorApp.csproj"
}
~~~

When `ixc` resolves a dependency whose `axsharp.companion.json` contains `UiId`/`UiVersion`, it automatically:

- Adds a **NuGet package reference** to `UiHostProject` when the dependency is consumed as a NuGet package.
- Adds a **project reference** to `UiHostProject` when the dependency is consumed directly from source (i.e. the dependency has its own `AXSharp.config.json` with `UiHostProject` set).

If `UiHostProject` is not configured, the UI reference step is skipped and an informational message is logged.