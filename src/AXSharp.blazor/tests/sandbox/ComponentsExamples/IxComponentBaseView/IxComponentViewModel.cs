// ComponentsExamples
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Presentation;
using AXSharp.Presentation.Blazor.Interfaces;

namespace ax_blazor_example
{
    public class IxComponentViewModel : RenderableViewModelBase 
    {
        public IxComponentViewModel()
        {
        }
        public IxComponent Component { get;  set; }
        public override object Model { get => this.Component; set { this.Component = value as IxComponent; } }

        public string DummyText { get; set; } = "cus";
    
    }
}
