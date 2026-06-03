// AXSharp.Connector
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;

namespace AXSharp.Connector;

/// <summary>
///     Indicates the AX/Structured-Text source file from which this type was transpiled.
///     The path is forward-slash separated; its base depends on the assembly-level provenance
///     attribute: it is relative to the repository root when <see cref="SourceRepositoryAttribute" />
///     is present on the assembly, otherwise relative to the project <c>src</c> folder (when
///     <see cref="SourceLibraryAttribute" /> is present, or in legacy output with neither).
/// </summary>
/// <note type="note">
///     This attribute is emitted in the connector building process. It should not be declared by the
///     framework consumers.
/// </note>
[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface,
    AllowMultiple = false, Inherited = false)]
public class SourceFileAttribute : Attribute
{
    /// <summary>
    ///     Creates new instance of <see cref="SourceFileAttribute" />
    /// </summary>
    /// <param name="sourceFile">Source file path, relative to the project <c>src</c> folder, forward-slash separated.</param>
    public SourceFileAttribute(string sourceFile)
    {
        SourceFile = sourceFile ?? throw new ArgumentNullException(nameof(sourceFile));
    }

    /// <summary>
    ///     Gets the source file path, relative to the project <c>src</c> folder, forward-slash separated.
    /// </summary>
    public string SourceFile { get; }
}
