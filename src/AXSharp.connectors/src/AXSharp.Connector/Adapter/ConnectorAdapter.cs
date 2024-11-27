// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;

namespace AXSharp.Connector;

/// <summary>
///     Provides basic abstractions for twin connectors.
/// </summary>
public class ConnectorAdapter
{
    private ConnectorFactory _adapter;

    /// <summary>
    ///     Create new instance of empty <see cref="ConnectorAdapter" />
    /// </summary>
    public ConnectorAdapter()
    {
    }

    /// <summary>
    ///     Creates new instance of <see cref="ConnectorAdapter" /> for specific target controller type.
    /// </summary>
    /// <param name="onlineConnectorFactoryType">Type of adapter</param>
    public ConnectorAdapter(Type onlineConnectorFactoryType)
    {
        ConnectorFactoryType = onlineConnectorFactoryType;
    }

    /// <summary>
    ///     Gets or sets ConnectorFactory type
    /// </summary>
    protected Type ConnectorFactoryType { get; set; }

    /// <summary>
    ///     Get an instance of adapter stored in <see cref="ConnectorFactory" /> property.
    /// </summary>
    public ConnectorFactory AdapterFactory
    {
        get { return _adapter ??= Activator.CreateInstance(ConnectorFactoryType) as ConnectorFactory; }
    }

    /// <summary>
    ///     Adapter's parameters.
    /// </summary>
    public object[] Parameters { get; set; }

    /// <summary>
    ///     Creates new instance of <see cref="ConnectorAdapter" />.
    /// </summary>
    /// <param name="parameters">Connector parameters.</param>
    /// <returns></returns>
    public Connector GetConnector(object[] parameters)
    {
        var connector = AdapterFactory.CreateConnector(parameters);
        connector.ConnectorAdapter = this;
        return connector;
    }
}