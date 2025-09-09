using Microsoft.Extensions.Configuration;
using DotNetEnv;
using ChatBot.Configuration;

namespace ChatBot.Services;

public interface IConfigurationService
{
    ChatBotSettings GetChatBotSettings();
    IConfiguration GetConfiguration();
}

public class ConfigurationService : IConfigurationService
{
    private readonly IConfiguration _configuration;
    private readonly ChatBotSettings _chatBotSettings;

    public ConfigurationService(string? environment = null)
    {
        // Load .env file first (lowest priority)
        LoadEnvironmentFile();

        // Build configuration from multiple sources
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // Add environment-specific settings
        environment ??= Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        
        if (!string.IsNullOrEmpty(environment))
        {
            configBuilder.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
        }

        // Environment variables override everything (highest priority)
        configBuilder.AddEnvironmentVariables();

        _configuration = configBuilder.Build();
        _chatBotSettings = LoadAndValidateSettings();
    }

    public ChatBotSettings GetChatBotSettings() => _chatBotSettings;

    public IConfiguration GetConfiguration() => _configuration;

    private void LoadEnvironmentFile()
    {
        try
        {
            var envFile = Path.Combine(Directory.GetCurrentDirectory(), ".env");
            if (File.Exists(envFile))
            {
                Env.Load(envFile);
            }
        }
        catch (Exception ex)
        {
            // Log warning but don't fail - .env file is optional
            Console.WriteLine($"Warning: Could not load .env file: {ex.Message}");
        }
    }

    private ChatBotSettings LoadAndValidateSettings()
    {
        var settings = ChatBotSettings.LoadFromConfiguration(_configuration);

        // Override with environment variables if they exist
        OverrideWithEnvironmentVariables(settings);

        // Validate settings
        try
        {
            settings.Validate();
        }
        catch (ArgumentException ex)
        {
            throw new InvalidOperationException($"Invalid configuration: {ex.Message}", ex);
        }

        return settings;
    }

    private void OverrideWithEnvironmentVariables(ChatBotSettings settings)
    {
        // Check for specific environment variable names
        var ollamaModel = Environment.GetEnvironmentVariable("OLLAMA_MODEL");
        if (!string.IsNullOrEmpty(ollamaModel))
        {
            settings.ModelName = ollamaModel;
        }

        var ollamaUrl = Environment.GetEnvironmentVariable("OLLAMA_URL");
        if (!string.IsNullOrEmpty(ollamaUrl))
        {
            settings.BaseUrl = ollamaUrl;
        }

        var timeoutMinutes = Environment.GetEnvironmentVariable("OLLAMA_TIMEOUT_MINUTES");
        if (!string.IsNullOrEmpty(timeoutMinutes) && int.TryParse(timeoutMinutes, out var timeout))
        {
            settings.RequestTimeoutMinutes = timeout;
        }

        var temperature = Environment.GetEnvironmentVariable("CHAT_TEMPERATURE");
        if (!string.IsNullOrEmpty(temperature) && double.TryParse(temperature, out var temp))
        {
            settings.Temperature = temp;
        }

        var topP = Environment.GetEnvironmentVariable("CHAT_TOP_P");
        if (!string.IsNullOrEmpty(topP) && double.TryParse(topP, out var topPValue))
        {
            settings.TopP = topPValue;
        }

        var systemPrompt = Environment.GetEnvironmentVariable("CHAT_SYSTEM_PROMPT");
        if (!string.IsNullOrEmpty(systemPrompt))
        {
            settings.SystemPrompt = systemPrompt;
        }
    }
}