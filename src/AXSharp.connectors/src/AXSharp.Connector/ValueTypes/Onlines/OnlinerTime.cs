// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using AXSharp.Connector.ValueTypes.Online;
using AXSharp.Connector.ValueTypes.Shadows;
using AXSharp.Connector.ValueValidation;

namespace AXSharp.Connector.ValueTypes;

/// <summary>
///     Class providing access to the TIME type online variable.
/// </summary>
public class OnlinerTime : OnlinerBase<TimeSpan>, IOnlineTime, IShadowTime
{
    /// <summary>
    ///     Gets the max value of <see cref="OnlinerTime" />.
    /// </summary>
    public static readonly TimeSpan MaxValue = TimeSpan.FromTicks(92233720368540000);

    /// <summary>
    ///     Gets the min value of <see cref="OnlinerTime" />.
    /// </summary>
    public static readonly TimeSpan MinValue = TimeSpan.FromTicks(-92233720368540000);

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerTime" /> class.
    /// </summary>
    public OnlinerTime()
    {
        validator = new TimeValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerTime" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerTime(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new TimeValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override TimeSpan InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : MaxValue;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override TimeSpan InstanceMinValue => AttributeMinSet ? AttributeMinimum : MinValue;
}