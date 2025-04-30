// AXSharp.Connector
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;

namespace AXSharp.Connector;

/// <summary>
///     Provides extensions for <see cref="DummyConnector" /> and <see cref="DummyConnectorFactory" />.
/// </summary>
public static class DummyConnectorExtensions
{
    /// <summary>
    ///     Create new adapter for <see cref="DummyConnector" />.
    /// </summary>
    /// <param name="adapterBuilder">Adapter builder.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Connector adapter for DummyConnector.</returns>
    public static ConnectorAdapter CreateDummy(this ConnectorAdapterBuilder adapterBuilder)
    {
        if (adapterBuilder == null) throw new ArgumentNullException(nameof(adapterBuilder));
        return new ConnectorAdapter(typeof(DummyConnectorFactory)) { Parameters = new object[] { } };
    }
}