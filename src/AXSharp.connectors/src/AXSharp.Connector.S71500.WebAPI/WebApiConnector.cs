// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AXSharp.Connector.ValueTypes;
using Polly;
using Polly.Retry;
using Serilog;
using Serilog.Events;
using Siemens.Simatic.S7.Webserver.API.Exceptions;
using Siemens.Simatic.S7.Webserver.API.Models.Responses;
using Siemens.Simatic.S7.Webserver.API.Services;
using Siemens.Simatic.S7.Webserver.API.Services.IdGenerator;
using Siemens.Simatic.S7.Webserver.API.Services.RequestHandling;
using System.Diagnostics;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace AXSharp.Connector.S71500.WebApi;

/// <summary>
/// Provides connector to mediate connection with AX# twins over WebAPI connection.
/// </summary>
public class WebApiConnector : Connector
{
    private volatile object _locker = new();


    /// <summary>
    /// Creates new instance of <see cref="WebApiConnector"/>.
    /// </summary>
    /// <param name="ipAddress">Target's IP address.</param>
    /// <param name="userName">User name.</param>
    /// <param name="password">Password.</param>
    /// <param name="customServerCertHandler">Customized server certificate handler.</param>
    /// <param name="ignoreSSLErros">When set to true ssl error are ignored.</param>
    /// <param name="dbName">Root DB name (AX uses 'TGlobalVariablesDB')</param>
    public WebApiConnector(string ipAddress, string userName, string password,
        Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>? customServerCertHandler,
        bool ignoreSSLErros,
        eTargetProjectPlatform platform = eTargetProjectPlatform.SIMATICAX,
        string dbName = "\"TGlobalVariablesDB\"")
    {
        IPAddress = ipAddress;
        DBName = dbName;
        TargetPlatform = platform;
        UserName = userName;
        UserPassword = password;

        if (ignoreSSLErros)
            ServerCertificateCallback.CertificateCallback =
                (sender, cert, chain, sslPolicyErrors) => true;

        var splitter = new ApiRequestSplitterByBytes();

        var serviceFactory = new ApiStandardServiceFactory();
        var client = serviceFactory.GetHttpClient(ipAddress, UserName, UserPassword);
        requestHandler = new ApiHttpClientRequestHandler(client,
            new ApiRequestFactory(ReqIdGenerator, RequestParameterChecker), ApiResponseChecker, splitter);

        requestHandler.Init();

        _throttle = new SemaphoreSlim(this.ConcurrentRequestMaxCount);

        NumberOfInstances++;
    }

    /// <summary>
    /// Creates new instance of <see cref="WebApiConnector"/>.
    /// </summary>
    /// <param name="ipAddress">Target's IP address.</param>
    /// <param name="userName">User name.</param>
    /// <param name="password">Password.</param>
    /// <param name="ignoreSSLErros">When set to 'true' the connection will ignore SSL errors.</param>
    /// <param name="dbName">Root DB name (AX uses 'TGlobalVariablesDB')</param>
    public WebApiConnector(string ipAddress, string userName, string password, bool ignoreSSLErros,
        eTargetProjectPlatform platform = eTargetProjectPlatform.SIMATICAX,
        string dbName = "\"TGlobalVariablesDB\"")
    {
        IPAddress = ipAddress;
        DBName = dbName;
        TargetPlatform = platform;
        UserName = userName;
        UserPassword = password;

        if (ignoreSSLErros)
            ServerCertificateCallback.CertificateCallback =
                (sender, cert, chain, sslPolicyErrors) => true;

        var serviceFactory = new ApiStandardServiceFactory();
        Client = serviceFactory.GetHttpClient(ipAddress, UserName, UserPassword ?? string.Empty);

        var splitter = new ApiRequestSplitterByBytes();
        requestHandler = new ApiHttpClientRequestHandler(Client,
            new ApiRequestFactory(ReqIdGenerator, RequestParameterChecker), ApiResponseChecker, splitter);

        requestHandler.Init();

        requestHandler.ApiLogout();
        requestHandler.ApiLogin(UserName, UserPassword ?? string.Empty, true);

        _throttle = new SemaphoreSlim(this.ConcurrentRequestMaxCount);

        NumberOfInstances++;
    }

    /// <inheritdoc />
#pragma warning disable CS8618
    internal WebApiConnector()
#pragma warning restore CS8618
    {
    }

    private readonly HttpClient Client;

    private GUIDGenerator ReqIdGenerator { get; } = new();
    private ApiRequestParameterChecker RequestParameterChecker { get; } = new();
    private ApiResponseChecker ApiResponseChecker { get; } = new();

    private readonly ApiHttpClientRequestHandler requestHandler;

