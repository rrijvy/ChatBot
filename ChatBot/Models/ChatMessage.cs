namespace ChatBot.Models;

public class ChatMessage
{
    public string Role { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }

    public ChatMessage()
    {
        Timestamp = DateTime.Now;
    }

    public ChatMessage(string role, string content) : this()
    {
        Role = role;
        Content = content;
    }

    public static ChatMessage User(string content) => new("user", content);
    public static ChatMessage Assistant(string content) => new("assistant", content);
    public static ChatMessage System(string content) => new("system", content);
}