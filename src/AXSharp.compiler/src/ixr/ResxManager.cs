using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources.NetStandard;

namespace AXSharp.ixr_doc
{
    /// <summary>
    /// Manager for creating and generating resx file
    /// </summary>
    public static class ResxManager
    {
        /// <summary>
        /// Add resources from dictionary to resx file
        /// </summary>
        /// <param name="outputDirectory">Path to resx file</param>
        /// <param name="dictionary">Dictionary with resources</param>
        public static void AddResourcesFromDictionary(string outputDirectory, string outputFileName, Dictionary<string, StringValueWrapper> dictionary)
        {
            var outResxFile = !string.IsNullOrEmpty(outputDirectory) ? EnsureOutputFile(outputDirectory, outputFileName) : EnsureOutputFile(outputFileName);
            
            using (ResXResourceWriter resx = new ResXResourceWriter(outResxFile))
            {
                foreach (KeyValuePair<string, StringValueWrapper> kvp in dictionary)
                {
                    resx.AddResource(new ResXDataNode(kvp.Key, kvp.Value.RawValue) { Comment = $"{kvp.Value.FileName},{kvp.Value.Line}" });
                }
            }
        }

        //Create new file, if file doesnt exist
        private static string EnsureOutputFile(string outputDirectory, string fileName)
        {
            var outputFileName = Path.Combine(outputDirectory, fileName);

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            if (!File.Exists(outputFileName))
            {
                File.Create(outputFileName).Close();
            }

            return outputFileName;
        }

        private static string EnsureOutputFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Close();
            }

            return fileName;
        }
    }
}
