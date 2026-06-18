namespace CybersecurityChatbot.Part2GUI.Models
{
    // This class stores details that the chatbot remembers about the user.
    // It helps the chatbot personalise replies instead of starting over every time.
    public class UserMemory
    {
        // The user's name. The default name is used until the user provides a real name.
        public string Name { get; set; } = "friend";

        // The cybersecurity topic the user likes most, for example privacy or phishing.
        public string FavouriteTopic { get; set; } = string.Empty;

        // This stores the last topic discussed so follow-up questions can work.
        public string LastTopic { get; set; } = string.Empty;

        // This stores the last detected mood, for example worried, curious or frustrated.
        public string LastSentiment { get; set; } = "neutral";
    }
}
