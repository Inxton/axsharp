
using System.Reflection;
using AXSharp.Connector.Localizations;

namespace lib2
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
            var assembly = Assembly.GetAssembly(typeof(lib2.PlcTranslator));
            var resource = assembly.GetType("lib2.Resources.PlcStringResources");
            this.SetLocalizationResource(resource, assembly);
        }
    }
}