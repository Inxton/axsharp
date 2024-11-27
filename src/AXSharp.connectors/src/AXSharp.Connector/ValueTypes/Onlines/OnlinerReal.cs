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
///     Class providing access to the REAL type online variable.
/// </summary>
public class OnlinerReal : OnlinerBase<float>, IOnlineReal, IShadowReal
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerReal" /> class.
    /// </summary>
    public OnlinerReal()
    {
        validator = new RealValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerReal" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerReal(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new RealValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value of <see cref="OnlinerReal" />.
    /// </summary>
    public static float MaxValue => 3.402823466e+38f;

    /// <summary>
    ///     Gets the min value of <see cref="OnlinerReal" />.
    /// </summary>
    public static float MinValue => -3.402823466e+38f;

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override float InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : MaxValue;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override float InstanceMinValue => AttributeMinSet ? AttributeMinimum : MinValue;
}