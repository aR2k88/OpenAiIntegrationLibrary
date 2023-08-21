using OpenAiIntegrationLibrary.Models.General;

namespace OpenAiIntegrationLibrary.Models.ResponseModels.ChatCompletionChunkResponse;

class ChatCompletionChunkResponse
{
    public string Id { get; set; }
    public string Object { get; set; }
    public long Created { get; set; }
    public string Model { get; set; }
    public ChunkChoice[] Choices { get; set; }
}