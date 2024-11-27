// AXSharp.Abstractions
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Presentation
{
    /// <summary>
    /// Abstract class for implementation of renderable ViewModel types.    
    /// </summary>
    public abstract class RenderableViewModelBase
        : BindableBase
    {
        /// <summary>
        /// Creates new instance of <see cref="RenderableViewModelBase"/>
        /// </summary>
        public RenderableViewModelBase()
        {
            
        }

        /// <summary>
        /// Gets or sets model object for this ViewModel type.
        /// </summary>
        public abstract object Model { get; set; }        
    }
}
