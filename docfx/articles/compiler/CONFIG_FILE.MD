# Ixc config file

**AXSharp.config.json** can contain settings for the compiler. Upon the first run of `ixc` a default `AXSharp.config.json` file is created if one does not already exist.

Some options can be overridden from the CLI. For example, the `-o` flag overrides `OutputProjectFolder`.

**Default config file**

~~~json
{
    "OutputProjectFolder": "ix"
}
~~~

**Config file example (library with UI companion)**

~~~json
{
    "OutputProjectFolder": "../ix",
    "UiHostProject": "../blazorapp/blazorapp.csproj"
}
~~~

## Parameters

| Parameter | CLI flag | Meaning | Default |
| --- | --- | --- | --- |
| `OutputProjectFolder` | `-o` | Directory where `ixc` emits the twin project. Path relative to the AX project folder. | `ix` |
| `ProjectFile` | `-p` | Name of the output `.csproj` file. | *(project name)* |
| `UseBase` | `-b` | Use `$base` for base types in inherited classes. Obsolete. | `false` |
| `NoDependencyUpdate` | `-u` | Prevent dependent twin packages from being installed via apax. | `false` |
| `IgnoreS7Pragmas` | `-s` | Ignore `S7.extern` pragmas; compile all members regardless of communication settings. | `false` |
| `SkipDependencyCompilation` | `-d` | Skip compilation of referenced AX# dependency projects. | `false` |
| `TargetPlatfromMoniker` | `-t` | Target platform: `ax` or `tia`. | `ax` |
| `UiHostProject` | `-a` | Path (relative to the AX project folder or absolute) of the `.csproj` that hosts or consumes UI companion NuGet packages. In library development this is the Blazor/UI application project; in application development this is the application project itself. When set, `ixc` automatically installs UI companion package references into this project. | *(none)* |

## UI companion packages

When a PLC library ships a UI companion package (Blazor components, auto-generated views, etc.), `ixc` can automatically wire up the dependency for you.

**Library author** — set `UiHostProject` in the library's `AXSharp.config.json` to the path of the UI project that is built alongside the twin project. When `ixc` runs it will:

1. Determine the UI package ID from the `<PackageId>` element in the referenced `.csproj`, or fall back to `<apax-package-name>.UI` when the file does not exist.
2. Write that ID and the library version into `axsharp.companion.json` alongside the usual twin connector fields.

**Consumer** — set `UiHostProject` in your project's `AXSharp.config.json` to point at the application project (e.g. a Blazor server app) that should receive the dependency. When `ixc` resolves a dependency that carries UI companion info, it automatically adds the corresponding NuGet package reference (or project reference when working directly from source) to the configured host project.

~~~json
{
    "OutputProjectFolder": "../ix",
    "UiHostProject": "../MyBlazorApp/MyBlazorApp.csproj"
}
~~~
