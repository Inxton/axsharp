// AXSharp.Compiler
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Text.RegularExpressions;

namespace AXSharp.Compiler;

/// <summary>
///     Normalizes a git remote URL into a canonical, credential-free <c>https</c> form suitable for
///     embedding in <c>AXSharp.Connector.SourceRepositoryAttribute</c> and for building source
///     permalinks. Best-effort: input that is not recognizable as a remote URL is returned unchanged.
/// </summary>
public static class GitUrlNormalizer
{
    // scp-like syntax: [user@]host:org/repo[.git]  (no URI scheme)
    private static readonly Regex ScpLike = new(@"^(?:[^@/]+@)?([^/:]+):(.+)$", RegexOptions.Compiled);

    /// <summary>
    ///     Normalizes <paramref name="raw" /> to canonical <c>https</c>, stripping any embedded
    ///     credentials, port and trailing <c>.git</c>/slash. Returns <see cref="string.Empty" /> for
    ///     null/blank input and the trimmed input verbatim when it is not a recognizable remote URL.
    /// </summary>
    public static string Normalize(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return string.Empty;

        var url = raw.Trim();

        if (!url.Contains("://"))
        {
            var scp = ScpLike.Match(url);
            return scp.Success
                ? BuildHttps(scp.Groups[1].Value, scp.Groups[2].Value)
                : url; // not a remote URL — leave untouched
        }

        return Uri.TryCreate(url, UriKind.Absolute, out var uri)
            ? BuildHttps(uri.Host, uri.AbsolutePath)
            : url;
    }

    private static string BuildHttps(string host, string path)
    {
        path = path.Trim('/');
        if (path.EndsWith(".git", StringComparison.OrdinalIgnoreCase))
            path = path.Substring(0, path.Length - 4);

        return $"https://{host}/{path}";
    }
}
