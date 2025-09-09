using ChatBot.Interfaces;
using ChatBot.Models;
using ChatBot.Utilities;

namespace ChatBot.UI;

public class ConsoleInterface
{
    private readonly IChatService _chatService;

    public ConsoleInterface(IChatService chatService)
    {
        _chatService = chatService;
    }

    public void DisplayWelcomeMessage()
    {
        Console.WriteLine(Constants.Messages.Welcome);
        Console.WriteLine("This bot uses Ollama for contextual conversations.");
        Console.WriteLine("Available commands:");
        Console.WriteLine("  • Type any message to chat with the bot");
        Console.WriteLine("  • 'quit', 'exit', or 'bye' - End the conversation");
        Console.WriteLine("  • 'clear' - Clear conversation history");
        Console.WriteLine("  • 'history' - View conversation history");
        Console.WriteLine("  • 'config' - Show current configuration");
        Console.WriteLine("  • 'help' - Show this help message");
        Console.WriteLine(new string('-', 50));
    }

    public async Task<bool> TestConnectionAsync()
    {
        Console.Write("Testing connection to Ollama... ");
        try
        {
            bool isConnected = await _chatService.TestConnectionAsync();
            if (isConnected)
            {
                Console.WriteLine(Constants.Messages.ConnectionSuccess);
                return true;
            }
            else
            {
                DisplayConnectionError();
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{Constants.Icons.Error} Connection error!");
            Console.WriteLine($"Error: {ex.Message}");
            DisplayConnectionHelp();
            return false;
        }
    }

    public async Task RunChatLoopAsync()
    {
        Console.WriteLine("\nYou can start chatting now!");
        Console.WriteLine();

        while (true)
        {
            Console.Write("You: ");
            var userInput = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(userInput))
                continue;

            // Handle exit commands
            if (IsExitCommand(userInput))
            {
                Console.WriteLine(Constants.Messages.Goodbye);
                break;
            }

            // Handle special commands
            if (await HandleSpecialCommandsAsync(userInput))
                continue;

            // Get bot response
            await HandleUserMessageAsync(userInput);
        }
    }

    private bool IsExitCommand(string input)
    {
        return Constants.Commands.ExitCommands.Contains(input.ToLower());
    }

    private async Task<bool> HandleSpecialCommandsAsync(string input)
    {
        switch (input.ToLower())
        {
            case Constants.Commands.Clear:
                _chatService.ClearHistory();
                Console.WriteLine(Constants.Messages.HistoryCleared);
                return true;

            case Constants.Commands.History:
                DisplayConversationHistory();
                return true;

            case "config":
                DisplayConfiguration();
                return true;

            case "help":
                DisplayHelp();
                return true;

            default:
                return false;
        }
    }

    private async Task HandleUserMessageAsync(string userMessage)
    {
        try
        {
            Console.Write("Bot: ");
            var response = await _chatService.GetResponseAsync(userMessage);
            Console.WriteLine(response);
            Console.WriteLine();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"{Constants.Icons.Error} Error: {ex.Message}");
            Console.WriteLine();

            if (ex.InnerException is HttpRequestException)
            {
                DisplayConnectionHelp();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{Constants.Icons.Error} Unexpected error: {ex.Message}");
            Console.WriteLine();
        }
    }

    private void DisplayConversationHistory()
    {
        Console.WriteLine(Constants.Messages.HistoryTitle);
        var history = _chatService.GetHistory();
        foreach (var msg in history.Skip(1)) // Skip system message
        {
            var icon = msg.Role == Constants.Roles.User ? Constants.Icons.User : Constants.Icons.Bot;
            Console.WriteLine($"{icon} {msg.Role.ToUpper()}: {msg.Content}");
        }
        Console.WriteLine();
    }

    private void DisplayConfiguration()
    {
        try
        {
            // This is a simple way to show config - in a real app you might inject the config service
            Console.WriteLine("📋 Configuration Information:");
            Console.WriteLine("Configuration is loaded from multiple sources in this priority order:");
            ConfigurationHelper.ShowConfigurationSources();
            Console.WriteLine("To modify settings, you can:");
            Console.WriteLine("  • Edit appsettings.json or appsettings.{Environment}.json");
            Console.WriteLine("  • Set environment variables (OLLAMA_MODEL, OLLAMA_URL, etc.)");
            Console.WriteLine("  • Create a .env file in the application directory");
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{Constants.Icons.Error} Could not display configuration: {ex.Message}");
        }
    }

    private void DisplayHelp()
    {
        Console.WriteLine("🤖 ChatBot Help:");
        Console.WriteLine();
        Console.WriteLine("Commands:");
        Console.WriteLine("  • Type any message to chat with the bot");
        Console.WriteLine("  • 'quit', 'exit', 'bye' - Exit the application");
        Console.WriteLine("  • 'clear' - Clear conversation history");
        Console.WriteLine("  • 'history' - Show conversation history");
        Console.WriteLine("  • 'config' - Show configuration information");
        Console.WriteLine("  • 'help' - Show this help message");
        Console.WriteLine();
        Console.WriteLine("Configuration:");
        Console.WriteLine("  • The bot loads settings from appsettings.json, environment variables, and .env files");
        Console.WriteLine("  • You can change the model by setting OLLAMA_MODEL environment variable");
        Console.WriteLine("  • Default model: deepseek-r1:7b-qwen-distill-q8_0");
        Console.WriteLine();
    }

    private void DisplayConnectionError()
    {
        Console.WriteLine(Constants.Messages.ConnectionFailed);
        DisplayConnectionHelp();
    }

    private void DisplayConnectionHelp()
    {
        Console.WriteLine("Please make sure Ollama is running locally on http://localhost:11434");
        Console.WriteLine("You can start Ollama by running: ollama serve");
        Console.WriteLine("💡 Tips:");
        Console.WriteLine("  • Check available models: ollama list");
        Console.WriteLine("  • Pull a model: ollama pull model-name");
        Console.WriteLine("  • Set OLLAMA_MODEL environment variable to change the model");
    }
}