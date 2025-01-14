// AXSharp.Compiler.Abstractions
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

// ReSharper disable once CheckNamespace

namespace AXSharp.Compiler;

public interface ITargetProject
{
    /// <summary>
    ///     Get folder for project metadata.
    /// </summary>
    string GetMetaDataFolder { get; }

    string ProjectRootNamespace { get; }

    /// <summary>
    ///     Provisions project structure.
    /// </summary>
    void ProvisionProjectStructure();

    void GenerateResources();

    void GenerateCompanionData();

    void InstallAXSharpDependencies(IEnumerable<object> dependencies);
    
    IEnumerable<IReference> LoadReferences();
}