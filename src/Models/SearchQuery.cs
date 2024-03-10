namespace Mana.Models;

public static class SearchQuery
{
    public static string BuildQuery(DateTime? from, DateTime? to, int count = 100, bool newestFirst = true)
    {
        if (!from.HasValue || !to.HasValue)
        {
            throw new ArgumentNullException(nameof(from), "Both start and end times must be provided.");
        }

        string sort = newestFirst ? "-@timestamp" : "@timestamp";

        return $@"
{{
    ""query"": {{
        ""bool"": {{
            ""must"": [
                {{
                    ""range"": {{
                        ""@timestamp"": {{
                            ""gte"": ""{from.Value.ToUniversalTime():yyyy-MM-ddTHH:mm:ssK}"",
                            ""lt"": ""{to.Value.ToUniversalTime():yyyy-MM-ddTHH:mm:ssK}"",                            
                            ""format"": ""2006-01-02T15:04:05Z07:00""
                        }}
                    }}
                }},
                {{
                    ""match_all"": {{}}
                }}
            ]
        }}
    }},
    ""sort"": [
        ""{sort}""
    ],
    ""from"": 0,
    ""size"": {count},
    ""aggs"": {{
        ""histogram"": {{
            ""date_histogram"": {{
                ""field"": ""@timestamp"",
                ""fixed_interval"": ""30s""
            }}
        }}
    }}
}}
";
    }
}