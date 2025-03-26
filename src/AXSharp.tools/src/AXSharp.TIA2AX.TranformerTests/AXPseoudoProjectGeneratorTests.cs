using Xunit;
using AXSharp.TIA2AX.Transformer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AXSharp.TIA2AX.Transformer.Tests
{
    public class AXPseoudoProjectGeneratorTests
    {

      
        [Fact()]
        public void CreateTest()
        {
            var assemblyLocation = new FileInfo(Assembly.GetExecutingAssembly().Location);
            AXPseoudoProjectGenerator.Create(assemblyLocation.DirectoryName,
               "output",
               new string[] { Path.Combine(assemblyLocation.DirectoryName, "samples", "ExportViacDbBezInstancneho.db") }, new Options() { Namespace = "nmspc" });

            var expectedConfiguration = File.ReadAllText(Path.Combine(assemblyLocation.DirectoryName, "Expected", "configuration.st"));
            var actualConfiguration = File.ReadAllText(Path.Combine(assemblyLocation.DirectoryName, "output", "src", "configuration.st"));

            Assert.Equal(expectedConfiguration, actualConfiguration);
           
            var expectedTypes = File.ReadAllText(Path.Combine(assemblyLocation.DirectoryName, "Expected", "ExportViacDbBezInstancneho.st"));
            var actualTypes = File.ReadAllText(Path.Combine(assemblyLocation.DirectoryName, "output", "src", "ExportViacDbBezInstancneho.db.st"));

            Assert.Equal(expectedTypes.Split('\n').Length, actualTypes.Split('\n').Length);

            var exp = expectedTypes.Split('\n').Select(p => p.Trim()).ToArray();
            var act = actualTypes.Split('\n').Select(p => p.Trim()).ToArray();
            for (int i = 0; i < exp.Length; i++)
            {
                Assert.Equal(exp[i], act[i]);
            }
        }

        [Fact()]
        public void RunToolWithArguments()
        {
            var assemblyLocation = new FileInfo(Assembly.GetExecutingAssembly().Location);

            // Define the path to the executable file.
            string exePath = Path.Combine(assemblyLocation.DirectoryName, "AXSharp.TIA2AX.Transformer.exe");

            // Define the arguments to pass.
            // If the arguments might contain spaces, wrap them in quotes.
            string arguments = $"-o {assemblyLocation.DirectoryName} -d {Path.Combine(assemblyLocation.DirectoryName, "samples", "ExportViacDbBezInstancneho.db")} -n nmspc";

            try
            {
                // Set up the process start information.
                ProcessStartInfo startInfo = new ProcessStartInfo(exePath)
                {
                    Arguments = arguments, // Pass the arguments.
                    UseShellExecute = false, // Don't use the shell to start the process.
                    CreateNoWindow = true, // Don't create a new window.
                    RedirectStandardOutput = true, // Redirect standard output (if needed).
                    RedirectStandardError = true // Redirect standard error (if needed).
                };

                // Start the process with the info specified.
                using (Process process = Process.Start(startInfo))
                {
                    // Read the output (if you want to do something with it).
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    // Wait for the process to finish.
                    process.WaitForExit();

                    // Write the output to the console.
                    Console.WriteLine("Output:");
                    Console.WriteLine(output);

                    // Write any errors to the console.
                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine("Error:");
                        Console.WriteLine(error);
                    }

                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                // An exception occurred, write the details.
                Console.WriteLine("An error occurred:");
                Console.WriteLine(ex.Message);
                throw ex;
            }

            var expectedConfiguration = File.ReadAllText(Path.Combine(assemblyLocation.DirectoryName, "Expected", "configuration.st"));

            var actualConfiguration = File.ReadAllText(Path.Combine(assemblyLocation.DirectoryName, "PseudoAX", "src", "configuration.st"));

            Assert.Equal(expectedConfiguration, actualConfiguration);

            var expectedTypes = File.ReadAllText(Path.Combine(assemblyLocation.DirectoryName, "Expected", "ExportViacDbBezInstancneho.st"));

            var actualTypes = File.ReadAllText(Path.Combine(assemblyLocation.DirectoryName, "PseudoAX", "src", "ExportViacDbBezInstancneho.db.st"));

            Assert.Equal(expectedTypes.Split('\n').Length, actualTypes.Split('\n').Length);

            var exp = expectedTypes.Split('\n').Select(p => p.Trim()).ToArray();
            var act = actualTypes.Split('\n').Select(p => p.Trim()).ToArray();
            for (int i = 0; i < exp.Length; i++)
            {
                Assert.Equal(exp[i], act[i]);
            }
        }
    }
}