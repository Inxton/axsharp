// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Reflection;

namespace AXSharp.Connector;

/// <summary>
///     Indicates that the member should not be reflected within the ix framework.
/// </summary>
public class IgnoreReflectionAttribute : Attribute
{
    /// <summary>
    ///     Checks for the existence of <see cref="IgnoreReflectionAttribute" /> declaration on given property.
    /// </summary>
    /// <param name="property">PropertyInfo</param>
    /// <returns>True when the property declares <see cref="IgnoreReflectionAttribute" />.</returns>
    public static bool HasIgnoreReflectionAttribute(PropertyInfo property)
    {
        var ignoreReflection = property.GetCustomAttribute(typeof(IgnoreReflectionAttribute), false);

        if (ignoreReflection != null)
            return true;
        return false;
    }

    /// <summary>
    ///     Checks for the existence of <see cref="IgnoreReflectionAttribute" /> declaration on given property or object
    ///     instance.
    /// </summary>
    /// <param name="property"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool HasIgnoreReflectionAttribute(PropertyInfo property, object obj)
    {
        var ignoreReflection = property?.GetCustomAttribute(typeof(IgnoreReflectionAttribute), false);

        if (ignoreReflection != null) return true;

        if (obj?.GetType().GetCustomAttribute(typeof(IgnoreReflectionAttribute), true) is IgnoreReflectionAttribute)
            return true;

        return false;
    }
}