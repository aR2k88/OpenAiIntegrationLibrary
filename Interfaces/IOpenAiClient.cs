using System.IO;
using System.Threading.Tasks;
using OpenAiIntegrationLibrary.Models;
using OpenAiIntegrationLibrary.Models.General;
using OpenAiIntegrationLibrary.Models.ResponseModels.ChatCompletionResponse;
using OpenAiIntegrationLibrary.Models.ResponseModels.ImageCreationResponseModel;
using OpenAiIntegrationLibrary.Models.ResponseModels.TranscribeAudioResponseModel;

namespace OpenAiIntegrationLibrary.Interfaces;

public interface IOpenAiClient
{
    Task<ChatCompletionResponse> ChatCompletionRequestAsync(string assistantDescription, string userMessage);
    Task<ChatCompletionResponse> ChatCompletionRequestAsync(Message[] messages);
    Task<ImageCreationResponseModel> ImageCreationRequestAsync(string promptText, int numberOfImages, OpenAiImageSize imageSize);
    Task<TranscribeAudioResponseModel> TranscribeAudioAsync(MemoryStream memoryStream);
    Task<TranscribeAudioResponseModel> TranscribeAudioAsync(string filePath);

}