// AXSharp.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AX.ST.Semantic;

namespace AXSharp.Compiler.Core;

/// <summary>
///     Provides abstraction for source emitting classes.
/// </summary>
public interface ISourceBuilder
{
    /// <summary>
    ///     Gets string output of the builder.
    /// </summary>
    string Output { get; }

    /// <summary>
    ///     Get source group name. The output of this builder will be places into sub-folder name after this
    ///     <see cref="Group" /> property.
    /// </summary>
    string Group { get; }

    /// <summary>
    ///     Suffix of output files.
    /// </summary>
    string OutputFileSuffix { get; }

    /// <summary>
    /// Gets builder type. Builder type is used by CompilerOmits attribute to prevent compilation of constructs for specific
    /// compilation outputs.
    /// </summary>
    string BuilderType { get; }

    /// <summary>
    /// Get the semantic compilation for this builder.
    /// </summary>
    Compilation Compilation { get; }

    ICompilerOptions? CompilerOptions { get; }

    eCommAccessibility TypeCommAccessibility { get; }
}