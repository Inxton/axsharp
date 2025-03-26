using CommandLine;

namespace AXSharp.LocalizablesToResx
{
    public class Options
    {
        [Option('f', "file", Required = false, HelpText = "Source file, from which resx will be generated.")]
        public string? SourceFile { get; set; }

        [Option('d', "directory", Required = false, HelpText = "Source director, which contains files for resx gen.")]
        public string? SourceDirectory { get; set; }

        [Option('i', "identifier", Required = false, HelpText = "Localizable identifier, from which regex for searching is created. If empty, default value \"Localizer\" is used.")]
        public string? Identifier { get; set; }

        [Option('o', "output", Required = true, HelpText = "Required output resx file.")]
        public string? OutputResx { get; set; }

    }
}
