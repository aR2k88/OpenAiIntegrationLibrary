using System.Text.Json.Serialization;

namespace OpenAiIntegrationLibrary.Models.ResponseModels.TranscribeAudioResponseModel;

public class TranscribeAudioResponseModel
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
}