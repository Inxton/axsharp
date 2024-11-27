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
/// Represents wrapper for a PLC DATE_AND_TIME variable.
/// </summary>
public class WebApiDateTime : OnlinerDateTime, IWebApiPrimitive
{
    private readonly WebApiConnector _webApiConnector;

    /// <inheritdoc />
    public WebApiDateTime()
    {
        _webApiConnector = new WebApiConnector();
    }

    /// <inheritdoc />
    public WebApiDateTime(ITwinObject parent,
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
                    _plcWriteRequestData = WebApiConnector.CreateWriteRequest(Symbol, GetTIAJObjectFromDate(CyclicToWrite), _webApiConnector.DBName);
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
        switch (_webApiConnector.TargetPlatform)
        {
            case eTargetProjectPlatform.TIAPORTAL:
                UpdateRead(ParseFromTIAJson(value));
                break;

            case eTargetProjectPlatform.SIMATICAX:
                UpdateRead(GetFromBinary(value));
                break;

            default:
                UpdateRead(GetFromBinary(value));
                break;
        }
    }

    /// <inheritdoc />
    public override async Task<DateTime> GetAsync()
    {
        return await _webApiConnector.ReadAsync<DateTime>(this);
    }

    private DateTime ParseFromTIAJson(string value)
    {
        try
        {
            var val = Newtonsoft.Json.JsonConvert.DeserializeObject<DateTimeTia>(value);
            return new DateTime(val.year, val.month, val.day, val.hour, val.minute, (int)(val.second), (int)((val.second * 1000) % 1000), DateTimeKind.Local);
        }
        catch (Exception)
        {
            //swallow
        }

        return MinValueTIA;
    }

    private JObject GetTIAJObjectFromDate(DateTime dateTime)
    {
        if (dateTime <= MinValueTIA)
            dateTime = MinValueTIA;

        return JObject.FromObject(new DateTimeTia(dateTime));
    }

    private DateTime GetFromBinary(string value)
    {
        if (long.TryParse(value, out var val))
        {
            return GetFromBinary(val);
        }

        return DateTime.MinValue;
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

public class DateTimeTia
{
    public DateTimeTia()
    {
    }

    public DateTimeTia(DateTime fromDateTime)
    {
        this.year = fromDateTime.Year;
        this.month = fromDateTime.Month;
        this.day = fromDateTime.Day;
        this.hour = fromDateTime.Hour;
        this.minute = fromDateTime.Minute;
        this.second = (double)fromDateTime.Second;
        this.second = second + (((double)fromDateTime.Millisecond) / 1000);
    }

    public int year { get; set; }
    public int month { get; set; }
    public int day { get; set; }
    public int hour { get; set; }
    public int minute { get; set; }
    public double second { get; set; }
}