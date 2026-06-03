// AXSharp.Compiler
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

namespace AXSharp.Compiler;

/// <summary>
///     Maps the <c>--source-origin</c> option value to an <see cref="ISourceOriginProvider" />:
///     <c>apax</c> forces package mode, <c>off</c> disables emission, anything else (including
///     <c>auto</c>, null or unrecognized) selects git auto-detection.
/// </summary>
public static class SourceOriginProviderFactory
{
    /// <summary>Creates the provider for the given mode (case-insensitive).</summary>
    public static ISourceOriginProvider Create(string? mode)
        => (mode ?? string.Empty).Trim().ToLowerInvariant() switch
        {
            "apax" => new ApaxSourceOriginProvider(),
            "off" => new NoSourceOriginProvider(),
            _ => new GitSourceOriginProvider()
        };
}
