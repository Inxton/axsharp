using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using AXSharp.Connector.Localizations;

namespace AXSharp.Connector.Localizations
{
    /// <summary>
    /// Provides a translator for localized PLC strings
    /// </summary>
    public class Translator
    {
        //private ResxLocalizations LocalizationResources { get; set; }
        
        private ResourceManager _resourceManager;

        private CultureInfo Culture = CultureInfo.InvariantCulture;

        /// <summary>
        /// Translates localized string.
        /// </summary>
        /// <param name="originalString">Localized string.</param>
        /// <param name="twin">Twin element to which the string is attached.</param>
        /// <returns></returns>
        public string Translate(string originalString, ITwinElement twin, CultureInfo culture = null)
        {
            if(culture == null) culture = Culture;

            
            return Localize(originalString, twin, culture);
        }

        /// <summary>
        /// Sets the localization resource for this translator.
        /// </summary>
        /// <param name="resourceType">Type of resource to be used.</param>
        public void SetLocalizationResource(Type resourceType)
        {
            if (resourceType != null)
            {
                _resourceManager = new ResourceManager(resourceType)
                {
                    IgnoreCase = true
                };
            }
        }

        internal static IEnumerable<string> GetTranslatable(string input,
            List<string> localizables = null)
        {
            localizables ??= new List<string>();

            if (string.IsNullOrEmpty(input)) return localizables;

            var position = 0;
            var recoveryPosition = 0;
            while (position < input.Length)
            {
                try
                {
                    position = input.IndexOf("<#", position);
                    var start = position;

                    if (position >= 0) recoveryPosition = position;

                    if (start >= 0)
                    {
                        position = input.IndexOf("#>", position);
                        if (position >= 0)
                        {
                            var end = position;

                            var localizableItem = input.Substring(start, end - start + 2);

                            if (!localizables.Contains(localizableItem)) localizables.Add(localizableItem);
                        }
                        else
                        {
                            position = recoveryPosition + 2;
                        }
                    }
                }
                catch
                {
                    // Ignore to prevent runtime errors.
                }

                if (position == -1) break;
            }

            return localizables;
        }

        private string LocalizeInParents(string token, ITwinElement rootObj, CultureInfo culture, string translation = null)
        {
            var obj = rootObj?.GetParent();

            while (obj != null)
            {
                translation = obj.Translate(token, culture);
                if (translation != null) return translation;

                obj = obj.GetParent();
            }

            return null;
        }

        public string Localize(string str, ITwinElement twinElement, CultureInfo culture)
        {
            
            foreach (var localizable in GetTranslatable(str))
            {
                var validIdentifier = LocalizationHelper.CreateId(localizable.CleanUpLocalizationTokens());

                // Search in first level resource
                var translation = _resourceManager?.GetString(validIdentifier, culture);
                
                // Search in parent resources
                if (translation == null)
                {
                    try
                    {
                        return LocalizeInParents(str, twinElement, culture);
                    }
                    catch
                    {
                        // Ignore to prevent runtime errors.
                    }
                }

                // Set key if not found anywhere
                translation ??= localizable;

                str = str.Replace(localizable, translation.CleanUpLocalizationTokens());
            }

            return str;
        }
    }
}
