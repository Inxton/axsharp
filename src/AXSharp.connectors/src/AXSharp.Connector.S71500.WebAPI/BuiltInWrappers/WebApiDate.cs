// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector.S71500.WebApi;

/// <summary>
/// Represents wrapper for a PLC DATE variable.
/// </summary>
public class WebApiDate : OnlinerDate, IWebApiPrimitive
{
    private readonly WebApiConnector _webApiConnector;

    /// <inheritdoc />
    public WebApiDate()
    {
        _webApiConnector = new WebApiConnector();
    }

    /// <inheritdoc />
    public WebApiDate(ITwinObject parent,
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
            switch (_webApiConnector.TargetPlatform)
            {
                case eTargetProjectPlatform.TIAPORTAL:
                    _plcWriteRequestData = WebApiConnector.CreateWriteRequest(Symbol, GetFromDateTIA(CyclicToWrite), _webApiConnector.DBName);
                    break;
                case eTargetProjectPlatform.SIMATICAX:
                    _plcWriteRequestData = WebApiConnector.CreateWriteRequest(Symbol, GetFromDate(CyclicToWrite), _webApiConnector.DBName);
                    break;
                default:
                    _plcWriteRequestData = WebApiConnector.CreateWriteRequest(Symbol, GetFromDate(CyclicToWrite), _webApiConnector.DBName);
                    break;
            }

            return _plcWriteRequestData;
        }
    }

    /// <inheritdoc />
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
        if (long.TryParse(value, out var val))
        {
            return GetFromBinary(val);
        }

        return DateOnly.MinValue;
    }

    private DateOnly GetFromBinary(long value)
    {
        switch (_webApiConnector.TargetPlatform)
        {
            case eTargetProjectPlatform.TIAPORTAL:
                int val = ((int)value) - 1;
                return DateOnly.FromDayNumber(val).AddYears(1989);

            case eTargetProjectPlatform.SIMATICAX:
                var valAx = value / 100;
                return valAx.AdjustForLeapDate();
            
            default:
                var valdef = value / 100;
                return DateOnly.FromDateTime(DateTime.FromBinary(valdef).AddYears(1969));
        }
    }

    private long GetFromDateTIA(DateOnly date)
    {
        if (date <= TIAMinValue)
            date = TIAMinValue;

        var retval = date.ToDateTime(TimeOnly.MinValue) - TIAMinValue.ToDateTime(TimeOnly.MinValue);

        return (long)retval.TotalDays;
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