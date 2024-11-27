// AXSharp.nuget.update
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Runtime.CompilerServices;
using CommandLine.Text;
using CommandLine;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


[assembly:InternalsVisibleTo("AXSharp.nuget.update.Tests")]

namespace AXSharp.nuget.update;

public static class Program
{
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(Update);
    }

    public static void Update(Options o)
    {
        if (o.FileToUpdate!.EndsWith(".json"))
        {
            UpdateTools(o);
        }
        else if (o.FileToUpdate.EndsWith(".csproj"))
        {
            UpdatePackages(o);
        }
    }

    public static void UpdatePackages(Options o)
    {
        var doc = LoadCsProjFile(o.FileToUpdate);
        UpdatePackageVersion(doc, o.PackageId, o.NewVersion);
        SaveCsProjFile(doc, o.FileToUpdate);
    }

    public static void UpdateTools(Options o)
    {
        var doc = JObject.Parse(File.ReadAllText(o.FileToUpdate));
        doc.SelectToken($"tools.['{o.PackageId}'].version")?.Replace(o.NewVersion);
        File.WriteAllText(o.FileToUpdate, doc.ToString());
    }

    static XmlDocument LoadCsProjFile(string csProjPath)
    {
        var xmlDocument = new XmlDocument();
        xmlDocument.Load(csProjPath);
        return xmlDocument;
    }

    static void UpdatePackageVersion(XmlDocument projectFile, string packageId, string newVersion)
    {
        // Find the package reference you want to update
        XmlNode packageReference = projectFile.SelectSingleNode("//PackageReference[@Include='" + packageId + "']");

        // Update the package version
        if (packageReference != null)
        {
            packageReference.Attributes["Version"].Value = newVersion;
        }
    }

    static void SaveCsProjFile(XmlDocument xmlDocument, string fileName)
    {
        // Save the changes to the csproj file
        xmlDocument.Save(fileName);
    }
}


public class Options
{
    [Option('p', "target-file", Required = true, HelpText = "csproj or dotnet-tools.json")]
    public string? FileToUpdate { get; set; }

    [Option('i', "package-id", Required = true,
        HelpText = "Package id of a package that will be updated.")]
    public string? PackageId { get; set; }

    [Option('v', "update-to-version", Required = true,
        HelpText = "Version to which the packages will be updated.")]
    public string? NewVersion { get; set; }
}

