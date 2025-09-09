namespace ChatBot.Utilities;

public static class Constants
{
    public static class Roles
    {
        public const string System = "system";
        public const string User = "user";  
        public const string Assistant = "assistant";
    }

    public static class Commands
    {
        public static readonly string[] ExitCommands = { "quit", "exit", "bye" };
        public const string Clear = "clear";
        public const string History = "history";
    }

    public static class Messages
    {
        public const string Welcome = "?? Welcome to the Contextual ChatBot!";
        public const string ConnectionSuccess = "? Connected!";
        public const string ConnectionFailed = "? Connection failed!";
        public const string Goodbye = "?? Goodbye! Thanks for chatting!";
        public const string HistoryCleared = "?? Conversation history cleared!";
        public const string HistoryTitle = "\n?? Conversation History:";
    }

    public static class Icons
    {
        public const string User = "??";
        public const string Bot = "??";
        public const string Error = "?";
    }
}