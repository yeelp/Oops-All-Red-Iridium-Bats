using System;
using StardewModdingAPI;

namespace OopsAllRedIridiumBats;

internal sealed class Logger
{
    private readonly IMonitor _monitor;

    private static Logger? _instance;

    private Logger(IMonitor monitor)
    {
        _monitor = monitor;
    }

    public static Logger GetInstance()
    {
        if(_instance is null)
        {
            throw new NullReferenceException("Getting Logger before it is created!");
        }
        return _instance;
    }

    public static void CreateLogger(IMonitor monitor)
    {
        _instance = new Logger(monitor);
    }

    public void Debug(string msg)
    {
        if(OopsAllRedIridiumBats.Config.DebugMode)
        {
            _monitor.Log(msg, LogLevel.Debug);
        }
    }

    public void Info(string msg)
    {
        _monitor.Log(msg, LogLevel.Info);
    }

    public void Warn(string msg)
    {
        _monitor.Log(msg, LogLevel.Warn);
    }

    public void Err(string msg)
    {
        _monitor.Log(msg, LogLevel.Error);
    }
}