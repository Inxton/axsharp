using AXSharp.Compiler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace AXSharp.ixc_doc
{
    internal class Options : ICompilerOptions
    {
        [Option('x', "source-project-folder", Required = false, HelpText = "Simatic-ax project folder")]
        public string? AxSourceProjectFolder { get; set; }

        [Option('o', "output-project-folder", Required = false, HelpText = "Output project folder where compiler emits result.")]
        public string? OutputProjectFolder { get; set; }

        [Option('p', "project-file", Required = false, Default = "",
            HelpText = "Output project file")]
        public string? ProjectFile { get; set; }

        [Option('b', "use-base-symbol", Required = false, Default = false,
            HelpText = "Will use base symbol in inherited types")]
        public bool UseBase { get; set; }

        [Option('u', "no-dependency-update", Required = false, Default = false,
            HelpText = "Prevent dependency of twins from apax")]
        public bool NoDependencyUpdate { get; set; }

        public bool IgnoreS7Pragmas { get; set; }
        public bool SkipDependencyCompilation { get; set; }

        [Option('t', "target-platform-moniker", Required = false, Default = "ax",
        HelpText = "Instructs the compiler to adjust for target platform differences. Possible values 'ax', 'tia'")]
        public string TargetPlatfromMoniker { get; set; }

        public string? UiHostProject { get; set; }

        public string SourceOrigin { get; set; } = "auto";
    }
}
