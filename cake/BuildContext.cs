// Build
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Build.FilteredSolution;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Common.Tools.DotNet.Restore;
using Cake.Common.Tools.DotNet.Run;
using Cake.Common.Tools.DotNet.Test;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;
using Polly;
using static NuGet.Packaging.PackagingConstants;
using Path = System.IO.Path;

public class BuildContext : FrostingContext
{
    public string Artifacts  => Path.Combine(Environment.WorkingDirectory.FullPath, "..//artifacts//");

    public string ArtifactsApax => Path.Combine(Environment.WorkingDirectory.FullPath, "..//artifacts//apax//");
    
    public string TestResults => Path.Combine(Environment.WorkingDirectory.FullPath, "..//TestResults//");

    public string RootDir => Path.GetFullPath(Path.Combine(Environment.WorkingDirectory.FullPath, "..//src//"));
    public string ApaxRegistry => "inxton";
    
    public string WorkDirName => Environment.WorkingDirectory.GetDirectoryName();

    public string DocumentationOutputDir => Path.GetFullPath(Path.Combine(Environment.WorkingDirectory.FullPath, "..//docs//"));

    public string DocumentationSource => Path.GetFullPath(Path.Combine(Environment.WorkingDirectory.FullPath, "..//docfx//"));

    public string ScrDir => Path.GetFullPath(Path.Combine(Environment.WorkingDirectory.FullPath, "..//src//"));

    public string TemplatesDir => Path.GetFullPath(Path.Combine(Environment.WorkingDirectory.FullPath, "..//templates//"));

    public Cake.Common.Tools.DotNet.Build.DotNetBuildSettings DotNetBuildSettings { get; }

    public DotNetRestoreSettings DotNetRestoreTemplatesSettings { get; }
    
    public Cake.Common.Tools.DotNet.Test.DotNetTestSettings DotNetTestSettings { get; }

    public DotNetRunSettings DotNetRunSettings { get; }

    public BuildParameters BuildParameters { get; }

    public BuildContext(ICakeContext context, BuildParameters buildParameters)
        : base(context)
    {

        BuildParameters = buildParameters;

        DotNetBuildSettings = new DotNetBuildSettings()
        {
            Verbosity = buildParameters.Verbosity,
            Configuration = buildParameters.Configuration,
            NoRestore = false,
            MSBuildSettings = new DotNetMSBuildSettings()
            {
                Verbosity = buildParameters.Verbosity
            }
        };
        
        DotNetRestoreTemplatesSettings = new DotNetRestoreSettings() { NoCache = true, IgnoreFailedSources = true };
        
        DotNetTestSettings = new DotNetTestSettings()
        {
            Verbosity = buildParameters.Verbosity,
            Configuration = buildParameters.Configuration,
            NoRestore = true,
            NoBuild = true,
            DiagnosticOutput = true,
            VSTestReportPath = TestResults,
        };

        DotNetRunSettings = new DotNetRunSettings()
        {
            Verbosity = buildParameters.Verbosity,
            Framework = "net9.0",
            Configuration = buildParameters.Configuration,
            NoBuild = true,
            NoRestore = true,
        };
    }

    public void UploadTestPlc(string workingDirectory, string targetIp,
        string targetPlatform)
    {
        var lockFile = Path.Combine(workingDirectory, "apax-lock.json");
        if (File.Exists(lockFile)) this.DeleteFile(lockFile);

        this.Log.Information($"Installing dependencies for ax project '{workingDirectory}' at {targetIp}");

        this.ProcessRunner.Start(Helpers.GetApaxCommand(), new ProcessSettings()
        {
            Arguments = " install",
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            Silent = false
        }).WaitForExit();


        this.Log.Information($"Building ax project '{workingDirectory}' at {targetIp}");


        this.ProcessRunner.Start(Helpers.GetApaxCommand(), new ProcessSettings()
        {
            Arguments = " build",
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            RedirectedStandardOutputHandler = (a) => string.Join(System.Environment.NewLine, a),
            Silent = false
        }).WaitForExit();


        this.Log.Information($"Uploading ax project '{workingDirectory}' at {targetIp}");

        this.ProcessRunner.Start(Helpers.GetApaxCommand(), new ProcessSettings()
        {
            Arguments = " download",
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            RedirectedStandardOutputHandler = (a) => string.Join(System.Environment.NewLine, a),
            Silent = false
        }).WaitForExit();
        
        // this.ProcessRunner.Start(Helpers.GetApaxCommand(), new ProcessSettings()
        // {
        //     Arguments =
        //         $" sld -t {targetIp} -i {targetPlatform} --accept-security-disclaimer --default-server-interface -r",
        //     WorkingDirectory = workingDirectory,
        //     RedirectStandardOutput = false,
        //     RedirectStandardError = false
        // }).WaitForExit();
    }

