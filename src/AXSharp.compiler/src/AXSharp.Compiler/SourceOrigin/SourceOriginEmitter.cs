// AXSharp.Compiler
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

namespace AXSharp.Compiler;

/// <summary>
///     Renders the single assembly-level provenance attribute for a resolved <see cref="SourceOrigin" />.
/// </summary>
public static class SourceOriginEmitter
{
    /// <summary>
    ///     Returns the <c>[assembly: ...]</c> declaration for <paramref name="origin" />:
    ///     <c>AXSharp.Connector.SourceRepository</c> in repository mode,
    ///     <c>AXSharp.Connector.SourceLibrary</c> in library mode, or <c>null</c> in None mode
    ///     (no attribute is emitted).
    /// </summary>
    public static string? RenderAssemblyAttribute(SourceOrigin origin)
    {
        switch (origin.Mode)
        {
            case SourceOriginMode.Repository:
                return $"[assembly: AXSharp.Connector.SourceRepository({Verbatim(origin.Url)}, {Verbatim(origin.Commit)}, {Verbatim(origin.Branch)})]";
            case SourceOriginMode.Library:
                return $"[assembly: AXSharp.Connector.SourceLibrary({Verbatim(origin.PackageName)}, {Verbatim(origin.PackageVersion)})]";
            default:
                return null;
        }
    }

    private static string Verbatim(string? value)
        => "@\"" + (value ?? string.Empty).Replace("\"", "\"\"") + "\"";
}
