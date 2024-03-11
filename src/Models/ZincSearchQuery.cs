using Mana.Converters;

using Newtonsoft.Json;

namespace Mana.Models;

public class Bool
{
    [JsonProperty("must")]
    public List<Must> Must { get; set; }
}

public class Must
{
    [JsonProperty("range")]
    public Range Range { get; set; }
}

public class Query
{
    [JsonProperty("bool")]
    public Bool Bool { get; set; }
}

public class Range
{
    [JsonProperty("@timestamp")]
    public Timestamp Timestamp { get; set; }
}

public class ZincSearchQuery
{
    [JsonProperty("query")]
    public Query Query { get; set; }

    [JsonProperty("sort")]
    public List<string> Sort { get; set; } = ["-@timestamp"];

    [JsonProperty("from")]
    public int From { get; set; }

    [JsonProperty("size")]
    public int Size { get; set; } = 100;
}

public class Timestamp
{
    [JsonProperty("gte")]
    [System.Text.Json.Serialization.JsonConverter(typeof(DateTimeRfc3339Converter))]
    public DateTime Gte { get; set; }

    [JsonProperty("lt")]
    [System.Text.Json.Serialization.JsonConverter(typeof(DateTimeRfc3339Converter))]
    public DateTime Lt { get; set; }

    [JsonProperty("format")]
    public string Format { get; set; } = "2006-01-02T15:04:05Z07:00";
}