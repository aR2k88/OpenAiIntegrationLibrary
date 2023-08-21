using OpenAiIntegrationLibrary.Models.General;

namespace OpenAiIntegrationLibrary.Models.RequestModels.ChatCompletionRequest;

class ChatCompletionRequest
{
    public string Model { get; set; }
    public List<Message> Messages { get; set; }
}