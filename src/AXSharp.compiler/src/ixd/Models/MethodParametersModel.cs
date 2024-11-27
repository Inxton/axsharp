using AX.ST.Semantic.Model.Declarations.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.ixc_doc.Models
{
    public class MethodParametersModel
    {

        public MethodParametersModel(string name, string fullName, ITypeDeclaration type)
        {
            Name = name;
            FullName = fullName;
            Type = type;
        }

        public string Name { get; set; }
        public string FullName { get; set; }
        public ITypeDeclaration Type { get; set; }
    }
}
