// AXSharp.Connector
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

namespace AXSharp.Connector;

/// <summary>
/// Specifies how the variables should be subscribed to.
/// </summary>
public enum ReadSubscriptionMode
{
    /// <summary>
    /// The variables must explicitly subscribed for cyclic reading in order to be read.
    /// [NOTE] Variables can be also polled by the UI.
    /// </summary>
    Polling,
    /// <summary>
    /// Variables used by calling getter of Cyclic property are automatically subscribed for cyclic reading.
    /// Automatic subscription may create overhead in case of large number of variables.
    /// </summary>
    AutoSubscribeUsedVariables,
}