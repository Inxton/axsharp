// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Connector;

/// <summary>
///     Provides interface for information about value types symbols.
/// </summary>
public interface ITwinPrimitiveInfo
{
    /// <summary>
    ///     Gets group of this value type.
    /// </summary>
    uint Group { get; }

    /// <summary>
    ///     Gets length of this value type in bytes.
    /// </summary>
    int Length { get; }

    /// <summary>
    ///     Get the symbol of this tag.
    /// </summary>
    string Symbol { get; }
}