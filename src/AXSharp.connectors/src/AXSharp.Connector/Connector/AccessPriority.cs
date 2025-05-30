namespace AXSharp.Connector;

/// <summary>
/// Specifies the access priority levels for connector operations.
/// </summary>
public enum eAccessPriority
{
    /// <summary>
    /// Standard access priority. Used for regular operations.
    /// </summary>
    Normal,     
    /// <summary>
    /// Lower than normal access priority. Used for background or less important operations.
    /// </summary>
    Low,        
    /// <summary>
    /// Higher than normal access priority. Used for time-critical or prioritized operations.
    /// </summary>
    Prioritare
}