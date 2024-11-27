// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Globalization;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector.ValueValidation;

/// <summary>
///     Provides validation rule for <see cref="OnlinerString" /> type.
/// </summary>
public class StringValueValidationRule : OnlinerValidationRule<string>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StringValueValidationRule" /> class.
    /// </summary>
    /// <param name="onliner">
    ///     Onliner type.
    /// </param>
    public StringValueValidationRule(OnlinerString onliner) : base(onliner)
    {
    }

    /// <summary>
    ///     Validates value of <see cref="OnlinerString" />
    /// </summary>
    /// <param name="value">Value to validate.</param>
    /// <param name="culture">Culture.</param>
    /// <returns>Validation result.</returns>
    public override ValidationResult Validate(string value, CultureInfo culture)
    {
        if (value == null) return new ValidationResult(false, null);

        foreach (var character in value)
        {
            byte numCharValue = 0;

            try
            {
                numCharValue = Convert.ToByte(character);
            }
            catch (Exception)
            {
                ValidationErrorTip = string.Format("Non permitted character: {0}.", character);
                return new ValidationResult(false, ValidationErrorTip);
            }

            if (numCharValue > 254)
            {
                ValidationErrorTip = string.Format("Non permitted character: {0}.", character);
                return new ValidationResult(false, ValidationErrorTip);
            }
        }

        return new ValidationResult(true, null);
    }
}