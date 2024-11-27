// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Globalization;

namespace AXSharp.Connector.ValueValidation;

/// <summary>
///     Generic class provides base for tags validation rules.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class OnlinerValidationRule<T>
{
    private readonly IValueBoundaries<T> onliner;


    /// <summary>
    ///     Initializes a new instance of the <see cref="ByteValueValidationRule" /> class.
    /// </summary>
    /// <param name="onliner">
    ///     Onliner type.
    /// </param>
    protected OnlinerValidationRule(IValueBoundaries<T> onliner)
    {
        this.onliner = onliner;
    }

    /// <summary>
    ///     Gets the max. allowed value.
    /// </summary>
    public T Max => onliner.InstanceMaxValue;

    /// <summary>
    ///     Gets the min. allowed value.
    /// </summary>
    public T Min => onliner.InstanceMinValue;

    /// <summary>
    ///     Provides validation tip.
    /// </summary>
    public string ValidationErrorTip { get; protected set; }

    /// <summary>
    ///     Validates value.
    /// </summary>
    /// <param name="value">Value to be validated.</param>
    /// <param name="culture">Culture.</param>
    /// <returns></returns>
    public abstract ValidationResult Validate(T value, CultureInfo culture);
}