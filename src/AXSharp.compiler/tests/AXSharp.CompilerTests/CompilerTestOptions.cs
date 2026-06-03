// AXSharp.Compiler.CsTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AXSharp.Compiler;

namespace AXSharp.CompilerTests;


public class CompilerTestOptions : ICompilerOptions
{
    private string _outputProject = null;
    public string? OutputProjectFolder
    {
        get
        {
           return _outputProject;
        }
        set
        {
            _outputProject = value;
        }
    }
    public string? ProjectFile { get => null; set { } }
    public bool UseBase { get => false; set { } }
    public bool NoDependencyUpdate { get => false; set { } }
    public bool IgnoreS7Pragmas { get => false; set { } }
    public bool SkipDependencyCompilation { get => false; set { } }
    public string TargetPlatfromMoniker { get; set; } = "ax";
    public string? UiHostProject { get; set; }

    // Tests default to legacy behavior (no provenance emission) for deterministic, repo-independent output.
    public string SourceOrigin { get; set; } = "off";
}