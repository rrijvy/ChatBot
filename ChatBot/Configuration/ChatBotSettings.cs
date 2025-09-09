using Microsoft.Extensions.Configuration;

namespace ChatBot.Configuration;

public class ChatBotSettings
{
    public const string SectionName = "ChatBot";

    public string ModelName { get; set; } = "deepseek-r1:7b-qwen-distill-q8_0";
    public string BaseUrl { get; set; } = "http://localhost:11434";
    public int RequestTimeoutMinutes { get; set; } = 2;
    public double Temperature { get; set; } = 0.7;
    public double TopP { get; set; } = 0.9;
    public string SystemPrompt { get; set; } = "You are a helpful and friendly AI assistant. You maintain context from our conversation and provide thoughtful, relevant responses.";

    public TimeSpan RequestTimeout => TimeSpan.FromMinutes(RequestTimeoutMinutes);

    public static ChatBotSettings LoadFromConfiguration(IConfiguration configuration)
    {
        var settings = new ChatBotSettings();
        configuration.GetSection(SectionName).Bind(settings);
        return settings;
    }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(ModelName))
            throw new ArgumentException("ModelName cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(BaseUrl))
            throw new ArgumentException("BaseUrl cannot be null or empty.");

        if (!Uri.TryCreate(BaseUrl, UriKind.Absolute, out _))
            throw new ArgumentException("BaseUrl must be a valid URL.");

        if (RequestTimeoutMinutes <= 0)
            throw new ArgumentException("RequestTimeoutMinutes must be greater than 0.");

        if (Temperature < 0 || Temperature > 2)
            throw new ArgumentException("Temperature must be between 0 and 2.");

        if (TopP < 0 || TopP > 1)
            throw new ArgumentException("TopP must be between 0 and 1.");

        if (string.IsNullOrWhiteSpace(SystemPrompt))
            throw new ArgumentException("SystemPrompt cannot be null or empty.");
    }
}