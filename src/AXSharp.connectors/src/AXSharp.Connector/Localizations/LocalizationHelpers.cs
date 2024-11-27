using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.Connector.Localizations
{
    /// <summary>
    /// Provides helper methods for string localizations.
    /// </summary>
    public static class LocalizationHelper
    {
        private static Dictionary<char, string> Mappings = new Dictionary<char, string>()
        {
            {' ', "_"},
            {'!', "_EXCLAMATIONMARK_"},
            {'"', "_QUOTATIONMARKS_"},
            {'#', "_HASHTAG_"},
            {'$', "_DOLLAR_"},
            {'%', "_PERCENT_"},
            {'&', "_AMPERSAND_"},
            {'\'', "_APOSTROPHE_"},
            {'(', "_BRACKET_"},
            {')', "_ENDBRACKET_"},
            {'*', "_ASTERISK_"},
            {'+', "_PLUS_"},
            {',', "_COMMA_"},
            {'-', "_DASH_"},
            {'.', "_DOT_"},
            {'/', "_SLASH_"},
            {'0', "_ZERO_"},
            {'1', "_ONE_"},
            {'2', "_TWO_"},
            {'3', "_THREE_"},
            {'4', "_FOUR_"},
            {'5', "_FIVE_"},
            {'6', "_SIX_"},
            {'7', "_SEVEN_"},
            {'8', "_EIGHT_"},
            {'9', "_NINE_"},
            {':', "_COLON_"},
            {';', "_SEMICOLON_"},
            {'<', "_LESSTHAN_"},
            {'=', "_EQUALS_"},
            {'>', "_GREATERTHAN_"},
            {'?', "_QUESTIONMARK_"},
            {'@', "_ATSIGN_"},
            {'[', "_SQUAREBRACKET_"},
            {'\\', "_BACKSLASH_"},
            {']', "_ENDSQUAREBRACKET_"},
            {'^', "_CARET_"},
            {'_', "_UNDERSCORE_"},
            {'`', "_BACKTICK_"},
            {'{', "_CURLYBRACKET_"},
            {'|', "_PIPE_"},
            {'}', "_ENDCURLYBRACKET_"},
            {'~', "_TILDE_"},
            {'€', "_EURO_"},
        };

        private static string TryToGetValueByChar(char key)
        {
            Mappings.TryGetValue(key, out var value);
            return value;
        }

        /// <summary>
        /// Create C# compliant identifier from an arbitrary string.
        /// </summary>
        /// <param name="rawText">String from which to create identifier.</param>
        /// <returns>C# compliant identifier.</returns>
        public static string CreateId(string rawText)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in rawText)
            {
                //try to find character in mappings
                var mapchar = TryToGetValueByChar(c);
                if (mapchar != null)
                {
                    sb.Append(mapchar);
                }
                //add only if is letter
                else if (Char.IsLetter(c))
                {
                    sb.Append(c);
                }
                else
                {
                    //unknown
                    sb.Append('_');
                }
            }
            return sb.ToString();


        }

        /// <summary>
        /// Cleans up localization tokens from a string.
        /// </summary>
        /// <param name="localized">String with localization tokens</param>
        /// <returns>String without localization tokens.</returns>
        public static string CleanUpLocalizationTokens(this string localized)
        {
            if (localized != null) return localized.Replace("<#", string.Empty).Replace("#>", string.Empty);

            return string.Empty;
        }
    }
}
