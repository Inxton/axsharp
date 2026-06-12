// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector.S71500.WebApi;

/// <summary>
/// Normalizes string values to the capacity declared in the PLC before they are written.
/// </summary>
internal static class StringValueNormalization
{
    /// <summary>
    /// Capacity assumed when the primitive carries no declared capacity (STRING/WSTRING without [n]).
    /// </summary>
    internal const int DefaultCapacity = 254;

    /// <summary>
    /// Truncates <paramref name="value"/> to the declared capacity of <paramref name="onliner"/>.
    /// Capacity ≤ 0 means the declaration is unknown (legacy or hand-built twins) and falls back to 254.
    /// Truncation operates on UTF-16 code units; surrogate pairs may be split (PLC WCHAR is UCS-2).
    /// </summary>
    internal static string NormalizeToDeclaredCapacity(this OnlinerBase<string> onliner, string? value,
        WebApiConnector connector)
    {
        value ??= string.Empty;
        var capacity = onliner.Capacity > 0 ? onliner.Capacity : DefaultCapacity;
        if (value.Length <= capacity) return value;
        connector.Logger?.Warning(
            "Value for '{Symbol}' exceeds declared string capacity {Capacity} (actual length {ActualLength}); the value will be truncated.",
            onliner.Symbol, capacity, value.Length);
        return value[..capacity];
    }
}
