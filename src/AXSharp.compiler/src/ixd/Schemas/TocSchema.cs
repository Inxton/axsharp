using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AX.ST.Semantic.Model.Declarations;
using System.Security.Cryptography;

namespace AXSharp.ixc_doc.Schemas
{
    public class TocSchema
    {
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<Item> Items { get; set; } = new List<Item>();

        public partial class Item
        {
            public Item(INamespaceDeclaration namespaceDeclaration, string name)
            {
                Uid= Helpers.Helpers.GetBaseUid(namespaceDeclaration);
                Name= Helpers.Helpers.GetBaseUid(namespaceDeclaration.FullyQualifiedName);
            }

            public Item(string uid, string name)
            {
                Uid = uid;
                Name = name;
            }

            [JsonProperty("uid")]
            public string Uid { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
            public List<Item> Items { get; set; } = new List<Item>();
        }
    }
}

