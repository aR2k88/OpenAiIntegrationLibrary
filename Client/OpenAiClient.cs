using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenAiIntegrationLibrary.ExceptionHandling;
using OpenAiIntegrationLibrary.Helpers;
using OpenAiIntegrationLibrary.Interfaces;
using OpenAiIntegrationLibrary.Models;
using OpenAiIntegrationLibrary.Models.General;
using OpenAiIntegrationLibrary.Models.ResponseModels.ChatCompletionResponse;
using OpenAiIntegrationLibrary.Models.ResponseModels.ImageCreationResponseModel;
using OpenAiIntegrationLibrary.Models.ResponseModels.TranscribeAudioResponseModel;

namespace OpenAiIntegrationLibrary.Client;

public class OpenAiClient : IOpenAiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _chatModel;
    private readonly string _audioModel;
    private const string CompletionsUrl = "https://api.openai.com/v1/chat/completions";
    private const string ImageCreationUrl = "https://api.openai.com/v1/images/generations";
    private const string TranscribeAudioUrl = "https://api.openai.com/v1/audio/transcriptions";
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpClient">The HTTP client instance to use for API requests.</param>
    /// <param name="openAiApiKey">The OpenAI API key for authorization.</param>
    /// <param name="chatModel">The name of the model to use. Defaults to "gpt-3.5-turbo".</param>
    /// <param name="audioModel">The name of the model to use. Defaults to "whisper-1".</param>
    public OpenAiClient(HttpClient httpClient, string chatModel, string audioModel)
    {
        _httpClient = httpClient;
        _chatModel = chatModel;
        _audioModel = audioModel;
        if (_httpClient.BaseAddress is not null) _httpClient.BaseAddress = null;
    }

    /// <summary>
    /// Sends a request to the OpenAI API for image creation based on the provided prompt.
    /// </summary>
    /// <param name="promptText">The prompt text to generate images from.</param>
    /// <param name="numberOfImages">The number of images to generate. Only valid numbers between 1-10, any value above or below will be assigned to nearest valid number.</param>
    /// <param name="imageSize">The desired size of the generated images.</param>
    /// <returns>An <see cref="ImageCreationResponseModel"/> containing the generated image data.</returns>
    /// <exception cref="ApiException">Thrown when the API request fails or an exception occurs.</exception>
    public async Task<ImageCreationResponseModel> ImageCreationRequestAsync(string promptText, int numberOfImages, OpenAiImageSize imageSize)
    {
        try
        {
            numberOfImages = numberOfImages switch
            {
                > 10 => 10,
                < 1 => 1,
                _ => numberOfImages
            };
            var requestData = new
            {
                prompt = promptText,
                n = numberOfImages,
                size = EnumConverter.ImageSizeEnumToRequest(imageSize)
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ImageCreationUrl, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                // Deserialize the response content into your ApiResponse class
                var apiResponse = JsonConvert.DeserializeObject<ImageCreationResponseModel>(responseContent);
                return apiResponse;
            }

            throw new ApiException("OpenAI API request failed with status code: " + response.StatusCode);
        }
        catch (Exception ex)
        {
            // Handle exceptions here
            throw new ApiException("An error occurred while sending OpenAI API request.", ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="assistantDescription">Description for the assistant.</param>
    /// <param name="userMessage">Query prompt</param>
    /// <returns></returns>
    public async Task<ChatCompletionResponse> ChatCompletionRequestAsync(string assistantDescription, string userMessage)
    {
        var requestData = new
        {
            model = _chatModel,
            messages = new[]
            {
                new { role = "system", content = assistantDescription },
                new { role = "user", content = userMessage }
            }
        };

        return await DoChatCompletionRequestAsync(requestData);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="messages">Format: [role = system/user, content = '*content*']</param>
    /// <returns></returns>
    public async Task<ChatCompletionResponse> ChatCompletionRequestAsync(Message[] messages)
    {
        var requestData = new
        {
            model = _chatModel,
            messages = messages
        };
        return await DoChatCompletionRequestAsync(requestData);
    }

    public async Task<TranscribeAudioResponseModel> TranscribeAudioAsync(string filePath)
    {
        try
        {
            var formDataContent = new MultipartFormDataContent();
            formDataContent.Add(new StreamContent(File.OpenRead(filePath)), "file", Path.GetFileName(filePath));
            formDataContent.Add(new StringContent(_audioModel), "model");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/audio/transcriptions", formDataContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<TranscribeAudioResponseModel>(responseContent);
                return apiResponse;
            }
            throw new Exception("OpenAI API request failed with status code: " + response.StatusCode);
        }
        catch (Exception ex)
        {
            // Handle exceptions here
            throw new Exception("An error occurred while sending OpenAI API request.", ex);
        }
    }

    public async Task<TranscribeAudioResponseModel> TranscribeAudioAsync(MemoryStream memoryStream)
    {
        try
        {
            var content = new MultipartFormDataContent();
            var byteArrayContent = new ByteArrayContent(memoryStream.ToArray());
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");
            content.Add(byteArrayContent, "file", "filename.wav");
            content.Add(new StringContent(_audioModel), "model");

            var response = await _httpClient.PostAsync(TranscribeAudioUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            await memoryStream.DisposeAsync();
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = JsonConvert.DeserializeObject<TranscribeAudioResponseModel>(responseContent);
                return apiResponse;
            }

            throw new ApiException("OpenAI API request failed with status code: " + response.StatusCode);
        }
        catch (Exception ex)
        {
            throw new ApiException("An error occurred while sending OpenAI API request.", ex);
        }
    }

    private async Task<ChatCompletionResponse> DoChatCompletionRequestAsync(dynamic requestData)
    {
        try
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync(CompletionsUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                // Deserialize the response content into your ApiResponse class
                var apiResponse = JsonConvert.DeserializeObject<ChatCompletionResponse>(responseContent);
                return apiResponse;
            }

            throw new ApiException("OpenAI API request failed with status code: " + response.StatusCode);
        }
        catch (Exception ex)
        {
            // Handle exceptions here
            throw new ApiException("An error occurred while sending OpenAI API request.", ex);
        }
    }
}