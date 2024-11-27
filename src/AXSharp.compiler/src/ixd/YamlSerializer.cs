using AXSharp.ixc_doc.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using Polly;

namespace AXSharp.ixc_doc
{
    internal class YamlSerializer
    {
        private Options _options { get; set; }

        public YamlSerializer(Options o)
        {
            _options = o;
        }
        public string TocToYaml(TocSchema schema)
        {
            var stringBuilder = new StringBuilder();
            var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull).Build();
            stringBuilder.AppendLine(serializer.Serialize(schema));

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@$"{_options.OutputProjectFolder}\toc.yml"))
            {

                file.WriteLine("### YamlMime:TableOfContent");
                file.WriteLine(stringBuilder.ToString());
            }

            //Console.WriteLine("");
            //Console.WriteLine("### YamlMime:TableOfContent");
            //Console.WriteLine(stringBuilder);
            return stringBuilder.ToString();
        }

        public string SchemaToYaml(YamlSchema model, string fileName)
        {
            var stringBuilder = new StringBuilder();
            var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull).Build();

            stringBuilder.AppendLine(serializer.Serialize(model));

            
            
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@$"{EnsureDirectory(_options.OutputProjectFolder)}\{fileName}.yml"))
            {

                file.WriteLine("## YamlMime:ManagedReference");
                file.WriteLine(stringBuilder.ToString()); 
            }
            //Console.WriteLine("");
            //Console.WriteLine("## YamlMime:ManagedReference");
            //Console.WriteLine(stringBuilder);
            
            return stringBuilder.ToString();
        }

        private string EnsureDirectory(string directory)
        {
            Policy
                .Handle<IOException>()
                .WaitAndRetry(5, a => TimeSpan.FromMilliseconds(500))
                .Execute(() =>
                {
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                });

            return directory;
        }
    }
}
