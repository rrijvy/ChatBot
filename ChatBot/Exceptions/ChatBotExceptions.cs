namespace ChatBot.Exceptions;

public class OllamaException : Exception
{
    public OllamaException(string message) : base(message) { }
    public OllamaException(string message, Exception innerException) : base(message, innerException) { }
}

public class OllamaConnectionException : OllamaException
{
    public OllamaConnectionException(string message) : base(message) { }
    public OllamaConnectionException(string message, Exception innerException) : base(message, innerException) { }
}

public class OllamaTimeoutException : OllamaException
{
    public OllamaTimeoutException(string message) : base(message) { }
    public OllamaTimeoutException(string message, Exception innerException) : base(message, innerException) { }
}