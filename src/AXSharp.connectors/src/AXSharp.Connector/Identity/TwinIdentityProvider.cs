// AXSharp.Connector
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector.Identity;

/// <summary>
///     Provides access to the objects by their identities.
/// </summary>
public class TwinIdentityProvider
{
    private static readonly NullTwinIdentity _nullTwin = new();

    private readonly Connector _connector;
    private readonly Dictionary<OnlinerULInt, ITwinIdentity> _identities = new();

    private readonly List<OnlinerULInt> _identitiesTags = new();

    private readonly SortedDictionary<ulong, ITwinIdentity> _sortedIdentities = new();

    /// <summary>
    ///     Creates new instance of <see cref="TwinIdentityProvider" />.
    /// </summary>
    [Obsolete("Use `TwinIdentityProvider(Connector connector)` instead.")]
    public TwinIdentityProvider()
    {
    }

    /// <summary>
    ///     Creates an instance of <seealso cref="TwinIdentityProvider" />
    /// </summary>
    public TwinIdentityProvider(Connector connector)
    {
        ArgumentNullException.ThrowIfNull(connector);
        _connector = connector;
    }

    /// <summary>
    ///     Get dictionary of identities.
    /// </summary>
    public SortedDictionary<ulong, ITwinIdentity> Identities
    {
        get
        {
            if (_sortedIdentities.Count == 0) SortIdentitiesAsync().Wait();

            return _sortedIdentities;
        }
    }

    /// <summary>
    ///     Get count of identities.
    /// </summary>
    public long IdentitiesCount
    {
        get
        {
            if (_identities != null) return _identities.Count();
            if (_sortedIdentities != null) return _sortedIdentities.Count();

            return 0;
        }
    }

    private ITwinIdentity GetReferencedTwinByIdentity(ITwinIdentity obj)
    {
        try
        {
            if (obj.Identity.GetLastAvailableValue() != 0)
            {
                var vrt = _sortedIdentities.FirstOrDefault(p => p.Key == obj.Identity.GetLastAvailableValue()).Value;
                if (vrt != null)
                {
                    return vrt;
                }

                ((ITwinObject)obj).GetParent().GetConnector().Logger.Debug(
                    $"Identity:'{obj.Identity.GetLastAvailableValue()}' for symbol: '{obj.Symbol}' could was not found.");
                return obj;
            }

            return obj;
        }
        catch (Exception)
        {
            return obj;
        }
    }

    private bool HasMemberByIdentityAttribute(ITwinIdentity obj)
    {
        if (obj is ITwinObject)
        {
            var d = obj as ITwinObject;
            if (d.GetParent() != null && d.GetSymbolTail() != null)
            {
                var property = d.GetParent().GetType().GetProperty(d.GetSymbolTail());
                if (property != null)
                {
                    var a = property.GetCustomAttributes(true)
                        .FirstOrDefault(p => p.GetType() == typeof(MemberByIdentityAttribute));
                    return a != null;
                }
            }
        }

        return false;
    }

    /// <summary>
    ///     Adds twin object to the list of identities.
    /// </summary>
    /// <param name="twinObject">twin object</param>
    public void AddIdentity(ITwinIdentity twinObject)
    {
        ArgumentNullException.ThrowIfNull(twinObject);

        _identitiesTags.Add(twinObject.Identity);

        if (!HasMemberByIdentityAttribute(twinObject)
            && !_identities.ContainsKey(twinObject.Identity))
            _identities.Add(twinObject.Identity, twinObject);
        else
            _connector?.Logger.Debug($"Identity {twinObject.Symbol} : {twinObject.Identity} ignored.");
    }

    /// <summary>
    ///     Gets twin object by identity, if the object implements <see cref="ITwinIdentity" />.
    ///     If object does not implements <see cref="ITwinIdentity" /> the same object is returned.
    /// </summary>
    /// <param name="obj">Object with identity</param>
    /// <returns>twin object with given identity.</returns>
    public dynamic GetTwinByIdentity(object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        if (obj is ITwinIdentity identity) return GetTwinByIdentity(identity);

        return obj;
    }

    /// <summary>
    ///     Gets twin object by identity.
    /// </summary>
    /// <param name="obj">Twin object.</param>
    /// <returns>Twin object with given identity.</returns>
    public dynamic GetTwinByIdentity(ITwinIdentity obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        return GetReferencedTwinByIdentity(obj);
    }

    /// <summary>
    ///     Gets twin object by identity.
    /// </summary>
    /// <param name="identity">Twin identity address.</param>
    /// <returns>Twin object with given identity.</returns>
    public ITwinIdentity GetTwinByIdentity(ulong identity)
    {
        try
        {
            if (identity != 0)
            {
                var vrt = Identities.FirstOrDefault(p => p.Key == identity).Value;
                if (vrt != null)
                    return vrt;
                return _nullTwin;
            }

            return _nullTwin;
        }
        catch (Exception)
        {
            return _nullTwin;
        }
    }

