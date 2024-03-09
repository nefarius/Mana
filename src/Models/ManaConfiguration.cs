namespace Mana.Models;

/// <summary>
///     Elasticsearch/Zincsearch server parameters.
/// </summary>
public sealed class ElasticConfig
{
    public string ServerUrl { get; set; } = "http://localhost:4080/";

    public string Username { get; set; } = "admin";

    public string Password { get; set; } = "admin";
}

/// <summary>
///     Mana logging options.
/// </summary>
public sealed class LoggingConfig
{
    public bool LogToZinc { get; set; } = true;
    
    public Uri NodeUrl { get; set; } = new Uri("http://localhost:4080");
}

/// <summary>
///     Mandatory app settings.
/// </summary>
public sealed class ManaConfiguration
{
    public ElasticConfig Elastic { get; set; } = new();

    public LoggingConfig Logging { get; set; } = new();
}