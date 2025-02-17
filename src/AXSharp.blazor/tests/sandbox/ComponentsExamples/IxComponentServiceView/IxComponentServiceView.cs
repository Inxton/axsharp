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
using AXSharp.Presentation.Blazor.Controls.RenderableContent;

namespace ax_blazor_example
{
    public partial class IxComponentServiceView
    {
        protected override void OnInitialized()
        {
            StartPolling(Component);
        }


        public override void Dispose()
        {
            this.StopPolling();
        }
    }
}
