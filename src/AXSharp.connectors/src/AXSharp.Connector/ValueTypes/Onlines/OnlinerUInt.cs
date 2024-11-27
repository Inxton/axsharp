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
///     Class providing access to the UINT type online variable.
/// </summary>
public class OnlinerUInt : OnlinerBase<ushort>, IOnlineUInt, IShadowUInt
{
    /// <summary>
    ///     Gets the min value of <see cref="OnlinerUInt" />.
    /// </summary>
    public static readonly ushort MinValue = ushort.MinValue;

    /// <summary>
    ///     Gets the max value of <see cref="OnlinerUInt" />.
    /// </summary>
    public static readonly ushort MaxValue = ushort.MaxValue;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerUInt" /> class.
    /// </summary>
    public OnlinerUInt()
    {
        validator = new UIntValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerUInt" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerUInt(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new UIntValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override ushort InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : MaxValue;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override ushort InstanceMinValue => AttributeMinSet ? AttributeMinimum : MinValue;
}