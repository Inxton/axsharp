// AXSharp.Connector
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

namespace AXSharp.Connector;

/// <summary>
/// Specifies how the connector should behave when a communication exception occurs.
/// </summary>
public enum CommExceptionBehaviour
{

    /// <summary>
    /// Re-Throws any exception. Exceptions must be handled by user code.
    /// Should be used in all scenarios except for HMI.
    /// </summary>
    ReThrow,

    /// <summary>
    /// Ignores communication exceptions, but logs information about the problem.
    /// Typically would be used for HMI.
    /// </summary>
    Ignore
}