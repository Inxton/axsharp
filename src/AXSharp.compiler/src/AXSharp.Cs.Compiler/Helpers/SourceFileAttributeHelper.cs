// AXSharp.Compiler.Cs
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System.IO;
using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;

namespace AXSharp.Compiler.Cs.Helpers;

/// <summary>
///     Helpers for emitting the <c>AXSharp.Connector.SourceFileAttribute</c> onto generated types.
/// </summary>
public static class SourceFileAttributeHelper
{
    /// <summary>
    ///     Produces the <c>[AXSharp.Connector.SourceFileAttribute(...)]</c> declaration for the given type,
    ///     carrying the source file path relative to the project <c>src</c> folder (forward-slash separated).
    ///     Returns an empty string when the declaration has no source location (e.g. types parsed from
    ///     dependency metadata), making emission a safe no-op.
    /// </summary>
    public static string GetSourceFileAttribute(this ITypeDeclaration declaration, AxProject axProject)
    {
        var filename = declaration.Location?.GetLineSpan().Filename;
        if (string.IsNullOrEmpty(filename))
            return string.Empty;

        var relative = Path.GetRelativePath(axProject.SrcFolder, filename).Replace('\\', '/');
        return $"[AXSharp.Connector.SourceFileAttribute(@\"{relative}\")]\n";
    }
}
