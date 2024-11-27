using AXSharp.ixc_doc.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.ixc_doc.Helpers
{
    public class YamlSerializerHelper
    {
        public YamlSerializerHelper()
        {
          
        }
        //declared schema. which we serialize
        public YamlSchema Schema { get; set; } = new YamlSchema();
        public TocSchema TocSchema { get; set; } = new TocSchema();


        //helpers lists for items
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Item> NamespaceItems { get; set; } = new List<Item>();

        public List<string> NamespaceChildren { get; set; } = new List<string>();

    
        //helpers lists for references

        public List<Reference> References { get; set; } = new List<Reference>();  
        public List<Reference> NamespaceReferences { get; set; } = new List<Reference>();  

       
    }
}
