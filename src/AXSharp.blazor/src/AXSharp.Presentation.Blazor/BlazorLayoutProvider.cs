// AXSharp.Presentation.Blazor
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Collections.Generic;
using AXSharp.Abstractions.Presentation;

namespace AXSharp.Presentation.Blazor
{
    /// <summary>
    ///  Provider class, which contains assembly info about layout types.
    /// </summary>
    public class BlazorLayoutProvider : ILayoutProvider
    {
        /// <summary>
        /// Create new instance of <see cref="BlazorLayoutProvider"/>.
        /// </summary>
            public BlazorLayoutProvider()
            {
                _layoutDictionary = new Dictionary<Layout, (string assembly, string fullTypeName)>();
                _layoutDictionary[Layout.Stack] = ("AXSharp.Presentation.Blazor.Controls", "AXSharp.Presentation.Blazor.Controls.Layouts.StackPanelLayout");
                _layoutDictionary[Layout.Wrap] = ("AXSharp.Presentation.Blazor.Controls", "AXSharp.Presentation.Blazor.Controls.Layouts.WrapPanelLayout");
                _layoutDictionary[Layout.Tabs] = ("AXSharp.Presentation.Blazor.Controls", "AXSharp.Presentation.Blazor.Controls.Layouts.TabControlLayout");
                _layoutDictionary[Layout.UniformGrid] = ("AXSharp.Presentation.Blazor.Controls", "AXSharp.Presentation.Blazor.Controls.Layouts.UniformGridLayout");
            }
            private readonly Dictionary<Layout, (string assembly, string fullTypeName)> _layoutDictionary;
        
            /// <summary>
            /// Gets control for given layout.
            /// </summary>
            /// <param name="layoutType">Layout type</param>
            /// <returns>Layout control assembly, and full type name.</returns>
            public (string assembly, string fullTypeName) GetControl(Layout layoutType)
            {
                return _layoutDictionary[layoutType];
            }
           
    }
}
