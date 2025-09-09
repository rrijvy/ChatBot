using ChatBot.Models;
using ChatBot.Configuration;
using ChatBot.Interfaces;

namespace ChatBot.Services;

public class ChatService : IChatService, IDisposable
{
    private readonly IOllamaApiClient _ollamaClient;
    private readonly ConversationService _conversationService;
    private readonly ChatBotSettings _settings;
    private readonly HttpClient _httpClient;

    public ChatService(ChatBotSettings? settings = null)
    {
        _settings = settings ?? new ChatBotSettings();
        _httpClient = new HttpClient
        {
            Timeout = _settings.RequestTimeout
        };
        _ollamaClient = new OllamaApiClient(_httpClient, _settings.BaseUrl);
        _conversationService = new ConversationService(_settings.SystemPrompt);
    }

    public async Task<string> GetResponseAsync(string userMessage)
    {
        try
        {
            // Add user message to conversation history
            _conversationService.AddUserMessage(userMessage);

            // Build context from conversation history
            var context = _conversationService.BuildContextFromHistory();

            // Create request for Ollama API
            var request = new OllamaRequest
            {
                Model = _settings.ModelName,
                Prompt = context,
                Stream = false,
                Options = new OllamaOptions
                {
                    Temperature = _settings.Temperature,
                    TopP = _settings.TopP
                }
            };

            // Get response from Ollama
            var ollamaResponse = await _ollamaClient.GenerateAsync(request);
            var assistantResponse = ollamaResponse.Response?.Trim() ?? "I'm sorry, I couldn't generate a response.";

            // Add assistant response to conversation history
            _conversationService.AddAssistantMessage(assistantResponse);

            return assistantResponse;
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException(
                $"Failed to connect to Ollama. Please ensure Ollama is running locally on {_settings.BaseUrl}",
                ex);
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            throw new InvalidOperationException("Request timed out. The model might be taking too long to respond.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error getting response from chatbot: {ex.Message}", ex);
        }
    }

    public void ClearHistory()
    {
        _conversationService.ClearHistory();
    }

    public IReadOnlyList<ChatMessage> GetHistory()
    {
        return _conversationService.GetHistory();
    }

    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            return await _ollamaClient.TestConnectionAsync(_settings.ModelName);
        }
        catch
        {
            return false;
        }
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}