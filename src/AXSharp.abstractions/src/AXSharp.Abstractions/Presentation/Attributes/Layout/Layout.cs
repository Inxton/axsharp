// AXSharp.Abstractions
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Linq;

namespace AXSharp.Abstractions.Presentation;

public enum Layout
{
    /// <summary>
    /// Stack controls in order of declaration.
    /// </summary>
    Stack,
    /// <summary>
    /// Wraps controls in order or declaration.
    /// </summary>
    Wrap,
    /// <summary>
    /// Organizes the controls into tab control. Each control that declares <see cref="ContainerAttribute"/> will be placed
    /// into separate tab.
    /// </summary>
    Tabs,
    /// <summary>
    /// Places the controls into scroll panel.
    /// </summary>
    Scroll,
    GroupBox,
    Border,
    UniformGrid,    
}


