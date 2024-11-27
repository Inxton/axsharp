// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector.Localizations;
using System.Globalization;

namespace AXSharp.Connector;

/// <summary>
///     Basic contract for any type that is product of building process.
/// </summary>
public interface ITwinElement
{
    /// <summary>
    ///     Get symbol of this instance.
    /// </summary>
    string Symbol { get; }

    /// <summary>
    ///     Gets Name of this instance.
    /// </summary>
    string AttributeName { get; }

    string GetAttributeName(CultureInfo culture);
    
    string GetHumanReadable(CultureInfo culture);

    /// <summary>
    ///     Provides a string combined from <see cref="AttributeName" /> of ancestors (<see cref="GetParent" />) of this
    ///     instance and the tail of this instance.
    /// </summary>
    string HumanReadable { get; }

    /// <summary>
    ///     Gets the parent object of this instance.
    ///     Parent object is the object that created this instance.
    /// </summary>
    /// <returns>Parent object.</returns>
    ITwinObject GetParent();

    /// <summary>
    ///     Get symbol tail of this instance.
    /// </summary>
    /// <returns></returns>
    string GetSymbolTail();

    /// <summary>
    /// Add this element for polling the in the next connector read cycle.
    /// </summary>
    void Poll();

    public AXSharp.Connector.Localizations.Translator Interpreter { get; }
}