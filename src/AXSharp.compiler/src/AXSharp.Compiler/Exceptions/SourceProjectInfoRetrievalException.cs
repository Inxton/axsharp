// AXSharp.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Compiler.Exceptions;

/// <summary>
/// Provides information about and exception while retrieving information about
/// references apax project.
/// </summary>
public class SourceProjectInfoRetrievalException : Exception
{
    /// <inheritdoc />
    public SourceProjectInfoRetrievalException(string message, Exception exception) : base(message, exception)
    {

    }
}