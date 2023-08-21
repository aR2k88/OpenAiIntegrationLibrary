using OpenAiIntegrationLibrary.Models.General;

namespace OpenAiIntegrationLibrary.Models.ResponseModels.ChatCompletionResponse;

public class ChatCompletionResponse
{
    public string id { get; set; }
    public string @object { get; set; }
    public long created { get; set; }
    public string model { get; set; }
    public Choice[] choices { get; set; }
    public Usage usage { get; set; }
}
