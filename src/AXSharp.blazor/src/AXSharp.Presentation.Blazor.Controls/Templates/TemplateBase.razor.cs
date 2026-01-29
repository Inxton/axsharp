using AXSharp.Connector.ValueTypes;
using AXSharp.Presentation.Blazor.Controls.RenderableContent;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector;
using System.Globalization;


namespace AXSharp.Presentation.Blazor.Controls.Templates
{
    public abstract class TemplateBase<T> : RenderableComponentBase
    {
        /// <summary>
        /// Gets the tooltip or human readable name of the Onliner.
        /// </summary>
        protected string ToolTipOrHumanReadable => string.IsNullOrEmpty(Onliner.AttributeToolTip)
            ? Onliner.GetHumanReadable(CultureInfo.CurrentUICulture) 
            : Onliner.AttributeToolTip;

        /// <summary>
        /// Gets the symbol of the Onliner.
        /// </summary>
        protected string Symbol => Onliner.Symbol;

        private IJSObjectReference? module;

        [Inject]
        public IJSRuntime JSRuntime
        {
            get;
            set;
        }

        /// <summary>
        /// The Onliner associated with this template.
        /// </summary>
        [Parameter]
        public virtual OnlinerBase<T> Onliner { get; set; }

        /// <summary>
        /// Indicates whether the control is read-only.
        /// </summary>
        [Parameter]
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Indicates whether the label should be hidden.
        /// </summary>
        [Parameter]
        public bool HideLabel { get; set; } = false;

        /// <summary>
        /// The unit of measurement for the value.
        /// </summary>
        [Parameter]
        public string? Unit { get; set; }

        /// <summary>
        /// The format string for displaying the value.
        /// </summary>
        [Parameter]
        public string? Format { get; set; }

        /// <summary>
        /// The last known value of the Onliner.
        /// </summary>
        protected T LastValue { get; set; }

        /// <summary>
        /// Gets or sets the current value of the Onliner.
        /// </summary>
        protected T Value
        {
            get
            {
                if (!HasFocus)
                {
                    switch(Onliner)
                    {
                        case OnlinerBase<string> onlinerString:
                            LastValue = (T)(object)onlinerString.GetCyclic(CultureInfo.CurrentUICulture);
                            break;
                        case OnlinerBase<T> onliner:
                            LastValue = onliner.Cyclic;
                            break;
                    }
                    return LastValue;
                }
                else
                {
                    return LastValue;
                }
            }
            set
            {
                LastValue = value;
                Onliner.Edit = value;
            }

        }

        internal string AccessStatus { get; set; }
        internal string ComponentId { get; set; }
        internal string OnlinerSymbol { get => Onliner.Symbol.Replace(".", "-"); } 
        protected override Task OnInitializedAsync()
        {
            AccessStatus = Onliner.AccessStatus.Failure ? "is-invalid" : "";
            ComponentId = Onliner.Symbol + "_" + Guid.NewGuid().ToString();
            return base.OnInitializedAsync();
        }

        /// <summary>
        /// Gets the label for the control based on the Onliner's attribute name and units.
        /// </summary>
        /// <returns></returns>
        protected string GetLabel()
        {
            var retVal = Onliner.GetAttributeName(CultureInfo.CurrentUICulture) + (string.IsNullOrWhiteSpace(Onliner.AttributeUnits) ? null : $" [{Onliner.AttributeUnits}]");            
            return retVal;
        }
    }
}
