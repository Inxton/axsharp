// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

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
            var lastIdentity = 0ul;
            if (_identitiesTags.Count > 0)
            {
                lastIdentity = _identitiesTags.Max(p => p.LastValue);
            }

            List<ITwinPrimitive> IdentitiesToWrite = new();

            _connector.Logger.Information("Assigning missing identities...");
            foreach (var it in _identitiesTags)
            {
                if (it.LastValue == 0)
                {
                    it.Cyclic = ++lastIdentity;
                    IdentitiesToWrite.Add(it);  
                }
               
            }
            await _connector.WriteBatchAsync(IdentitiesToWrite);

            _connector.Logger.Information("Reading identities done.");
            _connector.Logger.Information(
                $"Number of identities: {_identitiesTags.Count} | Unique :{_identities.Count}");
        }

        return _identitiesTags;
    }

    /// <summary>
    ///     Refreshes and sorts identities.
    /// </summary>
    public async Task ConstructIdentitiesAsync()
    {
        await ReadIdentitiesAsync();
        await SortIdentitiesAsync();
    }

    /// <summary>
    ///     Sorts identities.
    /// </summary>
    internal async Task SortIdentitiesAsync()
    {
        await Task.Run(() =>
        {
            _connector?.Logger.Information("Sorting identities...");
            _sortedIdentities.Clear();
            foreach (var identity in _identities)
            {
                var key = identity.Key.LastValue == 0 ? identity.Key.GetAsync().Result : identity.Key.LastValue;
                if (!_sortedIdentities.ContainsKey(key))
                    _sortedIdentities.Add(key, identity.Value);
            }

            _connector?.Logger.Information("Sorting identities done.");
        });
    }
}