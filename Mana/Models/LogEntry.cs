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
    public DateTime Timestamp { get; set; }

    public string LoggerName { get; set; }

    public LogLevel Level { get; set; }

    public string Message { get; set; }

    public static LogEntry FromSearchHit(Hit hit)
    {
        return new LogEntry
        {
            Timestamp = hit.Timestamp,
            LoggerName = hit.Source.LogName,
            Message = hit.Source.Log
        };
    }
}