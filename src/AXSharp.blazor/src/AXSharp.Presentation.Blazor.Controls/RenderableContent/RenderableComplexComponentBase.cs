// AXSharp.Presentation.Blazor.Controls
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector;
using AXSharp.Presentation.Blazor.Interfaces;

namespace AXSharp.Presentation.Blazor.Controls.RenderableContent
{
    /// <summary>
    ///  Base class for complex components with only code-behind.
    /// </summary>
    public class RenderableComplexComponentBase<T> : RenderableComponentBase, IRenderableComplexComponentBase where T : ITwinElement
    {
        private T _component;

        [Parameter]
        public T Component
        {
            get => _component;
            set
            {
                if (_component == null)
                {
                    _component = value;
                    this.OnComponentChanged();
                    return;
                }

                if (_component != null && !_component.Equals(value))
                {
                    _component = value;
                    this.OnComponentChanged();
                }
            } 
        }

        /// <summary>
        /// Method called when the component (the context component of this rcc) is changed.
        /// </summary>
        public virtual void OnComponentChanged()
        {

        }
    }
}
