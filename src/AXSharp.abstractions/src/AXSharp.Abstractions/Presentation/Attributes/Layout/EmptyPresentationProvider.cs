// AXSharp.Abstractions
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Text;

namespace AXSharp.Abstractions.Presentation
{
    public class EmptyPresentationProvider : ILayoutProvider
    {
        public (string assembly, string fullTypeName) GetControl(Layout layoutType)
        {
            //throw new PresentationProviderNotAssignedException("Presentation provider was not created. " +
            //                                                           "You must assign appropriate presentation provider by using 'AXSharp.Abstractions.Presentation.PresentationProvider.Create method'");

            return ("Presentation provider was not created.",
                    "You must assign appropriate presentation provider by using 'AXSharp.Abstractions.Presentation.PresentationProvider.Create method'");

        }

    }
}
