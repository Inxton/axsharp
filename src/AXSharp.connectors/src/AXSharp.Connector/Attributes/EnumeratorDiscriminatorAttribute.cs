// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;

namespace AXSharp.Connector;

/// <summary>
///     Attribute provides information about the member being treated as enum.
/// </summary>
/// <note type="note">
///     This attribute is used emitted in connector building process. It should not be declared by the
///     framework consumers.
/// </note>
public class EnumeratorDiscriminatorAttribute : Attribute
{
    /// <summary>
    ///     Creates new instance of <see cref="EnumeratorDiscriminatorAttribute" />
    /// </summary>
    /// <param name="enumeratorType">Enumerator type</param>
    public EnumeratorDiscriminatorAttribute(Type enumeratorType)
    {
        EnumeratorType = enumeratorType ?? throw new ArgumentNullException(nameof(enumeratorType));
    }

    /// <summary>
    ///     Get the enumerator type.
    /// </summary>
    public Type EnumeratorType { get; }
}