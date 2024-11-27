// AXSharp.Abstractions
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Presentation.Attributes
{
    using System;

    public class PresentationContainerAttribute : Attribute
    {
        protected PresentationContainerAttribute()
        {

        }
        
        public PresentationContainerAttribute(string assembly, string fullTypeName) 
        {
            this.FullTypeName = fullTypeName;
            this.Assembly = assembly;
        }

        public PresentationContainerAttribute(string assembly, string fullTypeName, object parentHeader)
        {
            this.FullTypeName = fullTypeName;
            this.Assembly = assembly;
            this.ParentHeader = parentHeader;
        }

        public string Assembly { get; protected set; }

        public string FullTypeName { get; protected set; }

        public object ParentHeader { get; protected set; }
        
    }
}
