// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Globalization;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector.S71500.WebApi;

/// <summary>
/// Represents wrapper for a PLC LTIME_OF_DAY variable.
/// </summary>
public class WebApiLTimeOfDay : OnlinerLTimeOfDay, IWebApiPrimitive
{
    private readonly WebApiConnector _webApiConnector;

    /// <inheritdoc />
    public WebApiLTimeOfDay()
    {
        _webApiConnector = new WebApiConnector();
    }

    /// <inheritdoc />
    public WebApiLTimeOfDay(ITwinObject parent,
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
                    _plcWriteRequestData = WebApiConnector.CreateWriteRequest(Symbol, CyclicToWrite.TotalNanoseconds.ToString(CultureInfo.InvariantCulture), _webApiConnector.DBName);
                    break;

                case eTargetProjectPlatform.SIMATICAX:
                    _plcWriteRequestData = WebApiConnector.CreateWriteRequest(Symbol, (CyclicToWrite.TotalMilliseconds * 1000000).ToString(CultureInfo.InvariantCulture), _webApiConnector.DBName);
                    break;

                default:
                    _plcWriteRequestData = WebApiConnector.CreateWriteRequest(Symbol, (CyclicToWrite.TotalMilliseconds * 1000000).ToString(CultureInfo.InvariantCulture), _webApiConnector.DBName);
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
                if (long.TryParse(value, out var valTia))
                {
                    UpdateRead(TimeSpan.FromMicroseconds(valTia / 1000)); // value in nanoseconds
                }
                break;

            case eTargetProjectPlatform.SIMATICAX:
                if (long.TryParse(value, out var valAx))
                {
                    UpdateRead(TimeSpan.FromMilliseconds(valAx / 1000000));
                }
                break;

            default:
                if (long.TryParse(value, out var valdef))
                {
                    UpdateRead(TimeSpan.FromMilliseconds(valdef / 1000000));
                }
                break;
        }
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