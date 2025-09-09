using ChatBot.Factories;
using ChatBot.UI;

try
{
    // Create ChatService from configuration (appsettings.json + environment variables + .env file)
    using var chatService = ChatServiceFactory.CreateChatServiceFromConfiguration();
    var consoleInterface = new ConsoleInterface(chatService);

    // Start the application
    consoleInterface.DisplayWelcomeMessage();

    // Test connection
    if (!await consoleInterface.TestConnectionAsync())
    {
        return;
    }

    // Run the main chat loop
    await consoleInterface.RunChatLoopAsync();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"❌ Configuration Error: {ex.Message}");
    Console.WriteLine();
    Console.WriteLine("Please check your configuration files:");
    Console.WriteLine("- appsettings.json");
    Console.WriteLine("- .env file (optional)");
    Console.WriteLine("- Environment variables");
    Console.WriteLine();
    Console.WriteLine("Example .env file:");
    Console.WriteLine("OLLAMA_MODEL=deepseek-r1:7b-qwen-distill-q8_0");
    Console.WriteLine("OLLAMA_URL=http://localhost:11434");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Unexpected error: {ex.Message}");
    Console.WriteLine("Please check the application logs for more details.");
}
