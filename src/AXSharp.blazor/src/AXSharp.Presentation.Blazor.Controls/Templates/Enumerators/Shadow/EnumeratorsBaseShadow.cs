using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.Presentation.Blazor.Controls.Templates
{
    public partial class EnumeratorsBaseShadow<T> : TemplateBase<T>
    {
        [Parameter]
        public EnumeratorDiscriminatorAttribute EnumDiscriminatorAttribute { get; set; }
        
        public Array Names { get; set; }
        public EnumToIntConverter EnumToIntConverter { get; set; }
        protected override Task OnInitializedAsync()
        {
            EnumToIntConverter = new EnumToIntConverter(EnumDiscriminatorAttribute);
            Names = Enum.GetNames(EnumDiscriminatorAttribute.EnumeratorType);
            UpdateShadowValuesOnChange(Onliner);
            return base.OnInitializedAsync();
        }
    }
}