    private ApiHttpClientRequestHandler RequestHandler => requestHandler;

    private readonly object concurentCountMutex = new();

    [Obsolete()]
    private async Task AntiThrottling(IEnumerable<ITwinPrimitive> primitives)
    {
        var concurrent = 0;

        do
        {
            lock (concurentCountMutex)
            {
                concurrent = concurrentRequest;
            }

            if (concurrent >= ConcurrentRequestMaxCount) await Task.Delay(ConcurrentRequestDelay);
        } while (concurrent >= ConcurrentRequestMaxCount);

        if (concurrent > ConcurrentRequestMaxCount) throw new Exception($"Too many requests {concurrent}");

        lock (concurentCountMutex)
        {
            concurrentRequest = concurrentRequest + 1;
        }
    }

    [Obsolete()]
    private void ReleaseConcurrent(IEnumerable<ITwinPrimitive> primitives)
    {
        lock (concurentCountMutex)
        {
            concurrentRequest = concurrentRequest - 1;
        }
    }

    /// <summary>
    ///     Gets number of instance of WebAPI connector in this application.
    /// </summary>
    public static int NumberOfInstances { get; private set; }

    /// <summary>
    ///     Get the address of the target system.
    /// </summary>
    internal string IPAddress { get; }

    internal string UserName { get; }
    internal string UserPassword { get; }

    internal string DBName { get; }

    /// <inheritdoc />
    public override Connector BuildAndStart()
    {
        StartReadWriteOps();
        return this;
    }

