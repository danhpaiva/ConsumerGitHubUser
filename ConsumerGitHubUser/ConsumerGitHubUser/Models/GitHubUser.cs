using System.Text.Json.Serialization;

namespace ConsumerGitHubUser.Models;

public class GitHubUser
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("company")]
    public string? Company { get; set; }

    [JsonPropertyName("location")]
    public string? Location { get; set; }

    [JsonPropertyName("login")]
    public string? Login { get; set; }
}
