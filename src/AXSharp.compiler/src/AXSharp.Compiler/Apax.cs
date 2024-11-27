// AXSharp.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using NuGet.Versioning;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace AXSharp.Compiler;

/// <summary>
///     Data transfer object for `apax.yml` file.
/// </summary>
public class Apax
{
    /// <summary>
    /// Creates new instance of <see cref="Apax"/>
    /// </summary>
    [Obsolete("Use `CreateApax` instead.")]
    public Apax()
    {
        
    }

    /// <summary>
    ///     Gets or sets ax project name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Ax project type.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the version of the AX project.
    /// </summary>
    
    public string? Version { get; set; }

    /// <summary>
    ///    Gets or sets ax targets.
    /// </summary>
    public IEnumerable<string>? Targets { get; set; }

    /// <summary>
    ///    Gets or sets ax project files to include in package.
    /// </summary>
    public IEnumerable<string>? Files { get; set; }

    /// <summary>
    ///    Gets or sets ax projects development dependencies.
    /// </summary>
    public IDictionary<string, string>? DevDependencies { get; set; }

    /// <summary>
    ///  Gets or sets ax projects dependencies.
    /// </summary>
    public IDictionary<string, string>? Dependencies { get; set; }

    /// <summary>
    /// Creates new instance of <see cref="Apax"/>.
    /// </summary>
    /// <param name="projectFile">Project file from which the ApaxFile object will be created.</param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static Apax CreateApaxDto(string projectFile)
    {
        try
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            return deserializer.Deserialize<Apax>(File.ReadAllText(projectFile));
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException(
                "'apax.yml' file was not found in the working directory. Make sure your current directory is simatic-ax project directory or provide source directory argument (for details see ixc --help)");
        }
    }

    public static Apax TryCreateApaxDto(string projectFile)
    {
        try
        {
            if (!File.Exists(projectFile))
                return null;

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            return deserializer.Deserialize<Apax>(File.ReadAllText(projectFile));
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException(
                "'apax.yml' file was not found in the working directory. Make sure your current directory is simatic-ax project directory or provide source directory argument (for details see ixc --help)");
        }
    }

    /// <summary>
    /// Update version in an apax.yml file
    /// </summary>
    /// <param name="apaxFile">Apax file to update.</param>
    /// <param name="version">Version.</param>
    /// <exception cref="FileNotFoundException"></exception>
    public static void UpdateVersion(string apaxFile, string version)
    {
        try
        {
            var apax = CreateApaxDto(apaxFile);
            apax.Version = version;


            using (var tw = new StreamWriter(apaxFile))
            {
                var serializer = new SerializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();

                serializer.Serialize(tw, apax);
            }
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException(
                "'apax.yml' file was not found in the working directory. Make sure your current directory is simatic-ax project directory or provide source directory argument (for details see ixc --help)");
        }
    }
}