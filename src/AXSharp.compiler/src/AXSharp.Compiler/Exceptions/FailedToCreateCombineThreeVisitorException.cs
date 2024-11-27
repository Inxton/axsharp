// AXSharp.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Compiler.Exceptions;

#pragma warning disable CS1591
[Serializable]
public class FailedToCreateCombineThreeVisitorException : Exception
{
    public FailedToCreateCombineThreeVisitorException(string s) : base(s)
    {
    }
}
#pragma warning restore CS1591