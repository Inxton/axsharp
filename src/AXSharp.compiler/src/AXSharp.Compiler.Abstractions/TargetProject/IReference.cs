// AXSharp.Compiler.Abstractions
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

// ReSharper disable once CheckNamespace

namespace AXSharp.Compiler;

public interface IReference
{
    string ReferencePath { get; }
    string Version { get; }

    /// <summary>
    /// Gets file path of meta data for this reference.
    /// </summary>
    string MetadataPath { get; }

    /// <summary>
    /// Get file path of ix project information for this reference.
    /// </summary>
    string ProjectInfo { get; }

    /// <summary>
    /// Gets whether this reference is ix project.
    /// </summary>
    bool IsIxDependency { get; }
}