# Contextual ChatBot with Ollama

A well-organized C# console application that creates a contextual chatbot using Ollama for local AI inference. Features comprehensive configuration management with support for multiple configuration sources.

## Features

- **Contextual Conversations**: The bot remembers previous messages in the conversation
- **Local AI**: Uses Ollama running locally (no external API calls required)
- **Flexible Configuration**: Multiple configuration sources (JSON, environment variables, .env files)
- **Easy Commands**: Simple commands for managing conversation history and configuration
- **Error Handling**: Graceful handling when Ollama is not available
- **Clean Architecture**: Well-organized code with separation of concerns
- **Extensible Design**: Modular architecture that's easy to extend and maintain

## Quick Start

1. **Install Ollama**:
   ```bash
   # Download from https://ollama.ai/
   ollama serve
   ollama pull deepseek-r1:7b-qwen-distill-q8_0
   ```

2. **Run the ChatBot**:
   ```bash
   dotnet run
   ```

3. **Chat Commands**:
   - Type any message to chat
   - `config` - Show configuration info
   - `clear` - Clear conversation history  
   - `history` - View conversation history
   - `help` - Show available commands
   - `quit`, `exit`, `bye` - Exit application

## Configuration

### Priority Order (highest to lowest)
1. **Environment Variables** (OLLAMA_MODEL, OLLAMA_URL, etc.)
2. **appsettings.{Environment}.json** (e.g., appsettings.Development.json)
3. **appsettings.json** (base configuration)
4. **.env file** (for development convenience)
5. **Default values** (hardcoded fallbacks)

### Quick Configuration Examples

**Set model via environment variable:**
```bash
# Windows
set OLLAMA_MODEL=llama3.2:latest

# Linux/Mac  
export OLLAMA_MODEL=llama3.2:latest
```

**Create .env file:**
```env
OLLAMA_MODEL=deepseek-r1:7b-qwen-distill-q8_0
OLLAMA_URL=http://localhost:11434
CHAT_TEMPERATURE=0.7
```

**Edit appsettings.json:**
```json
{
  "ChatBot": {
    "ModelName": "llama3.2:latest",
    "BaseUrl": "http://localhost:11434",
    "Temperature": 0.8
  }
}
```

See [CONFIGURATION.md](CONFIGURATION.md) for complete configuration guide.

## Project Structure

```
ChatBot/
??? Program.cs                      # Application entry point
??? Configuration/
?   ??? ChatBotSettings.cs          # Configuration model with validation
??? Services/
?   ??? IChatService.cs             # Chat service interface  
?   ??? ChatService.cs              # Main chat orchestration
?   ??? ConversationService.cs      # Conversation history management
?   ??? OllamaApiClient.cs          # Ollama HTTP client
?   ??? ConfigurationService.cs     # Multi-source configuration loader
??? Models/
?   ??? ChatMessage.cs              # Chat message structure
?   ??? OllamaRequest.cs            # API request models
?   ??? OllamaResponse.cs           # API response models
??? UI/
?   ??? ConsoleInterface.cs         # Console user interface
??? Utilities/
?   ??? Constants.cs                # Application constants
?   ??? ConfigurationHelper.cs      # Configuration utilities
??? Exceptions/
?   ??? ChatBotExceptions.cs        # Custom exception types
??? Factories/
?   ??? ChatServiceFactory.cs       # Service creation patterns
??? appsettings.json                # Base configuration
??? appsettings.Development.json    # Development overrides
??? .env                           # Environment variables (optional)
??? CONFIGURATION.md               # Detailed configuration guide
```

## Architecture Overview

### **Configuration System**
- **`ConfigurationService`**: Loads and validates settings from multiple sources
- **`ChatBotSettings`**: Strongly-typed configuration with validation
- **`ConfigurationHelper`**: Utilities for displaying and managing configuration

### **Services Layer**
- **`ChatService`**: Main orchestration service implementing `IChatService`
- **`ConversationService`**: Manages conversation history and context building
- **`OllamaApiClient`**: Handles HTTP communication with Ollama API

### **Factory Pattern**
- **`ChatServiceFactory`**: Creates services with different configuration sources
- Supports development, production, and custom configurations

## Available Configuration Options

| Setting | Environment Variable | Default | Description |
|---------|---------------------|---------|-------------|
| ModelName | OLLAMA_MODEL | deepseek-r1:7b-qwen-distill-q8_0 | Ollama model to use |
| BaseUrl | OLLAMA_URL | http://localhost:11434 | Ollama server URL |
| RequestTimeoutMinutes | OLLAMA_TIMEOUT_MINUTES | 2 | Request timeout |
| Temperature | CHAT_TEMPERATURE | 0.7 | Response creativity (0-2) |
| TopP | CHAT_TOP_P | 0.9 | Nucleus sampling (0-1) |
| SystemPrompt | CHAT_SYSTEM_PROMPT | Default | AI behavior prompt |

## Usage Examples

### Basic Usage
```bash
dotnet run
```

### With Custom Model
```bash
set OLLAMA_MODEL=llama3.2:latest
dotnet run
```

### Development Mode
```bash
set ASPNETCORE_ENVIRONMENT=Development
dotnet run
```

## Common Models

| Model | Size | Use Case |
|-------|------|----------|
| `llama3.2:1b` | Small | Fast responses |
| `llama3.2:latest` | Medium | General purpose |
| `deepseek-r1:7b-qwen-distill-q8_0` | Large | Complex reasoning |
| `codellama:latest` | Medium | Code assistance |

## Example Conversation

```
?? Welcome to the Contextual ChatBot!
Testing connection to Ollama... ? Connected!

You: What model are you using?
Bot: I'm running on deepseek-r1:7b-qwen-distill-q8_0 through Ollama...

You: config
?? Configuration Information:
Configuration is loaded from multiple sources...

You: What did I ask about earlier?
Bot: You asked about what model I'm using. I told you I'm running on deepseek-r1...
```

## Extending the ChatBot

### Add New Configuration Sources
```csharp
public class DatabaseConfigurationService : IConfigurationService
{
    // Load configuration from database
}
```

### Add New Commands
```csharp
case "save":
    await SaveConversationAsync();
    return true;
```

### Environment-Specific Settings
Create `appsettings.{Environment}.json` files:
- `appsettings.Development.json` - Development settings
- `appsettings.Production.json` - Production settings  
- `appsettings.Testing.json` - Testing settings

## Error Handling

The application provides detailed error messages for:
- **Configuration errors**: Invalid settings, missing required values
- **Connection errors**: Ollama not running, network issues
- **Model errors**: Model not found, API errors
- **Timeout errors**: Request taking too long

## Troubleshooting

### Configuration Issues
```bash
# Check current configuration
dotnet run
# Type: config

# Validate configuration
# The app will show validation errors on startup
```

### Model Not Found
```bash
# List available models
ollama list

# Pull a model
ollama pull llama3.2:latest

# Set the model
set OLLAMA_MODEL=llama3.2:latest
```

### Connection Issues
```bash
# Start Ollama
ollama serve

# Check if running
curl http://localhost:11434/api/tags
```

## Best Practices

1. **Environment Variables for Deployment**: Use environment variables in production
2. **appsettings.json for Defaults**: Keep base configuration in JSON files
3. **.env for Development**: Use .env files for local development
4. **Validation**: The app validates all configuration on startup
5. **Secrets Management**: Never commit sensitive data to version control

## Dependencies

- **Microsoft.Extensions.Configuration** - Configuration framework
- **DotNetEnv** - .env file support
- **System.Text.Json** - JSON serialization
- **HttpClient** - REST API communication