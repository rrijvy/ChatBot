using ChatBot.Configuration;
using ChatBot.Interfaces;
using ChatBot.Services;

namespace ChatBot.Factories;

public static class ChatServiceFactory
{
    /// <summary>
    /// Creates a ChatService with the provided settings
    /// </summary>
    public static IChatService CreateChatService(ChatBotSettings? settings = null)
    {
        return new ChatService(settings ?? new ChatBotSettings());
    }

    /// <summary>
    /// Creates a ChatService with custom model and base URL
    /// </summary>
    public static IChatService CreateChatServiceWithCustomModel(string modelName, string baseUrl = "http://localhost:11434")
    {
        var settings = new ChatBotSettings
        {
            ModelName = modelName,
            BaseUrl = baseUrl
        };
        
        return new ChatService(settings);
    }

    /// <summary>
    /// Creates a ChatService using configuration from appsettings.json and environment variables
    /// Priority: Environment Variables > appsettings.{Environment}.json > appsettings.json > .env file
    /// </summary>
    public static IChatService CreateChatServiceFromConfiguration(string? environment = null)
    {
        var configService = new ConfigurationService(environment);
        var settings = configService.GetChatBotSettings();
        
        return new ChatService(settings);
    }

    /// <summary>
    /// Creates a ChatService from environment variables only (legacy method - kept for backward compatibility)
    /// </summary>
    [Obsolete("Use CreateChatServiceFromConfiguration() instead for better configuration support")]
    public static IChatService CreateChatServiceFromEnvironment()
    {
        var settings = new ChatBotSettings
        {
            ModelName = Environment.GetEnvironmentVariable("OLLAMA_MODEL") ?? "deepseek-r1:7b-qwen-distill-q8_0",
            BaseUrl = Environment.GetEnvironmentVariable("OLLAMA_URL") ?? "http://localhost:11434",
        };

        // Override timeout if specified
        var timeoutMinutes = Environment.GetEnvironmentVariable("OLLAMA_TIMEOUT_MINUTES");
        if (!string.IsNullOrEmpty(timeoutMinutes) && int.TryParse(timeoutMinutes, out var timeout))
        {
            settings.RequestTimeoutMinutes = timeout;
        }

        return new ChatService(settings);
    }

    /// <summary>
    /// Creates a ChatService for development with detailed logging and debugging features
    /// </summary>
    public static IChatService CreateDevelopmentChatService()
    {
        return CreateChatServiceFromConfiguration("Development");
    }

    /// <summary>
    /// Creates a ChatService for production with optimized settings
    /// </summary>
    public static IChatService CreateProductionChatService()
    {
        return CreateChatServiceFromConfiguration("Production");
    }
}