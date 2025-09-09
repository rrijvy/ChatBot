# ChatBot Configuration Examples

## 1. appsettings.json Configuration

```json
{
  "ChatBot": {
    "ModelName": "deepseek-r1:7b-qwen-distill-q8_0",
    "BaseUrl": "http://localhost:11434",
    "RequestTimeoutMinutes": 2,
    "Temperature": 0.7,
    "TopP": 0.9,
    "SystemPrompt": "You are a helpful and friendly AI assistant."
  }
}
```

## 2. Environment Variables

Set these environment variables to override configuration:

```bash
# Windows (Command Prompt)
set OLLAMA_MODEL=llama3.2:latest
set OLLAMA_URL=http://localhost:11434
set OLLAMA_TIMEOUT_MINUTES=3
set CHAT_TEMPERATURE=0.8
set CHAT_TOP_P=0.95
set CHAT_SYSTEM_PROMPT=You are a development assistant.

# Windows (PowerShell)
$env:OLLAMA_MODEL="llama3.2:latest"
$env:OLLAMA_URL="http://localhost:11434"

# Linux/Mac
export OLLAMA_MODEL=llama3.2:latest
export OLLAMA_URL=http://localhost:11434
export OLLAMA_TIMEOUT_MINUTES=3
```

## 3. .env File

Create a `.env` file in the application directory:

```env
# Ollama Configuration
OLLAMA_MODEL=deepseek-r1:7b-qwen-distill-q8_0
OLLAMA_URL=http://localhost:11434
OLLAMA_TIMEOUT_MINUTES=2

# Chat Settings
CHAT_TEMPERATURE=0.7
CHAT_TOP_P=0.9
CHAT_SYSTEM_PROMPT=You are a helpful and friendly AI assistant.
```

## Configuration Priority (highest to lowest)

1. **Environment Variables** - Highest priority
2. **appsettings.{Environment}.json** - Environment-specific settings
3. **appsettings.json** - Base configuration
4. **.env file** - Development convenience
5. **Default values** - Fallback values

## Available Models

Common Ollama models you can use:

- `llama3.2:latest`
- `llama3.2:1b` (smaller, faster)
- `deepseek-r1:7b-qwen-distill-q8_0` (reasoning model)
- `codellama:latest` (code-focused)
- `mistral:latest`
- `qwen2.5:latest`

## Configuration Parameters

| Parameter | Description | Default | Range/Options |
|-----------|-------------|---------|---------------|
| ModelName | Ollama model to use | deepseek-r1:7b-qwen-distill-q8_0 | Any valid Ollama model |
| BaseUrl | Ollama server URL | http://localhost:11434 | Valid HTTP/HTTPS URL |
| RequestTimeoutMinutes | Request timeout | 2 | > 0 |
| Temperature | Response randomness | 0.7 | 0.0 - 2.0 |
| TopP | Nucleus sampling | 0.9 | 0.0 - 1.0 |
| SystemPrompt | AI assistant behavior | Default friendly prompt | Any text |

## Environment-Specific Configuration

### Development Environment
- Higher temperature for creativity
- Longer timeout for debugging
- More detailed system prompt

### Production Environment  
- Lower temperature for consistency
- Shorter timeout for performance
- Concise system prompt