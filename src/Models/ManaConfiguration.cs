namespace Mana.Models;

public sealed class ElasticConfig
{
    public string ServerUrl { get; set; } = "http://localhost:4080/";

    public string Username { get; set; } = "admin";

    public string Password { get; set; } = "admin";
}

public sealed class ManaConfiguration
{
    public ElasticConfig Elastic { get; set; } = new();
}