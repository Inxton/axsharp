// AXSharp.Abstractions
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Linq;
using AXSharp.Abstractions.Presentation;
using AXSharp.Presentation.Attributes;

namespace AXSharp.Abstractions.Presentation;

/// <summary>
/// Provides description of the container that shall contain the controls for data items.
/// </summary>
public class ContainerAttribute : PresentationContainerAttribute
{
    /// <summary>Initializes a new instance of the <see cref="ContainerAttribute" /> class.</summary>
    public ContainerAttribute(Layout layoutType) : base()
    {        
        var containerInfo = PresentationProvider.Get.LayoutProvider.GetControl(layoutType);
        this.Assembly = containerInfo.assembly;
        this.FullTypeName = containerInfo.fullTypeName;
    }

    /// <summary>
    /// Creates new instance of <see cref="ContainerAttribute"/>
    /// </summary>
    /// <param name="layoutType">Container's layout type</param>
    /// <param name="parentHeader">Container's parent header</param>
    public ContainerAttribute(Layout layoutType, object parentHeader) : base()
    {
        var containerInfo = PresentationProvider.Get.LayoutProvider.GetControl(layoutType);
        this.Assembly = containerInfo.assembly;
        this.FullTypeName = containerInfo.fullTypeName;
        this.ParentHeader = parentHeader;
    }

    /// <summary>
    /// Creates new instance of <see cref="ContainerAttribute"/>.
    /// </summary>
    /// <param name="assembly">Container's assembly name.</param>
    /// <param name="fullTypeName">Container full type name.</param>
    public ContainerAttribute(string assembly, string fullTypeName) : base(assembly, fullTypeName)
    {

    }

    /// <summary>
    /// Creates new instance of <see cref="ContainerAttribute"/>
    /// </summary>
    /// <param name="assembly">Container's assembly name</param>
    /// <param name="fullTypeName">Container's full type name.</param>
    /// <param name="parentHeader">Container's parent header object.</param>
    public ContainerAttribute(string assembly, string fullTypeName, object parentHeader) : base(assembly, fullTypeName, parentHeader)
    {

    }
}