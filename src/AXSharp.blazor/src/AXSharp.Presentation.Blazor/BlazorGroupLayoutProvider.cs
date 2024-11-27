using AXSharp.Abstractions.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.Presentation.Blazor
{
    public class BlazorGroupLayoutProvider : IGroupLayoutProvider
    {
        /// <summary>
        ///  Provider class, which contains assembly info about layout types.
        /// </summary>

        /// <summary>
        /// Create new instance of <see cref="BlazorGroupLayoutProvider"/>.
        /// </summary>
        public BlazorGroupLayoutProvider()
        {
            _layoutDictionary = new Dictionary<GroupLayout, (string assembly, string fullTypeName)>();
            _layoutDictionary[GroupLayout.Border] = ("AXSharp.Presentation.Blazor.Controls", "AXSharp.Presentation.Blazor.Controls.Layouts.BorderLayout");
            _layoutDictionary[GroupLayout.GroupBox] = ("AXSharp.Presentation.Blazor.Controls", "AXSharp.Presentation.Blazor.Controls.Layouts.GroupBoxLayout");
        }
        private readonly Dictionary<GroupLayout, (string assembly, string fullTypeName)> _layoutDictionary;

        /// <summary>
        /// Gets control for given layout.
        /// </summary>
        /// <param name="layoutType">GroupLayout type</param>
        /// <returns>Layout control assembly, and full type name.</returns>
        public (string assembly, string fullTypeName) GetControl(GroupLayout layoutType)
        {
            return _layoutDictionary[layoutType];
        }

        
    }
}
