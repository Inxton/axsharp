
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
            var assembly = Assembly.GetAssembly(typeof(units.PlcTranslator));
            var resource = assembly.GetType("units.Resources.PlcStringResources");
            this.SetLocalizationResource(resource, assembly);
        }
    }
}