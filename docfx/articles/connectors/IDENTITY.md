# Twin Identity System

## Overview

The identity system in AXSharp allows twin objects to be looked up by a unique numeric identity (`ulong`). This is used by components that need to resolve references between twin objects at runtime (e.g. the `[MemberByIdentity]` attribute).

The core class is [`TwinIdentityProvider`](~/api/AXSharp.Connector.Identity.TwinIdentityProvider.yml), which is responsible for assigning, writing, and sorting identity values.

## How identities work

1. During twin construction, each object that implements `ITwinIdentity` is registered via `AddIdentity`.
2. When the application starts, `ConstructIdentitiesAsync` is called to assign identity values, write them to the PLC, and build a sorted lookup dictionary.
3. Other parts of the system can then resolve objects by their identity using `GetTwinByIdentity`.

## Constructing identities

```csharp
await connector.IdentityProvider.ConstructIdentitiesAsync();
```

`ConstructIdentitiesAsync` performs the following steps:

1. **Assign** -- Each identity tag is assigned a `ulong` value using the provided `identityProvider` function. If no function is supplied, a default provider based on `string.GetHashCode()` of the symbol is used.
2. **Write** -- The assigned values are written to the PLC via `WriteBatchAsync`. Values are always written fresh, regardless of what was previously stored on the PLC.
3. **Sort** -- The identities are sorted into a `SortedDictionary<ulong, ITwinIdentity>` using the locally assigned values (`Cyclic`), not values read back from the PLC.

This ensures that stale or inconsistent identity values left on the PLC from prior sessions cannot cause errors.

## The `failOnDuplicate` parameter

```csharp
await connector.IdentityProvider.ConstructIdentitiesAsync(failOnDuplicate: false);
```

By default, `ConstructIdentitiesAsync` throws a `DuplicateIdentityException` when two symbols resolve to the same identity value. You can set `failOnDuplicate` to `false` to log a warning and skip the duplicate entry instead.

## Custom identity providers

The default identity provider uses `string.GetHashCode()`, which has two important limitations:

- **Not deterministic across processes** -- In .NET Core/.NET 5+, `string.GetHashCode()` is randomized per process. Identity values will differ between application restarts.
- **Collision risk** -- `GetHashCode()` returns a 32-bit value. For large PLC programs with many symbols, hash collisions become increasingly likely (birthday paradox).

For production use, supply a custom identity provider that guarantees uniqueness:

```csharp
await connector.IdentityProvider.ConstructIdentitiesAsync(
    identityProvider: tag => tag.Cyclic = MyStableHashFunction(tag.Symbol)
);
```

A good custom provider should:
- Produce deterministic values for the same symbol across process restarts.
- Guarantee uniqueness (or near-uniqueness) across all symbols in the program.
- Use the full 64-bit `ulong` range to minimize collision probability.

## Troubleshooting

### `DuplicateIdentityException`

This exception is thrown when two different symbols are assigned the same identity value. Common causes:

- **Hash collision** with the default `GetHashCode()`-based provider. Solution: use a custom identity provider with better distribution, or set `failOnDuplicate: false` if identity-based lookups are not critical for your application.
- **Stale PLC values** (resolved in current version) -- Previously, the identity system would read back values from the PLC after writing. If the PLC returned stale data from a prior session, this could cause spurious duplicate errors. The system now uses the locally assigned values directly, avoiding this issue.
