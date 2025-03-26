// AXSharp.Connector
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

#undef TRACE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector.BuilderHelpers;

/// <summary>
///     Helper methods for creating and manipulating arrays in Twin objects.
/// </summary>
public static class Arrays
{
    /// <summary>
    ///     Instantiates an array.
    /// </summary>
    /// <param name="array">Array</param>
    /// <param name="parent">Parent object.</param>
    /// <param name="readableTail">Human readable tail.</param>
    /// <param name="symbolTail">Symbol tail.</param>
    /// <param name="initializer">Intializes</param>
    [Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void InstantiateArray(Array array,
        ITwinObject parent,
        string readableTail,
        string symbolTail,
        Func<ITwinObject, string, string, object> initializer,
        IEnumerable<(int LowerBound, int UpperBound)> arrayBounds)
    {
        var indice = string.Empty;
        var arrayFieldIndex = 0;

        // Rank
        foreach (var rank in arrayBounds)
        {
            indice = string.IsNullOrEmpty(indice) ? string.Empty : $"{indice},";

            // Index
            for (int index = rank.LowerBound; index <= rank.UpperBound; index++)
            {
                var currentIndice = $"{indice}{index}";
                var a = initializer(parent,
                    $"{readableTail}[{currentIndice}]",
                    $"{symbolTail}[{currentIndice}]");

                array.SetValue(a, arrayFieldIndex++);
            }
        }
    }

    [Obsolete("Internal use only")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void InstantiateArray(this Array array,
        Func<object> initializer,
        IEnumerable<(int LowerBound, int UpperBound)> arrayBounds)
    {
        var indice = string.Empty;
        var arrayFieldIndex = 0;

        // Rank
        foreach (var rank in arrayBounds)
        {
            indice = string.IsNullOrEmpty(indice) ? string.Empty : $"{indice},";

            // Index
            for (int index = rank.LowerBound; index <= rank.UpperBound; index++)
            {
                var a = initializer();
                array.SetValue(a, arrayFieldIndex++);
            }
        }
    }

    private static int GetNumberOfElements((int LowerBound, int UpperBound) bounds)
    {
        return bounds.LowerBound == 0 ? bounds.UpperBound + 1 : bounds.UpperBound - bounds.LowerBound;
    }
}