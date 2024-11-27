
using System.Reflection;
using AXSharp.Connector.Localizations;

namespace units
{
    public sealed class PlcTranslator : Translator
    {
        private static readonly PlcTranslator instance = new PlcTranslator();

        public static PlcTranslator Instance
        {
            get
            {
                return instance;
            }
        }

        private PlcTranslator() 
        {
            var defaultResourceType = Assembly.GetAssembly(typeof(units.PlcTranslator))
                .GetType("units.Resources.PlcStringResources");
            this.SetLocalizationResource(defaultResourceType);
        }
    }
}