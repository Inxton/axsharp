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
/// This connector facilitates communication with Siemens S7 PLCs using WebAPI.
/// Supports priority-based access control and configurable batch operations.
/// </summary>
public class WebApiConnector : Connector
{
    private volatile object _locker = new();


    /// <summary>
    /// Creates a new instance of <see cref="WebApiConnector"/>.
    /// </summary>
    /// <param name="ipAddress">Target's IP address.</param>
    /// <param name="userName">User name for authentication.</param>
    /// <param name="password">Password for authentication.</param>
    /// <param name="customServerCertHandler">Optional custom server certificate handler.</param>
    /// <param name="ignoreSSLErros">When set to true, SSL errors are ignored.</param>
    /// <param name="platform">Target project platform (default is SIMATICAX).</param>
    /// <param name="dbName">Root DB name (default is 'TGlobalVariablesDB').</param>
    /// <param name="maxConcurrentRequest">Determines max concurrent R/W requests against the controller.</param>
    /// <param name="concurrentRequestDelay">Determines delay between concurrent requests.</param>
    public WebApiConnector(string ipAddress, string userName, string password,
        Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>? customServerCertHandler,
        bool ignoreSSLErros,
        eTargetProjectPlatform platform = eTargetProjectPlatform.SIMATICAX,
        string dbName = "\"TGlobalVariablesDB\"",
        int maxConcurrentRequest = 4,
        int concurrentRequestDelay = 0)
    {
        IPAddress = ipAddress;
        DBName = dbName;
        TargetPlatform = platform;
        UserName = userName;
        UserPassword = password;
        this.ConcurrentRequestMaxCount = maxConcurrentRequest;
        this.ConcurrentRequestDelay = concurrentRequestDelay;


        if (ignoreSSLErros)
            ServerCertificateCallback.CertificateCallback =
                (sender, cert, chain, sslPolicyErrors) => true;

        var splitter = new ApiRequestSplitterByBytes();

        var serviceFactory = new ApiStandardServiceFactory();
        var client = serviceFactory.GetHttpClient(ipAddress, UserName, UserPassword);
        requestHandler = new ApiHttpClientRequestHandler(client,
            new ApiRequestFactory(ReqIdGenerator, RequestParameterChecker), ApiResponseChecker, splitter);

        requestHandler.Init();

        antiThrottlingSemaphore = new SemaphoreSlim(this.ConcurrentRequestMaxCount);


        NumberOfInstances++;
    }

