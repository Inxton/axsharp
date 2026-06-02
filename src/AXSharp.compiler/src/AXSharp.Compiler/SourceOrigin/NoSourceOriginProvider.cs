// AXSharp.Compiler
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

namespace AXSharp.Compiler;

/// <summary>
///     Resolves None-mode provenance: no assembly-level attribute is emitted and per-type paths stay
///     rooted at the project <c>src</c> folder. This reproduces the pre-feature (legacy) behavior.
/// </summary>
public sealed class NoSourceOriginProvider : ISourceOriginProvider
{
    /// <inheritdoc />
    public SourceOrigin Resolve(AxProject project) => SourceOrigin.None(project.SrcFolder);
}
