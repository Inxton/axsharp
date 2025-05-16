// Build
// Copyright (c) 2023 MTS spol. s r.o,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/AXOpen/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/AXOpen/blob/master/LICENSE
// Third party licenses: https://github.com/inxton/AXOpen/blob/master/notices.md

using System;
using System.IO;
using System.Text;
using Cake.Core.IO;
using Path = System.IO.Path;

public static class ApaxCmd
{
    public static void UpdateApaxVersion(this BuildContext context, string file, string version)
    {
        var sb = new StringBuilder();
        foreach (var line in System.IO.File.ReadLines(file))
        {
            var newLine = line;
          
            if (line.Trim().StartsWith("version"))
            {
                var semicPosition = line.IndexOf(":");
                var lenght = line.Length - semicPosition;

                newLine = $"{line.Substring(0, semicPosition)} : '{version}'";
            }

            sb.AppendLine(newLine);
        }

        System.IO.File.WriteAllText(file, sb.ToString());
    }

    public static void ApaxPack(this BuildContext context, string apaxFolder)
    {
        {
            context.ProcessRunner.Start(Helpers.GetApaxCommand(), new ProcessSettings()
            {
                Arguments = $"pack --key={context.ApaxSignKey}",
                WorkingDirectory = apaxFolder,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                Silent = false
            }).WaitForExit();
        }
    }

    public static void ApaxCopyArtifacts(this BuildContext context, string folder, string name)
    {
            var packageFile = $"{context.ApaxRegistry}-{name}-{GitVersionInformation.SemVer}.apax.tgz";
            var sourceFile = Path.Combine(folder, packageFile);
            File.Copy(sourceFile, Path.Combine(context.ArtifactsApax, packageFile));
    }

    public static void ApaxPublishAllArtefacts(this BuildContext context)
    {
        context.ProcessRunner.Start(Helpers.GetApaxCommand(), new ProcessSettings()
        {
            Arguments =
                $"login --registry https://npm.pkg.github.com --username {context.GitHubUser} --password {context.GitHubToken}",
            WorkingDirectory = context.ArtifactsApax,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            Silent = false
        }).WaitForExit();

        foreach (var apaxPackageFile in Directory.EnumerateFiles(context.ArtifactsApax))
        {
            var process = context.ProcessRunner.Start(Helpers.GetApaxCommand(), new ProcessSettings()
            {
                Arguments = $"publish -p {apaxPackageFile} -r  https://npm.pkg.github.com",
                WorkingDirectory = context.ArtifactsApax,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                Silent = false
            });

            process.WaitForExit();

            if (process.GetExitCode() != 0)
            {
                throw new PublishFailedException();
            }
        }
    }
}

public class PublishFailedException : Exception
{
}