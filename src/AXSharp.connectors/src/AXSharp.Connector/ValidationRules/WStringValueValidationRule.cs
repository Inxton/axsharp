// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Globalization;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector.ValueValidation;

/// <summary>
///     Provides validation rule for <see cref="OnlinerWString" /> type.
/// </summary>
public class WStringValueValidationRule : OnlinerValidationRule<string>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="WStringValueValidationRule" /> class.
    /// </summary>
    /// <param name="onliner">
    ///     Onliner type.
    /// </param>
    public WStringValueValidationRule(OnlinerWString onliner) : base(onliner)
    {
    }

    /// <summary>
    ///     Validates value of <see cref="OnlinerWString" />
    /// </summary>
    /// <param name="value">Value to validate.</param>
    /// <param name="culture">Culture.</param>
    /// <returns>Validation result.</returns>
    public override ValidationResult Validate(string value, CultureInfo culture)
    {
        return new ValidationResult(true, null);
    }
}