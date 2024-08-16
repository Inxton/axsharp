// AXSharp.Compiler.Abstractions
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Compiler;

public interface ICompilerOptions
{
    string? OutputProjectFolder { get; set; }
    string? ProjectFile { get; set; }
    bool UseBase { get; set; }

    bool NoDependencyUpdate { get; set; }

    bool IgnoreS7Pragmas { get; set; }

    bool SkipDependencyCompilation { get; set; }
}