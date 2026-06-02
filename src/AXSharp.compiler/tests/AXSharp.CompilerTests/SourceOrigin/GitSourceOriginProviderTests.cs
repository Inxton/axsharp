// AXSharp.CompilerTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using AXSharp.Compiler;
using LibGit2Sharp;

namespace AXSharp.CompilerTests.SourceOrigin;

public class GitSourceOriginProviderTests
{
    private const string Apax = "name: gitsample\nversion: 1.0.0\ntype: app\n";

    private static void InitRepo(string dir, string? remoteUrl)
    {
        Repository.Init(dir);
        using var repo = new Repository(dir);
        Commands.Stage(repo, "*");
        var sig = new Signature("tester", "tester@example.com", DateTimeOffset.UnixEpoch.AddDays(1));
        repo.Commit("init", sig, sig);
        if (remoteUrl != null)
            repo.Network.Remotes.Add("origin", remoteUrl);
    }

    [Fact]
    public void Resolves_repository_mode_with_normalized_url_sha_and_branch()
    {
        using var fixture = new TempAxProjectFixture(Apax);
        InitRepo(fixture.Root, "git@github.com:org/repo.git");

        string expectedSha, expectedBranch;
        using (var repo = new Repository(fixture.Root))
        {
            expectedSha = repo.Head.Tip.Sha;
            expectedBranch = repo.Head.FriendlyName;
        }

        var origin = new GitSourceOriginProvider().Resolve(new AxProject(fixture.Root));

        Assert.Equal(SourceOriginMode.Repository, origin.Mode);
        Assert.Equal("https://github.com/org/repo", origin.Url);
        Assert.Equal(expectedSha, origin.Commit);
        Assert.Equal(expectedBranch, origin.Branch);
        Assert.Equal(
            TempAxProjectFixture.NormalizeDir(fixture.Root),
            TempAxProjectFixture.NormalizeDir(origin.BaseFolder));
    }

    [Fact]
    public void Falls_back_to_library_when_no_remote()
    {
        using var fixture = new TempAxProjectFixture(Apax);
        InitRepo(fixture.Root, remoteUrl: null);

        var project = new AxProject(fixture.Root);
        var origin = new GitSourceOriginProvider().Resolve(project);

        Assert.Equal(SourceOriginMode.Library, origin.Mode);
        Assert.Equal("gitsample", origin.PackageName);
        Assert.Equal("1.0.0", origin.PackageVersion);
        Assert.Equal(project.SrcFolder, origin.BaseFolder);
    }

    [Fact]
    public void Emits_empty_branch_on_detached_head()
    {
        using var fixture = new TempAxProjectFixture(Apax);
        InitRepo(fixture.Root, "git@github.com:org/repo.git");
        using (var repo = new Repository(fixture.Root))
        {
            Commands.Checkout(repo, repo.Head.Tip); // detach HEAD at the tip commit
        }

        var origin = new GitSourceOriginProvider().Resolve(new AxProject(fixture.Root));

        Assert.Equal(SourceOriginMode.Repository, origin.Mode);
        Assert.Equal(string.Empty, origin.Branch);
    }
}
