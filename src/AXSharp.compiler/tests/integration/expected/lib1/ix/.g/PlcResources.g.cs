
using System.Reflection;
using AXSharp.Connector.Localizations;

namespace lib1
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
            var defaultResourceType = Assembly.GetAssembly(typeof(lib1.PlcTranslator))
                .GetType("lib1.Resources.PlcStringResources");
            this.SetLocalizationResource(defaultResourceType);
        }
    }
}