namespace Mana.Models;

public static class SearchQuery
{
    public static string BuildQuery(DateTime? from, DateTime? to, int count = 100)
    {
        if (!from.HasValue || !to.HasValue)
            throw new ArgumentNullException("Both start and end times must be provided.");

        return $@"
{{
    ""query"": {{
        ""bool"": {{
            ""must"": [
                {{
                    ""range"": {{
                        ""@timestamp"": {{
                            ""gte"": ""{from.Value.ToUniversalTime():yyyy-MM-ddTHH:mm:ssK}"",
                            ""lt"": ""{to.Value.ToUniversalTime():yyyy-MM-ddTHH:mm:ssK}""
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
        ""-@timestamp""
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