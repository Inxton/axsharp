// Build
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Cake.Common.Tools.DotNet;
using CommandLine;

public class BuildParameters
{
    [Option('t', "do-test", Required = false, Default = false, HelpText = "Runs tests")]
    public bool DoTest { get; set; }

    [Option('d', "do-docs", Required = false, Default = false, HelpText = "Generates documentation")]
    public bool DoDocs { get; set; }

    [Option('k', "do-pack", Required = false, Default = false, HelpText = "Creates packages")]
    public bool DoPack { get; set; }

    [Option('p', "do-publish", Required = false, Default = false, HelpText = "Publishes packages")]
    public bool DoPublish { get; set; }

    [Option('c', "configuration", Required = false, Default = "Release", HelpText = "Configuration")]
    public string Configuration { get; set; }

    [Option('v', "verbosity", Required = false, Default = DotNetVerbosity.Minimal, HelpText = "Verbosity (default Minimal)")]
    public DotNetVerbosity Verbosity { get; set; }

    [Option('l', "test-level", Required = false, Default = 1, HelpText = "Test level 1 - 3")]
    public int TestLevel { get; set; }

    [Option('r', "do-publish-release", Required = false, Default = false, HelpText = "Publishes release on GH")]
    public bool DoPublishRelease { get; set; }
}