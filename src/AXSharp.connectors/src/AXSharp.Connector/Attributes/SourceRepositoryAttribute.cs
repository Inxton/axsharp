using System;

namespace AXSharp.Connector;

/// <summary>
///     Identifies the remote git repository the transpiled types in this assembly originate from.
///     When this attribute is present, the per-type <see cref="SourceFileAttribute" /> path is
///     <b>relative to the repository root</b> (forward-slash separated). Combined with
///     <see cref="Commit" />, a source permalink can be formed as <c>{Url}/blob/{Commit}/{SourceFile}</c>.
/// </summary>
/// <note type="note">
///     This attribute is emitted in the connector building process. It should not be declared by the
///     framework consumers.
/// </note>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
public class SourceRepositoryAttribute : Attribute
{
    /// <summary>
    ///     Creates a new instance of <see cref="SourceRepositoryAttribute" />.
    /// </summary>
    /// <param name="url">Canonical (https) remote repository URL.</param>
    /// <param name="commit">Full commit SHA the sources were transpiled from.</param>
    /// <param name="branch">Branch name; empty when the repository is in a detached-HEAD state.</param>
    public SourceRepositoryAttribute(string url, string commit, string branch)
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
        Commit = commit ?? throw new ArgumentNullException(nameof(commit));
        Branch = branch ?? throw new ArgumentNullException(nameof(branch));
    }

    /// <summary>
    ///     Gets the canonical (https) remote repository URL.
    /// </summary>
    public string Url { get; }

    /// <summary>
    ///     Gets the full commit SHA the sources were transpiled from.
    /// </summary>
    public string Commit { get; }

    /// <summary>
    ///     Gets the branch name; empty when the repository is in a detached-HEAD state.
    /// </summary>
    public string Branch { get; }
}
