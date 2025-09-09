using ChatBot.Models;
using System.Text;

namespace ChatBot.Services;

public class ConversationService
{
    private readonly List<ChatMessage> _conversationHistory;
    private readonly string _systemPrompt;

    public ConversationService(string systemPrompt)
    {
        _systemPrompt = systemPrompt;
        _conversationHistory = new List<ChatMessage>();
        AddSystemMessage();
    }

    public void AddUserMessage(string content)
    {
        _conversationHistory.Add(ChatMessage.User(content));
    }

    public void AddAssistantMessage(string content)
    {
        _conversationHistory.Add(ChatMessage.Assistant(content));
    }

    public void ClearHistory()
    {
        _conversationHistory.Clear();
        AddSystemMessage();
    }

    public IReadOnlyList<ChatMessage> GetHistory() => _conversationHistory.AsReadOnly();

    public string BuildContextFromHistory()
    {
        var contextBuilder = new StringBuilder();

        foreach (var message in _conversationHistory)
        {
            switch (message.Role.ToLower())
            {
                case "system":
                    contextBuilder.AppendLine($"System: {message.Content}");
                    break;
                case "user":
                    contextBuilder.AppendLine($"Human: {message.Content}");
                    break;
                case "assistant":
                    contextBuilder.AppendLine($"Assistant: {message.Content}");
                    break;
            }
        }

        contextBuilder.AppendLine("Assistant:");
        return contextBuilder.ToString();
    }

    private void AddSystemMessage()
    {
        _conversationHistory.Add(ChatMessage.System(_systemPrompt));
    }
}