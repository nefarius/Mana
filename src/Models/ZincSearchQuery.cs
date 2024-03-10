using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;

namespace Mana.Models;

public class Bool
{
    [JsonPropertyName("must")]
    public List<Must> Must { get; set; }
}

public class MatchAll
{
}

public class Must
{
    [JsonPropertyName("range")]
    public Range Range { get; set; }

    [JsonPropertyName("match_all")]
    public MatchAll MatchAll { get; set; } = new();
}

public class Query
{
    [JsonPropertyName("bool")]
    public Bool Bool { get; set; }
}

public class Range
{
    [JsonPropertyName("@timestamp")]
    public Timestamp Timestamp { get; set; }
}

public class ZincSearchQuery
{
    [JsonPropertyName("query")]
    public Query Query { get; set; }

    [JsonPropertyName("sort")]
    public List<string> Sort { get; set; } = ["-@timestamp"];

    [JsonPropertyName("from")]
    public int From { get; set; } = 0;

    [JsonPropertyName("size")]
    public int Size { get; set; } = 100;
}

public class Timestamp
{
    [JsonPropertyName("gte")]
    public DateTime Gte { get; set; }

    [JsonPropertyName("lt")]
    public DateTime Lt { get; set; }

    [JsonPropertyName("format")]
    public string Format { get; set; } = "2006-01-02T15:04:05Z07:00";
}