    public void RunTestsFromFilteredSolution(string filteredSolutionFile)
    {
        foreach (var project in FilteredSolution.Parse(filteredSolutionFile).solution.projects
                     .Select(p => new FileInfo(Path.Combine(this.ScrDir, p)))
                     .Where(p => p.Name.ToUpperInvariant().Contains("TEST")))
        {
            foreach (var framework in this.TargetFrameworks)
            {
                this.DotNetTestSettings.VSTestReportPath = Path.Combine(this.TestResults, $"{project.Name}_{framework}.xml");
                this.DotNetTestSettings.Framework = framework;
                this.DotNetTest(Path.Combine(project.FullName), this.DotNetTestSettings);
            }
        }
    }

    public void PushNugetPackages(string artifactDirectory)
    {
        if (Helpers.CanReleaseInternal())
        {
            foreach (var nugetFile in Directory.EnumerateFiles(Path.Combine(this.Artifacts, artifactDirectory), "*.nupkg")
                         .Select(p => new FileInfo(p)))
            {
                this.DotNetNuGetPush(nugetFile.FullName,
                    new Cake.Common.Tools.DotNet.NuGet.Push.DotNetNuGetPushSettings()
                    {
                        ApiKey = Environment.GetEnvironmentVariable("GH_TOKEN"),
                        Source = "https://nuget.pkg.github.com/inxton/index.json",
                        SkipDuplicate = true
                    });
            }
        }
    }

    public IEnumerable<string> TargetFrameworks { get; } = new List<string>() { "net9.0", "net8.0" };
    public string ApaxSignKey { get; set; } = System.Environment.GetEnvironmentVariable("APAX_SIGN_KEY");
    public string GitHubUser { get; set; } = System.Environment.GetEnvironmentVariable("InxtonDev");
    public string GitHubToken { get; set; } = System.Environment.GetEnvironmentVariable("GH_TOKEN");
    


    public IEnumerable<(string ax, string approject, string solution)> GetTemplateProjects()
    {
        var templates = new List<(string ax, string approject, string solution)>()
        {
            (Path.Combine(this.TemplatesDir, "working\\templates\\axsharpblazor\\ax\\"),
                Path.Combine(this.TemplatesDir,"working\\templates\\axsharpblazor\\axsharpblazor.app\\axsharpblazor.hmi.csproj"),
                    Path.Combine(this.TemplatesDir,"working\\templates\\axsharpblazor\\axsharpblazor.sln")),

            (Path.Combine(this.TemplatesDir, "working\\templates\\axsharpconsole\\ax"),
                Path.Combine(this.TemplatesDir,"working\\templates\\axsharpconsole\\axsharpconsole\\axsharpconsole.app.csproj"),
                    Path.Combine(this.TemplatesDir,"working\\templates\\axsharpconsole\\axsharpconsole.sln"))
           
        };

        return templates;
    }


    private static void DeleteDirectory(string target_dir)
    {
        if (!Directory.Exists(target_dir))
            return;
        
        string[] files = Directory.GetFiles(target_dir);
        string[] dirs = Directory.GetDirectories(target_dir);

        foreach (string file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }

        foreach (string dir in dirs)
        {
            DeleteDirectory(dir);
        }

        Directory.Delete(target_dir, false);
    }

    public void CleaUpAllBinsAndObjs()
    {
        foreach (var directory in Directory.EnumerateDirectories(this.ScrDir, "*.*", SearchOption.AllDirectories).Select(p => new DirectoryInfo(p))
                     .Where(p => (p.Name == "bin" || p.Name == "obj") && !string.IsNullOrEmpty(p.LinkTarget)))
        {
            DeleteDirectory(directory.FullName);
        }
    }

    public void CheckLicenseComplianceInArtifacts()
    {        
        //var licensedFiles = Directory.EnumerateFiles(Path.Combine(context.RootDir, "apax", ".apax", "packages"),
        var licensedFiles = Directory.EnumerateFiles(Path.Combine(this.ScrDir, "apax", "stc"),
                "AX.*.*",
                SearchOption.AllDirectories)
            .Select(p => new FileInfo(p));

        if (licensedFiles.Count() < 5)
            throw new Exception("");


        foreach (var nugetFile in Directory.EnumerateFiles(this.Artifacts, "*.nupkg", SearchOption.AllDirectories))
        {
            using (var zip = ZipFile.OpenRead(nugetFile))
            {
                var ouptutDir = Path.Combine(this.Artifacts, "verif");
                zip.ExtractToDirectory(Path.Combine(this.Artifacts, "verif"));

                if (Directory.EnumerateFiles(ouptutDir, "*.*", SearchOption.AllDirectories)
                    .Select(p => new FileInfo(p))
                    .Any(p => licensedFiles.Any(l => l.Name == p.Name)))
                {
                    throw new Exception("");
                }

                Directory.Delete(ouptutDir, true);
            }
        }
    }
}