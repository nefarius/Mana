using System.Drawing;
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

public sealed class ColorCache
{
    private readonly Dictionary<string, Color> _colors = new();
    private readonly Random _random = new();

    public Color GetFor(string loggerName)
    {
        if (_colors.TryGetValue(loggerName, out Color color))
            return color;

        var entry = Color.FromArgb(unchecked((int)0xFF000000) + (_random.Next(0xFFFFFF) & 0x7F7F7F));

        _colors[loggerName] = entry;

        return entry;
    }
}

public class LogEntry
{
    private static readonly Regex TraceRegex = new(@"TRACE");

    private static readonly Regex DebugRegex = new(@"DEBUG|debug");

    private static readonly Regex InfoRegex = new(@"INFO|INF|informational");

    private static readonly Regex WarnRegex = new(@"WARN|WRN|WARNING");

    private static readonly Regex ErrorRegex = new(@"ERROR|ERR|error");

    private static readonly Regex FatalRegex = new(@"FATAL|FTL|fatal");

    private readonly ColorCache _cache;

    public LogEntry(ColorCache cache)
    {
        _cache = cache;
    }

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
                    return $"{Color256.DarkRed52.AsEscapeSequence()}{Level}{Formatting.Reset}";
                case LogLevel.Warning:
                    return $"{Color256.Yellow226.AsEscapeSequence()}{Level}{Formatting.Reset}";
                case LogLevel.Info:
                    return $"{Color256.Aquamarine86.AsEscapeSequence()}{Level}{Formatting.Reset}";
                case LogLevel.None:
                case LogLevel.Trace:
                case LogLevel.Debug:
                default:
                    return Level.ToString();
            }
        }
    }

    public string TerminalLine =>
        $"{Color256.Yellow226.AsEscapeSequence()}{Timestamp.ToLocalTime():s}{Formatting.Reset} " +
        $"{_cache.GetFor(LoggerName).AsEscapeSequence()}{LoggerName}{Formatting.Reset} [{LogLevelTerminalString}] - {Message}";

    public static LogEntry FromSearchHit(Hit hit, ColorCache cache = default)
    {
        var level = LogLevel.None;

        if (hit.Source is not null && !string.IsNullOrEmpty(hit.Source.Log))
        {
            if (FatalRegex.IsMatch(hit.Source.Log))
                level = LogLevel.Fatal;
            else if (ErrorRegex.IsMatch(hit.Source.Log))
                level = LogLevel.Error;
            else if (WarnRegex.IsMatch(hit.Source.Log))
                level = LogLevel.Warning;
            else if (InfoRegex.IsMatch(hit.Source.Log))
                level = LogLevel.Info;
            else if (DebugRegex.IsMatch(hit.Source.Log))
                level = LogLevel.Debug;
            else if (TraceRegex.IsMatch(hit.Source.Log))
                level = LogLevel.Trace;
        }

        return new LogEntry(cache)
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