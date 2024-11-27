using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.ixc_doc.Models
{
    // properties names must be with lower letters and same like xml tags, because of getcomments method acquiring comments through reflection
    public class Comments
    {
        public string summary { get; set; }
        public Dictionary<string, string> param { get; set; } = new();
        public string returns { get; set; }
        public string remarks { get; set; }
    }
}
