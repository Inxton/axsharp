// AXSharp.Compiler.Cs
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

namespace AXSharp.Compiler.Cs.Exceptions;

#pragma warning disable CS1591
[Serializable]
public class FailedGettingPackageReferenceException : Exception
{
    public FailedGettingPackageReferenceException(string s, Exception exception)
        : base(s, exception)
    {
    }
#pragma warning restore CS1591
}