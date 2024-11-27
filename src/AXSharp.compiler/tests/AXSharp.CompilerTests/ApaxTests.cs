// AXSharp.CompilerTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Xunit;
using AXSharp.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AXSharp.CompilerTests
{
    public class ApaxTests
    {

        public ApaxTests()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var executingAssemblyFileInfo
                = new FileInfo(Assembly.GetExecutingAssembly().FullName);
#pragma warning restore CS8604 // Possible null reference argument.

            testFolder = executingAssemblyFileInfo.Directory!.FullName;
        }

        private readonly string testFolder;


        [Fact]
        public void should_load_and_parse_apax_workspace_file()
        {
           var apaxWorkspaceFile = Apax.CreateApaxDto(Path.Combine(testFolder, @"samples//plt//apax.yml"));

           Assert.Equal("plt", apaxWorkspaceFile.Name);
           Assert.Equal("workspace", apaxWorkspaceFile.Type);
           Assert.Equal("0.1.0", apaxWorkspaceFile.Version);
        }

        [Fact()]
        public void should_load_and_parse_apax_library_file()
        {
            var apaxWorkspaceFile = Apax.CreateApaxDto(Path.Combine(testFolder, @"samples//plt//lib2//apax.yml"));

            Assert.Equal("plt-lib2", apaxWorkspaceFile.Name);
            Assert.Equal("lib", apaxWorkspaceFile.Type);
            Assert.Equal("0.0.0", apaxWorkspaceFile.Version);

            Assert.Equal("1500,llvm,axunit-llvm", string.Join(",", apaxWorkspaceFile.Targets.Select(p => p)));
            Assert.Equal("bin", string.Join(",", apaxWorkspaceFile.Files.Select(p => p)));

            Assert.Equal("@ax/sdk : 3.0.8", string.Join(",", apaxWorkspaceFile.DevDependencies.Select(p => $"{p.Key} : {p.Value}")));
        }

        [Fact()]
        public void should_load_and_parse_apax_app_file()
        {
            var apaxWorkspaceFile = Apax.CreateApaxDto(Path.Combine(testFolder, @"samples//plt//app//apax.yml"));

            Assert.Equal("plt-app", apaxWorkspaceFile.Name);
            Assert.Equal("app", apaxWorkspaceFile.Type);
            Assert.Equal("0.1.0", apaxWorkspaceFile.Version);

            Assert.Equal("1500,axunit-llvm", string.Join(",", apaxWorkspaceFile.Targets.Select(p => p)));

            Assert.Equal("plt-lib : ^0.1.0,plt-lib2 : ^0.1.0", string.Join(",", apaxWorkspaceFile.Dependencies.Select(p => $"{p.Key} : {p.Value}")));

            Assert.Equal("@ax/sdk : 3.0.8", string.Join(",", apaxWorkspaceFile.DevDependencies.Select(p => $"{p.Key} : {p.Value}")));
        }

        [Fact()]
        public void should_update_apax_version()
        {
            var apaxWorkspaceFile = Apax.CreateApaxDto(Path.Combine(testFolder, @"samples//plt1//app//apax.yml"));
            Assert.Equal("plt-app", apaxWorkspaceFile.Name);
            Assert.Equal("app", apaxWorkspaceFile.Type);
            Assert.Equal("0.1.0", apaxWorkspaceFile.Version);

            Apax.UpdateVersion(Path.Combine(testFolder, @"samples//plt1//app//apax.yml"), "33.88.50");
            apaxWorkspaceFile = Apax.CreateApaxDto(Path.Combine(testFolder, @"samples//plt1//app//apax.yml"));

            Assert.Equal("plt-app", apaxWorkspaceFile.Name);
            Assert.Equal("app", apaxWorkspaceFile.Type);
            Assert.Equal("33.88.50", apaxWorkspaceFile.Version);

            Assert.Equal("1500,axunit-llvm", string.Join(",", apaxWorkspaceFile.Targets.Select(p => p)));

            Assert.Equal("plt-lib : ^0.1.0,plt-lib2 : ^0.1.0", string.Join(",", apaxWorkspaceFile.Dependencies.Select(p => $"{p.Key} : {p.Value}")));

            Assert.Equal("@ax/sdk : 3.0.8", string.Join(",", apaxWorkspaceFile.DevDependencies.Select(p => $"{p.Key} : {p.Value}")));
        }
    }
}