using System.Text.RegularExpressions;

namespace Mana.Models;

public enum LogLevel
{
    None,
    Trace,
    Debug,
    Info,
    Warning,
    Error,
    Fatal
}

public class LogEntry
{
    private static readonly Regex _traceRegex = new(@"TRACE");

    private static readonly Regex _debugRegex = new(@"DEBUG|debug");

    private static readonly Regex _infoRegex = new(@"INFO|informational");

    private static readonly Regex _warnRegex = new(@"WARN|WARNING");

    private static readonly Regex _errornRegex = new(@"ERROR|error");

    private static readonly Regex _fatalnRegex = new(@"FATAL|FTL|fatal");

    public DateTime Timestamp { get; set; }

    public string LoggerName { get; set; }

    public LogLevel Level { get; set; }

    public string Message { get; set; }

    public static LogEntry FromSearchHit(Hit hit)
    {
        var level = LogLevel.None;

        if (hit.Source is not null && !string.IsNullOrEmpty(hit.Source.Log))
        {
            if (_fatalnRegex.IsMatch(hit.Source.Log))
                level = LogLevel.Fatal;
            else if (_errornRegex.IsMatch(hit.Source.Log))
                level = LogLevel.Error;
            else if (_warnRegex.IsMatch(hit.Source.Log))
                level = LogLevel.Warning;
            else if (_infoRegex.IsMatch(hit.Source.Log))
                level = LogLevel.Info;
            else if (_debugRegex.IsMatch(hit.Source.Log))
                level = LogLevel.Debug;
            else if (_traceRegex.IsMatch(hit.Source.Log))
                level = LogLevel.Trace;
        }

        return new LogEntry
        {
            Timestamp = hit.Timestamp,
            LoggerName = string.IsNullOrEmpty(hit.Source?.LogName) 
                ? hit.Source?.ContainerName 
                : hit.Source?.LogName,
            Level = level,
            Message = hit.Source?.Log
        };
    }
}