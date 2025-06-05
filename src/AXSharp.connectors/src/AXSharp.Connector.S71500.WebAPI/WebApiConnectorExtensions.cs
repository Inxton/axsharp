// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

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
    /// <param name="maxConcurrentRequest">Determines max concurrent R/W requests against the controller.</param>
    /// <param name="concurrentRequestDelay">Determines delay between concurrent requests.</param>
    /// <returns>Connector adapter for WebAPI connection.</returns>
    public static ConnectorAdapter CreateWebApi(this ConnectorAdapterBuilder adapter,
        string ipAddress, string userName, string password, bool ignoreSSLErros,
        eTargetProjectPlatform platform = eTargetProjectPlatform.SIMATICAX,
        string dbName = "\"TGlobalVariablesDB\"",
        int maxConcurrentRequest = 4,
        int concurrentRequestDelay = 0)
    {
        return new ConnectorAdapter(typeof(WebApiConnectorFactory))
            { Parameters = new object[] { ipAddress, userName, password, ignoreSSLErros, platform, dbName, maxConcurrentRequest, concurrentRequestDelay } };
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
    /// <param name="maxConcurrentRequest">Determines max concurrent R/W requests against the controller.</param>
    /// <param name="concurrentRequestDelay">Determines delay between concurrent requests.</param>
    /// <returns>Connector adapter for WebAPI connection.</returns>
    public static ConnectorAdapter CreateWebApi(this ConnectorAdapterBuilder adapter,
        string ipAddress, string userName, string password,
        Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>? customServerCertHandler,
        bool ignoreSslErrors = false,
        eTargetProjectPlatform platform = eTargetProjectPlatform.SIMATICAX,
        string dbName = "\"TGlobalVariablesDB\"",
        int maxConcurrentRequest = 4,
        int concurrentRequestDelay = 0)
    {
        return new ConnectorAdapter(typeof(WebApiConnectorFactory))
        {
            Parameters = new object[]
            {
                ipAddress, userName, password, customServerCertHandler, ignoreSslErrors, platform, dbName,
                maxConcurrentRequest, concurrentRequestDelay
            }
        };
    }

    public static DateOnly GetDateOnly(this int value)
    {
        // 1 tick = 100 ns
        // Use DateTimeKind.Utc for correct interpretation
        var dateTime = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddDays(value);

        // Return the date portion in UTC
        return DateOnly.FromDateTime(dateTime.ToUniversalTime());
    }

    public static DateOnly GetDateOnly(this long value)
    {
        // 1 tick = 100 ns
        // Use DateTimeKind.Utc for correct interpretation
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddTicks(value);

        // Return the date portion in UTC
        return DateOnly.FromDateTime(dateTime.ToUniversalTime());
    }

    public static DateTime ToUtcDateTime(this long value)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddTicks(value);
    }
}