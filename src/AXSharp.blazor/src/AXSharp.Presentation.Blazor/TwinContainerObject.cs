// AXSharp.Presentation.Blazor
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Text;
using AXSharp.Connector;

namespace AXSharp.Presentation.Blazor
{
    public class TwinContainerObject
    {

        public TwinContainerObject(ITwinObject twin, string id)
        {
            Twin = twin;
            Id = id;
        }
        public ITwinObject Twin { get;  }
        public string Id { get;  }

    }
}
