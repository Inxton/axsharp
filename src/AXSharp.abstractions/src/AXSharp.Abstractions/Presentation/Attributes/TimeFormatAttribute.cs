// AXSharp.Abstractions
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Globalization;

namespace AXSharp.Abstractions.Presentation;

[AttributeUsage(AttributeTargets.Property)]
public class TimeFormatAttribute : Attribute
{
    public TimeFormatAttribute(string format)
    {
        this.FormatString = format.Replace(".", @"\.").Replace(":", @"\:");
    }

    public string FormatString { get; private set; }

    private string defaultFormatString = "d\\.hh\\:mm\\:ss\\.fff";

    private IEnumerable<string> ParseFormats = new string[]
    {
        "d\\.hh\\:mm\\:ss\\.fff",
        "hh\\:mm\\:ss\\.fff",
        "h\\:mm\\:ss\\.fff",
        "mm\\:ss\\.fff",
        "m\\:ss\\.fff",
        "ss\\.fff",
        "s\\.fff",
        "s\\.fff",
        "s\\.ff",
        "s\\.f"
    };

    private string GetValidFormats()
    {
        string retVal = string.Empty;
        foreach (var item in ParseFormats)
        {
            retVal = $"{retVal}\n{item.Replace("\\", string.Empty)}";
        }

        return retVal;
    }

    public TimeSpan ToTimeSpan(string value)
    {
        TimeSpan retVal;

        if(TimeSpan.TryParseExact(value, this.FormatString, CultureInfo.InvariantCulture, out retVal))
        {
            return retVal;
        }

        foreach (var parsingFormat in ParseFormats)
        {
            if(TimeSpan.TryParseExact(value, parsingFormat, CultureInfo.InvariantCulture, out retVal))
            {
                return retVal;
            }
        }
               
        if(TimeSpan.TryParse(value, out retVal))
        {
               return retVal;
        }
              
        throw new Exception($"Invalid input format. The format should be in one of the following formats: {this.GetValidFormats()}");
    } 
    
    public string FromTimeSpan(TimeSpan value)
    {

        if(value.Days > 0)
        {
           return value.ToString("d\\.hh\\:mm\\:ss\\.fff", CultureInfo.InvariantCulture);
        }

        if (value.Hours > 0)
        {
            return value.ToString("hh\\:mm\\:ss\\.fff", CultureInfo.InvariantCulture);
        }

        if (value.Minutes > 0)
        {
            return value.ToString("mm\\:ss\\.fff", CultureInfo.InvariantCulture);
        }

        if (value.Seconds > 0)
        {
            return value.ToString("ss\\.fff", CultureInfo.InvariantCulture);
        }

        if (value.Milliseconds > 0)
        {
            return value.ToString("ss\\.fff", CultureInfo.InvariantCulture);
        }

        return value.ToString(defaultFormatString, CultureInfo.InvariantCulture);       
    }
}

