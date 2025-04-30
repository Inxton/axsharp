
// Build
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Build.FilteredSolution;
using Cake.Common;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Clean;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.DocFx;
using Cake.DocFx.Build;
using Cake.Frosting;
using Cake.Powershell;
using CliWrap;
using CommandLine;
using AXSharp.nuget.update;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Packaging;
using Octokit;
using Polly;
using Credentials = Octokit.Credentials;
using Path = System.IO.Path;
using ProductHeaderValue = Octokit.ProductHeaderValue;
using static NuGet.Packaging.PackagingConstants;


public static class Program
{
    public static int Main(string[] args)
    {
        var retVal = 0;
        Parser.Default.ParseArguments<BuildParameters>(args)
            .WithParsed<BuildParameters>(o =>
            {
                retVal = new CakeHost()
                    .ConfigureServices(services => services.AddSingleton(o))
                    .UseContext<BuildContext>()
                    .Run(args);
            });

        return retVal;
    }
}

[TaskName("CleanUp")]
public sealed class CleanUpTask : FrostingTask<BuildContext>
{    
    public override void Run(BuildContext context)
    {
        context.DotNetClean(Path.Combine(context.ScrDir, "AXSharp.sln"), new DotNetCleanSettings() { Verbosity = context.BuildParameters.Verbosity });
        context.CleaUpAllBinsAndObjs();
        context.CleanDirectory(context.Artifacts);
        context.CleanDirectory(context.TestResults);      
    }
}

[TaskName("Provision")]
[IsDependentOn(typeof(CleanUpTask))]
public sealed class ProvisionTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DotNetBuildSettings.MSBuildSettings.Properties.Add("NoWarn", new List<string>() 
            { "1234;2345;8602;10012;8618;0162;8605;1416;3270;1504;8600;8618;" +
                "CS0618;CS1591;BL0007;BL0005;CA1416;CA2200;CS0105;CS0108;CS0109;CS0162;CS0168;CS0169;CS219;CS0414;CS0436;CS0472;CS0618;CS1591;CS1998;CS8604;" +
                "CS8601;SYSLIB0051;SYSLIB0014;CS8625;CS0219;CS8625;CS8625;CS8620;RZ2012;RZ10012;CS4014;CS8981;CS8603;CS8766;CS8619;CS0649;CS8321"
            });
            
        ProvisionProjectWideTools(context);
    }

    private static void ProvisionProjectWideTools(BuildContext context)
    {
        context.ProcessRunner.Start(@"dotnet", new Cake.Core.IO.ProcessSettings()
        {
            Arguments = $" tool restore --no-cache --ignore-failed-sources ",

        });

        context.ProcessRunner.Start(Helpers.GetApaxCommand(), new Cake.Core.IO.ProcessSettings()
        {
            Arguments = $" install",
            WorkingDirectory = Path.Combine(context.ScrDir, "apax"),
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            Silent = false,

        }).WaitForExit();


        // Copy apax packages to different directory
        var destinationDir = Path.Combine(context.ScrDir, "apax//stc");
        var sourceDir = Path.Combine(context.ScrDir, $"apax//.apax//packages//@ax//{Helpers.GetStcNameByPlatform()}");
        Helpers.CopyApaxPackages(sourceDir, destinationDir, context.ScrDir);

    }
}

[TaskName("Build")]
[IsDependentOn(typeof(ProvisionTask))]
public sealed class BuildTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DotNetBuild(Path.Combine(context.ScrDir, "AXSharp.compiler\\src\\ixc\\AXSharp.ixc.csproj"), context.DotNetBuildSettings);

        var axprojects = new List<string>()
        {
            Path.Combine(context.ScrDir, "AXSharp.blazor\\tests\\sandbox\\ax-blazor-example\\"),
            Path.Combine(context.ScrDir, "sanbox\\integration\\ix-integration-plc\\"),
            Path.Combine(context.ScrDir, "AXSharp.examples\\hello.world.console\\hello.world.console.plc"),
            Path.Combine(context.ScrDir, "AXSharp.connectors\\tests\\ax-test-project\\"),
            Path.Combine(context.ScrDir, "tests.integrations\\integrated\\src\\ax\\"),
            Path.Combine(context.TemplatesDir, "working\\templates\\axsharpblazor\\ax\\"),
            Path.Combine(context.TemplatesDir, "working\\templates\\axsharpconsole\\ax")
           
        };


        foreach (var axproject in axprojects)
        {
            context.DotNetRunSettings.WorkingDirectory = Path.Combine(context.ScrDir, axproject);
          
            context.DotNetRun(Path.Combine(context.ScrDir, "AXSharp.compiler\\src\\ixc\\AXSharp.ixc.csproj"), context.DotNetRunSettings);
        }

        context.DotNetBuild(Path.Combine(context.ScrDir, "AXSharp.sln"), context.DotNetBuildSettings);

    }
}

