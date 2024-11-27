// See https://aka.ms/new-console-template for more information
using AXSharp.LocalizablesToResx;
using CommandLine;
using CommandLine.Text;
using System.Resources.NetStandard;
using System.Text.RegularExpressions;


Parser.Default.ParseArguments<Options>(args)
.WithParsed(o =>
{
    Main(o);
});



void Main(Options o)
{
    Console.WriteLine("** ltr (LocalizablesToResx) **");
    Console.WriteLine("Generator of resx file from source code based on localizable identifier.");
    Console.WriteLine("Generating...");
    // Create a Regex
    ResXGen.CreateRegex(o.Identifier);

    // Create in memory dictionary with unique localizable values 
    ResXGen.CreateResxDictionary(o);

    // Write dictionary into resx file
    ResXGen.WriteToResx(o.OutputResx);

    Console.WriteLine($"Returned {ResXGen.count} records.");
    Console.WriteLine($"Location: {o.OutputResx}");
    Console.WriteLine("Done.");


}

/// <summary>
/// Main static class containing logic of acquiring localized strings and generating resx file.
/// </summary>
public static class ResXGen
{
    public static uint count;
    public static Regex LocalizableRegex;
    public static Dictionary<string, string> ResxDictionary = new Dictionary<string, string>();


    /// <summary>
    /// Create regex based on -i argument. If argument is empty, default regex "Localizer\[.*?\]" is used.
    /// </summary>
    /// <param name="identifier">Identifier used in regex</param>
    public static void CreateRegex(string identifier)
    {
        string pattern;
        if (identifier == null)
        {
            //use default identifier for regex
            pattern = @"Localizer\[.*?\]";
        }
        else
        {
            pattern = $@"{identifier}\[.*?\]";
        }

        LocalizableRegex = new Regex(pattern);
    }

    /// <summary>
    /// Creates dictionary of values acquired from input files.
    /// </summary>
    /// <param name="o"></param>
    public static void CreateResxDictionary(Options o)
    {
        if (o.SourceFile != null)
        {
            if (!File.Exists(o.SourceFile))
            {
                Console.WriteLine("Source file does not exist!");
                return;
            }
            else
            {
                AddLocalizablesToDictionary(o.SourceFile);
            }
        }

        if (o.SourceDirectory != null)
        {
            if (!Directory.Exists(o.SourceDirectory))
            {
                Console.WriteLine("Director does not exist!");
                return;
            }
            else
            {
                CreateResxDictionaryRecursive(o.SourceDirectory);
            }
        }

    }

    /// <summary>
    /// Writes created dictionary of localizable values into resx file.
    /// </summary>
    /// <param name="outputPath">Output path of resx file</param>
    public static void WriteToResx(string outputPath)
    {
        using (ResXResourceWriter resx = new ResXResourceWriter(outputPath))
        {
            foreach (var item in ResxDictionary)
            {
                resx.AddResource(item.Value, item.Value);
            }
        }
    }

    private static void CreateResxDictionaryRecursive(string sourceDir)
    {
        foreach (string d in Directory.GetDirectories(sourceDir))
        {
            foreach (string f in Directory.GetFiles(d, "*.razor"))
            {
                AddLocalizablesToDictionary(f);
            }

            CreateResxDictionaryRecursive(d);
        }
    }

    private static void AddLocalizablesToDictionary(string filePath)
    {
        string ln;
        using (StreamReader file = new StreamReader(filePath))
        {
            while ((ln = file.ReadLine()) != null)
            {
                MatchCollection matches = LocalizableRegex.Matches(ln);

                foreach (Match match in matches)
                {
                    var value = match.Value;

                    string[] sp = value.Split('\"');
                    // get text inside [""] 
                    if (sp.Length > 1 && ResxDictionary.TryAdd(sp[1], sp[1]))
                    {
                        count++;
                    }
                }

            }
            file.Close();
        }
    }
    

}


