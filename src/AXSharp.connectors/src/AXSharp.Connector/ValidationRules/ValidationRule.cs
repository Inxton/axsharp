// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Connector.ValueValidation;

/// <summary>
///     Provides return value of a validation process.
/// </summary>
public class ValidationResult
{
    /// <summary>
    ///     Creates new instance of <see cref="ValidationResult" />
    /// </summary>
    /// <param name="isValid">Result of the validation is valid when true.</param>
    /// <param name="validationErrorTip">Validation tip.</param>
    public ValidationResult(bool isValid, string validationErrorTip)
    {
        IsValid = isValid;
        ValidationErrorTip = validationErrorTip;
    }

    /// <summary>
    ///     Gets infomation about the validation errror.
    /// </summary>
    public string ValidationErrorTip { get; }

    /// <summary>
    ///     Gets resutl of the validation. When true validation is valid.
    /// </summary>
    public bool IsValid { get; }
}