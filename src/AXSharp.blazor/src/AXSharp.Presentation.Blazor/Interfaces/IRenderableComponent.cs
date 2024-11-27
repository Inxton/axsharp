// AXSharp.Presentation.Blazor
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector;

namespace AXSharp.Presentation.Blazor.Interfaces
{
    public interface IRenderableComponent
    {
        /// <summary>
        /// Adds <see cref="element"/> to the polling queue.
        /// <param name="element">Element to be added to the polling queue.</param>
        /// <param name="pollingInterval">Sets polling interval for the element.</param>
        void AddToPolling(ITwinElement element, int pollingInterval = 250);

        /// <summary>
        /// Removes elements added for polling from this component.
        /// </summary>
        void RemovePolledElements();

        
    }
}
