using ChatBot.Models;

namespace ChatBot.Interfaces;

public interface IChatService : IDisposable
{
    Task<string> GetResponseAsync(string userMessage);
    void ClearHistory();
    IReadOnlyList<ChatMessage> GetHistory();
    Task<bool> TestConnectionAsync();
}