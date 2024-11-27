// AXSharp.Abstractions
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Text;

namespace AXSharp.Abstractions.Presentation
{
    public class EmptyGroupProvider: IGroupLayoutProvider
    {
        public (string assembly, string fullTypeName) GetControl(GroupLayout layoutType)
        {
            //throw new PresentationProviderNotAssignedException("Presentation provider was not created. " +
            //                                                           "You must assign appropriate presentation provider by using 'AXSharp.Abstractions.Presentation.PresentationProvider.Create method'");

            return ("Group provider was not created.",
                    "You must assign appropriate group provider by using 'AXSharp.Abstractions.Presentation.PresentationProvider.Create method'");

        }

    }
}
