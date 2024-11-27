// AXSharp.Abstractions
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

namespace AXSharp.Presentation.UIExceptions
{
    public static class UIExceptionHandling
    {
        public delegate void AppUiExceptionHandlerDelegate(object sender, Exception e);

        public static AppUiExceptionHandlerDelegate AppUiExceptionHandler;
    }
}
