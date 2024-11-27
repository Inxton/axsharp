// AXSharp.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Runtime.Serialization;

namespace AXSharp.Compiler.Core;

/// <summary>
///     Provides exception details when while crossing syntax tree no matching type is found in the semantic tree.
/// </summary>
[Serializable]
public class TypeNotFoundInSemanticTreeException : Exception
{
    /// <summary>
    ///     Creates new instance of <see cref="TypeNotFoundInSemanticTreeException" />
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public TypeNotFoundInSemanticTreeException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TypeNotFoundInSemanticTreeException" /> class with serialized
    ///     data.
    /// </summary>
    /// <param name="info">
    ///     The <see cref="SerializationInfo" /> that holds the serialized object
    ///     data about the exception being thrown.
    /// </param>
    /// <param name="context">
    ///     The <see cref="StreamingContext" /> that contains contextual
    ///     information about the source or destination.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="info" /> is <see langword="null" />.
    /// </exception>
    /// <exception cref="SerializationException">
    ///     The class name is <see langword="null" /> or
    ///     <see cref="Exception.HResult" /> is zero (0).
    /// </exception>
    // ReSharper disable once UnusedMember.Global
    protected TypeNotFoundInSemanticTreeException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TypeNotFoundInSemanticTreeException" /> class with a specified error
    ///     message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public TypeNotFoundInSemanticTreeException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TypeNotFoundInSemanticTreeException" /> class with a specified error
    ///     message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">
    ///     The exception that is the cause of the current exception, or a null reference (
    ///     <see langword="Nothing" /> in Visual Basic) if no inner exception is specified.
    /// </param>
    // ReSharper disable once UnusedMember.Global
    public TypeNotFoundInSemanticTreeException(string message, Exception innerException) : base(message, innerException)
    {
    }
}