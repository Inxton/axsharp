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
///     Class providing access to the DATE_AND_TIME (DT) type online variable.
/// </summary>
public class OnlinerDateTime : OnlinerBase<DateTime>, IOnlineDateTime, IShadowDateTime
{
    /// <summary>
    ///     Gets the max value of <see cref="OnlinerDateTime" />.
    /// </summary>
    public static readonly DateTime MaxValue = new(2262, 4, 11, 23, 47, 16, 854); // TODO: Do we remain in ms range?

    /// <summary>
    ///     Gets the min value of <see cref="OnlinerDateTime" />.
    /// </summary>
    public static readonly DateTime MinValue = new(1970, 01, 01, 0, 0, 0, 0);


    /// <summary>
    ///     Gets the max value of <see cref="OnlinerDateTime" /> for TIA project.
    /// </summary>
    public static readonly DateTime MaxValueTIA = new(2089, 12, 31, 23, 59, 59, 999);

    /// <summary>
    ///     Gets the min value of <see cref="OnlinerDateTime" /> for TIA project.
    /// </summary>
    public static readonly DateTime MinValueTIA = new(1990, 01, 01, 0, 0, 0, 0);


    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerDateTime" /> class.
    /// </summary>
    public OnlinerDateTime()
    {
        validator = new DateTimeValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerDateTime" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerDateTime(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new DateTimeValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override DateTime InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : MaxValue;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override DateTime InstanceMinValue => AttributeMinSet ? AttributeMinimum : MinValue;
}