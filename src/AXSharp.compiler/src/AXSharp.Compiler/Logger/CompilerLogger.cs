// AXSharp.Compiler
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using Serilog;
using Serilog.Core;

namespace AXSharp.Compiler;

/// <summary>
///     Provides logger for the compiler.
/// </summary>
public static class Log
{
    static Log()
    {
        Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
    }

    /// <summary>
    ///     Gets the logger.
    /// </summary>
    public static Logger Logger { get; }
}