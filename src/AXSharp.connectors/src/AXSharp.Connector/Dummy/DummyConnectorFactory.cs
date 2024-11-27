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
///     Dummy connector factory class. Serves as offline connector with no connection to a controller.
/// </summary>
public class DummyConnectorFactory : ConnectorFactory
{
    /// <summary>
    ///     Creates dummy connector.
    /// </summary>
    /// <param name="parameters">Connection parameters.</param>
    /// <returns></returns>
    public override Connector CreateConnector(object[] parameters)
    {
        return new DummyConnector();
    }

    /// <summary>
    ///     Creates <see cref="bool" /> dummy tag of PLC value type BOOL
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerBool" /></returns>
    public override OnlinerBool CreateBOOL(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerBool(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="byte" /> dummy tag of PLC value type BYTE
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerByte" /></returns>
    public override OnlinerByte CreateBYTE(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerByte(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="int" /> dummy tag of PLC value type DINT
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerDInt" /></returns>
    public override OnlinerDInt CreateDINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerDInt(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="uint" /> dummy tag of PLC value type DWORD
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerDWord" /></returns>
    public override OnlinerDWord CreateDWORD(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerDWord(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="short" /> dummy tag of PLC value type INT
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerInt" /></returns>
    public override OnlinerInt CreateINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerInt(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="long" /> dummy tag of PLC value type LINT
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerLInt" /></returns>
    public override OnlinerLInt CreateLINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerLInt(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="double" /> dummy tag of PLC value type LREAL
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerLReal" /></returns>
    public override OnlinerLReal CreateLREAL(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerLReal(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="TimeSpan" /> dummy tag of PLC value type LTIME
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerLTime" /></returns>
    public override OnlinerLTime CreateLTIME(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerLTime(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="ulong" /> dummy tag of PLC value type LWORD
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerLWord" /></returns>
    public override OnlinerLWord CreateLWORD(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerLWord(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="float" /> dummy tag of PLC value type REAL
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerReal" /></returns>
    public override OnlinerReal CreateREAL(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerReal(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="sbyte" /> dummy tag of PLC value type SINT
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerSInt" /></returns>
    public override OnlinerSInt CreateSINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerSInt(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="string" /> dummy tag of PLC value type STRING
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerString" /></returns>
    public override OnlinerString CreateSTRING(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerString(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="TimeSpan" /> dummy tag of PLC value type TIME
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerTime" /></returns>
    public override OnlinerTime CreateTIME(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerTime(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="DateTime" /> dummy tag of PLC value type DATE_AND_TIME (DT)
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerDateTime" /></returns>
    public override OnlinerDateTime CreateDATE_AND_TIME(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerDateTime(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="DateTime" /> dummy tag of PLC value type DATE
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerDate" /></returns>
    public override OnlinerDate CreateDATE(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerDate(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="TimeSpan" /> dummy tag of PLC value type TIME_OF_DAY (TOD)
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerTimeOfDay" /></returns>
    public override OnlinerTimeOfDay CreateTIME_OF_DAY(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerTimeOfDay(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="uint" /> dummy tag of PLC value type UDINT
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerUDInt" /></returns>
    public override OnlinerUDInt CreateUDINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerUDInt(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="ushort" /> dummy tag of PLC value type UINT
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerUInt" /></returns>
    public override OnlinerUInt CreateUINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerUInt(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="ulong" /> dummy tag of PLC value type ULINT
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerULInt" /></returns>
    public override OnlinerULInt CreateULINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerULInt(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="byte" /> dummy tag of PLC value type USINT
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerUSInt" /></returns>
    public override OnlinerUSInt CreateUSINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerUSInt(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="ushort" /> dummy tag of PLC value type WORD
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerWord" /></returns>
    public override OnlinerWord CreateWORD(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerWord(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="string" /> dummy tag of PLC value type WSTRING
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerWString" /></returns>
    public override OnlinerWString CreateWSTRING(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerWString(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="string" /> dummy tag of PLC value type WCHAR
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerWChar" /></returns>
    public override OnlinerWChar CreateWCHAR(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerWChar(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="DateOnly" /> dummy tag of PLC value type LDATE
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerDate" /></returns>
    public override OnlinerDate CreateLDATE(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerDate(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="DateTime" /> dummy tag of PLC value type LDATE_AND_TIME
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerLDateTime" /></returns>
    public override OnlinerLDateTime CreateLDATE_AND_TIME(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerLDateTime(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="TimeSpan" /> dummy tag of PLC value type LTIME_OF_DAY
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerLTimeOfDay" /></returns>
    public override OnlinerLTimeOfDay CreateLTIME_OF_DAY(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerLTimeOfDay(parent, readableTail, symbolTail);
    }

    /// <summary>
    ///     Creates <see cref="string" /> dummy tag of PLC value type CHAR
    /// </summary>
    /// <param name="parent">Parent object of <see cref="AXSharp.Connector.ITwinObject" /> type.</param>
    /// <param name="readableTail">Human readable tail of this value tag.</param>
    /// <param name="symbolTail">Symbol tail of this value tag.</param>
    /// <returns>New instance of <see cref="OnlinerChar" /></returns>
    public override OnlinerChar CreateCHAR(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new OnlinerChar(parent, readableTail, symbolTail);
    }
}