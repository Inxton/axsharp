// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Compiler;

/// <summary>
///     Provide information about project reference from csproj file.
/// </summary>
public class ProjectReference : IProjectReference
{
    /// <summary>
    ///     Creates new instance of <see cref="ProjectReference" />
    /// </summary>
    /// <param name="directory">Referenced project directory</param>
    /// <param name="projectPath">Referenced project csproj file</param>
    public ProjectReference(string directory, string projectPath)
    {
        ProjectFileInfo = new FileInfo(Path.GetFullPath(Path.Combine(directory, projectPath)));
    }

    /// <summary>
    ///     Gets information about csproj file.
    /// </summary>
    public FileInfo ProjectFileInfo { get; }

    /// <summary>
    ///     Get csproj file path.
    /// </summary>
    public string ProjectFilePath => ProjectFileInfo.FullName;

    /// <summary>
    ///     Gets inner references of csproj file.
    /// </summary>
    public IEnumerable<IReference> References { get; internal set; } = new List<IReference>();

    /// <summary>
    ///     Gets path of the csproj file.
    /// </summary>
    public string ReferencePath => ProjectFileInfo.Directory.FullName;

    /// <summary>
    ///     Returns version (defaults to 0.0.0.0);
    /// </summary>
    public string Version => "0.0.0.0";

    /// <inheritdoc />
    public string MetadataPath => Path.Combine(ReferencePath, ".meta", "meta.json");

    /// <inheritdoc />
    public string ProjectInfo => Path.Combine(ReferencePath, ".meta", "sourceinfo.json");

    /// <inheritdoc />
    public bool IsIxDependency => File.Exists(MetadataPath);
}