[TaskName("Test")]
[IsDependentOn(typeof(BuildTask))]
public sealed class TestsTask : FrostingTask<BuildContext>
{
    // Tasks can be asynchronous
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoTest)
        {
            context.Log.Warning($"Skipping tests");
            return;
        }


        if (context.BuildParameters.TestLevel >= 1)
        {
            context.RunTestsFromFilteredSolution(Path.Combine(context.ScrDir, "AXSharp-L1-tests.slnf"));
        }
        if (context.BuildParameters.TestLevel >= 2)
        {
            context.RunTestsFromFilteredSolution(Path.Combine(context.ScrDir, "AXSharp-L2-tests.slnf"));
        }
        if (context.BuildParameters.TestLevel >= 3)
        {
            // This must be run in a separate environment!
            try
            {
                context.UploadTestPlc(
                    Path.GetFullPath(Path.Combine(context.WorkDirName, "..//..//src//AXSharp.connectors//tests//ax-test-project//")),
                    Environment.GetEnvironmentVariable("AXTARGET"),
                    Environment.GetEnvironmentVariable("AXTARGETPLATFORMINPUT"));
                
                context.RunTestsFromFilteredSolution(Path.Combine(context.ScrDir, "AXSharp-L3-tests_WebApi.slnf"));
            }
            catch
            {
                    System.Console.WriteLine("Some WebAPI tests failed. RUN IN APPROPRIATE EVNIRONMENT");
            }

            try
            {
                context.UploadTestPlc(
                    Path.GetFullPath(Path.Combine(context.WorkDirName, "..//..//src//tests.integrations//integrated//src//ax")),
                    Environment.GetEnvironmentVariable("AXTARGET"),
                    Environment.GetEnvironmentVariable("AXTARGETPLATFORMINPUT"));
                
                context.RunTestsFromFilteredSolution(Path.Combine(context.ScrDir, "AXSharp-L3-tests_Integration.slnf"));
            }
            catch
            {
                System.Console.WriteLine("Some WebAPI tests failed. RUN IN APPROPRIATE EVNIRONMENT");
            }
        }



    }




}

[TaskName("CreateArtifacts")]
[IsDependentOn(typeof(TestsTask))]
public sealed class CreateArtifactsTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoPack)
        {
            context.Log.Warning($"Skipping packaging.");
            return;
        }

        PackPackages(context, Path.Combine(context.ScrDir, "AXSharp-packable-only.slnf"));
    }



    private static void PackPackages(BuildContext context, string solutionToPack)
    {
        context.DotNetPack(solutionToPack,
            new Cake.Common.Tools.DotNet.Pack.DotNetPackSettings()
            {
                OutputDirectory = Path.Combine(context.Artifacts, @"nugets"),
                NoRestore = true,
                NoBuild = false,
            });
    }
}

[TaskName("GenerateApiDocumentation")]
[IsDependentOn(typeof(CreateArtifactsTask))]
public sealed class GenerateApiDocumentationTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoDocs)
        {
            context.Log.Warning($"Skipping documentation generation.");
            return;
        }

        context.ProcessRunner.Start(@"dotnet", new Cake.Core.IO.ProcessSettings()
        {
            WorkingDirectory = context.DocumentationSource,
            Arguments = $"docfx build"
        }).WaitForExit();
    }
}


[TaskName("Check license compliance")]
[IsDependentOn(typeof(GenerateApiDocumentationTask))]
public sealed class LicenseComplianceCheckTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {       
        context.CheckLicenseComplianceInArtifacts();
    }
}


[TaskName("PushPackages task")]
[IsDependentOn(typeof(LicenseComplianceCheckTask))]
public sealed class PushPackages : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoPublish)
        {
            context.Log.Warning($"Skipping package push.");
            return;
        }

        context.PushNugetPackages("nugets");
    }
}

[TaskName("Publish release")]
[IsDependentOn(typeof(PushPackages))]
public sealed class PublishReleaseTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoPublishRelease)
        {
            context.Log.Warning($"Skipping package release.");
            return;
        }

        if (Helpers.CanReleaseInternal())
        {
            context.Log.Warning($"Creating release publication.");

            var githubToken = context.Environment.GetEnvironmentVariable("GH_TOKEN");
            var githubClient = new GitHubClient(new ProductHeaderValue("AXSHARP"));
            githubClient.Credentials = new Credentials(githubToken);

            var release = githubClient.Repository.Release.Create(
                "inxton",
                "axsharp",
                new NewRelease($"{GitVersionInformation.SemVer}")
                {
                    Name = $"{GitVersionInformation.SemVer}",
                    TargetCommitish = GitVersionInformation.Sha,
                    Body = $"Release v{GitVersionInformation.SemVer}",
                    Draft = !Helpers.CanReleasePublic(),
                    Prerelease = !string.IsNullOrEmpty(GitVersionInformation.PreReleaseTag)
                }
            ).Result;
        }
    }
}


