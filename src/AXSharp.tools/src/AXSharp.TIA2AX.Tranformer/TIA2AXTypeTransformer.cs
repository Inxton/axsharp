using AXSharp.TIA2AX.Transformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TAXSharp.TIA2AX.Transformer
{
    public class TIA2AXTypeTransformer
    {
        private HashSet<string> knownTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            // IEC61131-3 primitive types
            "BOOL", "SINT", "INT", "DINT", "LINT", "USINT", "UINT", "UDINT", "ULINT",
            "REAL", "LREAL", "STRING", "BYTE", "WORD", 
            "DWORD", "LWORD", 
            "TIME", "DATE", "TIME_OF_DAY", "LTIME_OF_DAY","DATE_AND_TIME", "LDATE_AND_TIME","DTL"
        };

        public static string GetTransformation(string input, Options options)
        {
            var t = new TIA2AXTypeTransformer();
            return t.Tranform(input, options);
        }
        private string Tranform(string input, Options options)
        {
            string output = TransformTypes(input);
            output = ConvertDataBlocksToStructs(output);

            output = RemoveUnknownTypeDeclarations(output);
            output = $"NAMESPACE {options.Namespace}\n {output} \nEND_NAMESPACE";
            return output;
        }

        private string RemoveUnknownTypeDeclarations(string code)
        {
            // Normalize line endings and split the code into lines
            var lines = code.Replace("\r\n", "\n").Split('\n');
            var firstPassCode = new List<string>();

            bool allTypesFounded = false;
            // First pass to collect all type names
            for (int i = 0; i < lines.Length; i++)
            {
                var trimmedLine = lines[i].Trim();

                if (trimmedLine.Equals("TYPE", StringComparison.OrdinalIgnoreCase) )
                {
                    if ((i+3) > lines.Length) 
                    { 
                        allTypesFounded |= true;
                        break;
                    }

                    var dateType = lines[i+2].Trim().Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries)[0];

                    knownTypes.Add(dateType); // Collect type names
                }

            }


            // Second pass to remove unknown type declarations
            var cleanedCode = new List<string>();

            var insideTypeDeclaration = false;

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();

                // Detect the beginning of a type declaration
                if (trimmedLine.StartsWith("TYPE", StringComparison.OrdinalIgnoreCase))
                {
                    insideTypeDeclaration = true;
                    cleanedCode.Add(line); // Add the type declaration line
                    continue;
                }

                if (insideTypeDeclaration)
                {
                    // Detect the end of the type declaration
                    if (trimmedLine.Equals("END_TYPE", StringComparison.OrdinalIgnoreCase))
                    {
                        insideTypeDeclaration = false;
                        cleanedCode.Add(line); // Add the end type line
                    }
                    else if (trimmedLine.StartsWith("STRUCT", StringComparison.OrdinalIgnoreCase) ||
                             trimmedLine.StartsWith("END_STRUCT", StringComparison.OrdinalIgnoreCase))
                    {
                        cleanedCode.Add(line); // STRUCT declarations are part of type definition
                    }
                    else if (trimmedLine.EndsWith(";"))
                    {
                        // Handle variable declarations
                        var variableType = ExtractVariableType(trimmedLine);
                        if (variableType != null && knownTypes.Contains(variableType))
                        {
                            cleanedCode.Add(line); // Add the line if the type is known
                        }
                        // If the type is unknown, do not add the line to the cleaned code
                    }
                    else
                    {
                        // Add any lines that don't look like variable declarations (like comments)
                        cleanedCode.Add(line);
                    }
                }
                else
                {
                    cleanedCode.Add(line); // If we're not inside a type declaration, add all lines
                }
            }

            // Join the cleaned lines back into a single string
            return string.Join(Environment.NewLine, cleanedCode);
        }

        private string ExtractVariableType(string variableDeclaration)
        {
            // Split the declaration into parts and extract the type portion
            var parts = variableDeclaration.Split(new[] { ' ', ';', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);

            // Assuming "type" is always before "variableName;"
            // If there's an array, it might be "ARRAY[1..10] OF REAL MyVariable;"
            for (int i = 0; i < parts.Length; i++)
            {
                // If the current part is "OF", the actual type is the next part
                if (parts[i].Equals("OF", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 < parts.Length)
                    {
                        return parts[i + 1];
                    }

                    break;
                }

                // If the current part is a known primitive type or there's no "OF", the type is the current part
                if (knownTypes.Contains(parts[i]))
                {
                    return parts[i];
                }
            }

            // No identifiable type found
            return null;
        }

        public string TransformTypes(string input)
        {
            // Define a counter for the new types
            int typeCounter = 1;

            // Find nested structures and transform them into separate type definitions
            var nestedStructPattern = new Regex(@"(\w+)\s*:\s*Struct(.*?)END_STRUCT;", RegexOptions.Singleline);
            var match = nestedStructPattern.Match(input);

            while (match.Success)
            {
                string newTypeName = $"t{match.Groups[1].Value}_{typeCounter++}";
                // Add the END_TYPE at the end of the struct type definition
                string newTypeDefinition = $"TYPE {newTypeName} :\nSTRUCT{match.Groups[2].Value}END_STRUCT;\nEND_TYPE\n\n";

                input = input.Replace(match.Value, $"{match.Groups[1].Value} : {newTypeName};\n");
                input = newTypeDefinition + input;

                match = match.NextMatch();
            }

            // Remove quotation marks around type name and add a colon
            input = Regex.Replace(input, @"TYPE\s*""(\w+)""", "TYPE\n{S7.extern=ReadWrite}\n$1 :");

            // Remove the VERSION line
            input = Regex.Replace(input, @"\s*VERSION\s*:\s*\d+(\.\d+)?\s*\r?\n", "\n", RegexOptions.Multiline);

            // Remove quotation marks from variable names
            // This pattern looks for the quote marks around words and removes them.
            input = Regex.Replace(input, @"\""(.*?)\""", "$1");

            // replad LTD tia portal type to LDATE_AND_TIME of ax
            // https://console.simatic-ax.siemens.io/docs/axcode/xlad/lad-editor/data-types#ldate_and_time
            input = Regex.Replace(input, @"(:\s*)LDT(\s*;)", "$1LDATE_AND_TIME$2", RegexOptions.Multiline);

            // move pragmas to new line before..
            input = Regex.Replace(input, @"(.+){(.*?)}(.+)", "\t\t{$2}\n$1$3");

            //DTL - clear pragmas and 
            input = Regex.Replace(input, @"(?=\{[^}]*?InstructionName\s*:=\s*'DTL';).*?{(.*?;)?\s*(.*?;)?\s*([^;{}]+)}", m =>
            {
                if (m.Groups.Count == 4)
                {
                    if (m.Groups[2].Length > 1)
                    {
                        return "{" + m.Groups[3].Value.Trim() + "}";
                    }
                }
                return "";
            });

            return input;
        }

        public string ConvertDataBlocksToStructs(string fileContent)
        {
            // Match all occurrences of DATA_BLOCK
            MatchCollection matches = Regex.Matches(fileContent, @"(DATA_BLOCK\s+(\w+)\s*.*?END_DATA_BLOCK)", RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                // Extract the name of the data block and the actual block content
                if (!match.Success)
                {
                    throw new InvalidOperationException("Invalid DATA_BLOCK definition.");
                }
                string blockContent = match.Groups[1].Value;
                string name = match.Groups[2].Value;

                // Create pragma directive with the name of the data block
                string pragmaDirective = $"{{#ix-db: {name}}}\n{{S7.extern=ReadWrite}}\n";

                // Replace DATA_BLOCK and any modifiers with CLASS PUBLIC and the name, removing DATA_BLOCK modifiers
                string transformedBlock = Regex.Replace(blockContent, @"DATA_BLOCK\s+\w+", $"CLASS PUBLIC {name} ");

                // Replace VAR with VAR PUBLIC, ensuring VAR is a standalone word
                transformedBlock = Regex.Replace(transformedBlock, @"\bVAR\b", "VAR PUBLIC", RegexOptions.Multiline);

                // Remove BEGIN and END_DATA_BLOCK
                transformedBlock = Regex.Replace(transformedBlock, @"\s*BEGIN\s*|\s*END_DATA_BLOCK\s*", "");

                // Append END_CLASS at the end
                transformedBlock += "\nEND_CLASS\n";

                // Prepend pragma directive above the transformed data block
                transformedBlock = pragmaDirective + transformedBlock;

                // Perform a more complex replacement within CLASS...END_CLASS to remove RETAIN and NON_RETAIN
                transformedBlock = Regex.Replace(transformedBlock, @"(CLASS PUBLIC\s+(\w+)\s*(.*?)END_CLASS)", (Match m) =>
                {
                    // This will replace RETAIN and NON_RETAIN within each match of CLASS...END_CLASS
                    return Regex.Replace(m.Value, @"\b(RETAIN|NON_RETAIN)\b", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                }, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                // Replace the original data block in the input with the transformed block
                fileContent = fileContent.Replace(blockContent, transformedBlock);
            }

            return fileContent;
        }
    }
}
