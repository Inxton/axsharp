// AXSharp.Compiler.Abstractions
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

namespace AXSharp.Compiler;

public interface ICompilerOptions
{
    string? OutputProjectFolder { get; set; }
    string? ProjectFile { get; set; }
    bool UseBase { get; set; }

    bool NoDependencyUpdate { get; set; }

    bool IgnoreS7Pragmas { get; set; }

    bool SkipDependencyCompilation { get; set; }

    /// <summary>
    /// Provides target platform moniker to instruct the compiler about target specific options.
    /// </summary>
    string TargetPlatfromMoniker { get; set; }
}