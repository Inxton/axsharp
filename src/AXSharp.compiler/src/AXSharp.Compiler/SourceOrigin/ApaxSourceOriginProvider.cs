// AXSharp.Compiler
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

namespace AXSharp.Compiler;

/// <summary>
///     Resolves Library-mode provenance from the project's apax metadata. Per-type paths are rooted
///     at the project <c>src</c> folder. Missing name/version map to empty strings.
/// </summary>
public sealed class ApaxSourceOriginProvider : ISourceOriginProvider
{
    /// <inheritdoc />
    public SourceOrigin Resolve(AxProject project)
        => SourceOrigin.Library(
            project.SrcFolder,
            project.ProjectInfo.Name ?? string.Empty,
            project.ProjectInfo.Version ?? string.Empty);
}
