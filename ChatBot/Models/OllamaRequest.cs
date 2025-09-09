namespace ChatBot.Models;

public class OllamaRequest
{
    public string Model { get; set; } = string.Empty;
    public string Prompt { get; set; } = string.Empty;
    public bool Stream { get; set; } = false;
    public OllamaOptions? Options { get; set; }
}

public class OllamaOptions
{
    public double Temperature { get; set; } = 0.7;
    public double TopP { get; set; } = 0.9;
}