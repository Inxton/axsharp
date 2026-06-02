// AXSharp.Compiler
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

namespace AXSharp.Compiler;

/// <summary>
///     The kind of provenance resolved for a transpiled assembly. Determines which assembly-level
///     attribute is emitted and how the per-type source path is rooted.
/// </summary>
public enum SourceOriginMode
{
    /// <summary>Git repository with a usable remote; per-type path is repository-root relative.</summary>
    Repository,

    /// <summary>apax package; per-type path is <c>src</c>-folder relative.</summary>
    Library,

    /// <summary>No provenance emitted; per-type path is <c>src</c>-folder relative (legacy).</summary>
    None
}

/// <summary>
///     Resolved provenance of a transpiled AX project: the base folder against which per-type source
///     paths are relativized, plus the repository or package identity to emit at assembly level.
/// </summary>
public sealed class SourceOrigin
{
    private SourceOrigin(
        SourceOriginMode mode,
        string baseFolder,
        string? url,
        string? commit,
        string? branch,
        string? packageName,
        string? packageVersion)
    {
        Mode = mode;
        BaseFolder = baseFolder;
        Url = url;
        Commit = commit;
        Branch = branch;
        PackageName = packageName;
        PackageVersion = packageVersion;
    }

    /// <summary>Gets the resolved provenance mode.</summary>
    public SourceOriginMode Mode { get; }

    /// <summary>Gets the folder against which per-type source paths are relativized.</summary>
    public string BaseFolder { get; }

    /// <summary>Gets the canonical remote URL (Repository mode); otherwise <c>null</c>.</summary>
    public string? Url { get; }

    /// <summary>Gets the commit SHA (Repository mode); otherwise <c>null</c>.</summary>
    public string? Commit { get; }

    /// <summary>Gets the branch name, empty on detached HEAD (Repository mode); otherwise <c>null</c>.</summary>
    public string? Branch { get; }

    /// <summary>Gets the apax package name (Library mode); otherwise <c>null</c>.</summary>
    public string? PackageName { get; }

    /// <summary>Gets the apax package version (Library mode); otherwise <c>null</c>.</summary>
    public string? PackageVersion { get; }

    /// <summary>Creates a Repository-mode origin rooted at the repository working directory.</summary>
    public static SourceOrigin Repository(string baseFolder, string url, string commit, string branch)
        => new(SourceOriginMode.Repository, baseFolder, url, commit, branch, null, null);

    /// <summary>Creates a Library-mode origin rooted at the project <c>src</c> folder.</summary>
    public static SourceOrigin Library(string baseFolder, string name, string version)
        => new(SourceOriginMode.Library, baseFolder, null, null, null, name, version);

    /// <summary>Creates a None-mode origin rooted at the project <c>src</c> folder (legacy, no emission).</summary>
    public static SourceOrigin None(string baseFolder)
        => new(SourceOriginMode.None, baseFolder, null, null, null, null, null);
}
