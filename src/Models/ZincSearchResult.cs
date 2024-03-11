using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Mana.Models;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class Aggregations
{
    [JsonPropertyName("histogram")]
    public Histogram Histogram { get; set; }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class Bucket
{
    [JsonPropertyName("doc_count")]
    public int DocCount { get; set; }

    [JsonPropertyName("key")]
    public DateTime Key { get; set; }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class Histogram
{
    [JsonPropertyName("buckets")]
    public List<Bucket> Buckets { get; } = new();
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class Hit
{
    [JsonPropertyName("_index")]
    public string Index { get; set; }

    [JsonPropertyName("_type")]
    public string Type { get; set; }

    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("_score")]
    public int Score { get; set; }

    [JsonPropertyName("@timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("_source")]
    public SourceEntry Source { get; set; }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class HitMeta
{
    [JsonPropertyName("total")]
    public Total Total { get; set; }

    [JsonPropertyName("max_score")]
    public int MaxScore { get; set; }

    [JsonPropertyName("hits")]
    public List<Hit> Hits { get; } = new();
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class SearchResult
{
    [JsonPropertyName("took")]
    public int Took { get; set; }

    [JsonPropertyName("timed_out")]
    public bool TimedOut { get; set; }

    [JsonPropertyName("_shards")]
    public Shards Shards { get; set; }

    [JsonPropertyName("hits")]
    public HitMeta Hits { get; set; }

    [JsonPropertyName("aggregations")]
    public Aggregations Aggregations { get; set; }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class Shards
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("successful")]
    public int Successful { get; set; }

    [JsonPropertyName("skipped")]
    public int Skipped { get; set; }

    [JsonPropertyName("failed")]
    public int Failed { get; set; }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class SourceEntry
{
    [JsonPropertyName("@log_name")]
    public string LogName { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("worker")]
    public int Worker { get; set; }

    [JsonPropertyName("container_id")]
    public string ContainerId { get; set; }

    [JsonPropertyName("container_name")]
    public string ContainerName { get; set; }

    [JsonPropertyName("log")]
    public string Log { get; set; }

    [JsonPropertyName("source")]
    public string Source { get; set; }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class Total
{
    [JsonPropertyName("value")]
    public int Value { get; set; }
}