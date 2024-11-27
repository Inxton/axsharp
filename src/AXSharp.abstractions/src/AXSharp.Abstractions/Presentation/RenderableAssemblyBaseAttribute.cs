// AXSharp.Abstractions
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Presentation
{
    using System;    

    /// <summary>
    /// Assembly attribute to notify consumers that the assembly is contains types eligible in the automated UI rendering process.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class RenderableAssemblyBaseAttribute : Attribute
    {
    }
}
