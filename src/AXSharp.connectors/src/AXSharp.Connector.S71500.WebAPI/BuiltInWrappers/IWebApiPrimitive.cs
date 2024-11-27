// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Connector.S71500.WebApi;

internal interface IWebApiPrimitive : ITwinPrimitive
{
    ApiPlcReadRequest PeekPlcReadRequestData { get; }

    ApiPlcWriteRequest PeekPlcWriteRequestData { get; }

    ApiPlcReadRequest PlcReadRequestData { get; }
    ApiPlcWriteRequest PlcWriteRequestData { get; }

    void Read(string value);
}