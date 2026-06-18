using System;

namespace CybersecurityChatbot.Part2GUI.Models
{
    // This class represents one message shown in the chat screen.
    // It helps the GUI display bot messages on the left and user messages on the right.
    public class ChatMessage
    {
        // This stores the actual message text.
        public string Text { get; set; } = string.Empty;

        // This tells the GUI if the message belongs to the user.
        public bool IsFromUser { get; set; }

        // This stores the name that must appear above the message bubble.
        public string Sender { get; set; } = string.Empty;

        // This stores the time when the message was created.
        public string Time { get; set; } = DateTime.Now.ToString("HH:mm");
    }
}
