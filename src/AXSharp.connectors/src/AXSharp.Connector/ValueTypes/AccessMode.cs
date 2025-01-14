// AXSharp.Connector
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

namespace AXSharp.Connector.ValueTypes;

/// <summary>
///     Enumerates permitted access to a tag member.
/// </summary>
public enum ReadWriteAccess
{
    /// <summary>
    ///     Read only.
    /// </summary>
    Read,

    /// <summary>
    ///     Read Write.
    /// </summary>
    ReadWrite
}