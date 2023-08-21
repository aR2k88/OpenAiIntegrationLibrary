
namespace OpenAiIntegrationLibrary.Models.General;

public class Choice
{
    public int index { get; set; }
    public Message message { get; set; }
    public string finish_reason { get; set; }
}