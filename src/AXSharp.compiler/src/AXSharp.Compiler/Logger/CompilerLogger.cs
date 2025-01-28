// AXSharp.Compiler
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace AXSharp.Compiler;

/// <summary>
///     Provides logger for the compiler.
/// </summary>
public static class Log
{
    private static Logger logger;

    /// <summary>
    ///    Configures the logger.
    /// </summary>
    /// <param name="logLevel"></param>
    public static void ConfigureLogger(LogEventLevel logLevel)
    {
        switch(logLevel)
        {
            case LogEventLevel.Verbose:
                Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .MinimumLevel.Verbose()
                            .CreateLogger();
                break;
            case LogEventLevel.Debug:
                Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .MinimumLevel.Debug()
                            .CreateLogger();
                break;
            case LogEventLevel.Information:
                Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .MinimumLevel.Information()
                            .CreateLogger();
                break;
            case LogEventLevel.Warning:
                Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .MinimumLevel.Warning()
                            .CreateLogger();
                break;
            case LogEventLevel.Error:
                Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .MinimumLevel.Error()
                            .CreateLogger();
                break;
            case LogEventLevel.Fatal:
                Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .MinimumLevel.Fatal()
                            .CreateLogger();
                break;
            default:                
                Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .MinimumLevel.Information()
                            .CreateLogger();
                break;
        }

        
    }

    /// <summary>
    ///     Gets the logger.
    /// </summary>
    public static Logger Logger 
    { 
        get
        {
            if (logger == null)
            {
                ConfigureLogger(LogEventLevel.Information);
            }

            return logger;
        } 
        private set => logger = value; 
    }
}