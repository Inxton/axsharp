using Siemens.Simatic.S7.Webserver.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.TIA2AXSharp
{
    public class TIABrowseElement : ITIAGenericObject
    {
        public TIABrowseElement(string symbol, ApiPlcProgramDataType datatype, bool isNested)
        {
            Symbol = symbol;
            Datatype = datatype;
            IsNested = isNested;
        }
        public string Symbol { get; set; }
        public ApiPlcProgramDataType Datatype { get; set; }
        public bool IsNested { get; set; }
        public List<TIABrowseElement> Children { get; set; } = new List<TIABrowseElement>();
    }
}
