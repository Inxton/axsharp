using System.Reflection;
using AXSharp.Compiler;
using Xunit;
using System;
using System.IO;
using System.Linq;

namespace AXSharp.CompilerTests;

public class ConditionalDependenciesTests
{
    private readonly string _root;
    private const string ExpectedCsProjFile = "conditional_test.csproj"; // derived from apax name 'conditional-test'

    public ConditionalDependenciesTests()
    {
        var fi = new FileInfo(Assembly.GetExecutingAssembly().Location);
        _root = Path.Combine(fi.Directory!.FullName, "samples", "conditional");
        Directory.CreateDirectory(_root);
    }

    [Fact]
    public void should_respect_itemgroup_condition_targetframework()
    {
        var projDir = Path.Combine(_root, "tfm");
        if (Directory.Exists(projDir)) Directory.Delete(projDir, true);
        Directory.CreateDirectory(projDir);

        // minimal src structure required by AxProject
        PrepareSrc(projDir);
        EnsureApax(projDir);

        var csproj = """
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFrameworks>net8.0;net9.0</TargetFrameworks>
  </PropertyGroup>
  <ItemGroup Condition="'$(TargetFramework)'=='net8.0'">
    <PackageReference Include="PkgOnlyNet8" Version="1.0.0" />
  </ItemGroup>
  <ItemGroup Condition="'$(TargetFramework)'=='net9.0'">
    <PackageReference Include="PkgOnlyNet9" Version="2.0.0" />
  </ItemGroup>
</Project>
""";
        File.WriteAllText(Path.Combine(projDir, ExpectedCsProjFile), csproj);

        var ax = new AXSharpProject(new AxProject(projDir), Array.Empty<Type>(), typeof(CsProject), new CompilerTestOptions(){ OutputProjectFolder = projDir});
        var target = (CsProject)ax.TargetProject;
        var refs = target.LoadReferences().OfType<PackageReference>().ToList();
        Assert.Contains(refs, r => r.Include == "PkgOnlyNet8");
       // Assert.Contains(refs, r => r.Include == "PkgOnlyNet9"); we only use single target framework in this test, so net9.0 package should not be included
        Assert.DoesNotContain(refs, r => r.Include == "PkgOnlyNet9");
    }

    [Fact]
    public void should_respect_element_condition_inside_itemgroup()
    {
        var projDir = Path.Combine(_root, "element");
        if (Directory.Exists(projDir)) Directory.Delete(projDir, true);
        Directory.CreateDirectory(projDir);
        PrepareSrc(projDir);
        EnsureApax(projDir);

        var csproj = """
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="AlwaysPkg" Version="1.0.0" />
    <PackageReference Include="CondPkg" Version="1.2.3" Condition="'$(TargetFramework)'=='net8.0'" />
    <PackageReference Include="SkippedPkg" Version="3.0.0" Condition="'$(TargetFramework)'=='net9.0'" />
  </ItemGroup>
</Project>
""";
        File.WriteAllText(Path.Combine(projDir, ExpectedCsProjFile), csproj);

        var ax = new AXSharpProject(new AxProject(projDir), Array.Empty<Type>(), typeof(CsProject), new CompilerTestOptions(){ OutputProjectFolder = projDir});
        var target = (CsProject)ax.TargetProject;
        var refs = target.LoadReferences().OfType<PackageReference>().ToList();
        Assert.Contains(refs, r => r.Include == "AlwaysPkg");
        Assert.Contains(refs, r => r.Include == "CondPkg");
        Assert.DoesNotContain(refs, r => r.Include == "SkippedPkg");
    }

    [Fact]
    public void should_merge_directory_build_props_properties()
    {
        var projDir = Path.Combine(_root, "dbp");
        if (Directory.Exists(projDir)) Directory.Delete(projDir, true);
        Directory.CreateDirectory(projDir);
        PrepareSrc(projDir);
        EnsureApax(projDir);

        File.WriteAllText(Path.Combine(projDir, "Directory.Build.props"), """
<Project><PropertyGroup><TargetFramework>net8.0</TargetFramework></PropertyGroup></Project>
""");

        var csproj = """
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <AssemblyName>ConditionalDbp</AssemblyName>
  </PropertyGroup>
  <ItemGroup Condition="'$(TargetFramework)'=='net8.0'">
    <PackageReference Include="DbpPkg" Version="4.5.6" />
  </ItemGroup>
</Project>
""";
        File.WriteAllText(Path.Combine(projDir, ExpectedCsProjFile), csproj);

        var ax = new AXSharpProject(new AxProject(projDir), Array.Empty<Type>(), typeof(CsProject), new CompilerTestOptions(){ OutputProjectFolder = projDir});
        var target = (CsProject)ax.TargetProject;
        var refs = target.LoadReferences().OfType<PackageReference>().ToList();
        Assert.Contains(refs, r => r.Include == "DbpPkg");
    }

    private static void EnsureApax(string dir)
    {
        var apax = Path.Combine(dir, "apax.yml");
        if (!File.Exists(apax))
        {
            File.WriteAllText(apax, "name: conditional-test\nversion: 0.0.1\n");
        }
    }

    private static void PrepareSrc(string dir)
    {
        var src = Path.Combine(dir, "src");
        Directory.CreateDirectory(src);
        // minimal dummy ST file
        File.WriteAllText(Path.Combine(src, "_dummy.st"), "PROGRAM DUMMY\nEND_PROGRAM");
    }
}
