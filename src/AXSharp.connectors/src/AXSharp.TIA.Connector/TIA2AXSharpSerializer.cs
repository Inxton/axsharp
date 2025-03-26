using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.TIA2AXSharp
{
    /// <summary>
    /// Class containeg methods for creating a TIA2AX adapter
    /// </summary>
    public static class TIA2AXSharpSerializer
    {
        private static JsonSerializerSettings _settings = new JsonSerializerSettings
        { 
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
        };
        private static JsonSerializer _serializer = JsonSerializer.Create(_settings);


        /// <summary>
        /// Deserialize .json file in form of TIARootObject
        /// </summary>
        /// <param name="path">Path to .json file</param>
        /// <returns>TIARootObject</returns>
        public static TIARootObject? Deserialize(string path)
        {

            if (!File.Exists(path)) return null;

            using (StreamReader file = File.OpenText(path))
            {
                return (TIARootObject?)_serializer.Deserialize(file, typeof(TIARootObject));
            }
        }

        /// <summary>
        /// Serialize TIARootObject adapter into .json
        /// </summary>
        /// <param name="adapter">adapter in form of TIARootObject</param>
        /// <param name="path">Path to .json file</param>
        public static void Serialize(TIARootObject adapter, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                _serializer.Serialize(writer, adapter);
            }
        }
    }
}
