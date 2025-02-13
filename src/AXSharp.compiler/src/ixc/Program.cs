// AXSharp.ixc
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using CliWrap;
using CommandLine;
using AXSharp.Compiler;
using AXSharp.Compiler.Cs.Onliner;
using AXSharp.Compiler.Cs.Plain;
using System.Text.Json;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;


namespace ixc;

public static class Program
{
    private const string Logo =
 @"     ___      ___   ___    _  _   
    /   \     \  \ /  /  _| || |_ 
   /  ^  \     \  V  /  |_  __  _|
  /  /_\  \     >   <    _| || |_ 
 /  _____  \   /  .  \  |_  __  _|
/__/     \__\ /__/ \__\   |_||_| 
";

    private static AXSharpProject? Project;

    public static void Main(string[] args)
    {
        LegalAcrobatics.LegalComplianceAcrobatics(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName).Wait();

        DisplayInfo();

        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                var recoverCurrentDirectory = Environment.CurrentDirectory;
                try
                {                    
                    Log.ConfigureLogger(o.Versbosity);

                    Log.Logger.Verbose(JsonSerializer.Serialize(o));
                    
                    Project = GenerateIxProject(o);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    Environment.CurrentDirectory = recoverCurrentDirectory;
                }
            });
    }

    private static string GetFullPath(string path)
    {
        if (Path.IsPathRooted(path))
        {
            Console.WriteLine("Path si rooted.");
            return path;
        }
        else
        {
            var fullPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, path));
            Console.WriteLine($"Path si relative '{fullPath}'.");
            return fullPath;
        }
    }

    private static AXSharpProject GenerateIxProject(Options options)
    {        
        var axProjectFolder = string.IsNullOrEmpty(options.AxSourceProjectFolder)
            ? Environment.CurrentDirectory
            : options.AxSourceProjectFolder;

        Environment.CurrentDirectory = GetFullPath(axProjectFolder);

        var ax = new AxProject(Environment.CurrentDirectory);
        var project = new AXSharpProject(ax, new[] { typeof(CsOnlinerSourceBuilder), typeof(CsPlainSourceBuilder) },
            typeof(CsProject), options);

        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        project.Generate();
        GetPlcResources(project.OutputFolder);

        sw.Stop();
        Log.Logger.Information($"Done in {TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds)}");
        return project;
    }

    private static void DisplayInfo()
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(Logo);
        Console.ForegroundColor = originalColor;

        Console.WriteLine($"AX# compiler CLI. Version: '{GitVersionInformation.SemVer}'");
        originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("THIS PROJECT IS POSSIBLE BECAUSE OF SOME AWESOME OPEN SOURCE PROJECTS\n" +
                          "THIRD PARTY LICENSES CAN BE FOUND AT \n" +
                          "https://github.com/inxton/axsharp/blob/master/notices.md");

        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"Using version '{LegalAcrobatics.StcVersion}' of stc.");
        Console.ForegroundColor = originalColor;

        if (int.Parse(GitVersionInformation.Major) < 1 || string.IsNullOrEmpty(GitVersionInformation.PreReleaseLabel))
        {
            originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("THIS IS PRE-RELEASE VERSION DO NOT USE IN PRODUCTION ENVIRONMENT!!!");
            Console.ForegroundColor = originalColor;
        }
    }

    private static void GetPlcResources(string folder)
    {
        List<PlcResource> plcResources = new List<PlcResource>() 
        {
            //Additional "power demanding" base classes should be placed here
            new PlcResource("UnitContainerBase"),
            new PlcResource("AXOpen.Core.AxoComponent"),
            new PlcResource("AXOpen.Core.AxoSequencerContainer"),
            new PlcResource("AXOpen.Core.AxoTask"),
            new PlcResource("AXOpen.Data.AxoDataExchange"),
            new PlcResource("AXOpen.Data.AxoDataFragmentExchange"),
        };

        string folderPOCO = Path.GetFullPath(Path.Combine(folder, ".g\\POCO\\"));
        string plcResourcesPath = Path.GetFullPath(Path.Combine(folder, "PlcResources.json"));

        var files = Directory.GetFiles(folderPOCO, "*.cs", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            string code = File.ReadAllText(file);

            var syntaxTree = CSharpSyntaxTree.ParseText(code);

            var root = syntaxTree.GetRoot();

            foreach (PlcResource plcResource in plcResources)
            {
                var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>().Where(cls => ExtendsClass(cls, plcResource.BaseClassName));

                foreach (var classDecl in classDeclarations)
                {
                    plcResource.Instances.Add(file.ToString().Replace(folderPOCO,"").Replace(".g.cs", "").Replace("\\", "."));
                    plcResource.InstanceCount++;
                }
            }
        }

        if (File.Exists(plcResourcesPath))
        {
            try
            {
                File.Delete(plcResourcesPath);
            }
            catch (Exception ex){}
        }

        string json = Newtonsoft.Json.JsonConvert.SerializeObject(plcResources, Newtonsoft.Json.Formatting.Indented);

        File.WriteAllText(plcResourcesPath, json);

        Console.WriteLine($"Data written to {plcResourcesPath}");
    }


    private static bool ExtendsClass(ClassDeclarationSyntax classDecl, string baseClassName)
    {
        return classDecl.BaseList?.Types.Any(baseType => baseType.ToString().Contains(baseClassName)) ?? false;
    }

    
    private class PlcResource
    {
        public string BaseClassName { get; set; }
        public int InstanceCount { get; set; }
        public List<string> Instances { get; set; }

        public PlcResource(string baseClassName)
        {
            BaseClassName = baseClassName;
            InstanceCount = 0;
            Instances = new List<string>();
        }
    }
}