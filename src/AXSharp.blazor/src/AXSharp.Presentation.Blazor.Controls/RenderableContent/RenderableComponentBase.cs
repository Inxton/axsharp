// AXSharp.Presentation.Blazor.Controls
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using AXSharp.Presentation.Blazor.Interfaces;
using AXSharp.Connector.ValueTypes.Online;
using System.Xml.Linq;
using Serilog;

namespace AXSharp.Presentation.Blazor.Controls.RenderableContent
{
    /// <summary>
    ///  Base class which implements methods to update UI when PLC values are changed.
    /// </summary>
    public partial class RenderableComponentBase : ComponentBase, IRenderableComponent, IDisposable
    {
        /// <summary>
        /// Gets or sets the RenderableContentControl that encapsulates this component.
        /// </summary>
        [Parameter]
        public object RccContainer { get; set; } = new Object();

        [Parameter] public int PollingInterval { get; set; } = 250;

        /// <summary>
        /// Disposes this object as well as communication resources used by this component.
        /// </summary>
        public virtual void Dispose()
        {
            RemovePolledElements();
        }

        /// <summary>
        /// Contains set of elements polled for this component.
        /// </summary>
        protected HashSet<ITwinElement> PolledElements { get; } = new HashSet<ITwinElement>();

        public bool HasFocus { get; set; }

        /// <summary>
        /// Adds <see cref="element"/> to the polling queue.
        /// >[!IMPORTANT] This method should be overriden in the derived class to limit the number of elements to be polled for large objects.
        /// > All inner primitive types of the element will be added to the polling queue by default. When creating override remember to add the polled element to
        /// > the <see cref="PolledElements"/> set that is needed for removal of the element from the polling queue once the component is disposed.
        /// </summary>
        /// <param name="element">Element to be added to the polling queue.</param>
        /// <param name="pollingInterval">Sets polling interval for the element.</param>
        public virtual void AddToPolling(ITwinElement element, int pollingInterval = 250) 
        {
            element.StartPolling(pollingInterval, this);
            PolledElements.Add(element);
        }

        /// <summary>
        /// Removes elements added for polling from this component.
        /// </summary>
        public void RemovePolledElements()
        {
            PolledElements.ToList().ForEach(p =>
            {
                p.StopPolling(this);
            });

            PolledElements.Clear();
        }

        /// <summary>
        ///  Method, which updates are primitive values of ITwinObject instance
        /// <param name="element">ITwinObject instance.</param>
        /// <param name="pollingInterval">Polling interval</param>
        /// </summary>
        private void UpdateValuesOnChange(ITwinObject element, int pollingInterval = 250)
        {
            if (element != null)
            {
                foreach (var twinPrimitive in element.RetrievePrimitives())
                {
                    var tag = (OnlinerBase)twinPrimitive;
                    tag.PropertyChanged += new PropertyChangedEventHandler(HandlePropertyChanged);
                }
            }
        }

        /// <summary>
        ///  Method, which updates primitive value.
        /// <param name="tag">IValueTag instance.</param>
        /// <param name="pollingInterval">Polling interval</param>
        /// </summary>
        private void UpdateValuesOnChange(OnlinerBase tag, int pollingInterval = 250)
        {
            tag.PropertyChanged += new PropertyChangedEventHandler(HandlePropertyChanged);
        }

        /// <summary>
        ///  Method, which updates are primitive values of ITwinObject instance
        /// <param name="element">ITwinElement instance.</param>
        /// <param name="pollingInterval">Polling interval</param>
        /// </summary>
        public void UpdateValuesOnChange(ITwinElement element, int pollingInterval = 250)
        {
            AddToPolling(element, pollingInterval);

            switch (element)
            {
                case ITwinObject o:
                    UpdateValuesOnChange(o, pollingInterval);
                    break;
                case OnlinerBase b:
                    UpdateValuesOnChange(b, pollingInterval);
                    break;
            }
        }

        /// <summary>
        ///  Method, which updates are primitive shadow values of ITwinObject
        /// <param name="element">ITwinObject instance.</param>
        /// </summary>
        public void UpdateShadowValuesOnChange(ITwinObject element)
        {
            if (element != null)
            {
                var tags = element.GetValueTags();
                foreach (dynamic tag in tags)
                {
                    tag.ShadowValueChangeEvent += new ValueChangedEventHandlerDelegate(HandleShadowPropertyChanged);
                }
            }
        }

        /// <summary>
        ///  Method, which updates shadow primitive value.
        /// <param name="tag">IValueTag instance.</param>
        /// </summary>
        public void UpdateShadowValuesOnChange(ITwinPrimitive tag)
        {
            ((dynamic)tag).ShadowValueChangeEvent += new ValueChangedEventHandlerDelegate(HandleShadowPropertyChanged);
        }

        /// <summary>
        ///  Method, which updates primitive value only, when element is out of focus.
        ///  Public property IsFocus can be set. If is true, element won't be updated.
        /// <param name="tag">IValueTag instance.</param>
        /// </summary>
        public void UpdateValuesOnChangeOutFocus(OnlinerBase tag)
        {
            tag.PropertyChanged += new PropertyChangedEventHandler(HandlePropertyChangedOnOutFocus);
        }

        protected void HandlePropertyChanged(object sender, PropertyChangedEventArgs a)
        {
            if (ShouldBeUpdated(sender as ITwinElement))
            {
                InvokeAsync(StateHasChanged);
            }
        }

        protected void HandleShadowPropertyChanged(object sender, ValueChangedEventArgs a)
        {
            InvokeAsync(StateHasChanged);
        }

        protected virtual bool ShouldBeUpdated(ITwinElement element)
        {
            if (element == null)
                return true;
            
            var renderableContentControl = (RccContainer as RenderableContentControl);
            
            if(renderableContentControl != null)
            {
                if (renderableContentControl.Context.GetParent().GetConnector().SubscriptionMode == ReadSubscriptionMode.AutoSubscribeUsedVariables)
                {
                    return true;
                }
                
                if (renderableContentControl.Context.GetParent().GetConnector().SubscriptionMode == ReadSubscriptionMode.Polling)
                {
                    return this.PolledElements.Contains(element);
                }
            }

            return true;
        }

        protected void HandlePropertyChangedOnOutFocus(object sender, PropertyChangedEventArgs a)
        {
            if (!HasFocus)
            {
                if (ShouldBeUpdated(sender as ITwinElement))
                {
                    InvokeAsync(StateHasChanged);
                }
            }
                
        }
    }
}
