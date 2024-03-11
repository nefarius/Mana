using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace Mana.Models;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class Aggregations
{
    [JsonProperty("histogram")]
    public Histogram Histogram { get; set; }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class Bucket
{
    [JsonProperty("doc_count")]
    public int DocCount { get; set; }

    [JsonProperty("key")]
    public DateTime Key { get; set; }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class Histogram
{
    [JsonProperty("buckets")]
    public List<Bucket> Buckets { get; } = new();
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class Hit
{
    [JsonProperty("_index")]
    public string Index { get; set; }

    [JsonProperty("_type")]
    public string Type { get; set; }

    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("_score")]
    public int Score { get; set; }

    [JsonProperty("@timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonProperty("_source")]
    public SourceEntry Source { get; set; }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class HitMeta
{
    [JsonProperty("total")]
    public Total Total { get; set; }

    [JsonProperty("max_score")]
    public int MaxScore { get; set; }

    [JsonProperty("hits")]
    public List<Hit> Hits { get; set; } = new();
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class SearchResult
{
    [JsonProperty("took")]
    public int Took { get; set; }

    [JsonProperty("timed_out")]
    public bool TimedOut { get; set; }

    [JsonProperty("_shards")]
    public Shards Shards { get; set; }

    [JsonProperty("hits")]
    public HitMeta Hits { get; set; }

    [JsonProperty("aggregations")]
    public Aggregations Aggregations { get; set; }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class Shards
{
    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("successful")]
    public int Successful { get; set; }

    [JsonProperty("skipped")]
    public int Skipped { get; set; }

    [JsonProperty("failed")]
    public int Failed { get; set; }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class SourceEntry
{
    [JsonProperty("@log_name")]
    public string LogName { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("worker")]
    public int Worker { get; set; }

    [JsonProperty("container_id")]
    public string ContainerId { get; set; }

    [JsonProperty("container_name")]
    public string ContainerName { get; set; }

    [JsonProperty("log")]
    public string Log { get; set; }

    [JsonProperty("source")]
    public string Source { get; set; }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class Total
{
    [JsonProperty("value")]
    public int Value { get; set; }
}