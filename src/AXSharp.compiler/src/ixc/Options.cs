// AXSharp.ixc
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using CommandLine;
using AXSharp.Compiler;

namespace ixc;

/// <summary>
///     Options for ixc cli.
/// </summary>
internal class Options : ICompilerOptions
{
    [Option('x', "source-project-folder", Required = false, HelpText = "Simatic-ax project folder")]
    public string? AxSourceProjectFolder { get; set; }

    [Option('o', "output-project-folder", Required = false,
        HelpText = "Output project folder where compiler emits result. It must be either absolute path or path relative to the Simatic-ax project folder.")]
    public string? OutputProjectFolder { get; set; }

    [Option('b', "use-base-symbol", Required = false, Default = false,
        HelpText = "Will use base symbol in inherited types. Obsolete used in early versions of sld")]
    public bool UseBase { get; set; }

    [Option('p', "project-file", Required = false, Default = "",
        HelpText = "Output project file")]
    public string? ProjectFile { get; set; }

    [Option('u', "no-dependency-update", Required = false, Default = false,
        HelpText = "Prevent dependency of twins from apax to install")]
    public bool NoDependencyUpdate { get; set; }

    [Option('s', "no-s7-pragmas", Required = false, Default = false,
        HelpText = "Compiler ignores S7.Extern=ReadWrite & S7.Extern=ReadOnly. Compiles all types and members regardless comm settings.")]
    public bool IgnoreS7Pragmas { get; set; }

    [Option('d', "skip-deps", Required = false, Default = false,
        HelpText = "Instructs the compiler to skip dependencies compilation of referenced AX# project.")]
    public bool SkipDependencyCompilation { get; set; }

    [Option('t', "target-platform-moniker", Required = false, Default = "ax",
        HelpText = "Instructs the compiler to adjust for target platform differences. Possible values 'ax', 'tia'")]
    public string TargetPlatfromMoniker { get; set; }
}

