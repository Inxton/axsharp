// AXSharp.CompilerTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.IO;

namespace AXSharp.CompilerTests.SourceOrigin;

/// <summary>
///     Creates a throwaway on-disk AX project (apax.yml + src/ with one .st file) under the temp
///     folder. Disposing removes the whole tree. Used by source-origin provider tests that need a
///     real <see cref="AXSharp.Compiler.AxProject" /> without polluting the repo with fixtures.
/// </summary>
internal sealed class TempAxProjectFixture : IDisposable
{
    public TempAxProjectFixture(string apaxYml)
    {
        Root = Path.Combine(Path.GetTempPath(), "axsharp-origin-tests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(Path.Combine(Root, "src"));
        File.WriteAllText(Path.Combine(Root, "apax.yml"), apaxYml);
        File.WriteAllText(Path.Combine(Root, "src", "dummy.st"), "CLASS dummy END_CLASS");
    }

    public string Root { get; }

    public void Dispose()
    {
        try
        {
            if (Directory.Exists(Root))
                Directory.Delete(Root, true);
        }
        catch
        {
            // best-effort cleanup
        }
    }

    public static string NormalizeDir(string path) =>
        Path.TrimEndingDirectorySeparator(Path.GetFullPath(path));
}
