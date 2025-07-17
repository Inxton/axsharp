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


namespace AXSharp.Presentation.Blazor.Controls.Templates
{
    public abstract class TemplateBase<T> : RenderableComponentBase
    {
        protected string ToolTipOrHumanReadable => string.IsNullOrEmpty(Onliner.AttributeToolTip)
            ? Onliner.HumanReadable
            : Onliner.AttributeToolTip;

        protected string Symbol => Onliner.Symbol;

        private IJSObjectReference? module;

        [Inject]
        public IJSRuntime JSRuntime
        {
            get;
            set;
        }

        [Parameter]
        public virtual OnlinerBase<T> Onliner { get; set; }

        [Parameter]
        public bool IsReadOnly { get; set; }

        [Parameter]
        public bool HideLabel { get; set; } = false;

        protected T LastValue { get; set; }

        protected T Value
        {
            get
            {
                if (!HasFocus)
                {
                    LastValue = Onliner.Cyclic; // if is only readed, update LastValue for "HasFocus" case
                    return Onliner.Cyclic;
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

        protected string GetLabel()
        {
            return Onliner.AttributeName + (string.IsNullOrWhiteSpace(Onliner.AttributeUnits) ? null : $" [{Onliner.AttributeUnits}]");
        }
    }
}
