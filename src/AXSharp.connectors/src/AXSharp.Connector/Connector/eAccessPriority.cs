namespace AXSharp.Connector;

/// <summary>
/// Specifies the access priority levels for connector operations.
/// These priorities determine how the connector handles read/write operations and resource allocation.
/// </summary>
public enum eAccessPriority
{
    /// <summary>
    /// Lower than normal access priority. Used for background or non-critical operations
    /// that can tolerate longer response times and delays.
    /// </summary>
    Low,

    /// <summary>
    /// Standard access priority. Used for regular operations that don't require special handling.
    /// This is the default priority level for most operations.
    /// </summary>
    Normal,     
    
    /// <summary>
    /// Priority level for operations triggered by direct user interaction.
    /// These operations are given precedence over normal operations to ensure responsive UI feedback.
    /// Typically used for immediate user-triggered reads/writes.
    /// </summary>
    UserInterface,

    /// <summary>
    /// Higher than normal access priority. Used for time-critical or prioritized operations
    /// that require minimal latency and immediate processing.
    /// </summary>
    High,

    /// <summary>
    /// Represents a custom access priority level that can be configured for specific use cases.
    /// This allows for flexible priority handling in specialized scenarios.
    /// </summary>
    Custom
}