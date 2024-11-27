// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Connector.ValueTypes.Shadows;

/// <summary>
///     Implementation contract for base types tags to access shadow values.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IShadow<T>
{
    /// <summary>
    ///     Gets symbol of this tag.
    /// </summary>
    string Symbol { get; }

    /// <summary>
    ///     Gets or sets name of this tag.
    /// </summary>
    string AttributeName { get; set; }

    /// <summary>
    ///     Gets or sets units of this tag.
    /// </summary>
    string AttributeUnits { get; set; }

    /// <summary>
    ///     Gets or sets shadow value of this tag.
    /// </summary>
    T Shadow { get; set; }

    /// <summary>
    ///     Gets or set shadow value of this tag.
    /// </summary>
    T Value { get; set; }

    /// <summary>
    ///     Raises when <see cref="Shadow" /> value changes.
    /// </summary>
    event ValueChangedEventHandlerDelegate ValueChanged;
}