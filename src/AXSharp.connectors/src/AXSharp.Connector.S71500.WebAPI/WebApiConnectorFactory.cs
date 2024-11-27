// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector.S71500.WebApi;

/// <inheritdoc />
public class WebApiConnectorFactory : ConnectorFactory
{
    /// <inheritdoc />
    public override Connector CreateConnector(object[] parameters)
    {
        if (parameters[3] is bool)
        {
            return new WebApiConnector((string)parameters[0],
                (string)parameters[1],
                (string)parameters[2],
                (bool)parameters[3],
                (eTargetProjectPlatform)parameters[4],
                (string)parameters[5]);
        }
        else
        {
            return new WebApiConnector((string)parameters[0],
                (string)parameters[1],
                (string)parameters[2],
                (Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>)parameters[3],
                (bool)parameters[4],
                (eTargetProjectPlatform)parameters[5],
                (string)parameters[6]);
        }
    }

    /// <inheritdoc />
    public override OnlinerBool CreateBOOL(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiBool(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerByte CreateBYTE(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiByte(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerWord CreateWORD(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiWord(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerDWord CreateDWORD(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiDWord(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerLWord CreateLWORD(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiLWord(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerSInt CreateSINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiSInt(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerUSInt CreateUSINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiUSInt(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerInt CreateINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiInt(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerUInt CreateUINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiUInt(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerDInt CreateDINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiDInt(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerUDInt CreateUDINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiUdInt(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerLInt CreateLINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiLInt(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerULInt CreateULINT(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiULInt(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerReal CreateREAL(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiReal(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerLReal CreateLREAL(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiLReal(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerString CreateSTRING(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiString(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerWString CreateWSTRING(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiWString(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerTime CreateTIME(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiTime(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerDateTime CreateDATE_AND_TIME(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiDateTime(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerDate CreateDATE(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiDate(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerTimeOfDay CreateTIME_OF_DAY(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiTimeOfDay(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerLTime CreateLTIME(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiLTime(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerWChar CreateWCHAR(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiWChar(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerDate CreateLDATE(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiLDate(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerLDateTime CreateLDATE_AND_TIME(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiLDateTime(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerLTimeOfDay CreateLTIME_OF_DAY(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiLTimeOfDay(parent, readableTail, symbolTail);
    }

    /// <inheritdoc />
    public override OnlinerChar CreateCHAR(ITwinObject parent, string readableTail, string symbolTail)
    {
        return new WebApiChar(parent, readableTail, symbolTail);
    }
}