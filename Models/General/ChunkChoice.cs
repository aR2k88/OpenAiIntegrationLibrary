
namespace OpenAiIntegrationLibrary.Models.General;

class ChunkChoice
{
    public int index { get; set; }
    public Delta delta { get; set; }
    public string finish_reason { get; set; }
}