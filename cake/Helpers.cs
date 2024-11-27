// Build
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Polly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

internal class Helpers
{
    public static readonly IEnumerable<string> PublishInternal = new List<string>() { "dev", "main", "master", "release" };
    public static readonly IEnumerable<string> PublishExternal = new List<string>() { "main", "master", "release" };

    public static bool CanReleaseInternal()
    {
        return PublishInternal.Any(predicate =>
            predicate == GitVersionInformation.BranchName ||
            GitVersionInformation.BranchName.StartsWith("releases/")
            || GitVersionInformation.BranchName.StartsWith("alfa/"));
    }

    public static bool CanReleasePublic()
    {
        return PublishExternal.Any(predicate =>
            predicate == GitVersionInformation.BranchName ||
            GitVersionInformation.BranchName.StartsWith("releases/"));
    }

    public static string GetStcNameByPlatform() 
        => RuntimeInformation.IsOSPlatform(OSPlatform.Linux)  ? "stc-linux-x64" : "stc-win-x64";

    public static string GetApaxCommand()
        => RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "apax" : "apax.cmd";
    public static void CopyApaxPackages(string sourceDir, string destinationDir, string rootDir)
    {

        // Get information about the source directory
        var dir = new DirectoryInfo(sourceDir);

        // Check if the source directory exists
        if (!dir.Exists)
            throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

        // Cache directories before we start copying
        DirectoryInfo[] dirs = dir.GetDirectories();

        // Create the destination directory
        Directory.CreateDirectory(destinationDir);

        // Get the files in the source directory and copy to the destination directory
        foreach (FileInfo file in dir.GetFiles())
        {
            string targetFilePath = Path.Combine(destinationDir, file.Name);
            file.CopyTo(targetFilePath, true);
        }

        // Copy content of sub directories
        foreach (DirectoryInfo subDir in dirs)
        {
            string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
            CopyApaxPackages(subDir.FullName, newDestinationDir, rootDir);
        }

        // Remove main .apax folder
        var apaxPath = Path.GetFullPath(Path.Combine(rootDir, "apax//.apax"));
        var apaxDir = new DirectoryInfo(apaxPath);
        // Check if the source directory exists and delete it
        if (apaxDir.Exists)
        {
            try
            {
                Directory.Delete(apaxPath, true);
            }
            catch (UnauthorizedAccessException)
            {
                //swallow
            }
        }

    }
}
