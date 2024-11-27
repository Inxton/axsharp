// AXSharp.ixc.Tests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Reflection;
using AXSharp.Compiler;

namespace AXSharp.ixcTests
{
    public class CliProgramTest
    {
        // For to me unknown reason when running the tests separately try-catch-finally is not needed. When running multiple tests from this class
        // the new instance is created and the 'TestFolder' takes Environment.CurrentDirectory, from the previous test.
        // try-catch-finally you find in the tests is the workaround, we recover current dir at the end of the test.

        public CliProgramTest()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var executingAssemblyFileInfo
                = new FileInfo(Assembly.GetExecutingAssembly().FullName);
#pragma warning restore CS8604 // Possible null reference argument.

            TestFolder = executingAssemblyFileInfo.Directory!.FullName;
        }

        private string TestFolder { get; }

        [Fact]
        public void should_run_with_default_settings()
        {
            var axProjectFolder = Path.Combine(TestFolder, "samples","plt","lib2");
            var outputDirectory = Path.Combine(axProjectFolder, "ix");
            if (Directory.Exists(outputDirectory))
            {
                Directory.Delete(outputDirectory, true);
            }

            var recoverDirectory = Environment.CurrentDirectory;
            Environment.CurrentDirectory = axProjectFolder;

            try
            {
                ixc.Program.Main(new string[0]);

                Assert.True(Directory.Exists(outputDirectory));
                Assert.Equal(6, Directory.EnumerateFiles(outputDirectory, "*.*", SearchOption.AllDirectories).Count());
            }
            catch
            {
                throw;
            }
            finally
            {
                Environment.CurrentDirectory = recoverDirectory;
            }
        }
        
        [Fact]
        public void should_run_with_setting_retrieved_from_config_file_settings()
        {
            var axProjectFolder = Path.Combine(TestFolder, "samples","plt","app");
            var config = AXSharpConfig.UpdateAndGetAXSharpConfig(axProjectFolder);
            var outputDirectory = Path.GetFullPath(Path.Combine(axProjectFolder, config.OutputProjectFolder));
            
            if (Directory.Exists(outputDirectory))
            {
                Directory.Delete(outputDirectory, true);
            }

            var recoverDirectory = Environment.CurrentDirectory;
            Environment.CurrentDirectory = axProjectFolder;
            
            try
            {
                ixc.Program.Main(new string[0]);

                Assert.True(Directory.Exists(outputDirectory));

                Assert.Equal(8, Directory.EnumerateFiles(outputDirectory, "*.*", SearchOption.AllDirectories).Count());
            }
            catch
            {
                throw;
            }
            finally
            {
                Environment.CurrentDirectory = recoverDirectory;
            }
            
        }
        
        [Fact]
        public void should_run_with_setting_retrieved_from_config_file_settings_but_override_from_cli()
        {
            var axProjectFolder = Path.Combine(TestFolder, "samples","plt","lib");
            var config = AXSharpConfig.UpdateAndGetAXSharpConfig(axProjectFolder);
            var outputDirectory = Path.GetFullPath(Path.Combine(axProjectFolder, $"..{Path.DirectorySeparatorChar}ix-lib-override"));
            
            if (Directory.Exists(outputDirectory))
            {
                Directory.Delete(outputDirectory, true);
            }

            var recoverDirectory = Environment.CurrentDirectory;
            Environment.CurrentDirectory = axProjectFolder;
            
            try
            {
                ixc.Program.Main(new string[] {"-o", Path.Combine($"..{Path.DirectorySeparatorChar}ix-lib-override")});

                Assert.True(Directory.Exists(outputDirectory));

                Assert.Equal(6, Directory.EnumerateFiles(outputDirectory, "*.*", SearchOption.AllDirectories).Count());
            }
            catch
            {
                throw;
            }
            finally
            {
                Environment.CurrentDirectory = recoverDirectory;
            }
            
        }

        [Fact]
        public void should_run_x_absolute_path_argument()
        {
            var axProjectFolder = Path.Combine(TestFolder, "samples","plt","app");
            ixc.Program.Main(new string[] {"-x", axProjectFolder });
        }

        [Fact]
        public void should_run_x_relative_path_argument()
        {
            var axProjectFolder = Path.Combine("samples","plt","app");
            ixc.Program.Main(new string[] { "-x", axProjectFolder });
        }
    }
    
    
}