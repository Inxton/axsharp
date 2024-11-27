// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/dev/notices.md


using System.Globalization;
using System.Text.RegularExpressions;

namespace AXSharp.Connector.Localizations;

public static class TranslatorExtension
{
    /// <summary>
    /// Extension method provides translation of a localized string attached to a twin element.
    /// </summary>
    /// <param name="twin">Twin element to which the string is attached.</param>
    /// <param name="originalString">Localized string to be translated.</param>
    /// <returns></returns>
    public static string Translate(this ITwinElement twin, string originalString, CultureInfo culture)
    {
        originalString ??= string.Empty;
        if (!originalString.Contains("<#"))
        {
            return originalString;
        }

        var translated = twin?.Interpreter?.Translate(originalString, twin, culture) ?? originalString;
        return translated.CleanUpLocalizationTokens();
    }
}