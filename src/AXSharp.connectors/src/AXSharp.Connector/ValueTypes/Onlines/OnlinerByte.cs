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
///     Class providing access to the BYTE type online variable.
/// </summary>
public class OnlinerByte : OnlinerBase<byte>, IOnlineByte, IShadowByte
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerByte" /> class.
    /// </summary>
    public OnlinerByte()
    {
        validator = new ByteValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerByte" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerByte(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new ByteValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value of <see cref="OnlinerByte" />.
    /// </summary>
    public static byte MaxValue => byte.MaxValue;

    /// <summary>
    ///     Gets the min value of <see cref="OnlinerByte" />.
    /// </summary>
    public static byte MinValue => byte.MinValue;

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override byte InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : MaxValue;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override byte InstanceMinValue => AttributeMinSet ? AttributeMinimum : MinValue;
}