    /// <summary>
    ///     Reads twin objects identities.
    /// </summary>
    /// <returns>Identity onliners.</returns>
    internal async Task<IEnumerable<OnlinerULInt>> ReadIdentitiesAsync()
    {
        if (_connector != null)
        {
            _connector.Logger.Information("Reading identities...");
            await _connector.ReadBatchAsync(_identitiesTags);
            _connector.Logger.Information(
                $"Number of identities: {_identitiesTags.Count} | Unique :{_identities.Count}");
        }

        return _identitiesTags;
    }


    /// <summary>
    /// Assigns identities to all elements.
    /// </summary>
    /// <param name="identities">Identities</param>
    /// <param name="identityProvider">Identity creator.</param>
    /// <returns>Assigned identities.</returns>
    public IEnumerable<OnlinerULInt> AssignIdentities(IEnumerable<OnlinerULInt> identities, Func<OnlinerULInt, ulong> identityProvider = null)
    {
        // If no identity provider is given, use default one based on hash code of the symbol.
        identityProvider ??= (x) => x.Cyclic = (ulong)x.Symbol.GetHashCode();

        _connector.Logger.Information("Assigning missing identities...");
        foreach (var it in identities)
        {
            it.Cyclic = identityProvider(it);
        }

        return identities;
    }

    /// <summary>
    /// Writes identities to the PLC.
    /// </summary>
    /// <param name="identitiesToWrite">List of identities to be written.</param>
    /// <returns></returns>
    public async Task WriteIdentities(IEnumerable<OnlinerULInt> identitiesToWrite)
    {
        if(_connector == null) return;
        await _connector.WriteBatchAsync(identitiesToWrite);
        _connector.Logger.Information("Identities have been written.");
    }

    /// <summary>
    ///    Constructs identities by assigning them locally, writing to PLC, and sorting by the assigned values.
    ///    Identity values are always written fresh to the PLC regardless of any previously stored values.
    ///    This ensures that stale or inconsistent identity values from prior sessions do not cause duplicate identity errors.
    /// </summary>
    /// <param name="identityProvider">
    ///    Function that assigns and returns an identity value for each <see cref="OnlinerULInt"/> tag.
    ///    When <c>null</c>, a default provider based on <see cref="string.GetHashCode()"/> of the symbol is used.
    ///    Note: <see cref="string.GetHashCode()"/> is not deterministic across process restarts in .NET Core/.NET 5+,
    ///    so identity values will differ between sessions. For stable identities, supply a custom provider.
    /// </param>
    /// <param name="failOnDuplicate">
    ///    When <c>true</c>, throws <see cref="DuplicateIdentityException"/> if two symbols resolve to the same identity value.
    ///    When <c>false</c>, logs a warning and skips the duplicate entry.
    /// </param>
    public async Task ConstructIdentitiesAsync(Func<OnlinerULInt, ulong> identityProvider = null, bool failOnDuplicate = true)
    {
        await WriteIdentities(AssignIdentities(_identitiesTags, identityProvider));
        await SortIdentitiesAsync(failOnDuplicate);
    }

    /// <summary>
    ///     Sorts identities using the locally assigned Cyclic values, without reading back from PLC.
    /// </summary>
    internal async Task SortIdentitiesAsync(bool failOnDuplicate = true)
    {
        _connector?.Logger.Information("Sorting identities from assigned values...");
        _sortedIdentities.Clear();
        foreach (var identity in _identities)
        {
            var key = identity.Key.Cyclic;
            if (!_sortedIdentities.ContainsKey(key) && key != 0)
            {
                _sortedIdentities.Add(key, identity.Value);
            }
            else
            {
                if (failOnDuplicate)
                {
                    throw new DuplicateIdentityException("There is a duplicate identity: " +
                                                         $"'{identity.Value.Symbol} : {key}.'" +
                                                         $" and '{_sortedIdentities[key].Symbol} : {key}.'" +
                                                         $" share the same identity value." +
                                                         $"The algorithm for assigning identities needs to be adjusted." +
                                                         $"Use an algorithm that guarantees unique identities and is less prone to collisions.");
                }
                else
                {
                    _connector?.Logger.Warning($"Duplicate identity detected: '{identity.Value.Symbol} : {key}' and '{_sortedIdentities[key].Symbol} : {key}' share the same identity value. " +
                                               $"This entry will be ignored."+
                                               $"The algorithm for assigning identities needs to be adjusted." +
                                               $"Use an algorithm that guarantees unique identities and is less prone to collisions." +
                                               $"Ignoring this warning may lead to unexpected behavior in those parts of the system that rely on unique identities.");
                }
            }
        }

        _connector?.Logger.Information("Sorting identities done.");
    }
}

/// <summary>
/// Exception thrown when a duplicate identity is detected.
/// </summary>
public class DuplicateIdentityException : Exception
{
    public DuplicateIdentityException(string message) : base(message)
    {
    }
}
