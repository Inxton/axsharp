// AXSharp.CompilerTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AXSharp.Compiler;

namespace AXSharp.CompilerTests;

public class GitUrlNormalizerTests
{
    [Theory]
    // scp-like SSH syntax -> https, strip .git
    [InlineData("git@github.com:org/repo.git", "https://github.com/org/repo")]
    [InlineData("git@github.com:org/repo", "https://github.com/org/repo")]
    // ssh:// scheme -> https, drop user + port, strip .git
    [InlineData("ssh://git@github.com/org/repo.git", "https://github.com/org/repo")]
    [InlineData("ssh://git@github.com:22/org/repo.git", "https://github.com/org/repo")]
    // https with embedded credentials -> stripped; .git stripped
    [InlineData("https://user:token@github.com/org/repo.git", "https://github.com/org/repo")]
    // already clean -> unchanged
    [InlineData("https://github.com/org/repo", "https://github.com/org/repo")]
    // trailing slash trimmed
    [InlineData("https://github.com/org/repo/", "https://github.com/org/repo")]
    public void Normalizes_remote_to_canonical_https(string raw, string expected)
    {
        Assert.Equal(expected, GitUrlNormalizer.Normalize(raw));
    }

    [Fact]
    public void Leaves_unrecognized_input_untouched()
    {
        Assert.Equal("some-local-path", GitUrlNormalizer.Normalize("some-local-path"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Returns_empty_for_blank(string? raw)
    {
        Assert.Equal(string.Empty, GitUrlNormalizer.Normalize(raw));
    }
}
