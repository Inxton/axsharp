// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Xml.Linq;
using AXSharp.Compiler.Cs.Exceptions;
using NuGet.Configuration;
using NuGet.Packaging.Core;

namespace AXSharp.Compiler;

/// <summary>
///     Provides information about package reference from csproj file.
/// </summary>
public class PackageReference : IPackageReference
{
    private static readonly string NugetDir =
        SettingsUtility.GetGlobalPackagesFolder(Settings.LoadDefaultSettings(null));

    internal static IEnumerable<(string? include, string? version)> GetVersionFromCentralPackageManagement(string csprojFile)
    {
        var scFile = new FileInfo(csprojFile);
        string? currentDirectory = scFile.DirectoryName;
        string targetFile = null;

        // Search for the file in the directory tree upstream
        while (currentDirectory != null)
        {
            var potentialFile = Path.Combine(currentDirectory, "Directory.Packages.props");
            if (File.Exists(potentialFile))
            {
                targetFile = potentialFile;
                break;
            }

            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        }

        if (targetFile == null)
        {
            return new List<(string?, string?)>();
        }

        XDocument xdoc = XDocument.Load(targetFile);
        var packageElements = xdoc.Descendants().Where(e => e.Name == "PackageVersion" || e.Name == "GlobalPackageReference");

        return packageElements.Select(pv => 
            (
                pv.Attribute("Include")?.Value,
                pv.Attribute("Version")?.Value
            ));
    }

    /// <summary>
    ///     Creates new instance of <see cref="PackageReference" />
    /// </summary>
    /// <param name="packageReferenceNode">Package reference node from csproj file.</param>
    /// <param name="projectFile">Csproj file.</param>
    /// <exception cref="FailedToRetrievePackageReferenceException"></exception>
    public static PackageReference CreateFromReferenceNode(XElement packageReferenceNode, string projectFile)
    {
        string include = string.Empty;
        try
        {
            include = (packageReferenceNode.Attribute(XName.Get(nameof(Include)))?.Value ?? packageReferenceNode.Attribute(XName.Get("Update"))?.Value)!;
            var version = packageReferenceNode.Attribute(XName.Get(nameof(Version)))?.Value ??
                          packageReferenceNode.Attribute(XName.Get("VersionOverride"))?.Value;

            version = version ??
                      GetVersionFromCentralPackageManagement(projectFile).FirstOrDefault(p => p.include == include).version;



            if (include == null)
            {
                throw new FailedToRetrievePackageReferenceException(
                    $"We were unable to determine package id of one of the packages referenced in project {projectFile}\n" +
                    $"The package id must be in 'Include' or 'Update' attribute of 'PackageReference' element." +
                    $"Make sure your csproj file is valid", null);
            }

            if (version == null)
            {
                throw new FailedToRetrievePackageReferenceException(
                    $"We were unable to determine version of the package '{include}' referenced in project {projectFile}\n" +
                    $"Make sure you have the version defined either in the project file or in central package management file 'Directory.Packages.props'\n" +
                    $"upstream in your projects' directory structure.", null);
            }

            var referencePath = PackageReferenceNugetPath(include, version);

            return new PackageReference(referencePath, include, version);
        }
        catch (Exception ex)
        {
            throw new FailedToRetrievePackageReferenceException($"Failed to retrieve package reference {ex.Message}", ex);
        }
    }

    /// <summary>
    ///     Create new instance of <see cref="PackageReference" /> from <see cref="PackageDependency" />
    /// </summary>
    /// <param name="packageDependency">Package dependency</param>
    public PackageReference(PackageDependency packageDependency) 
        : this(PackageReferenceNugetPath(packageDependency.Id, packageDependency.VersionRange.OriginalString), packageDependency.Id, packageDependency.VersionRange.OriginalString)
    {
        
    }

    public PackageReference(string packageFolder, string id, string version)
    {
        Include = id;
        Version = version;
        ReferencePath = packageFolder;
    }

    /// <summary>
    ///     Gets package name.
    /// </summary>
    public string Include { get; internal set; }

    /// <summary>
    ///     Gets package name.
    /// </summary>
    public string Name => Include;

    /// <summary>
    ///     Gets package version.
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    ///     Gets reference path.
    /// </summary>
    public string ReferencePath { get; }

    private static string PackageReferenceNugetPath(string packageName, string packageVersion)
    {
        return Path.Combine(NugetDir, packageName, CsProject.GetBestMatchedVersion(packageName, packageVersion));
    }

    /// <inheritdoc />
    public string MetadataPath => Path.Combine(ReferencePath, "content", ".meta", "meta.json");

    /// <inheritdoc />
    public string ProjectInfo => Path.Combine(ReferencePath, "content", ".meta", "sourceinfo.json");

    /// <inheritdoc />
    public bool IsIxDependency => File.Exists(MetadataPath);
}