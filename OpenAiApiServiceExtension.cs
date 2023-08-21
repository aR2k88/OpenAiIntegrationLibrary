using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using OpenAiIntegrationLibrary.Client;
using OpenAiIntegrationLibrary.Interfaces;

namespace OpenAiIntegrationLibrary;

public static class ApiServiceExtensions
{
    private const string DefaultChatModel = "gpt-3.5-turbo";
    private const string DefaultAudioModel = "whisper-1";
    /// <summary>
    /// Registers an OpenAI API service implementation in the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to register the services with.</param>
    /// <param name="openAiApiKey">The OpenAI API key for authorization.</param>
    /// <param name="chatModel">The default chat model. Defaults to "gpt-3.5-turbo".</param>
    /// <param name="audioModel">The default audio model. Defaults to "whisper-1".</param>
    /// <returns>The modified service collection with the registered OpenAI API service.</returns>
    public static IServiceCollection AddOpenAiApiService(
        this IServiceCollection services,
        string openAiApiKey,
        string chatModel = DefaultChatModel,
        string audioModel = DefaultAudioModel)
    {
        services.AddHttpClient<IOpenAiClient, OpenAiClient>(client =>
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAiApiKey);
            // Create an instance of ApiService with the provided openAiApiKey
            var apiService = new OpenAiClient(client, chatModel, audioModel);
            // Other configuration/setup if needed
            return apiService;
        });

        return services;
    }
}