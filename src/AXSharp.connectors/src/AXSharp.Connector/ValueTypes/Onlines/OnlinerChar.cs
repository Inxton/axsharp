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
///     Class providing access to the CHAR type online variable.
/// </summary>
public class OnlinerChar : OnlinerBase<char>, IOnlineChar, IShadowChar
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerByte" /> class.
    /// </summary>
    public OnlinerChar()
    {
        validator = new CharValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerByte" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerChar(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new CharValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value of <see cref="OnlinerChar" />.
    /// </summary>
    public static char MaxValue => (char)0x7F; //(char)0xFF; //TODO: Range should be 0xFF but WebApi refuses to write

    /// <summary>
    ///     Gets the min value of <see cref="OnlinerChar" />.
    /// </summary>
    public static char MinValue => (char)0x20; //(char)0x00; //TODO: Range should be 0x00 but WebApi refuses to write

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override char InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : MaxValue;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override char InstanceMinValue => AttributeMinSet ? AttributeMinimum : MinValue;
}