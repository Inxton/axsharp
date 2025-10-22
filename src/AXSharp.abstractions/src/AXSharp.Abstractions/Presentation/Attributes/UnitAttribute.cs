// AXSharp.Abstractions
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Globalization;

namespace AXSharp.Abstractions.Presentation;

[AttributeUsage(AttributeTargets.Property)]
public class UnitAttribute : Attribute
{
    public UnitAttribute(string unit)
    {
        this.UnitString = unit;
    }

    public string? UnitString { get; private set; }
}

