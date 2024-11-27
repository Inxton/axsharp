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