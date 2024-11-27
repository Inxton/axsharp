// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector.ValueTypes.Online;
using AXSharp.Connector.ValueTypes.Shadows;
using AXSharp.Connector.ValueValidation;

namespace AXSharp.Connector.ValueTypes;

/// <summary>
///     Class providing access to the ULINT type online variable.
/// </summary>
public class OnlinerULInt : OnlinerBase<ulong>, IOnlineULInt, IShadowULInt
{
    /// <summary>
    ///     Gets the min value of <see cref="OnlinerULInt" />.
    /// </summary>
    public static readonly ulong MinValue = ulong.MinValue;

    /// <summary>
    ///     Gets the max value of <see cref="OnlinerULInt" />.
    /// </summary>
    public static readonly ulong MaxValue = ulong.MaxValue;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerULInt" /> class.
    /// </summary>
    public OnlinerULInt()
    {
        validator = new ULIntValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerULInt" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerULInt(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new ULIntValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override ulong InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : MaxValue;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override ulong InstanceMinValue => AttributeMinSet ? AttributeMinimum : MinValue;
}