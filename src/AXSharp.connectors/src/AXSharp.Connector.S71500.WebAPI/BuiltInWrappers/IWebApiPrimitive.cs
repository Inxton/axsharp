// AXSharp.Connector.S71500.WebAPI
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

namespace AXSharp.Connector.S71500.WebApi;

internal interface IWebApiPrimitive : ITwinPrimitive
{
    ApiPlcReadRequest PeekPlcReadRequestData { get; }

    ApiPlcWriteRequest PeekPlcWriteRequestData { get; }

    ApiPlcReadRequest PlcReadRequestData { get; }
    ApiPlcWriteRequest PlcWriteRequestData { get; }

    void Read(string value);
}