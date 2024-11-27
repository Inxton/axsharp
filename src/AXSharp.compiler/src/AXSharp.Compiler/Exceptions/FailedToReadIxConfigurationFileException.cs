// AXSharp.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Runtime.Serialization;

namespace AXSharp.Compiler.Exceptions;

/// <summary>
/// Provides information about failure to process AXSharp.config.json file.
/// </summary>
public class FailedToReadIxConfigurationFileException : Exception, ISerializable
{
    /// <summary>
    /// Create new instance of <see cref="FailedToReadIxConfigurationFileException"/>
    /// </summary>
    /// <param name="s">Exception details</param>
    /// <param name="exception">Inner exception</param>
    public FailedToReadIxConfigurationFileException(string s, Exception exception) : base(s, exception)
    {

    }
}