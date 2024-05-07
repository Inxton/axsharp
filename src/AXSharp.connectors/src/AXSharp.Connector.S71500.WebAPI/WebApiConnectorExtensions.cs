// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector.S71500.WebApi;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace AXSharp.Connector;

/// <summary>
/// Provides extension methods for instantiating WebAPI connector.
/// </summary>
public static class WebApiConnectorExtensions
{
    /// <summary>
    /// Creates connector adapter for WebAPI communication.
    /// </summary>
    /// <param name="adapter">Adapter builder.</param>
    /// <param name="ipAddress">Target's IP address.</param>
    /// <param name="userName">User name.</param>
    /// <param name="password">Password.</param>
    /// <param name="ignoreSSLErros">When true connection will ignore SSL errors.</param>
    /// <param name="dbName">Name of default DB. The DB used to store all data in an AX project is 'TGlobalVariablesDB'.</param>
    /// <returns>Connector adapter for WebAPI connection.</returns>
    public static ConnectorAdapter CreateWebApi(this ConnectorAdapterBuilder adapter,
        string ipAddress, string userName, string password, bool ignoreSSLErros,
        eTargetProjectPlatform platform = eTargetProjectPlatform.SIMATICAX,
        string dbName = "\"TGlobalVariablesDB\"")
    {
        return new ConnectorAdapter(typeof(WebApiConnectorFactory))
        { Parameters = new object[] { ipAddress, userName, password, ignoreSSLErros, platform, dbName } };
    }

    /// <summary>
    /// Creates connector adapter for WebAPI communication.
    /// </summary>
    /// <param name="adapter">Adapter builder.</param>
    /// <param name="ipAddress">Target's IP address.</param>
    /// <param name="userName">User name.</param>
    /// <param name="password">Password.</param>
    /// <param name="customServerCertHandler">Customized server certificate handler.</param>
    /// <param name="ignoreSslErrors">When set to true ssl errors are ignored</param>
    /// <param name="dbName">Name of default DB. The DB used to store all data in an AX project is 'TGlobalVariablesDB'.</param>
    /// <returns>Connector adapter for WebAPI connection.</returns>
    public static ConnectorAdapter CreateWebApi(this ConnectorAdapterBuilder adapter,
        string ipAddress, string userName, string password,
        Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>? customServerCertHandler,
        bool ignoreSslErrors = false,
        eTargetProjectPlatform platform = eTargetProjectPlatform.SIMATICAX,
        string dbName = "\"TGlobalVariablesDB\"")
    {
        return new ConnectorAdapter(typeof(WebApiConnectorFactory))
        { Parameters = new object[] { ipAddress, userName, password, customServerCertHandler, ignoreSslErrors, platform, dbName } };
    }

    public static DateOnly AdjustForLeapDate(this long value)
    {
        var noLeap = DateOnly.FromDateTime(DateTime.FromBinary(value).AddYears(1969));
        var leapDays = DateTime.IsLeapYear(noLeap.Year) && ((noLeap.Month == 2 && noLeap.Day == 29) || noLeap.Month >= 3) ? -1 : 0;
        return noLeap.AddDays(leapDays);
    }

    public static DateTime AdjustForLeapDateTime(this long value)
    {
        var noLeap = DateTime.FromBinary(value).AddYears(1969);
        var leapDays = DateTime.IsLeapYear(noLeap.Year) && ((noLeap.Month == 2 && noLeap.Day == 29) || noLeap.Month >= 3) ? -1 : 0;
        return noLeap.AddDays(leapDays);
    }
}