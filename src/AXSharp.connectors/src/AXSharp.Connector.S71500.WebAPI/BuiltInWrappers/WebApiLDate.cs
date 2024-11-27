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
/// Represents wrapper for a PLC LDATE variable.
/// </summary>
public class WebApiLDate : OnlinerDate, IWebApiPrimitive
{
    private readonly WebApiConnector _webApiConnector;

    /// <inheritdoc />
    public WebApiLDate()
    {
        _webApiConnector = new WebApiConnector();
    }

    /// <inheritdoc />
    public WebApiLDate(ITwinObject parent,
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

    public void Read(string value)
    {
        UpdateRead(GetFromBinary(value));
    }

    /// <inheritdoc />
    public override async Task<DateOnly> GetAsync()
    {
        return await _webApiConnector.ReadAsync<DateOnly>(this);
    }


    private DateOnly GetFromBinary(string value)
    {
        return long.TryParse(value, out var val) ? GetFromBinary(val) : DateOnly.MinValue;
    }

    private DateOnly GetFromBinary(long value)
    {
        var val = value / 100;
        return val.AdjustForLeapDate(); // DateOnly.FromDateTime(DateTime.FromBinary(val).AddYears(1969));
    }

    private string GetFromDate(DateOnly date)
    {
        if (date <= MinValue)
            date = MinValue;

        var retval = date.ToDateTime(TimeOnly.MinValue) - MinValue.ToDateTime(TimeOnly.MinValue);
        return (new DateTime().AddDays(retval.TotalDays).ToBinary() * 100).ToString();
    }

    /// <inheritdoc />
    public override async Task<DateOnly> SetAsync(DateOnly value)
    {
        return await _webApiConnector.WriteAsync(this, value);
    }
}