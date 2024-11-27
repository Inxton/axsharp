// AXSharp.nuget.update.Tests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace AXSharp.nuget.update.Tests
{
    public class ixnugetupdatetest
    {
        public ixnugetupdatetest()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var executingAssemblyFileInfo
                = new FileInfo(Assembly.GetExecutingAssembly().FullName);
#pragma warning restore CS8604 // Possible null reference argument.

            TestFolder = executingAssemblyFileInfo.Directory!.FullName;
        }

        private string TestFolder { get; }

        [Fact]
        public void should_change_version_of_given_packagereference()
        {
            var testFile = Path.Combine(TestFolder, "samples\\IxTwin.csproj");
            var expected = Path.Combine(TestFolder, "samples\\expected\\IxTwin.csproj");

            Program.Main(new[]
            {
                "-p", testFile,
                "-i", "AXSharp.Abstractions", 
                "-v", "1.0.2"
            });

            Assert.True(FileCompare(testFile, expected));
        }

        [Fact]
        public void should_change_version_of_given_dotnet_tool()
        {
            var testFile = Path.Combine(TestFolder, "samples\\dotnet-tools.json");
            var expected = Path.Combine(TestFolder, "samples\\expected\\dotnet-tools.json");

            Program.Main(new[]
            {
                "-p", testFile,
                "-i", "AXSharp.ixc",
                "-v", "11.11.11-alpha.50"
            });

            Assert.True(FileCompare(testFile, expected));
        }

        private bool FileCompare(string file1, string file2)
        {
            var file1Lines = File.ReadAllLines(file1);
            var file2Lines = File.ReadAllLines(file2);

            if(file1Lines.Length != file2Lines.Length) return false;
            var lineNumber = 0;
            foreach (var line in file1Lines)
            {
                if (line.Trim() != file2Lines[lineNumber++].Trim()) 
                    return false;
            }

            return true;
        }
    }
    
    
}