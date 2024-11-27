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
/// Represents wrapper for a PLC LDATE_AND_TIME variable.
/// </summary>
public class WebApiLDateTime : OnlinerLDateTime, IWebApiPrimitive
{
    private readonly WebApiConnector _webApiConnector;

    /// <inheritdoc />
    public WebApiLDateTime()
    {
        _webApiConnector = new WebApiConnector();
    }

    /// <inheritdoc />
    public WebApiLDateTime(ITwinObject parent,
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
            _plcWriteRequestData = WebApiConnector.CreateWriteRequest(Symbol, GetFromDate(CyclicToWrite), _webApiConnector.DBName);
            return _plcWriteRequestData;
        }
    }

    /// <inheritdoc />
    public void Read(string value)
    {
        UpdateRead(GetFromBinary(value));
    }

    /// <inheritdoc />
    public override async Task<DateTime> GetAsync()
    {
        return await _webApiConnector.ReadAsync<DateTime>(this);
    }

    private DateTime GetFromBinary(string value)
    {
        return long.TryParse(value, out var val) ? GetFromBinary(val) : DateTime.MinValue;
    }

    private DateTime GetFromBinary(long val)
    {
        var dt = val / 100;
        return dt.AdjustForLeapDateTime(); // DateTime.FromBinary(dt).AddYears(1969);
    }


    private string GetFromDate(DateTime dateTime)
    {
        if (dateTime <= MinValue)
            dateTime = MinValue;

        var retval = dateTime - MinValue;
        return (new DateTime().AddMilliseconds(retval.TotalMilliseconds).ToBinary() * 100).ToString();
    }

    /// <inheritdoc />
    public override async Task<DateTime> SetAsync(DateTime value)
    {
        return await _webApiConnector.WriteAsync(this, value);
    }
}