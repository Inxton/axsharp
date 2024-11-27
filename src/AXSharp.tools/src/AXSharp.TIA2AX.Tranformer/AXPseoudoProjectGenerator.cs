using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using TAXSharp.TIA2AX.Transformer;

namespace AXSharp.TIA2AX.Transformer
{
    public class AXPseoudoProjectGenerator
    {
        public static void Create(string baseDirectory, string outputProject, IEnumerable<string> sources, Options options)
        {
            var generator = new AXPseoudoProjectGenerator();

            generator.CreateProjectStructure(baseDirectory, outputProject, options);
            generator.AddTiaDataTypes(baseDirectory, outputProject, options);

            var srcFolderPath = Path.Combine(baseDirectory, outputProject, "src");

            var configurationBuilder = new StringBuilder();
            configurationBuilder.AppendLine("CONFIGURATION MyConfiguration");
            configurationBuilder.AppendLine("TASK Main(Interval := T#10ms, Priority := 1);");
            configurationBuilder.AppendLine("PROGRAM P1 WITH Main: MyProgram;");
            configurationBuilder.AppendLine("\tVAR_GLOBAL");
            foreach (var source in sources)
            {
                var fileName = new FileInfo(source);
                var input = File.ReadAllText(source);
                var tranformed = string.Empty;
                using (var sw = new StreamWriter(Path.Combine(srcFolderPath, $"{fileName.Name}.st")))
                {
                    tranformed = TIA2AXTypeTransformer.GetTransformation(input, options);
                    sw.Write(tranformed);
                }


                foreach (var dbName in generator.GetDbNames(tranformed))
                {
                    configurationBuilder.AppendLine($"\t{{#ix-attr: [DBAttribute()]}}");
                    configurationBuilder.AppendLine($"\t{{S7.extern=ReadWrite}}");
                    configurationBuilder.AppendLine($"\t{dbName} : {options.Namespace}.{dbName};");
                }
            }
            configurationBuilder.AppendLine("\tEND_VAR");
            configurationBuilder.AppendLine("END_CONFIGURATION");

            configurationBuilder.Append(@"PROGRAM MyProgram
    VAR
        
    END_VAR
    ;
END_PROGRAM");

            using (var sw = new StreamWriter(Path.Combine(srcFolderPath, $"configuration.st")))
            {
                sw.Write(configurationBuilder.ToString());
            }
        }

        private void EnsureDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }


        private IEnumerable<string> GetDbNames(string source)
        {
            var regex = new Regex(@"{#ix-db: (?<dbName>\w+)}");
            var matches = regex.Matches(source);
            foreach (Match match in matches)
            {
                yield return match.Groups["dbName"].Value;
            }
        }

        private void CreateProjectStructure(string baseDirectory, string outputProject, Options option)
        {
            // Combine paths to create the full directory paths
            string projectDirectory = Path.Combine(baseDirectory, outputProject);
            string srcDirectory = Path.Combine(projectDirectory, "src");
            string testDirectory = Path.Combine(projectDirectory, "test");

            // Create the src and test directories
            EnsureDirectory(srcDirectory);
            EnsureDirectory(testDirectory);

            // Path for the apax.yml file
            string apaxFilePath = Path.Combine(projectDirectory, "apax.yml");

            // Content for the apax.yml file
            string apaxContent = $@"name: ""{option.Namespace.ToLower()}""
version: 0.0.0
type: app
targets:  
    - '1500'
 #   - plcsim
 #   - llvm
 #   - swcpu
 #   - vplc

devDependencies:
  ""@ax/sdk"": ^2311.0.1
";

            // Write the apax content to the file
            File.WriteAllText(apaxFilePath, apaxContent);
        }
        private void AddTiaDataTypes(string baseDirectory, string outputProject, Options option)
        {
            // Combine paths to create the full directory paths
            string projectDirectory = Path.Combine(baseDirectory, outputProject);
            string srcDirectory = Path.Combine(projectDirectory, "src");

            // Create the src and test directories
            EnsureDirectory(srcDirectory);

            string filePath = Path.Combine(srcDirectory, "DTL.st");

            // Content for DLT type
            string content = $@"NAMESPACE {option.Namespace.ToLower()}
   TYPE
      {{S7.extern=ReadWrite}}
      DTL :
         STRUCT
            YEAR  : UINT;
            MONTH : USINT;
            DAY: USINT;
            WEEKDAY: USINT;
            HOUR: USINT;
            MINUTE: USINT;
            SECOND: USINT;
            NANOSECOND: UDINT;
         END_STRUCT;
   END_TYPE
END_NAMESPACE
";

            // Write the apax content to the file
            File.WriteAllText(filePath, content);
        }
    }
}
