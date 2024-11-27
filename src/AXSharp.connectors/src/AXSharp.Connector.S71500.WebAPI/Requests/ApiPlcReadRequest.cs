// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Connector.S71500.WebApi;

/// <summary>
///     Provides handler for read request.
/// </summary>
internal class ApiPlcReadRequest : ApiRequestBase
{
    /// <summary>
    ///     Creates new instance of <see cref="ApiPlcReadRequest" />.
    /// </summary>
    /// <param name="symbol">Plc symbol to be read from the PLC</param>
    public ApiPlcReadRequest(string symbol)
        : base("PlcProgram.Read", "2.0", WebApiConnector.GetId)
    {
        Params = new Dictionary<string, object>
        {
            ["var"] = symbol
        };
    }
}