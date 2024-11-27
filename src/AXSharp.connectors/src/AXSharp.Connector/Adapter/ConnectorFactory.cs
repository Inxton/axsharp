// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector;

/// <summary>
///     Provides abstraction for creation of <see cref="Connector" /> and value types (tags) for twin connector.
/// </summary>
public abstract class ConnectorFactory
{
    /// <summary>
    ///     Creates new instance of connector.
    /// </summary>
    /// <remarks>
    ///     This class is typically used by the 'ixc' to create value types (tags) for given connector.
    /// </remarks>
    /// <param name="parameters">Connector parameters</param>
    /// <returns></returns>
    public abstract Connector CreateConnector(object[] parameters);

    /// <summary>
    ///     Creates <see cref="bool" /> tag of a value type BOOL;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerBool" /></returns>
    public abstract OnlinerBool CreateBOOL(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="byte" /> tag of a value type BYTE;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerByte" /></returns>
    public abstract OnlinerByte CreateBYTE(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="ushort" /> tag of a value type WORD;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerWord" /></returns>
    public abstract OnlinerWord CreateWORD(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="uint" /> tag of a value type DWORD;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerDWord" /></returns>
    public abstract OnlinerDWord CreateDWORD(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="ulong" /> tag of a value type DWORD;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerLWord" /></returns>
    public abstract OnlinerLWord CreateLWORD(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="sbyte" /> tag of a value type SINT;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerSInt" /></returns>
    public abstract OnlinerSInt CreateSINT(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="byte" /> tag of a value type USINT;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerUSInt" /></returns>
    public abstract OnlinerUSInt CreateUSINT(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="short" /> tag of a value type INT;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerInt" /></returns>
    public abstract OnlinerInt CreateINT(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="ushort" /> tag of a value type UINT;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerUInt" /></returns>
    public abstract OnlinerUInt CreateUINT(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="int" /> tag of a value type DINT;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerDInt" /></returns>
    public abstract OnlinerDInt CreateDINT(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="uint" /> tag of a value type UDINT;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerUDInt" /></returns>
    public abstract OnlinerUDInt CreateUDINT(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="long" /> tag of a value type LINT;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerLInt" /></returns>
    public abstract OnlinerLInt CreateLINT(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="ulong" /> tag of a value type ULINT;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerULInt" /></returns>
    public abstract OnlinerULInt CreateULINT(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="float" /> tag of a value type REAL;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerReal" /></returns>
    public abstract OnlinerReal CreateREAL(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="double" /> tag of a value type LREAL;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerLReal" /></returns>
    public abstract OnlinerLReal CreateLREAL(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="string" /> tag of a value type STRING;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerString" /></returns>
    public abstract OnlinerString CreateSTRING(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="string" /> tag of a value type WSTRING;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerWString" /></returns>
    public abstract OnlinerWString CreateWSTRING(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="TimeSpan" /> tag of a value type TIME;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerTime" /></returns>
    public abstract OnlinerTime CreateTIME(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="DateTime" /> tag of a value type DT, DATE_TIME;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerDateTime" /></returns>
    public abstract OnlinerDateTime CreateDATE_AND_TIME(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="DateTime" /> tag of a value type DATE;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerDate" /></returns>
    public abstract OnlinerDate CreateDATE(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="TimeSpan" /> tag of a value type TOD (Time of day);
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerTimeOfDay" /></returns>
    public abstract OnlinerTimeOfDay CreateTIME_OF_DAY(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="TimeSpan" /> tag of a value type LTIME;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerLTime" /></returns>
    public abstract OnlinerLTime CreateLTIME(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="char" /> tag of a value type WCHAR;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerWChar" /></returns>
    public abstract OnlinerWChar CreateWCHAR(ITwinObject parent, string readableTail, string symbolTail);

    
    /// <summary>
    ///     Creates <see cref="DateOnly" /> tag of a value type LDATE;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerDate" /></returns>
    public abstract OnlinerDate CreateLDATE(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="TimeSpan" /> tag of a value type LDATE_AND_TIME;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerLDateTime" /></returns>
    public abstract OnlinerLDateTime CreateLDATE_AND_TIME(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="TimeSpan" /> tag of a value type LTIME_OF_DAY;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerLTimeOfDay" /></returns>
    public abstract OnlinerLTimeOfDay CreateLTIME_OF_DAY(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="char" /> tag of a value type CHAR;
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerChar" /></returns>
    public abstract OnlinerChar CreateCHAR(ITwinObject parent, string readableTail, string symbolTail);

    /// <summary>
    ///     Creates <see cref="bool" /> empty tag of a value type BOOL;
    /// </summary>
    /// <returns></returns>
    public static OnlinerBool CreateBOOL()
    {
        return new OnlinerBool();
    }

    /// <summary>
    ///     Creates <see cref="byte" /> empty tag of a value type BYTE;
    /// </summary>
    /// <returns></returns>
    public static OnlinerByte CreateBYTE()
    {
        return new OnlinerByte();
    }

    /// <summary>
    ///     Creates <see cref="ushort" /> empty tag of a value type WORD;
    /// </summary>
    /// <returns></returns>
    public static OnlinerWord CreateWORD()
    {
        return new OnlinerWord();
    }

    /// <summary>
    ///     Creates <see cref="uint" /> empty tag of a value type DWORD;
    /// </summary>
    /// <returns></returns>
    public static OnlinerDWord CreateDWORD()
    {
        return new OnlinerDWord();
    }

    /// <summary>
    ///     Creates <see cref="ulong" /> empty tag of a value type LWORD;
    /// </summary>
    /// <returns></returns>
    public static OnlinerLWord CreateLWORD()
    {
        return new OnlinerLWord();
    }

    /// <summary>
    ///     Creates <see cref="sbyte" /> empty tag of a value type SINT;
    /// </summary>
    /// <returns></returns>
    public static OnlinerSInt CreateSINT()
    {
        return new OnlinerSInt();
    }

    /// <summary>
    ///     Creates <see cref="byte" /> empty tag of a value type USINT;
    /// </summary>
    /// <returns></returns>
    public static OnlinerUSInt CreateUSINT()
    {
        return new OnlinerUSInt();
    }

    /// <summary>
    ///     Creates <see cref="short" /> empty tag of a value type INT;
    /// </summary>
    /// <returns></returns>
    public static OnlinerInt CreateINT()
    {
        return new OnlinerInt();
    }

    /// <summary>
    ///     Creates <see cref="ushort" /> empty tag of a value type UINT;
    /// </summary>
    /// <returns></returns>
    public static OnlinerUInt CreateUINT()
    {
        return new OnlinerUInt();
    }

    /// <summary>
    ///     Creates <see cref="int" /> empty tag of a value type DINT;
    /// </summary>
    /// <returns></returns>
    public static OnlinerDInt CreateDINT()
    {
        return new OnlinerDInt();
    }


    /// <summary>
    ///     Creates <see cref="uint" /> empty tag of a value type UDINT;
    /// </summary>
    /// <returns></returns>
    public static OnlinerUDInt CreateUDINT()
    {
        return new OnlinerUDInt();
    }

    /// <summary>
    ///     Creates <see cref="long" /> empty tag of a value type LINT;
    /// </summary>
    /// <returns></returns>
    public static OnlinerLInt CreateLINT()
    {
        return new OnlinerLInt();
    }

    /// <summary>
    ///     Creates <see cref="ulong" /> empty tag of a value type ULINT;
    /// </summary>
    /// <returns></returns>
    public static OnlinerULInt CreateULINT()
    {
        return new OnlinerULInt();
    }

    /// <summary>
    ///     Creates <see cref="float" /> empty tag of a value type REAL;
    /// </summary>
    /// <returns></returns>
    public static OnlinerReal CreateREAL()
    {
        return new OnlinerReal();
    }

    /// <summary>
    ///     Creates <see cref="double" /> empty tag of a value type LREAL;
    /// </summary>
    /// <returns></returns>
    public static OnlinerLReal CreateLREAL()
    {
        return new OnlinerLReal();
    }

    /// <summary>
    ///     Creates <see cref="string" /> empty tag of a value type STRING;
    /// </summary>
    /// <returns></returns>
    public static OnlinerString CreateSTRING()
    {
        return new OnlinerString();
    }

    /// <summary>
    ///     Creates <see cref="string" /> empty tag of a value type WSTRING;
    /// </summary>
    /// <returns></returns>
    public static OnlinerWString CreateWSTRING()
    {
        return new OnlinerWString();
    }

    /// <summary>
    ///     Creates <see cref="TimeSpan" /> empty tag of a value type TIME;
    /// </summary>
    /// <returns></returns>
    public static OnlinerTime CreateTIME()
    {
        return new OnlinerTime();
    }

    /// <summary>
    ///     Creates <see cref="DateTime" /> empty tag of a value type DATE_AND_TIME (DT);
    /// </summary>
    /// <returns></returns>
    public static OnlinerDateTime CreateDATE_TIME()
    {
        return new OnlinerDateTime();
    }

    /// <summary>
    ///     Creates <see cref="DateTime" /> empty tag of a value type DATE;
    /// </summary>
    /// <returns></returns>
    public static OnlinerDate CreateDATE()
    {
        return new OnlinerDate();
    }

    /// <summary>
    ///     Creates <see cref="TimeSpan" /> empty tag of a value type TIME_OF_DAY;
    /// </summary>
    /// <returns></returns>
    public static OnlinerTimeOfDay CreateTIME_OF_DAY()
    {
        return new OnlinerTimeOfDay();
    }

    /// <summary>
    ///     Creates <see cref="TimeSpan" /> empty tag of a value type LTIME;
    /// </summary>
    /// <returns></returns>
    public static OnlinerLTime CreateLTIME()
    {
        return new OnlinerLTime();
    }

    /// <summary>
    ///     Creates <see cref="char" /> empty tag of a value type WCHAR;
    /// </summary>
    /// <returns></returns>
    public static OnlinerWChar CreateWCHAR()
    {
        return new OnlinerWChar();
    }

    /// <summary>
    ///     Creates <see cref="char" /> empty tag of a value type WCHAR;
    /// </summary>
    /// <returns></returns>
    public static OnlinerChar CreateCHAR()
    {
        return new OnlinerChar();
    }

    /// <summary>
    ///     Creates <see cref="DateOnly" /> empty tag of a value type WCHAR;
    /// </summary>
    /// <returns></returns>
    public static OnlinerDate CreateLDATE()
    {
        return new OnlinerDate();
    }

    /// <summary>
    ///     Creates <see cref="DateTime" /> empty tag of a value type WCHAR;
    /// </summary>
    /// <returns></returns>
    public static OnlinerLDateTime CreateLDATE_AND_TIME()
    {
        return new OnlinerLDateTime();
    }

    /// <summary>
    ///     Creates <see cref="TimeOnly" /> empty tag of a value type WCHAR;
    /// </summary>
    /// <returns></returns>
    public static OnlinerLTimeOfDay CreateLTIME_OF_DAY()
    {
        return new OnlinerLTimeOfDay();
    }
}