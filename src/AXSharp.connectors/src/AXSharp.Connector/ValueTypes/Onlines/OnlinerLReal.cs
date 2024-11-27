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
///     Class providing access to the LREAL type online variable.
/// </summary>
public class OnlinerLReal : OnlinerBase<double>, IOnlineLReal, IShadowLReal
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerLReal" /> class.
    /// </summary>
    public OnlinerLReal()
    {
        validator = new LRealValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerLReal" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerLReal(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new LRealValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value of <see cref="OnlinerLReal" />.
    /// </summary>
    public static double MaxValue => 1.7976931348623158E+308d;


    /// <summary>
    ///     Gets the min value of <see cref="OnlinerLReal" />.
    /// </summary>
    public static double MinValue => -1.7976931348623158E+308d;

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override double InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : MaxValue;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override double InstanceMinValue => AttributeMinSet ? AttributeMinimum : MinValue;
}