[TaskName("Templates build")]
[IsDependentOn(typeof(PublishReleaseTask))]
public sealed class TemplatesUpdateAndBuildTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoPublish || !Helpers.CanReleaseInternal() || !Helpers.CanReleasePublic())
        {
            context.Log.Warning($"Skipping template package build.");
            return;
        }

        var templatesDirectory = Path.Combine(context.TemplatesDir);
        var templateCsProjFiles = Directory.EnumerateFiles(templatesDirectory, "*.csproj", SearchOption.AllDirectories);

        foreach (var templateCsProjFile in templateCsProjFiles)
        {
            var packagesToUpdate = new List<string>()
            {
                "AXSharp.Abstractions",
                "AXSharp.Connector",
                "AXSharp.Connector.S71500.WebAPI",
                "AXSharp.Presentation.Blazor.Controls",
                "AXSharp.Presentation.Blazor"
            };

            foreach (var packageId in packagesToUpdate)
            {
                AXSharp.nuget.update.Program.Update(new Options() { NewVersion = GitVersionInformation.SemVer, PackageId = packageId, FileToUpdate = templateCsProjFile });
            }
        }
        var templateToolDotnetTools = Directory.EnumerateFiles(templatesDirectory, "dotnet-tools.json", SearchOption.AllDirectories);
        foreach (var templateCsProjFile in templateToolDotnetTools)
        {
            var packagesToUpdate = new List<string>() { "AXSharp.ixc" };
            foreach (var packageId in packagesToUpdate)
            {
                AXSharp.nuget.update.Program.Update(new Options() { NewVersion = GitVersionInformation.SemVer, PackageId = packageId, FileToUpdate = templateCsProjFile });
            }
        }

        foreach (var template in context.GetTemplateProjects())
        {
            context.ProcessRunner.Start(@"dotnet", new Cake.Core.IO.ProcessSettings()
            {
                Arguments = $"tool restore --no-cache",
                WorkingDirectory = template.ax

            }).WaitForExit();
        }

        foreach (var template in context.GetTemplateProjects())
        {
            context.ProcessRunner.Start(@"dotnet", new Cake.Core.IO.ProcessSettings()
            {
                Arguments = $" ixc ",
                WorkingDirectory = template.ax

            }).WaitForExit();
        }


        foreach (var template in context.GetTemplateProjects())
        {
            context.DotNetRestore(Path.Combine(context.TemplatesDir, template.solution), context.DotNetRestoreTemplatesSettings);
            context.DotNetBuild(Path.Combine(context.TemplatesDir, template.solution), context.DotNetBuildSettings);
        }
    }
}

[TaskName("Template tests")]
[IsDependentOn(typeof(TemplatesUpdateAndBuildTask))]
public class TemplateTests : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoPublish || !context.BuildParameters.DoTest)
        {
            context.Log.Warning($"Skipping template package push.");
            return;
        }

        if (context.BuildParameters.TestLevel >= 3)
        {
            foreach (var template in context.GetTemplateProjects())
            {
                context.UploadTestPlc(
                    Path.GetFullPath(Path.Combine(template.ax)),
                    Environment.GetEnvironmentVariable("AXTARGET"),
                    Environment.GetEnvironmentVariable("AXTARGETPLATFORMINPUT"));

                // context.DotNetRun(template.approject, context.DotNetRunSettings);
            }
        }
    }
}

[TaskName("Templates pack")]
[IsDependentOn(typeof(TemplateTests))]
public class TemplatesPackTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoPack)
        {
            context.Log.Warning($"Skipping template build.");
            return;
        }

       
       
        PackTemplatePackages(context,
            Path.Combine(context.TemplatesDir,"working", "AXSharp.templates.sln"));
        
        
        
        context.PushNugetPackages("templates");

        context.CheckLicenseComplianceInArtifacts();
    }

    private static void PackTemplatePackages(BuildContext context, string solutionToPack)
    {
        context.DotNetPack(solutionToPack,
            new Cake.Common.Tools.DotNet.Pack.DotNetPackSettings()
            {
                OutputDirectory = Path.Combine(context.Artifacts, @"templates"),
                Sources = new List<string>() { Path.Combine(context.Artifacts, "templates") },
                NoRestore = false,
                NoBuild = false,
            });
    }

}

[TaskName("Templates push")]
[IsDependentOn(typeof(TemplatesPackTask))]
public class TemplatesPush : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.BuildParameters.DoPublish)
        {
            context.Log.Warning($"Skipping template build.");
            return;
        }

        context.PushNugetPackages("templates");
    }
}

[TaskName("Default")]
[IsDependentOn(typeof(TemplatesPush))]
public class DefaultTask : FrostingTask
{
}
