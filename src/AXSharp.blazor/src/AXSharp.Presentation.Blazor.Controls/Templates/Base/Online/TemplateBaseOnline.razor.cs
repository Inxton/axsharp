using AXSharp.Connector.ValueTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


    

namespace AXSharp.Presentation.Blazor.Controls.Templates
{
    



    public partial class TemplateBaseOnline<T> : TemplateBase<T>
    {
        protected string ToolTipText => string.IsNullOrEmpty(Onliner.AttributeToolTip)
            ? Onliner.HumanReadable
            : Onliner.AttributeToolTip;

        protected override Task OnInitializedAsync()
        {
            AddToPolling(this.Onliner, this.PollingInterval);
            UpdateValuesOnChangeOutFocus(Onliner);
            return base.OnInitializedAsync();
        }
    }
}
