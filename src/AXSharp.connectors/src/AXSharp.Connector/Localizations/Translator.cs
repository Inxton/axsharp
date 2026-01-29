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
        private IEnumerable<ResourceManager> ResourceManagers
        {
            get
            {
                // IMPORTANT: order matters. Application should be searched first to allow overrides.
                // Do not cache: resources can be configured at runtime (e.g., SetPrimaryTranslatorResource).
                if (_applicationResourceManager != null) yield return _applicationResourceManager;
                if (_libraryResourceManager != null) yield return _libraryResourceManager;
            }
        }

        private static ResourceManager _applicationResourceManager;

        private ResourceManager _libraryResourceManager;

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
        /// <param name="originAssembly"></param>
        public void SetLocalizationResource(Type resourceType, Assembly originAssembly = null)
        {
            if (resourceType != null)
            {
                _libraryResourceManager = new ResourceManager(resourceType)
                {
                    IgnoreCase = true
                };
            }
            else
            {
                Console.WriteLine($"No resource type provided for `{originAssembly?.FullName}`");
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

                // Search through all resource managers for the key
                string translation = null;
                foreach (var resourceManager in ResourceManagers)
                {
                    translation = resourceManager?.GetString(validIdentifier, culture);
                    if (translation != null)
                    {
                        break; // Found translation, stop searching
                    }
                }

                // Search in parent resources if not found
                if (translation == null)
                {
                    try
                    {
                        translation = LocalizeInParents(localizable, twinElement, culture);
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

        /// <summary>
        /// Sets the primary application resource for all translators.
        /// This would be tipycally set to the resource containing the application's translations.
        /// Any matching localization key will be first searched in this resource and then in the library resource.
        /// You can leverage this to override library translations with application specific ones.
        /// </summary>
        /// <param name="resourceType"></param>
        public static void SetPrimaryTranslatorResource(Type resourceType)
        {
            _applicationResourceManager = new ResourceManager(resourceType)
            {
                IgnoreCase = true
            };
        }
    }
}
