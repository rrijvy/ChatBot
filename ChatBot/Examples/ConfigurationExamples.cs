using ChatBot.Factories;
using ChatBot.Interfaces;
using ChatBot.Utilities;

namespace ChatBot.Examples;

/// <summary>
/// Examples demonstrating different ways to configure the ChatBot
/// </summary>
public static class ConfigurationExamples
{
    /// <summary>
    /// Example: Create ChatService with default configuration
    /// </summary>
    public static IChatService CreateWithDefaults()
    {
        return ChatServiceFactory.CreateChatService();
    }

    /// <summary>
    /// Example: Create ChatService with custom model
    /// </summary>
    public static IChatService CreateWithCustomModel()
    {
        return ChatServiceFactory.CreateChatServiceWithCustomModel(
            modelName: "llama3.2:latest",
            baseUrl: "http://localhost:11434"
        );
    }

    /// <summary>
    /// Example: Create ChatService from configuration files and environment variables
    /// </summary>
    public static IChatService CreateFromConfiguration()
    {
        return ChatServiceFactory.CreateChatServiceFromConfiguration();
    }

    /// <summary>
    /// Example: Create ChatService for development environment
    /// </summary>
    public static IChatService CreateForDevelopment()
    {
        return ChatServiceFactory.CreateDevelopmentChatService();
    }

    /// <summary>
    /// Example: Create ChatService for production environment
    /// </summary>
    public static IChatService CreateForProduction()
    {
        return ChatServiceFactory.CreateProductionChatService();
    }

    /// <summary>
    /// Example: Test different configuration methods
    /// </summary>
    public static async Task DemonstrateConfigurationMethods()
    {
        Console.WriteLine("?? ChatBot Configuration Examples\n");

        // Show configuration loading order
        ConfigurationHelper.ShowConfigurationSources();

        try
        {
            // Test configuration loading
            Console.WriteLine("Testing configuration methods...\n");

            using var defaultService = CreateWithDefaults();
            Console.WriteLine("? Default configuration loaded");

            using var customService = CreateWithCustomModel();
            Console.WriteLine("? Custom model configuration loaded");

            using var configService = CreateFromConfiguration();
            Console.WriteLine("? Configuration file loading successful");

            // Test connection with current configuration
            Console.WriteLine("\nTesting connection...");
            bool connected = await configService.TestConnectionAsync();
            Console.WriteLine(connected ? "? Connection successful" : "? Connection failed");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"? Configuration error: {ex.Message}");
        }

        Console.WriteLine("\n?? To customize configuration:");
        Console.WriteLine("1. Edit appsettings.json");
        Console.WriteLine("2. Set environment variables (OLLAMA_MODEL, OLLAMA_URL)");
        Console.WriteLine("3. Create a .env file");
        Console.WriteLine("4. Create appsettings.Development.json for development settings");
    }
}