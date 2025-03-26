using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    namespace AXSharp.Compiler
    {
        public class CompanionInfo
        {

            public const string COMPANIONS_FILE_NAME = "axsharp.companion.json";

            public string Id { get; set; }
            public string Version { get; set; }

            private static string ToJson(CompanionInfo info)
            {
                return JsonConvert.SerializeObject(info);
            }

            private static CompanionInfo FromJson(string json)
            {
                return JsonConvert.DeserializeObject<CompanionInfo>(json);
            }

            // Serialize the object to a file
            public static void ToFile(CompanionInfo info, string filePath)
            {
                var json = ToJson(info);
                File.WriteAllText(filePath, json);
            }

            // Deserialize the object from a file
            public static CompanionInfo? FromFile(string filePath)
            {
                var json = File.ReadAllText(filePath);
                return FromJson(json);
            }

            public static CompanionInfo? TryFromFile(string filePath)
            {
                if (!File.Exists(filePath))
                    return null;

                var json = File.ReadAllText(filePath);
                return FromJson(json);
            }
    }
    }


