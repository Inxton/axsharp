// AXSharp.Connector
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector.Identity;

/// <summary>
///     Defines identity object contract.
/// </summary>
public interface ITwinIdentity
{
    /// <summary>
    ///     Gets the online identity.
    /// </summary>
    OnlinerULInt Identity { get; }

    /// <summary>
    ///     Gets name of a named twin object.
    /// </summary>
    string AttributeName { get; }

    /// <summary>
    ///     Gets full symbol path of a twin object.
    /// </summary>
    string Symbol { get; }

    /// <summary>
    ///     Gets full path of this twin object in human readable format.
    /// </summary>
    string HumanReadable { get; }
}