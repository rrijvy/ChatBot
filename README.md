# ?? ChatBot with Ollama

A professional-grade C# console application that creates contextual conversations using Ollama for local AI inference. Built with clean architecture, comprehensive configuration management, and extensible design patterns.

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](CONTRIBUTING.md)

## ? Features

- **?? Contextual Conversations**: Maintains conversation history and context across interactions
- **?? Local AI**: Uses Ollama running locally (no external API calls or data sharing)
- **?? Flexible Configuration**: Multiple configuration sources with priority management
- **?? Interactive Commands**: Built-in commands for history, configuration, and help
- **??? Robust Error Handling**: Comprehensive error handling with detailed messages
- **??? Clean Architecture**: Well-organized code with separation of concerns
- **?? Easy Extension**: Modular design for adding new features and AI providers

## ?? Quick Start

### Prerequisites

1. **[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)**
2. **[Ollama](https://ollama.ai/)** - Local AI inference engine

### Installation & Setup

```bash
# 1. Clone the repository
git clone https://github.com/rrijvy/ChatBot.git
cd ChatBot

# 2. Install and start Ollama (if not already done)
# Download from https://ollama.ai/
ollama serve

# 3. Pull a model (choose one)
ollama pull deepseek-r1:7b-qwen-distill-q8_0  # Default model (reasoning)
ollama pull llama3.2:latest                    # Alternative (general purpose)
ollama pull llama3.2:1b                        # Lightweight option

# 4. Run the ChatBot
cd ChatBot
dotnet run
```

### First Conversation

```
?? Welcome to the Contextual ChatBot!
Testing connection to Ollama... ? Connected!

You: Hello! What can you help me with?
Bot: Hello! I'm an AI assistant powered by Ollama running locally on your machine...

You: What did I just ask?
Bot: You asked what I can help you with. You greeted me and wanted to know about my capabilities...
```

## ?? Usage

### Available Commands

| Command | Description |
|---------|-------------|
| `help` | Show available commands and usage information |
| `config` | Display current configuration and sources |
| `history` | View the conversation history |
| `clear` | Clear the conversation history |
| `quit` / `exit` / `bye` | Exit the application |

### Configuration Options

The ChatBot supports multiple configuration methods with the following priority order:

1. **Environment Variables** (highest priority)
2. **appsettings.{Environment}.json**
3. **appsettings.json**
4. **.env file**
5. **Default values** (lowest priority)

#### Quick Configuration Examples

**Environment Variables:**
```bash
# Windows
set OLLAMA_MODEL=llama3.2:latest
set OLLAMA_URL=http://localhost:11434

# Linux/Mac
export OLLAMA_MODEL=llama3.2:latest
export OLLAMA_URL=http://localhost:11434
```

**.env File:**
```env
OLLAMA_MODEL=deepseek-r1:7b-qwen-distill-q8_0
OLLAMA_URL=http://localhost:11434
CHAT_TEMPERATURE=0.7
```

**appsettings.json:**
```json
{
  "ChatBot": {
    "ModelName": "llama3.2:latest",
    "BaseUrl": "http://localhost:11434",
    "Temperature": 0.8,
    "TopP": 0.9
  }
}
```

See [CONFIGURATION.md](ChatBot/CONFIGURATION.md) for complete configuration documentation.

## ??? Architecture

### Project Structure

```
ChatBot/
??? ?? Configuration/          # Settings and configuration management
??? ?? Services/              # Business logic and API clients
??? ?? Models/               # Data models and DTOs
??? ?? UI/                   # User interface components
??? ?? Utilities/            # Helper classes and constants
??? ?? Exceptions/           # Custom exception types
??? ?? Factories/            # Service creation patterns
??? ?? Examples/             # Usage examples and demos
```

### Key Components

- **ConfigurationService**: Multi-source configuration loading and validation
- **ChatService**: Main conversation orchestration
- **ConversationService**: History management and context building
- **OllamaApiClient**: HTTP communication with Ollama
- **ConsoleInterface**: User interaction handling

## ?? Configuration

### Available Settings

| Setting | Environment Variable | Default | Description |
|---------|---------------------|---------|-------------|
| ModelName | `OLLAMA_MODEL` | deepseek-r1:7b-qwen-distill-q8_0 | AI model to use |
| BaseUrl | `OLLAMA_URL` | http://localhost:11434 | Ollama server URL |
| RequestTimeoutMinutes | `OLLAMA_TIMEOUT_MINUTES` | 2 | Request timeout |
| Temperature | `CHAT_TEMPERATURE` | 0.7 | Response creativity (0-2) |
| TopP | `CHAT_TOP_P` | 0.9 | Nucleus sampling (0-1) |
| SystemPrompt | `CHAT_SYSTEM_PROMPT` | Default prompt | AI behavior instructions |

### Popular Models

| Model | Size | Best For | Speed |
|-------|------|----------|--------|
| `llama3.2:1b` | ~1GB | Quick responses, simple tasks | ??? |
| `llama3.2:latest` | ~4.7GB | General conversations | ?? |
| `deepseek-r1:7b-qwen-distill-q8_0` | ~7GB | Complex reasoning, analysis | ? |
| `codellama:latest` | ~3.8GB | Programming assistance | ?? |

## ?? Examples

### Basic Usage

```csharp
using ChatBot.Factories;

// Create with default configuration
using var chatService = ChatServiceFactory.CreateChatServiceFromConfiguration();

// Send a message
var response = await chatService.GetResponseAsync("Hello, how are you?");
Console.WriteLine(response);
```

### Custom Configuration

```csharp
using ChatBot.Configuration;

var settings = new ChatBotSettings
{
    ModelName = "llama3.2:latest",
    Temperature = 0.8,
    BaseUrl = "http://custom-ollama:11434"
};

using var chatService = ChatServiceFactory.CreateChatService(settings);
```

## ??? Development

### Building from Source

```bash
git clone https://github.com/rrijvy/ChatBot.git
cd ChatBot/ChatBot
dotnet build
dotnet run
```

### Running Tests

```bash
dotnet test
```

### Development Environment

```bash
set ASPNETCORE_ENVIRONMENT=Development
dotnet run
```

This uses `appsettings.Development.json` for development-specific settings.

## ?? Contributing

Contributions are welcome! Here are some ways you can help:

- ?? **Report bugs** by creating issues
- ?? **Suggest features** or improvements
- ?? **Submit pull requests** with bug fixes or new features
- ?? **Improve documentation**

### Development Guidelines

1. Follow C# coding conventions
2. Add unit tests for new features
3. Update documentation for changes
4. Ensure all tests pass before submitting PR

## ?? Requirements

- **.NET 8.0** or later
- **Ollama** running locally
- **Available model** (downloaded via Ollama)

## ?? Troubleshooting

### Common Issues

**Connection Failed:**
```bash
# Start Ollama
ollama serve

# Verify it's running
curl http://localhost:11434/api/tags
```

**Model Not Found:**
```bash
# List available models
ollama list

# Pull the required model
ollama pull deepseek-r1:7b-qwen-distill-q8_0
```

**Configuration Issues:**
- Use the `config` command in the app to see current settings
- Check the [CONFIGURATION.md](ChatBot/CONFIGURATION.md) guide
- Verify environment variables are set correctly

## ?? License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## ?? Acknowledgments

- **[Ollama](https://ollama.ai/)** - Local AI inference engine
- **[Microsoft .NET](https://dotnet.microsoft.com/)** - Application framework
- **AI Community** - For inspiring this project

## ?? Contact

- **GitHub**: [@rrijvy](https://github.com/rrijvy)
- **Issues**: [Project Issues](https://github.com/rrijvy/ChatBot/issues)

---

? **Star this repository if you find it useful!**