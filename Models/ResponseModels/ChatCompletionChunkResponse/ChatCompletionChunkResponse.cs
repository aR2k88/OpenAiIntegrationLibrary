using OpenAiIntegrationLibrary.Models.General;

namespace OpenAiIntegrationLibrary.Models.ResponseModels.ChatCompletionChunkResponse;

class ChatCompletionChunkResponse
{
    public string id { get; set; }
    public string @object { get; set; }
    public long created { get; set; }
    public string model { get; set; }
    public ChunkChoice[] choices { get; set; }
}