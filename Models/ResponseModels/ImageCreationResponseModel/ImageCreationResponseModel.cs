using OpenAiIntegrationLibrary.Models.General;

namespace OpenAiIntegrationLibrary.Models.ResponseModels.ImageCreationResponseModel;

public class ImageCreationResponseModel
{
    public long created { get; set; }
    public List<DataItem> data { get; set; }
}
