// AXSharp.Compiler
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using LibGit2Sharp;

namespace AXSharp.Compiler;

/// <summary>
///     Resolves Repository-mode provenance from the nearest enclosing git repository (innermost
///     <c>.git</c> — submodules/worktrees resolve to their own repository) that exposes an
///     <c>origin</c> remote. The per-type path base is the repository working directory; the remote
///     URL is normalized to canonical https. When no repository or no usable <c>origin</c> remote is
///     found, resolution delegates to the fallback (apax) provider.
/// </summary>
public sealed class GitSourceOriginProvider : ISourceOriginProvider
{
    private readonly ISourceOriginProvider _fallback;

    /// <summary>
    ///     Creates a new instance. <paramref name="fallback" /> is used when the project is not in a
    ///     git repository with a usable <c>origin</c> remote (defaults to <see cref="ApaxSourceOriginProvider" />).
    /// </summary>
    public GitSourceOriginProvider(ISourceOriginProvider? fallback = null)
        => _fallback = fallback ?? new ApaxSourceOriginProvider();

    /// <inheritdoc />
    public SourceOrigin Resolve(AxProject project)
    {
        var gitDir = Repository.Discover(project.ProjectFolder);
        if (string.IsNullOrEmpty(gitDir))
            return _fallback.Resolve(project);

        using var repo = new Repository(gitDir);

        var origin = repo.Network.Remotes["origin"];
        if (origin == null || string.IsNullOrWhiteSpace(origin.Url))
            return _fallback.Resolve(project);

        var url = GitUrlNormalizer.Normalize(origin.Url);
        var commit = repo.Head?.Tip?.Sha ?? string.Empty;
        var branch = repo.Info.IsHeadDetached ? string.Empty : repo.Head?.FriendlyName ?? string.Empty;
        var repoRoot = repo.Info.WorkingDirectory ?? project.ProjectFolder;

        return SourceOrigin.Repository(repoRoot, url, commit, branch);
    }
}
