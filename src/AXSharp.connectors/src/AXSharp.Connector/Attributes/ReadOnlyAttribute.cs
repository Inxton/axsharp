// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;

namespace AXSharp.Connector;

/// <summary>
///     Attribute allows to prevent writing to the members of twin connector.
///     <para>
///         <see cref="ReadOnlyAttribute" /> can be declared for member of a FB, GVL or STRUCT.
///     </para>
///     <note>
///         This attribute is typically defined in the declaration section of PLC block and then
///         trans-piled by ixc builder.
///     </note>
///     <note type="warning">
///         Use of <see cref="ReadOnlyAttribute" /> does not prevent the PLC program to write to the
///         variable that declares this attribute.
///     </note>
///     <example>
///         This example demonstrates declaration of <see cref="ReadOnlyAttribute" /> on a member of a FB.
///         <code>
///         FUNCTION_BLOCK fbSomeReadOnlyMembers 
///         VAR
///             {#ix-attr:[ReadOnly()]]}
///             _nonWrittableItem : BOOL;       // Member is readonly for .net application.
///             {#ix-attr:[ReadOnly()]]}
///             _nonWrittableFunctionBlock : fbNonWrittable; // Member and its members are readonly for the .net application.
///         END_VAR
///     </code>
///     </example>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class ReadOnlyAttribute : Attribute
{
    /// <summary>
    ///     Creates new instance of <see cref="ReadOnlyAttribute" />
    /// </summary>
    public ReadOnlyAttribute()
    {
    }
}