    /// <summary>
    /// Creates a new instance of <see cref="WebApiConnector"/>.
    /// </summary>
    /// <param name="ipAddress">Target's IP address.</param>
    /// <param name="userName">User name for authentication.</param>
    /// <param name="password">Password for authentication.</param>
    /// <param name="ignoreSSLErros">When set to true, SSL errors are ignored.</param>
    /// <param name="platform">Target project platform (default is SIMATICAX).</param>
    /// <param name="dbName">Root DB name (default is 'TGlobalVariablesDB').</param>
    /// <param name="maxConcurrentRequest">Determines max concurrent R/W requests against the controller.</param>
    /// <param name="concurrentRequestDelay">Determines delay between concurrent requests.</param>
    public WebApiConnector(string ipAddress, string userName, string password, bool ignoreSSLErros,
        eTargetProjectPlatform platform = eTargetProjectPlatform.SIMATICAX,
        string dbName = "\"TGlobalVariablesDB\"",
        int maxConcurrentRequest = 4,
        int concurrentRequestDelay = 0)
    {
        IPAddress = ipAddress;
        DBName = dbName;
        TargetPlatform = platform;
        UserName = userName;
        UserPassword = password;
        this.ConcurrentRequestMaxCount = maxConcurrentRequest;
        this.ConcurrentRequestDelay = concurrentRequestDelay;


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

        antiThrottlingSemaphore = new SemaphoreSlim(this.ConcurrentRequestMaxCount);

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

    private bool ConnectorStarted = false;

    /// <inheritdoc />
    public override Connector BuildAndStart()
    {
        if (!ConnectorStarted)
        {
            ConnectorStarted = true;
            StartReadWriteOps();
        }
        return this;
    }

    /// <summary>
    /// Re-authenticates the connector API session.
    /// Suspends cyclic read/write operations during the re-login process.
    /// </summary>
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

    /// <summary>
    /// Handles communication failures during batch operations.
    /// Logs errors and attempts to recover from permission changes.
    /// </summary>
    /// <param name="exception">Exception that occurred.</param>
    /// <param name="description">Description of the failure.</param>
    /// <param name="primitives">Affected primitives.</param>
    /// <param name="response">API bulk response.</param>
    /// <param name="originalRequest">Original API requests.</param>
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
    private Stopwatch stopwatchWrite = new();

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


    private SemaphoreSlim antiThrottlingSemaphore;

    private async Task AntiThrottling()
    {
        await antiThrottlingSemaphore.WaitAsync();
    }

    private void ReleaseConcurrent()
    {
        antiThrottlingSemaphore.Release();
    }

    /// <summary>
    /// Reads a batch of primitives asynchronously with the specified priority and batch settings.
    /// </summary>
    /// <param name="primitives">Collection of primitives to read.</param>
    /// <param name="priority">Access priority level that determines batch processing parameters.</param>
    /// <param name="chunkSize">Override for the number of items to process in each chunk. If not specified, uses the priority's default.</param>
    /// <param name="interChunkDelay">Override for the delay between chunks in milliseconds. If not specified, uses the priority's default.</param>
    /// <returns>A task representing the asynchronous read operation.</returns>
    public override async Task ReadBatchAsync(IEnumerable<ITwinPrimitive> primitives, eAccessPriority priority = eAccessPriority.Normal, int chunkSize = 250, int interChunkDelay = 250)
    {
        if (!primitives.Any()) return;

        var responseData = new ApiBulkResponse();
        var twinPrimitives = primitives as ITwinPrimitive[] ?? primitives.ToArray();

        if (Logger.IsEnabled(LogEventLevel.Debug)) stopwatch.Restart();

        if (Logger.IsEnabled(LogEventLevel.Verbose))
        {
            Logger
                .Verbose(
                    "Read priority {Priority} ChunkSize {ChunkSize} InterChunkDelay {InterChunkDelay} ms\n{Vars}",
                    priority,
                    chunkSize,
                    interChunkDelay,
                    string.Join(" ; ",
                        twinPrimitives.Select(p =>
                        {
                            var onliner = (OnlinerBase)p;
                            var holders = onliner.PollingHolders
                                .Select((a, idx) => $" [{idx}] {a.Key}")
                                .DefaultIfEmpty("[None Holder]");
                            return $"{onliner.Symbol} => {string.Join(";", holders)}";
                        })
                    )
                );
        }

        var webApiPrimitives = twinPrimitives.Cast<IWebApiPrimitive>().Distinct().ToArray();

        chunkSize = priority == eAccessPriority.Custom ? chunkSize : BatchSettings[priority].chunkSize ?? webApiPrimitives.Length;
        interChunkDelay = priority == eAccessPriority.Custom ? interChunkDelay : BatchSettings[priority].interChunkDelay ?? 0;

        var chunks = webApiPrimitives.Select((x, i) => new { x, i })
                                     .GroupBy(x => x.i / chunkSize)
                                     .Select(g => g.Select(x => x.x).ToArray());

        foreach (var chunk in chunks)
        {
            var requestSegment = chunk;
            var apiPrimitives = requestSegment as IWebApiPrimitive[] ?? requestSegment.ToArray();
            var segment = apiPrimitives.Select(p => p.PlcReadRequestData).ToList();

            try
            {
                await AntiThrottling();

                await RetryPolicy.ExecuteAsync(async () => responseData = await RequestHandler.ApiBulkAsync(segment));

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

                if (interChunkDelay > 0)
                {
                    await Task.Delay(interChunkDelay);
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
            finally
            {
                ReleaseConcurrent();
            }
        }

        if (Logger.IsEnabled(LogEventLevel.Debug))
            Logger.Debug("Bulk reading: {ItemsCount} items read in {ElapsedMs} ms.", twinPrimitives.Count(), stopwatch.ElapsedMilliseconds);

    }

    /// <summary>
    /// Writes a batch of primitives asynchronously with the specified priority and batch settings.
    /// </summary>
    /// <param name="primitives">Collection of primitives to write.</param>
    /// <param name="priority">Access priority level that determines batch processing parameters.</param>
    /// <param name="chunkSize">Override for the number of items to process in each chunk. If not specified, uses the priority's default.</param>
    /// <param name="interChunkDelay">Override for the delay between chunks in milliseconds. If not specified, uses the priority's default.</param>
    /// <returns>A task representing the asynchronous write operation.</returns>
    public override async Task WriteBatchAsync(IEnumerable<ITwinPrimitive> primitives, eAccessPriority priority = eAccessPriority.Normal, int chunkSize = 250, int interChunkDelay = 250)
    {
        if (primitives == null || !primitives.Any()) return;

        var responseData = new ApiBulkResponse();
        var twinPrimitives = primitives as ITwinPrimitive[] ?? primitives.ToArray();

        if (Logger.IsEnabled(LogEventLevel.Debug)) stopwatchWrite.Restart();

        if (twinPrimitives.Any())
        { 
            if (Logger.IsEnabled(LogEventLevel.Verbose))
                Logger.Verbose("Bulk writing: {ItemsCount} items.", twinPrimitives.Count());
        }

        var webApiPrimitives = twinPrimitives.Cast<IWebApiPrimitive>().Distinct().ToArray();

        chunkSize = priority == eAccessPriority.Custom ? chunkSize : BatchSettings[priority].chunkSize ?? webApiPrimitives.Length;
        interChunkDelay = priority == eAccessPriority.Custom ? interChunkDelay : BatchSettings[priority].interChunkDelay ?? 0;

        var chunks = webApiPrimitives.Select((x, i) => new { x, i })
                                     .GroupBy(x => x.i / chunkSize)
                                     .Select(g => g.Select(x => x.x).ToArray());

        foreach (var chunk in chunks)
        {
            var requestSegment = chunk;
            var apiPrimitives = requestSegment as IWebApiPrimitive[] ?? requestSegment.ToArray();

            try
            {
                await AntiThrottling();
                await RetryPolicy.ExecuteAsync(async () =>
                    await RequestHandler.ApiBulkAsync(apiPrimitives.Select(p => p.PlcWriteRequestData)));

                if (interChunkDelay > 0)
                {
                    await Task.Delay(interChunkDelay);
                }
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
            finally
            {
                ReleaseConcurrent();
            }
        }

        if (Logger.IsEnabled(LogEventLevel.Debug))
            Logger.Debug("Bulk writing: {ItemsCount} items written in {ElapsedMs} ms.", twinPrimitives.Count(), stopwatchWrite.ElapsedMilliseconds);

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

    /// <summary>
    /// Reads a single value from the PLC asynchronously.
    /// </summary>
    /// <typeparam name="T">Type of the value to be read.</typeparam>
    /// <param name="symbol">Symbol representing the PLC variable.</param>
    /// <returns>Tuple containing the result and the API response.</returns>
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

    /// <summary>
    /// Writes a single value to the PLC asynchronously.
    /// </summary>
    /// <typeparam name="T">Type of the value to be written.</typeparam>
    /// <param name="primitive">Primitive representing the PLC variable.</param>
    /// <param name="value">Value to be written.</param>
    /// <returns>The written value.</returns>
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

    /// <summary>
    /// Creates a read request for a PLC variable.
    /// </summary>
    /// <param name="symbol">Symbol representing the PLC variable.</param>
    /// <param name="root">Root DB name (default is 'TGlobalVariablesDB').</param>
    /// <returns>API PLC read request.</returns>
    internal static ApiPlcReadRequest CreateReadRequest(string symbol, string root = "\"TGlobalVariablesDB\"")
    {
        if (string.IsNullOrEmpty(root))
            return new ApiPlcReadRequest($"{symbol}");
        else
            return new ApiPlcReadRequest($"{root}.{symbol}");
    }

    /// <summary>
    /// Creates a write request for a PLC variable.
    /// </summary>
    /// <param name="symbol">Symbol representing the PLC variable.</param>
    /// <param name="value">Value to be written.</param>
    /// <param name="root">Root DB name (default is 'TGlobalVariablesDB').</param>
    /// <returns>API PLC write request.</returns>
    internal static ApiPlcWriteRequest CreateWriteRequest(string symbol, object value,
        string root = "\"TGlobalVariablesDB\"")
    {
        if (string.IsNullOrEmpty(root))
            return new ApiPlcWriteRequest($"{symbol}", value);
        else
            return new ApiPlcWriteRequest($"{root}.{symbol}", value);
    }

    /// <summary>
    /// Casts a generic connector to a <see cref="WebApiConnector"/>.
    /// </summary>
    /// <param name="connector">Generic connector instance.</param>
    /// <returns>WebApiConnector instance.</returns>
    internal static WebApiConnector Cast(Connector connector)
    {
        return connector as WebApiConnector ?? new WebApiConnector();
    }

    internal override async Task ReadBatchAsyncCyclic(IEnumerable<ITwinPrimitive> primitives,
        eAccessPriority priority = eAccessPriority.Normal, int chunkSize = 250, int interChunkDelay = 250)
    {
        await ReadBatchAsync(primitives, priority, chunkSize, interChunkDelay);
    }

    internal override async Task WriteBatchAsyncCyclic(IEnumerable<ITwinPrimitive> primitives, eAccessPriority priority = eAccessPriority.Normal, int chunkSize = 250, int interChunkDelay = 250)
    {
        await WriteBatchAsync(primitives, priority, chunkSize, interChunkDelay);
    }

    public eTargetProjectPlatform TargetPlatform { get; } = eTargetProjectPlatform.SIMATICAX;

    /// <summary>
    /// Gets the target platform moniker.
    /// </summary>
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