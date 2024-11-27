// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Connector.ValueTypes.Online;

/// <summary>
///     Implementation contract for base types tags to access online values from the PLC.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IOnline<T>
{
    /// <summary>
    ///     Get symbol of this tag.
    /// </summary>
    string Symbol { get; }

    /// <summary>
    ///     Gets name of this tag.
    /// </summary>
    string AttributeName { get; set; }

    /// <summary>
    ///     Get units for this tag.
    /// </summary>
    string AttributeUnits { get; set; }

    /// <summary>
    ///     Gets or sets cyclically accessed value of this tag.
    /// </summary>
    T Cyclic { get; set; }

    /// <summary>
    ///     Get or set the value that will be written to the plc in the next cyclical R/W loop.
    /// </summary>
    T Edit { get; set; }

    /// <summary>
    ///     Gets <see cref="Cyclic" /> value in when accessing via IOnline interface.
    /// </summary>
    T Value { get; set; }

    /// <summary>
    ///     Gets the last value retrieved from cyclical or batched reading. Without requesting cyclical read operation on this
    ///     onliner.
    /// </summary>
    T LastValue { get; }

    /// <summary>
    ///     Raises when <see cref="Cyclic" /> value changes. Provided that the value is set for cyclical reading.
    /// </summary>
    event ValueChangedEventHandlerDelegate ValueChanged;
}