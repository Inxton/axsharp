// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using AXSharp.Connector.Identity;
using AXSharp.Connector.Localizations;
using AXSharp.Connector.ValueTypes;
using Serilog;

namespace AXSharp.Connector;

#pragma warning disable CS0618
// Type or member is obsolete
/// <summary>
///     <para>Abstract base class provides implementation contract for the PLC connector and basic common underlying logic.</para>
/// </summary>
/// <seealso cref="RootTwinObject" />
/// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
public abstract class Connector : RootTwinObject, INotifyPropertyChanged
#pragma warning restore CS0618 // Type or member is obsolete
{
    private long _cyclicRwDuration;
    private int _errorCount;

    private ILogger _logger;
    private bool _monitorConnector;
    private long _rwCycleCount;
    private DateTime _startUpTime;

    private bool isRwLoopSuspended;
    private int readWriteCycleDealy;
    private int concurrentRequestDelay;
    private int concurrentRequestMaxCount;

    /// <summary>Creates an instance of Connector class</summary>
    /// <param name="parameters">
    ///     <para>Connection parameters</para>
    /// </param>
    /// <example>
    ///     <code language="cs">
    /// var connector = new IConnector();
    /// </code>
    /// </example>
    protected Connector(object[] parameters)
    {
        IdentityProvider = new TwinIdentityProvider(this);
        
    }

    /// <summary>
    ///     Creates new instance of Connector class
    /// </summary>
    protected Connector()
    {
        IdentityProvider = new TwinIdentityProvider(this);
    }

    /// <summary>
    /// Sets logger for this connector.
    /// >![NOTE] The default logger is implemented. Default implementation will log into console and in simple text file.
    /// </summary>
    /// <param name="logger">Logger</param>
    public void SetLoggerConfiguration(ILogger logger)
    {
        this._logger = logger;
    }


    /// <summary>
    ///     Provides logging capability for this connector.
    /// </summary>
    public ILogger Logger
    {
        get
        {
            return _logger ??= new LoggerConfiguration()
                .WriteTo
                .Console()
                .WriteTo
                .File($"connector{GetType()}.log",
                    outputTemplate: "{Timestamp:yyyy-MMM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}",
                    fileSizeLimitBytes: 100000)
                .MinimumLevel.Information()
                .CreateLogger();
        }
    }

    /// <summary>
    ///     Gets adapter for onliners.
    /// </summary>
    public ConnectorAdapter ConnectorAdapter { get; internal set; }

    /// <summary>
    /// Gets or sets how the connector should handle communication exceptions.
    /// </summary>
    public CommExceptionBehaviour ExceptionBehaviour { get; set; } = CommExceptionBehaviour.ReThrow;

    /// <summary>
    /// Gets or sets subscription mode for reading data from the controller.
    /// </summary>
    public ReadSubscriptionMode SubscriptionMode { get; set; } = ReadSubscriptionMode.AutoSubscribeUsedVariables;

    /// <summary>
    ///     Gets or sets error counter of the adapter.
    /// </summary>
    public int ErrorCount
    {
        get => _errorCount;
        set => SetField(ref _errorCount, value, "ErrorCount");
    }

    /// <summary>
    ///     Gets object identity provider.
    /// </summary>
    public TwinIdentityProvider IdentityProvider { get; }

    /// <summary>
    ///     Gets the number of Read Write cycles from the start of the connector.
    /// </summary>
    public long RwCycleCount
    {
        get => _rwCycleCount;
        protected set => SetField(ref _rwCycleCount, value, nameof(RwCycleCount));
    }

    /// <summary>
    ///     Gets the last duration of Read/Write cycle in milliseconds.
    /// </summary>
    public long CyclicRwDuration
    {
        get => _cyclicRwDuration;
        private set => SetField(ref _cyclicRwDuration, value, nameof(CyclicRwDuration));
    }

    /// <summary>
    ///     Get the connector's startup timestamp.
    /// </summary>
    public DateTime StartUpTime
    {
        get => _startUpTime;

        protected set => SetField(ref _startUpTime, value, nameof(StartUpTime));
    }

    /// <summary>
    ///     Gets or sets value indicating whether the performance of the monitor should be active.
    /// </summary>
    public bool MonitorConnector
    {
        get => _monitorConnector;
        set => SetField(ref _monitorConnector, value, nameof(MonitorConnector));
    }


    /// <summary>
    ///     Gets or sets delay between Read/Write cycles.
    /// </summary>
    public int ReadWriteCycleDelay
    {
        get
        {
            if (readWriteCycleDealy == 0) readWriteCycleDealy = 10;

            return readWriteCycleDealy;
        }

        set => SetField(ref readWriteCycleDealy, value, nameof(ReadWriteCycleDelay));
    }

    /// <summary>
    ///     Gets or sets delay between Concurrent Requests.
    ///     It is applied when ConcurrentRequestMaxCount is reached.
    /// </summary>
    public int ConcurrentRequestDelay
    {
        get
        {
            if (concurrentRequestDelay <= 10) concurrentRequestDelay = 10;

            return concurrentRequestDelay;
        }

        set => SetField(ref concurrentRequestDelay, value, nameof(concurrentRequestDelay));
    }


    /// <summary>
    ///     Gets or sets maximal count of Concurrent Request.
    ///     Maximum number of simultaneous requests is `4`.
    ///     >[!NOTE]
    ///     > The property will be capped to this value if higher value is assigned.
    ///     >[!IMPORTANT]
    ///     > When setting this value take into account that other devices may communicate with your target system.
    /// </summary>
    public int ConcurrentRequestMaxCount
    {
        get
        {
            if (concurrentRequestMaxCount > 4) concurrentRequestMaxCount = 4;
            if (concurrentRequestMaxCount <= 0) concurrentRequestMaxCount = 1;
            return concurrentRequestMaxCount;
        }

        set => SetField(ref concurrentRequestMaxCount, value, nameof(concurrentRequestMaxCount));
    }




    /// <summary>
    ///     Gets online value items tags attached to this connector.
    /// </summary>
    public IEnumerable<ITwinPrimitive> OnlineTags { get; } = new List<ITwinPrimitive>();

    /// <summary>
    ///     Gets or set whether RW loop is suspended.
    /// </summary>
    public bool IsRwLoopSuspended
    {
        get => isRwLoopSuspended;
        set
        {
            if (value)
                Logger.Warning("Cyclic RW loop is suspended.");
            else
                Logger.Warning("Cyclic RW loop was resumed.");

            SetField(ref isRwLoopSuspended, value, nameof(IsRwLoopSuspended));
        }
    }


    internal bool WriteProtectionSuspended { get; private set; }

    /// <summary>
    ///     Get
    /// </summary>
    internal ConcurrentDictionary<string, ITwinPrimitive> NextPeriodicReadSet { get; } = new();

    internal ConcurrentDictionary<string, ITwinPrimitive> Subscribed { get; } = new();

    internal ConcurrentDictionary<string, ITwinPrimitive> NextCycleWriteSet { get; } = new();

    /// <summary>
    ///     Implementation of <see cref="INotifyPropertyChanged" />
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    ///     Builds and starts this connector.
    /// </summary>
    public abstract Connector BuildAndStart();

    /// <summary>
    ///     Reads batch of value items from the plc.
    /// </summary>
    /// <param name="primitives">Primitive items to be read.</param>
    public abstract Task ReadBatchAsync(IEnumerable<ITwinPrimitive> primitives);

    internal abstract Task ReadBatchAsyncCyclic(IEnumerable<ITwinPrimitive> primitives);

    /// <summary>
    ///     Writes batch of value items to the plc.
    /// </summary>
    /// <param name="primitives">Primitive items to be written.</param>
    public abstract Task WriteBatchAsync(IEnumerable<ITwinPrimitive> primitives);

    internal abstract Task WriteBatchAsyncCyclic(IEnumerable<ITwinPrimitive> primitives);

    /// <summary>
    ///     Return symbol path combining parent's and member's symbol.
    /// </summary>
    /// <param name="parent">Parent's path.</param>
    /// <param name="member">Members name.</param>
    /// <returns>Combine symbol of parent and member.</returns>
    public static string CreateSymbol(string parent, string member)
    {
        if (string.IsNullOrWhiteSpace(member))
            throw new ArgumentException($"Argument {nameof(member)} cannot be null or empty.");
        var retVal = string.IsNullOrEmpty(parent) ? member : string.Format("{0}.{1}", parent, member);
        return retVal;
    }

    /// <summary>
    ///     Return humanized path combining parent's and member's names.
    /// </summary>
    /// <param name="parent">Parent's name.</param>
    /// <param name="member">Members name.</param>
    /// <returns>Combine symbol of parent and member.</returns>
    public static string CreateHumanReadable(string parent, string member)
    {
        var retVal = string.IsNullOrEmpty(parent) ? member : string.Format("{0}.{1}", parent, member);
        return retVal;
    }

    /// <summary>
    ///     Sets properties backing field value and notifies over INotifyPropertyChanged interface.
    /// </summary>
    /// <typeparam name="T">Type of backing field.</typeparam>
    /// <param name="field">Field.</param>
    /// <param name="value">Value to be set.</param>
    /// <param name="propertyName">Property name.</param>
    /// <returns></returns>
    protected bool SetField<T>(ref T field, T value, string propertyName)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        NotifyPropertyChanged(propertyName);
        return true;
    }

    private void NotifyPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    ///     Forces the connector to reload symbols.
    /// </summary>
    public abstract void ReloadConnector();

    /// <summary>
    ///     Subscribes to observe changes on 'Edit' property of all tags attached to this connector.
    /// </summary>
    /// <param name="subscriber"></param>
    internal void SubscribeAllEditValueObserver(OnlinerBase.ValueChangeDelegate subscriber)
    {
        foreach (var item in OnlineTags) item.EditValueChange = subscriber;
    }

    /// <summary>
    ///     Suspends write protection on this connector.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SuspendWriteProtection(string passPhrase)
    {
        switch (passPhrase)
        {
            case null:
                return;
            case
                "Hoj morho vetvo mojho rodu, kto kramou rukou siahne na tvoju slobodu a co i dusu das v tom boji divokom vol nebyt ako byt otrokom!"
                :
                WriteProtectionSuspended = true;
                break;
        }
    }

    /// <summary>
    ///     Resumes write protection on this connector.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ResumeWriteProtection()
    {
        WriteProtectionSuspended = false;
    }

    internal void AddToPeriodicWriteSet(ITwinPrimitive primitive)
    {
        NextCycleWriteSet[primitive.Symbol] = primitive;
    }

    internal void AddToNextPeriodicReadSet(ITwinPrimitive primitive)
    {
        NextPeriodicReadSet[primitive.Symbol] = primitive;
    }

    private static CultureInfo desiredCulture = CultureInfo.InvariantCulture;
    
    /// <summary>
    /// Sets the culture for this connector.
    /// </summary>
    /// <param name="culture">Desired culture</param>
    public static void SetCulture(CultureInfo culture)
    {
        desiredCulture = culture;
    }

   

    /// <summary>
    ///     Starts cyclical read write operation on this connector.
    /// </summary>
    protected async void StartReadWriteOps()
    {
        var sw = new Stopwatch();
        long cycleCount = 0;
        var startTimeStamp = DateTime.Now;
        
        await Task.Run(async () =>
        {
            while (true)
                if (!IsRwLoopSuspended)
                {
                    if (desiredCulture.Name != CultureInfo.InvariantCulture.Name 
                        && (Thread.CurrentThread.CurrentUICulture.Name != desiredCulture.Name ||
                        Thread.CurrentThread.CurrentCulture.Name != desiredCulture.Name))
                    {
                        Thread.CurrentThread.CurrentUICulture = desiredCulture;
                        Thread.CurrentThread.CurrentCulture = desiredCulture;
                    }

                    await Task.Delay(ReadWriteCycleDelay);
                    sw.Restart();
                    try
                    {
                        await CyclicWrite();
                        await CyclicRead();
                    }
                    catch 
                    {
                        ReloadConnector();
                    }
                    sw.Stop();
                    CyclicRwDuration = sw.ElapsedMilliseconds;

                    cycleCount++;
                    var totalSeconds = (long)(DateTime.Now - startTimeStamp).TotalSeconds;
                    if (totalSeconds != 0)
                        RwCycleCount = cycleCount / totalSeconds;
                }
            // This is actually a closed loop no return from the method.
            // ReSharper disable FunctionNeverReturns
        });
    }

    
    /// <summary>
    ///     Reads online variables required to be read.
    /// </summary>
    protected async Task CyclicRead()
    {
       
        var primitivesToRead = new List<ITwinPrimitive>();
        primitivesToRead.AddRange(NextPeriodicReadSet.Values);
        primitivesToRead.AddRange(Subscribed.Values);
        var distinctPrimitivesToRead = primitivesToRead.Distinct().ToList();

        if (distinctPrimitivesToRead.Any())
        {
            Logger.Debug($"Periodic reading of '{distinctPrimitivesToRead.Count()}' items.");
        }

        await ReadBatchAsyncCyclic(distinctPrimitivesToRead
            .Where(p => !(p.ReadOnce && p.AccessStatus.LastAccess != OnlinerBase.DefaultDateTime)));

        this.ClearPeriodicReadSet();
    }

    /// <summary>
    ///     Writes modified online variables.
    /// </summary>
    protected async Task CyclicWrite()
    {
        await WriteBatchAsyncCyclic(NextCycleWriteSet.Values);
        ClearPeriodicWriteSet();
    }

    private void ClearPeriodicWriteSet()
    {
        NextCycleWriteSet.Clear();
    }

    protected void ClearPeriodicReadSet()
    {
        NextPeriodicReadSet.Clear();
    }

    internal void Subscribe(ITwinPrimitive primitive)
    {
        this.Subscribed[primitive.Symbol] = primitive;
    }
}