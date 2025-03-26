using AXSharp.Connector.ValueTypes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector;

namespace AXSharp.Presentation.Blazor.Controls.Templates
{
    public partial class TemplateBaseShadow<T> : TemplateBase<T>
    {
        private OnlinerBase<T> _Onliner;

        [Parameter]
        public override OnlinerBase<T> Onliner
        {
            get { return _Onliner; }
            set
            {
                if (_Onliner != value)
                {
                    _Onliner = value;
                    UpdateShadowValuesOnChange(_Onliner);
                }
            }
        }

        protected override Task OnInitializedAsync()
        {
           // UpdateShadowValuesOnChange(Onliner);
            return base.OnInitializedAsync();
        }

        public override void ConfigurePolling()
        {
            // No polling for shadow values
        }
    }
}