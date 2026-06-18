namespace CybersecurityChatbot.Part2GUI.Services
{
    // This simple class carries the chatbot answer and the topic that matched.
    // It makes the code easier to expand later in Part 3.
    public class ChatResult
    {
        // This is the message that will be shown to the user.
        public string Reply { get; set; } = string.Empty;

        // This is the cybersecurity topic that matched the user input.
        public string Topic { get; set; } = string.Empty;
    }
}
