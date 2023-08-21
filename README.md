## OpenAi Integration Library
```markdown
# OpenAI Integration Library

The OpenAI Integration Library simplifies integration with the OpenAI API in your applications. It provides easy-to-use functions for various OpenAI API endpoints.

## Installation

To use the OpenAI Integration Library in your project, you need to install the NuGet package:

```shell
Install-Package YourPackageName
```

## Getting Started

1. Import the library in your code:

```csharp
using OpenAiIntegrationLibrary;
```

2. Set up the OpenAI API client in your application's configuration, such as in the `Startup.cs`:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    string openAiApiKey = Configuration["OpenAiApiKey"];
    services.AddOpenAiApiService(openAiApiKey);
    // Other service registrations and configurations
}
```

## Available Functions

The OpenAI Integration Library provides the following functions through the `IOpenAiClient` interface:

### ChatCompletionRequestAsync

Sends a chat completion request to the OpenAI API using either a description and user message or an array of messages.

```csharp
Task<ChatCompletionResponse> ChatCompletionRequestAsync(string assistantDescription, string userMessage);
Task<ChatCompletionResponse> ChatCompletionRequestAsync(Message[] messages);
```

### ImageCreationRequestAsync

Sends an image creation request to the OpenAI API using the provided prompt text, number of images, and image size.

```csharp
Task<ImageCreationResponseModel> ImageCreationRequestAsync(string promptText, int numberOfImages, OpenAiImageSize imageSize);
```

### TranscribeAudioAsync

Transcribes audio data from either a memory stream or a file path using the OpenAI API.

```csharp
Task<TranscribeAudioResponseModel> TranscribeAudioAsync(MemoryStream memoryStream);
Task<TranscribeAudioResponseModel> TranscribeAudioAsync(string filePath);
```

## Example Usage

Here's an example of how to use the OpenAI Integration Library for chat completion:

```csharp
var openAiClient = serviceProvider.GetRequiredService<IOpenAiClient>();

string assistantDescription = "You are a helpful assistant.";
string userMessage = "Hello!";
var response = await openAiClient.ChatCompletionRequestAsync(assistantDescription, userMessage);

foreach (var message in response.Messages)
{
    Console.WriteLine($"{message.Role}: {message.Content}");
}
```