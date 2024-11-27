using AXSharp.ixc_doc.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.ixc_doc.Models
{
    public  class NamespaceWrapper
    {
        public NamespaceWrapper(Item item)
        {
            NamespaceItem = item;
        }
        public Item NamespaceItem { get; set; }
        public List<string> NamespaceTemporaryChildren { get; set; } = new List<string>();
        public List<Reference> NamespaceReferences { get; set; } = new List<Reference>();
    }
}
