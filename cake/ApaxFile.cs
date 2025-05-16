// Copyright (c) 2023 MTS spol. s r.o,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/AXOpen/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/AXOpen/blob/master/LICENSE
// Third party licenses: https://github.com/inxton/AXOpen/blob/master/notices.md

using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;


    public class ApaxFile
    {
        //
        // Summary:
        //     Gets or sets ax project name.
        public string? Name { get; set; }

        //
        // Summary:
        //     Ax project type.
        public string? Type { get; set; }

        //
        // Summary:
        //     Gets or sets the version of the AX project.
        public string? Version { get; set; }

        //
        // Summary:
        //     Gets or sets ax targets.
        public IEnumerable<string>? Targets { get; set; }

        //
        // Summary:
        //     Gets or sets ax project files to include in package.
        public IEnumerable<string>? Files { get; set; }

        //
        // Summary:
        //     Gets or sets ax projects development dependencies.
        public IDictionary<string, string>? DevDependencies { get; set; }

        //
        // Summary:
        //     Gets or sets ax projects dependencies.
        public IDictionary<string, string>? Dependencies { get; set; }


        public IDictionary<string, string>? Catalogs { get; set; }

        public string? Registries { get; set; }

        public string InstallStrategy { get; set; }

        public string ApaxVersion { get; set; }

        public Dictionary<string, string> Scripts { get; set; }

        //
        // Summary:
        //     Creates new instance of AXSharp.Compiler.Apax.
        //
        // Parameters:
        //   projectFile:
        //     Project file from which the ApaxFile object will be created.
        //
        // Exceptions:
        //   T:System.IO.FileNotFoundException:
        public static ApaxFile CreateApaxDto(string projectFile)
        {
            try
            {
                return new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).IgnoreUnmatchedProperties().Build()
                    .Deserialize<ApaxFile>(File.ReadAllText(projectFile));
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("'apax.yml' file was not found in the working directory. Make sure your current directory is simatic-ax project directory or provide source directory argument (for details see ixc --help)");
            }
        }

        public static void UpdateDependencyVersions(string apaxFile, string version)
        {
            try
            {
                var apax = CreateApaxDto(apaxFile);


                foreach (var dependency in apax.Dependencies)
                {
                   // dependency = new KeyValuePair<string, string>(dependency.Key, version);
                }


                using (var tw = new StreamWriter(apaxFile))
                {
                    var serializer = new SerializerBuilder()
                        .WithNamingConvention(CamelCaseNamingConvention.Instance)
                        .Build();

                    serializer.Serialize(tw, apax);
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException(
                    "'apax.yml' file was not found in the working directory. Make sure your current directory is simatic-ax project directory or provide source directory argument (for details see ixc --help)");
            }
        }
    }
