using ChatBot.Configuration;
using ChatBot.Services;

namespace ChatBot.Utilities;

public static class ConfigurationHelper
{
    /// <summary>
    /// Displays current configuration settings (without sensitive data)
    /// </summary>
    public static void DisplayCurrentConfiguration(ChatBotSettings settings)
    {
        Console.WriteLine("?? Current Configuration:");
        Console.WriteLine($"  Model Name: {settings.ModelName}");
        Console.WriteLine($"  Base URL: {settings.BaseUrl}");
        Console.WriteLine($"  Timeout: {settings.RequestTimeoutMinutes} minutes");
        Console.WriteLine($"  Temperature: {settings.Temperature}");
        Console.WriteLine($"  Top P: {settings.TopP}");
        Console.WriteLine($"  System Prompt: {TruncateText(settings.SystemPrompt, 50)}");
        Console.WriteLine();
    }

    /// <summary>
    /// Validates and displays any configuration issues
    /// </summary>
    public static bool ValidateConfiguration(ChatBotSettings settings)
    {
        try
        {
            settings.Validate();
            Console.WriteLine("? Configuration is valid");
            return true;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"? Configuration error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Shows configuration loading priority and sources
    /// </summary>
    public static void ShowConfigurationSources()
    {
        Console.WriteLine("?? Configuration Loading Priority (highest to lowest):");
        Console.WriteLine("  1. Environment Variables (OLLAMA_MODEL, OLLAMA_URL, etc.)");
        Console.WriteLine("  2. appsettings.{Environment}.json");
        Console.WriteLine("  3. appsettings.json");
        Console.WriteLine("  4. .env file");
        Console.WriteLine("  5. Default values");
        Console.WriteLine();
    }

    /// <summary>
    /// Creates a sample configuration file content
    /// </summary>
    public static string GenerateSampleAppSettings()
    {
        return """
        {
          "ChatBot": {
            "ModelName": "your-model-name",
            "BaseUrl": "http://localhost:11434",
            "RequestTimeoutMinutes": 2,
            "Temperature": 0.7,
            "TopP": 0.9,
            "SystemPrompt": "You are a helpful AI assistant."
          }
        }
        """;
    }

    /// <summary>
    /// Creates a sample .env file content
    /// </summary>
    public static string GenerateSampleEnvFile()
    {
        return """
        # Ollama Configuration
        OLLAMA_MODEL=deepseek-r1:7b-qwen-distill-q8_0
        OLLAMA_URL=http://localhost:11434
        OLLAMA_TIMEOUT_MINUTES=2

        # Chat Settings
        CHAT_TEMPERATURE=0.7
        CHAT_TOP_P=0.9
        CHAT_SYSTEM_PROMPT=You are a helpful AI assistant.
        """;
    }

    private static string TruncateText(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
            return text;

        return text[..(maxLength - 3)] + "...";
    }
}