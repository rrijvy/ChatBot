using ChatBot.Interfaces;
using ChatBot.Models;
using System.Text.Json;

namespace ChatBot.Services;



public class OllamaApiClient : IOllamaApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public OllamaApiClient(HttpClient httpClient, string baseUrl)
    {
        _httpClient = httpClient;
        _baseUrl = baseUrl.TrimEnd('/');
    }

    public async Task<OllamaResponse> GenerateAsync(OllamaRequest request)
    {
        var jsonRequest = JsonSerializer.Serialize(request);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_baseUrl}/api/generate", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Ollama API returned {response.StatusCode}: {response.ReasonPhrase}. Error: {errorContent}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var ollamaResponse = JsonSerializer.Deserialize<OllamaResponse>(jsonResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return ollamaResponse ?? throw new InvalidOperationException("Failed to deserialize Ollama response");
    }

    public async Task<bool> TestConnectionAsync(string modelName)
    {
        try
        {
            var testRequest = new OllamaRequest
            {
                Model = modelName,
                Prompt = "Hello",
                Stream = false
            };

            var response = await GenerateAsync(testRequest);
            return !string.IsNullOrEmpty(response.Response);
        }
        catch
        {
            return false;
        }
    }
}