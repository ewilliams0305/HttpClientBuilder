using System.Text.Json.Serialization;

namespace HttpClientBuilder.Server.Models;

public class Echo
{
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}