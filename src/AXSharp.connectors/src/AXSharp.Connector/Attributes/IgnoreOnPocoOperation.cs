// AXSharp.Connector
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;

namespace AXSharp.Connector;

/// <summary>
/// This attribute will prevent R/W operation on on given member or type, when performing operation with POCO objects.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class)]
public class IgnoreOnPocoOperation : Attribute
{
    /// <summary>
    ///     Creates new instance of <see cref="IgnoreOnPocoOperation" />
    /// </summary>
    public IgnoreOnPocoOperation()
    {
    }
}