    public async Task ReLoginToConnectorApi()
    {
        var Conncected = false;

        IsRwLoopSuspended = true; // suspen cyclic R/W operations

        do
        {
            Task.Delay(2000).Wait(); // wait
            try
            {
                requestHandler.ReLogin(UserName, UserPassword ?? string.Empty, true);
                Logger.Warning($"Plc {IPAddress} Api ReLogin Done!");

                Siemens.Simatic.S7.Webserver.API.Enums.ApiPlcOperatingMode mode;

                do
                {
                    Task.Delay(2000).Wait();
                    mode = requestHandler.PlcReadOperatingMode().Result;

                    Logger.Warning($"Plc {IPAddress} Has mode {mode.ToString()}!");
                } while (mode != Siemens.Simatic.S7.Webserver.API.Enums.ApiPlcOperatingMode.Run);

                Conncected = true;
                IsRwLoopSuspended = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } while (!Conncected);
    }

    internal void HandleCommFailure(Exception exception, string description, IEnumerable<ITwinPrimitive> primitives,
        ApiBulkResponse response, IEnumerable<ApiRequestBase> originalRequest)
    {
        var firstFailedItemParams = string.Empty;

        var WasPermitionChanged = false;

        foreach (var errorResponse in response.ErrorResponses)
        {
            var failedItem = originalRequest.FirstOrDefault(p => p.Id == errorResponse.Id);
            if (failedItem == null)
            {
                var msg =
                    $"{errorResponse.Error.Message} [{errorResponse.Error.Code}] : {string.Join(";", originalRequest.Select(p => $"Id:{p.Id}| Method : {p.Method}, {string.Join("; ", p.Params.Select(p => $"{p.Key} : {p.Value}"))}"))}";
                Logger.Error(msg);
                continue;
            }

            var failedItemParams = string.Join(";", failedItem.Params.Select(p => $"{p.Key} : {p.Value}"));
            if (string.IsNullOrEmpty(firstFailedItemParams)) firstFailedItemParams = failedItemParams;

            Logger.Error($"{failedItemParams} : {errorResponse.Error.Message} [{errorResponse.Error.Code}]");

            if (!WasPermitionChanged && errorResponse.Error.Code ==
                Siemens.Simatic.S7.Webserver.API.Enums.ApiErrorCode.PermissionDenied) WasPermitionChanged = true;
        }

        foreach (var primitive in primitives)
            primitive?.AccessStatus.Update(RwCycleCount,
                $"{description}: '{exception.Message}' [{firstFailedItemParams}] ");

        if (WasPermitionChanged) ReLoginToConnectorApi(); // Start in async task.

        switch (ExceptionBehaviour)
        {
            case CommExceptionBehaviour.Ignore:
                break;

            case CommExceptionBehaviour.ReThrow:
                throw exception;
        }
    }

    internal void HandleCommFailure<T>(Exception exception, string description, ITwinPrimitive primitive,
        ApiResultResponse<T> response)
    {
        primitive.AccessStatus.Update(RwCycleCount, $"{description}: '{exception.Message}'");

        var errorResponse = response as ApiErrorModel;
        if (errorResponse != null)
            Logger.Error($"{errorResponse.Error.Message} [{errorResponse.Error.Code}]");
        else
            Logger.Error(
                $"There was an error accessing item {primitive.Symbol}, but we were not able to determine the reason.");

        switch (ExceptionBehaviour)
        {
            case CommExceptionBehaviour.Ignore:
                break;

            case CommExceptionBehaviour.ReThrow:
                throw exception;
        }
    }

    private Stopwatch stopwatch = new();

    private volatile int concurrentRequest = 0;


    private AsyncRetryPolicy _retryPolicy;

    // TODO: This has been added for additional resiliency.
    // when target system responds with HTTP exception 'premature end...' the connector will recover.
    // it may take several second before the exception arises.
    private AsyncRetryPolicy RetryPolicy
    {
        get
        {
            return _retryPolicy ??= Policy
                .Handle<HttpRequestException>()
                .RetryAsync(5, (exception, i) =>
                {
                    Log.Logger.Error(
                        $"{exception.Message} : {exception.InnerException?.Message} | Number of concurrent requests {concurrentRequest}");
                    Task.Delay(100).Wait();
                });
        }
    }


    private readonly SemaphoreSlim _throttle;

    private async Task AntiThrottling()
    {
        await _throttle.WaitAsync();
    }

    private void ReleaseConcurrent()
    {
        _throttle.Release();
    }

    /// <inheritdoc />
    public override async Task ReadBatchAsync(IEnumerable<ITwinPrimitive>? primitives, eAccessPriority priority = eAccessPriority.Normal)
    {
        if (!primitives.Any()) return;

        var responseData = new ApiBulkResponse();
        var twinPrimitives = primitives as ITwinPrimitive[] ?? primitives.ToArray();

        try
        {
            if (Logger.IsEnabled(LogEventLevel.Debug)) stopwatch.Restart();

            if (Logger.IsEnabled(LogEventLevel.Verbose))
                Logger
                    .Verbose("{vars}",
                        string.Join("\n",
                            twinPrimitives.Select(p =>
                                $"{((OnlinerBase)p).Symbol} | pollings: [{string.Join(";", ((OnlinerBase)p).PollingHolders.Select(a => a.Key.ToString()))}]")));

            await AntiThrottling();

            var webApiPrimitives = twinPrimitives.Cast<IWebApiPrimitive>().Distinct().ToArray();


            var requestSegment = webApiPrimitives;
            var apiPrimitives = requestSegment as IWebApiPrimitive[] ?? requestSegment.ToArray();
            var segment = apiPrimitives.Select(p => p.PlcReadRequestData).ToList();
            try
            {
                await RetryPolicy.ExecuteAsync(async () => responseData = await RequestHandler.ApiBulkAsync(segment));

                // This is needed when unassigned CHAR or WCHAR are read; they won't be present in the response.
                if (responseData.SuccessfulResponses.Count() != apiPrimitives.Length)
                {
                    foreach (var response in responseData.SuccessfulResponses)
                    {
                        var a = apiPrimitives.FirstOrDefault(p => p.PeekPlcReadRequestData.Id == response.Id);
                        if (a == null) continue;
                        a.Read(response.Result.ToString());
                        a.AccessStatus.Update(RwCycleCount);
                    }
                }
                else
                {
                    var position = 0;
                    apiPrimitives.ToList()
                        .ForEach(p =>
                        {
                            p.Read(responseData.SuccessfulResponses.ElementAt(position++).Result.ToString());
                            p.AccessStatus.Update(RwCycleCount);
                        });
                }
            }
            catch (ApiBulkRequestException apiException)
            {
                HandleCommFailure(apiException, "Batch read failed.", apiPrimitives, apiException.BulkResponse,
                    apiPrimitives.Select(p => p.PeekPlcReadRequestData));
            }
            catch (Exception e)
            {
                HandleCommFailure(e, "Batch read failed.", apiPrimitives, responseData,
                    apiPrimitives.Select(p => p.PeekPlcReadRequestData));
            }
        }
        finally
        {
            ReleaseConcurrent();
        }

        if (Logger.IsEnabled(LogEventLevel.Debug))
            Logger.Debug($"Bulk reading: {twinPrimitives.Count()} items read in {stopwatch.ElapsedMilliseconds} ms.");
    }

    /// <inheritdoc />
    public override async Task WriteBatchAsync(IEnumerable<ITwinPrimitive>? primitives, eAccessPriority priority = eAccessPriority.Normal)
    {
        if (primitives == null || !primitives.Any()) return;

        try
        {
            await AntiThrottling();

            var responseData = new ApiBulkResponse();
            var twinPrimitives = primitives as ITwinPrimitive[] ?? primitives.ToArray();

            if (twinPrimitives.Any())
                if (Logger.IsEnabled(LogEventLevel.Verbose))
                    Logger.Verbose($"Bulk writing: {twinPrimitives.Count()} items.");

            var webApiPrimitives = twinPrimitives.Cast<IWebApiPrimitive>().Distinct().ToArray();


            var requestSegment = webApiPrimitives;
            var apiPrimitives = requestSegment as IWebApiPrimitive[] ?? requestSegment.ToArray();
            try
            {
                await RetryPolicy.ExecuteAsync(async () =>
                    await RequestHandler.ApiBulkAsync(apiPrimitives.Select(p => p.PlcWriteRequestData)));
            }
            catch (ApiBulkRequestException apiException)
            {
                HandleCommFailure(apiException, "Batch write failed.", twinPrimitives, apiException.BulkResponse,
                    apiPrimitives.Select(p => p.PeekPlcWriteRequestData));
            }
            catch (Exception e)
            {
                HandleCommFailure(e, "Batch write failed.", twinPrimitives, responseData,
                    apiPrimitives.Select(p => p.PeekPlcWriteRequestData));
            }
        }
        finally
        {
            ReleaseConcurrent();
        }
    }

    /// <inheritdoc />
    public override async void ReloadConnector()
    {
        await Task.Run(() => true);
    }

    internal new void ClearPeriodicReadSet()
    {
        base.ClearPeriodicReadSet();
        var hitCount = RwCycleCount;
        //Task.Delay(300).Wait(); //TODO: we have to address this differently... preventing concurrency...
    }

    internal async Task<(T result, ApiResultResponse<T> response)> ReadAsync<T>(string symbol)
    {
        if (symbol.StartsWith("\""))
        {
            var response = await RequestHandler.PlcProgramReadAsync<T>($"{symbol}");
            return (response.Result, response);
        }
        else
        {
            var response = await RequestHandler.PlcProgramReadAsync<T>($"{DBName}.{symbol}");
            return (response.Result, response);
        }
    }

    internal async Task<T> ReadAsync<T>(IWebApiPrimitive primitive)
    {
        await ReadBatchAsync(new IWebApiPrimitive[] { primitive });
        return ((OnlinerBase<T>)primitive).LastValue;
    }


    internal async Task<T> ReadAsync<T>(IWebApiPrimitive primitive, T value)
    {
        await ReadBatchAsync(new IWebApiPrimitive[] { primitive });
        return ((OnlinerBase<T>)primitive).LastValue;
    }

    internal async Task<T> WriteAsync<T>(IWebApiPrimitive primitive, T value)
    {
        ((OnlinerBase<T>)primitive).SetValueToWrite(value);
        await WriteBatchAsync(new List<IWebApiPrimitive>() { primitive });
        return value;
    }

    private static volatile object mutex = new();
    private static int _id;

    internal static string GetId
    {
        get
        {
            lock (mutex)
            {
                return _id++.ToString(); // TODO: Consider something more appropriate here.
            }
        }
    }

    internal static ApiPlcReadRequest CreateReadRequest(string symbol, string root = "\"TGlobalVariablesDB\"")
    {
        if (string.IsNullOrEmpty(root))
            return new ApiPlcReadRequest($"{symbol}");
        else
            return new ApiPlcReadRequest($"{root}.{symbol}");
    }

    internal static ApiPlcWriteRequest CreateWriteRequest(string symbol, object value,
        string root = "\"TGlobalVariablesDB\"")
    {
        if (string.IsNullOrEmpty(root))
            return new ApiPlcWriteRequest($"{symbol}", value);
        else
            return new ApiPlcWriteRequest($"{root}.{symbol}", value);
    }

    internal static WebApiConnector Cast(Connector connector)
    {
        return connector as WebApiConnector ?? new WebApiConnector();
    }

    internal override async Task ReadBatchAsyncCyclic(IEnumerable<ITwinPrimitive> primitives,
        eAccessPriority priority = eAccessPriority.Normal)
    {
        await ReadBatchAsync(primitives, priority);
    }

    internal override async Task WriteBatchAsyncCyclic(IEnumerable<ITwinPrimitive> primitives, eAccessPriority priority = eAccessPriority.Normal)
    {
        await WriteBatchAsync(primitives);
    }

    public eTargetProjectPlatform TargetPlatform { get; } = eTargetProjectPlatform.SIMATICAX;

    /// <inherits/>    
    public override string TargetPlatformMoniker
    {
        get
        {
            switch (TargetPlatform)
            {
                case eTargetProjectPlatform.SIMATICAX:
                    return "ax";
                case eTargetProjectPlatform.TIAPORTAL:
                    return "tia";
                default:
                    return TargetPlatform.ToString();
            }
        }
    }
}