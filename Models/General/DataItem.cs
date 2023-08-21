using System.Text.Json.Serialization;

namespace OpenAiIntegrationLibrary.Models.General;

public class DataItem
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
}