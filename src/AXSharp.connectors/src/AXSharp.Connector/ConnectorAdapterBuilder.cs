// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Connector;

/// <summary>
///     Provides access to extension method for creating connector adapters.
/// </summary>
public class ConnectorAdapterBuilder
{
    private ConnectorAdapterBuilder()
    {
    }

    /// <summary>
    ///     Creates connector adapter builder class.
    /// </summary>
    /// <returns>Connector adapter builder.</returns>
    public static ConnectorAdapterBuilder Build()
    {
        return new ConnectorAdapterBuilder();
    }
}