// ComponentsExamples
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Presentation;

namespace ax_blazor_example
{
    public class StTest3ManualViewModel : RenderableViewModelBase
    {
        public StTest3ManualViewModel() 
        {
        }
        public stTest3 Component { get; set; }
        public override object Model { get => this.Component; set { this.Component = value as stTest3; } }

        public string DummyText { get; set; } = "sttest3";
    }
}
