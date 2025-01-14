// AXSharp.Connector
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System;

namespace AXSharp.Connector.ValueTypes.Online;

/// <summary>
///     Defines contract to access online value of <see cref="DateTime" />; DATE_AND_TIME (DT) type of the PLC.
/// </summary>
public interface IOnlineDateTime : IOnline<DateTime>
{
}