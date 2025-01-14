// AXSharp.Compiler.Cs
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

namespace AXSharp.Compiler.Cs.Exceptions;

/// <summary>
///     Failure to retrieve package information from csproj file.
/// </summary>
[Serializable]
public class FailedToRetrievePackageReferenceException : Exception
{
    /// <summary>
    ///     Creates new instance of <see cref="FailedGettingPackageReferenceException" />
    /// </summary>
    /// <param name="s">Message</param>
    /// <param name="exception">Inner exception</param>
    public FailedToRetrievePackageReferenceException(string s, Exception exception) : base(s, exception)
    {
    }
}