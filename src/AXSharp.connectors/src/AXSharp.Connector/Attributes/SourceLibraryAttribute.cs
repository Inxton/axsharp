using System;

namespace AXSharp.Connector;

/// <summary>
///     Identifies the apax package (name + version) the transpiled types in this assembly originate
///     from. This attribute is emitted when no usable git remote is available; when it is present,
///     the per-type <see cref="SourceFileAttribute" /> path is <b>relative to the project <c>src</c>
///     folder</b> (forward-slash separated).
/// </summary>
/// <note type="note">
///     This attribute is emitted in the connector building process. It should not be declared by the
///     framework consumers.
/// </note>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
public class SourceLibraryAttribute : Attribute
{
    /// <summary>
    ///     Creates a new instance of <see cref="SourceLibraryAttribute" />.
    /// </summary>
    /// <param name="name">apax package name.</param>
    /// <param name="version">apax package version.</param>
    public SourceLibraryAttribute(string name, string version)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Version = version ?? throw new ArgumentNullException(nameof(version));
    }

    /// <summary>
    ///     Gets the apax package name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Gets the apax package version.
    /// </summary>
    public string Version { get; }
}
