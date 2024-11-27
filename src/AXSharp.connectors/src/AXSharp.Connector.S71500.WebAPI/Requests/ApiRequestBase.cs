// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Siemens.Simatic.S7.Webserver.API.Models.Requests;

namespace AXSharp.Connector.S71500.WebApi;

/// <summary>
///     Provides base class for the requests.
/// </summary>
internal abstract class ApiRequestBase : ApiRequest
{
    /// <summary>
    ///     Creates new instance of <see cref="ApiRequestBase" />
    /// </summary>
    /// <param name="method">API method name.</param>
    /// <param name="jsonRpc">RPC version</param>
    /// <param name="id">API reguest ID</param>
    /// <param name="requestParams">Request parameters.</param>
    protected ApiRequestBase(string method, string jsonRpc, string id, Dictionary<string, object>? requestParams = null)
        : base(method, jsonRpc, id, requestParams)
    {
    }
}