# S71500 WebAPI Connector Documentation

## [API](~/api/AXSharp.Connector.S71500.WebAPI.yml)

This connector offers an interface to interact with S7-15XX PLC systems through the WebAPI.

For details how to make the data available see [extern accessibility](https://console.simatic-ax.siemens.io/docs/sld/extern-accessibility).

Here is an example of how to create an instance of this connector:

```C#
public static <PLC_TWIN_TYPE> Plc { get; } = new (ConnectorAdapterBuilder.Build().CreateWebApi(Environment.GetEnvironmentVariable("AXTARGET") ?? "10.10.101.1", "Everybody", "", true));
```


## Considerations for WebAPI Connector Performance

When working with S7-1500 based PLC systems, especially hardware-based ones, there are limitations regarding the volume of items that can be communicated over a specific communication interface. To prevent performance degradation, it's vital to prudently manage the number of PLC items your application uses at any given time. It is recommended to utilize polling instead of an automatic variable subscription.

Here is an example on how to set polling subscription mode:

```C#
Entry.Plc.Connector.SubscriptionMode = AXSharp.Connector.ReadSubscriptionMode.Polling;
```

For more details about polling within your components, see [Polling Data from the Controller in Components](../blazor/LIBRARIES.md#polling-data-from-the-plc) and [General Polling](README.md#polling).

### Monitoring and Logging 

The WebAPI connector comes with a built-in logging feature to provide insights into communication performance. You can use any Serilog logger configuration to capture the logs from the connector. The default logging level is `Information`, which provides basic details about booting, possible communication errors, and warnings. If you need more granular data to debug performance issues, consider setting the logging level to `Debug`. For even more comprehensive information, use the `Verbose` level.

Below is an example of how to set a custom logger for the connector:

```C#
// This configuration logs to both console and `connector.log` file with `Debug` logging level.
Entry.Plc.Connector.SetLoggerConfiguration(new LoggerConfiguration()
    .WriteTo
    .Console()
    .WriteTo
    .File($"connector.log",
        outputTemplate: "{Timestamp:yyyy-MMM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}",
        fileSizeLimitBytes: 100000)
    .MinimumLevel.Debug()
    .CreateLogger());
```

> [!WARNING]
> **Enabling monitoring will impact the connector perfomance.**

## Performance

Communication via WebAPI has inherent performance constraints based on the target system, request frequency, and payload size. The S71500.WebAPI connector tackles the 128kB limit for single requests by fragmenting them into manageable chunks. However, there is a restriction on the number of requests that can be sent to the controller simultaneously. The maximum threshold is four simultaneous requests, though this can be reduced. Any requests exceeding this limit will be queued and processed after a specified waiting period. 

> [!NOTE]
> It's worth noting that the controller might be communicating with other devices like HMI or OPC-UA, further intensifying the overall communication load.

Here's a C# code snippet demonstrating how to adjust the concurrent request limit and the associated delay:

```C#
Entry.Plc.Connector.ConcurrentRequestMaxCount = 1; // Reducing to a single request 
Entry.Plc.Connector.ConcurrentRequestDelay = 100; // Setting the waiting period to 100ms
```

## Batch Operations

The WebAPI connector now supports enhanced batch reading and writing operations with configurable priority levels and chunk settings. This feature allows for more efficient and controlled data exchange with the PLC.

### Priority Levels

Batch operations support four priority levels through the `eAccessPriority` enum:

- `Normal`: Standard priority for regular operations (default)
- `Low`: Lower priority for background or less important operations
- `Prioritare`: Higher priority for time-critical operations
- `Custom`: User-defined priority level for special cases

### Configurable Batch Settings

Each priority level can have its own configuration for chunk size and inter-chunk delay. This allows for fine-tuned control over how batch operations are processed:

```C#
// Configure batch settings for different priority levels
Entry.Plc.Connector.BatchSettings[eAccessPriority.Normal] = (chunkSize: 250, interChunkDelay: 250);
Entry.Plc.Connector.BatchSettings[eAccessPriority.Low] = (chunkSize: 100, interChunkDelay: 500);
Entry.Plc.Connector.BatchSettings[eAccessPriority.Prioritare] = (chunkSize: 500, interChunkDelay: 100);
```

### Using Batch Operations

Here's how to perform batch operations with different priority levels:

```C#
// Batch read with normal priority
await connector.ReadBatchAsync(primitives, eAccessPriority.Normal);

// Batch write with high priority and custom chunk settings
await connector.WriteBatchAsync(primitives, eAccessPriority.Prioritare);

// Background batch operation with low priority
await connector.ReadBatchAsync(primitives, eAccessPriority.Low);

// Custom will take chunkSize and interchunkDelay arguments into consieration.
await connector.WriteBatchAsync(primitives, eAccessPriority.Custom, chunkSize: 300, interChunkDelay: 150);
```

> [!TIP]
> Use appropriate priority levels based on your application's needs:
> - Use `Normal` for regular operations
> - Use `Low` for background updates or non-critical data
> - Use `Prioritare` for time-sensitive operations
> - Use custom chunk settings for specific performance requirements

> [!NOTE]
> The actual performance of batch operations depends on various factors including network conditions, PLC load, and the size of data being transferred. Monitor the connector's logs for insights into batch operation performance.

