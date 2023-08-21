using System.Text.Json.Serialization;

namespace OpenAiIntegrationLibrary.Models.General;

class Delta
{
    [JsonPropertyName("content")]
    public string Content { get; set; }
}