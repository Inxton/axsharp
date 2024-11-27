// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;

namespace AXSharp.Connector;

/// <summary>
///     Prevents ixc builder to create a member for specific group of output type (Shadow, Plain, Onliner).
///     <note type="note">This attribute must be declared in the PLC code to be effective during build process.</note>
///     <example>
///         This example demonstrates how to prevent the ixc builder to compile specific property into specific output
///         group.
///         <code>
///     // This will not compile to 'omitsInPlainString' member into respective 'Plain' type.
///     {#ix-attr:[CompilerOmits(CompilerOmissionGroups.BuilderPlainer))]} 
///     ommitsInPlainString : STRING(50) := 'THIS IS OMMITED IN PLAINER';     
///     // This will not compile to 'ommitsInPlainAndShadowerInterfaceString' member into respective 'Plain' type and Shadow interface.
///     {#ix-attr:[CompilerOmits(CompilerOmissionGroups.BuilderPlainer, CompilerOmissionGroups.BuilderShadowerInterface))]} 
///     ommitsInPlainAndShadowerInterfaceString : STRING(50) := 'THIS IS OMMITED IN PLAINER';     
/// </code>
///     </example>
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class CompilerOmitsAttribute : Attribute
{
    /// <summary>
    ///     Creates an instance of <see cref="CompilerOmitsAttribute" />.
    /// </summary>
    /// <param name="omissions">Determines group(s) of output type where the member shall be omitted.</param>
    public CompilerOmitsAttribute(params string[] omissions)
    {
        ArgumentNullException.ThrowIfNull(omissions);
        Omissions = omissions;
    }

    /// <summary>
    ///     Creates an instance of <see cref="CompilerOmitsAttribute" />.
    /// </summary>
    /// <param name="omissions">
    ///     Determines group(s) of output type where the member shall be omitted.
    ///     <see cref="CompilerOmissionGroups" />
    /// </param>
    public CompilerOmitsAttribute(params CompilerOmissionGroups[] omissions)
    {
        Omissions = omissions.ToList().ConvertAll(p => p.ToString());
    }

    /// <summary>
    ///     Gets the list of groups of output type in which the member will not be included.
    /// </summary>
    public IEnumerable<string> Omissions { get; }
}

/// <summary>
///     Builder omission groups enumerator. Enumerates builder output groups suitable
///     for the omission.
/// </summary>
public enum CompilerOmissionGroups
{
    /// <summary>
    ///     Omits in Onliner
    /// </summary>
    BuilderOnliner,

    /// <summary>
    ///     Omits in Onliner Interface
    /// </summary>
    BuilderOnlinerInterface,

    /// <summary>
    ///     Omits in Shadower Interface
    /// </summary>
    BuilderShadowerInterface,

    /// <summary>
    ///     Omits in Plain (POCO) objects.
    /// </summary>
    BuilderPlainer
}