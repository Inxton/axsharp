// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector.ValueTypes;
using Newtonsoft.Json.Linq;

namespace AXSharp.Connector.S71500.WebApi;

/// <summary>
/// Represents wrapper for a PLC LTIME variable.
/// </summary>
public class WebApiLTime : OnlinerLTime, IWebApiPrimitive
{
    private readonly WebApiConnector _webApiConnector;

    /// <inheritdoc />
    public WebApiLTime()
    {
        _webApiConnector = new WebApiConnector();
    }

    /// <inheritdoc />
    public WebApiLTime(ITwinObject parent,
        string readableTail,
        string symbolTail)
        : base(parent,
            readableTail,
            symbolTail)
    {
        _webApiConnector = WebApiConnector.Cast(parent.GetConnector());
    }

    private ApiPlcWriteRequest? _plcWriteRequestData;
    private ApiPlcReadRequest? _plcReadRequestData;

    /// <inheritdoc />
    ApiPlcReadRequest IWebApiPrimitive.PeekPlcReadRequestData => _plcReadRequestData ?? WebApiConnector.CreateReadRequest(Symbol, _webApiConnector.DBName);

    /// <inheritdoc />
    ApiPlcWriteRequest IWebApiPrimitive.PeekPlcWriteRequestData => _plcWriteRequestData ?? WebApiConnector.CreateWriteRequest(Symbol, CyclicToWrite, _webApiConnector.DBName);

    /// <inheritdoc />
    ApiPlcReadRequest IWebApiPrimitive.PlcReadRequestData
    {
        get
        {
            _plcReadRequestData = WebApiConnector.CreateReadRequest(Symbol, _webApiConnector.DBName);
            return _plcReadRequestData;
        }

    }
    
    /// <inheritdoc />
    ApiPlcWriteRequest IWebApiPrimitive.PlcWriteRequestData
    {
        get
        {
            // TODO: review this casting to string... reason: there is some problem while creating request from long.
            _plcWriteRequestData = WebApiConnector.CreateWriteRequest(Symbol, ToMicroSeconds(CyclicToWrite), _webApiConnector.DBName);
            return _plcWriteRequestData;
        }
    }

    /// <inheritdoc />
    public void Read(string value)
    {
        if (long.TryParse(value, out var val))
        {
            UpdateRead(TimeSpan.FromMilliseconds(ToMilliseconds(val)));
        }
    }


    private long ToMilliseconds(long nanoseconds)
    {
        return nanoseconds / 1000000L;
    }

    private long ToNanoseconds(long nanoseconds)
    {
        return nanoseconds * 1000000L;
    }

    private string ToMicroSeconds(TimeSpan value)
    {
        return ToNanoseconds((long)value.TotalMilliseconds).ToString();
    }

    /// <inheritdoc />
    public override async Task<TimeSpan> GetAsync()
    {
        return await _webApiConnector.ReadAsync<TimeSpan>(this);
    }

    /// <inheritdoc />
    public override async Task<TimeSpan> SetAsync(TimeSpan value)
    {
        return await _webApiConnector.WriteAsync(this, value);
    }
}