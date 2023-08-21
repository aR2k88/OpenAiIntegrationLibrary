using System.Text.Json.Serialization;

namespace OpenAiIntegrationLibrary.Models.General;

class ChunkChoice
{
    [JsonPropertyName("index")]
    public int Index { get; set; }
    [JsonPropertyName("delta")]
    public Delta Delta { get; set; }
    [JsonPropertyName("finish_reason")]
    public string finishReason { get; set; }
}