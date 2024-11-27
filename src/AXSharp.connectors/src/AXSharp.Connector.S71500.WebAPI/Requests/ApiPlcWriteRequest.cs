// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Connector.S71500.WebApi;

/// <summary>
///     Provides handler for PLC write requests.
/// </summary>
/// <typeparam name="T">Base type</typeparam>
internal class ApiPlcWriteRequest<T> : ApiRequestBase
{
    /// <summary>
    ///     Creates new instance of <see cref="ApiPlcWriteRequest" />
    /// </summary>
    /// <param name="symbol">Plc program symbol to be written.</param>
    /// <param name="value">Value to be written</param>
    public ApiPlcWriteRequest(string symbol, T value)
        : base("PlcProgram.Write", "2.0", WebApiConnector.GetId)
    {
        Params = new Dictionary<string, object>
        {
            ["var"] = symbol,
            ["value"] = value
        };
    }
}

/// <summary>
///     Provided non generic handler for PLC write requests.
/// </summary>
internal class ApiPlcWriteRequest : ApiPlcWriteRequest<object>
{
    /// <summary>
    ///     Creates new instance of <see cref="ApiPlcWriteRequest" />
    /// </summary>
    /// <param name="symbol">Plc program symbol to be written.</param>
    /// <param name="value">Value to be written.</param>
    public ApiPlcWriteRequest(string symbol, object value) : base(symbol, value)
    {
        Params = new Dictionary<string, object>
        {
            ["var"] = symbol,
            ["value"] = value
        };
    }
}