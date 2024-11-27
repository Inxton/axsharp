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
///     Class providing access to the WCHAR type online variable.
/// </summary>
public class OnlinerWChar : OnlinerBase<char>, IOnlineWChar, IShadowWChar
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerByte" /> class.
    /// </summary>
    public OnlinerWChar()
    {
        validator = new WCharValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerByte" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerWChar(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new WCharValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value of <see cref="OnlinerByte" />.
    /// </summary>
    public static char MaxValue =>(char)0xD7FE;//char.MaxValue; //TODO: Should be 0xFFFF, webapi does not accept

    /// <summary>
    ///     Gets the min value of <see cref="OnlinerByte" />.
    /// </summary>
    public static char MinValue => (char)0x0020; //char.MinValue; //TODO: Should be 0x0000, webapi does not accept

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override char InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : MaxValue;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override char InstanceMinValue => AttributeMinSet ? AttributeMinimum : MinValue;
}