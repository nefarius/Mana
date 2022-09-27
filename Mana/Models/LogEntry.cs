using System.Text.RegularExpressions;
using Mana.Util;

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

    private static readonly Regex _infoRegex = new(@"INFO|INF|informational");

    private static readonly Regex _warnRegex = new(@"WARN|WRN|WARNING");

    private static readonly Regex _errornRegex = new(@"ERROR|ERR|error");

    private static readonly Regex _fatalnRegex = new(@"FATAL|FTL|fatal");

    public string Id { get; set; }

    public DateTime Timestamp { get; init; }

    public string LoggerName { get; init; }

    public LogLevel Level { get; init; }

    public string Message { get; init; }

    private string LogLevelTerminalString
    {
        get
        {
            switch (Level)
            {
                case LogLevel.Fatal:
                case LogLevel.Error:
                    return $"{Formatting.Red}{Level}{Formatting.Reset}";
                case LogLevel.Warning:
                    return $"{Formatting.YellowHB}{Level}{Formatting.Reset}";
                default:
                    return Level.ToString();
            }
        }
    }

    public string TerminalLine =>
        $"{Formatting.BHYellow}{Timestamp}{Formatting.Reset} {LoggerName} [{LogLevelTerminalString}] - {Message}";

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
            Id = hit.Id,
            Timestamp = hit.Timestamp,
            LoggerName = string.IsNullOrEmpty(hit.Source?.LogName)
                ? hit.Source?.ContainerName.Trim('/')
                : hit.Source?.LogName,
            Level = level,
            Message = hit.Source?.Log
        };
    }
}