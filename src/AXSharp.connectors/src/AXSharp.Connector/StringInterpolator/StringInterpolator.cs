// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector;

/// <summary>
///     Provides extension methods for PLC's string interpolation.
/// </summary>
public static class StringInterpolator
{
    private const string OpenToken = "|[";
    private const string CloseToken = "]|";

    /// <summary>
    ///     Interpolates original string attached to a twin object.
    /// </summary>
    /// <param name="original">Original yet non-interpolated string.</param>
    /// <param name="obj">Object on which original string created.</param>
    /// <returns>Interpolated string.</returns>
    public static string Interpolate(this string original, object obj)
    {
        return obj is ITwinElement ? Interpolate(original, obj as ITwinElement) : original;
    }

    /// <summary>
    ///     Interpolates original string attached to a twin object.
    /// </summary>
    /// <param name="original">Original yet non-interpolated string.</param>
    /// <param name="obj"><see cref="ITwinElement" /> object on which original string created.</param>
    /// <returns>Interpolated string.</returns>
    public static string Interpolate(this string original, ITwinElement obj)
    {
        try
        {
            if (original == null)
                return string.Empty;

            if (!original.Contains(OpenToken))
                return original;

            var sb = new StringBuilder(original);

            foreach (var interpolated in ExtractString(original))
                sb.Replace(interpolated.Interpolated,
                    GetInterpolatedValue(interpolated.Interpolated, FindParent(interpolated.Ancestor, obj)));

            return sb.ToString();
        }
        catch (Exception)
        {
            // Ignore
        }

        return CleanUpTokens(original);
    }

    private static ITwinElement FindParent(int generation, ITwinElement obj)
    {
        var lastParent = obj;
        for (var i = 0; i < generation; i++) lastParent = lastParent.GetParent();

        return lastParent;
    }

    private static ITwinElement GetObject(string memberName, ITwinElement obj)
    {
        var property = obj.GetType().GetProperties().FirstOrDefault(p => p.Name == CleanUpTokens(memberName));
        if (property != null)
        {
            var retVal = property.GetValue(obj) as ITwinElement;
            if (retVal != null) return retVal;
        }

        return obj;
    }

    private static string GetInterpolatedValue(string path, ITwinElement obj)
    {
        if (!path.Contains('.')) return GetPropertyValue(CleanUpTokens(path), obj);

        var memberPath = path.Split('.');
        var lastObject = obj;
        for (var i = 0; i < memberPath.Length - 1; i++) lastObject = GetObject(memberPath[i], lastObject);

        return GetPropertyValue(CleanUpTokens(memberPath[memberPath.Length - 1]), lastObject);
    }


    private static PropertyInfo GetPropertyInfo(string interpolated, ITwinElement obj)
    {
        if (obj is OnlinerBase)
            return interpolated is "Cyclic" or "GetAsync" or "AsObj" or "Edit"
                ? obj.GetType().GetProperties().FirstOrDefault(p => p.Name == "Raw")
                : obj.GetType().GetProperties().FirstOrDefault(p => p.Name == $"{interpolated}");

        return obj.GetType().GetProperties().FirstOrDefault(p => p.Name == $"{interpolated}");
    }

    private static string GetPropertyValue(string interpolated, ITwinElement obj)
    {
        string retVal = null;
        var property = GetPropertyInfo(interpolated, obj);
        if (property != null) retVal = property.GetValue(obj).ToString();

        if (!string.IsNullOrEmpty(retVal))
            return retVal;
        return interpolated;
    }

    private static string CleanUpTokens(string interpolated)
    {
        if (interpolated.StartsWith("|[["))
        {
            var generationIndexEndTag = interpolated.IndexOf("]");
            interpolated = interpolated.Substring(generationIndexEndTag + 1,
                interpolated.Length - generationIndexEndTag - 1);
        }


        return interpolated.Replace(OpenToken, string.Empty).Replace(CloseToken, string.Empty);
    }

    private static IEnumerable<InterpolatedAncestor> ExtractString(string s)
    {
        var retVal = new List<InterpolatedAncestor>();
        var startIndex = 0;
        var endIndex = 0;
        var carret = 0;
        do
        {
            startIndex = s.IndexOf(OpenToken, carret);
            if (startIndex >= 0)
            {
                endIndex = s.IndexOf(CloseToken, startIndex);

                if (startIndex >= 0 && endIndex >= 0)
                {
                    var substitute = s.Substring(startIndex, endIndex - startIndex + CloseToken.Length);

                    if (substitute.StartsWith("|[["))
                    {
                        var ancestorAsString = substitute.Substring(3, substitute.IndexOf("]") - 3);
                        var ancestor = 0;

                        if (int.TryParse(ancestorAsString, out ancestor))
                            retVal.Add(new InterpolatedAncestor { Interpolated = substitute, Ancestor = ancestor });
                    }
                    else
                    {
                        retVal.Add(new InterpolatedAncestor { Interpolated = substitute, Ancestor = 0 });
                    }
                }
            }

            carret = endIndex;
        } while (startIndex >= 0);

        return retVal;
    }
}