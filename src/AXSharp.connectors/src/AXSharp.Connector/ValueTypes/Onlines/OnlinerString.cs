// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Globalization;
using System.Threading.Tasks;
using AXSharp.Connector.Localizations;
using AXSharp.Connector.ValueTypes.Online;
using AXSharp.Connector.ValueTypes.Shadows;
using AXSharp.Connector.ValueValidation;

namespace AXSharp.Connector.ValueTypes;

/// <summary>
///     Class providing access to the STRING type online variable.
/// </summary>
public class OnlinerString : OnlinerBase<string>, IOnlineString, IShadowString
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerString" /> class.
    /// </summary>
    public OnlinerString()
    {
        validator = new StringValueValidationRule(this);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnlinerString" /> class.
    /// </summary>
    /// <param name="parent">Parent object of this instance.</param>
    /// <param name="readableTail">Human readable tail of this instance.</param>
    /// <param name="symbolTail">Symbol tail of this instance.</param>
    public OnlinerString(ITwinObject parent, string readableTail, string symbolTail)
        : base(parent, readableTail, symbolTail)
    {
        validator = new StringValueValidationRule(this);
    }

    /// <summary>
    ///     Gets the max value for this instance.
    /// </summary>
    public override string InstanceMaxValue => AttributeMaxSet ? AttributeMaximum : string.Empty;

    /// <summary>
    ///     Gets the min value for this instance.
    /// </summary>
    public override string InstanceMinValue => AttributeMinSet ? AttributeMinimum : string.Empty;

    /// <summary>
    ///     Gets translated and interpolated string of <see cref="Cyclic" /> value. Sets <see cref="Cyclic" /> value.
    /// </summary>
    public override string Cyclic
    {
        get => base.Cyclic.Interpolate(this);
        set => base.Cyclic = value;
    }

    /// <inheritdoc />
    public override string GetCyclic(CultureInfo culture = default)
    {
        return this.Translate(this.Cyclic, culture);
    }

    /// <summary>
    ///     Gets the value of this <see cref="OnlinerString" />.
    /// </summary>
    /// <returns>
    ///     <see cref="GetAsync" />
    /// </returns>
    public override async Task<string> GetAsync()
    {
        var retVal = await base.GetAsync();
        return retVal.Interpolate(this).CleanUpLocalizationTokens();
    }

    public override async Task<string> GetAsync(CultureInfo culture)
    {
        return this.Translate(await this.GetAsync(), culture).Interpolate(this);
    }

    /// <summary>
    ///     Sets the value of this <see cref="OnlinerString" />.
    /// </summary>
    /// <param name="value">Value to be set</param>
    public override async Task<string> SetAsync(string value)
    {
        return await Task.Run(() =>
        {
            Cyclic = value;
            return value;
        });
    }
}