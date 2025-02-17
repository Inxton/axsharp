// AXSharp.Presentation.Blazor
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AXSharp.Connector;

namespace AXSharp.Presentation.Blazor.Interfaces
{
    public interface IRenderableComponent
    {

        void ConfigurePolling();
        /// <summary>
        /// Removes elements added for polling from this component.
        /// </summary>
        void StopPolling();
    }
}
