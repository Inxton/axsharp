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
///     Class providing access to the BOOL type online variable.
/// </summary>
public class OnlinerBool : OnlinerBase<bool>, IOnlineBool, IShadowBool
{
    /// <summary>
    ///     Creates new instance of <see cref="OnlinerBool" /> class.
    /// </summary>
    public OnlinerBool()
    {
        validator = new BoolValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerBool" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerBool(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new BoolValueValidationRule(this);
    }

    /// <summary>
    ///     Used to satisfy <see cref="OnlinerBase" /> requirements. In case of bool value <see cref="InstanceMinValue" /> does
    ///     not have effect.
    /// </summary>
    public override bool InstanceMinValue => true;

    /// <summary>
    ///     Used to satisfy <see cref="OnlinerBase" /> requirements. In case of bool value <see cref="InstanceMaxValue" /> does
    ///     not have effect.
    /// </summary>
    public override bool InstanceMaxValue => true;
}