// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector.Identity;

/// <summary>
///     Represents null or non-existing <see cref="ITwinIdentity" /> object.
/// </summary>
public class NullTwinIdentity : ITwinIdentity
{
    /// <summary>
    ///     Gets empty identity value.
    /// </summary>
    public OnlinerULInt Identity => new();

    /// <summary>
    ///     Gets unknown identity name.
    /// </summary>
    public string AttributeName => "agnostic";

    /// <summary>
    ///     Gets unknown symbol.
    /// </summary>
    public string Symbol => "agnostic";

    /// <summary>
    ///     Gets unknown human readable path.
    /// </summary>
    public string HumanReadable => "agnostic";
}