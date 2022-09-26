namespace Mana.Models;

public static class SearchQuery
{
    public static string BuildQuery(DateTime from, DateTime to, int count = 100)
    {
        return $@"
{{
    ""query"": {{
        ""bool"": {{
            ""must"": [
                {{
                    ""range"": {{
                        ""@timestamp"": {{
                            ""gte"": ""{from:O}"",
                            ""lt"": ""{to:O}"",
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