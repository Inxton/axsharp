// AXSharp.Presentation.Blazor
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Text;

namespace AXSharp.Presentation.Blazor.Exceptions
{
    public class ParameterWrongTypeRendererException : Exception
    {
        public ParameterWrongTypeRendererException() { }

        public ParameterWrongTypeRendererException(string nameOfObject)
            : base(String.Format("Wrong type of object passed as Context to RenderableContentControl! Make sure you passed object of type ITwinPrimitive or ITwinObject! Actual type is: {0}.", nameOfObject))
        {

        }
    }
}
