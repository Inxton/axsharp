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

public class GroupAttribute : PresentationGroupAttribute
{    
    /// <summary>Initializes a new instance of the <see cref="ContainerAttribute" /> class.</summary>
    public GroupAttribute(GroupLayout layoutType) : base()
    {
        var containterInfo = PresentationProvider.Get.GroupLayoutProvider.GetControl(layoutType);
        this.Assembly = containterInfo.assembly;
        this.FullTypeName = containterInfo.fullTypeName;
    }

    public GroupAttribute(GroupLayout layoutType, object parentHeader) : base()
    {
        var containterInfo = PresentationProvider.Get.GroupLayoutProvider.GetControl(layoutType);
        this.Assembly = containterInfo.assembly;
        this.FullTypeName = containterInfo.fullTypeName;
        this.ParentHeader = parentHeader;
    }

    public GroupAttribute(string assembly, string fullTypeName) : base(assembly, fullTypeName)
    {

    }

    public GroupAttribute(string assembly, string fullTypeName, object parentHeader) : base(assembly, fullTypeName, parentHeader)
    {

    }
}