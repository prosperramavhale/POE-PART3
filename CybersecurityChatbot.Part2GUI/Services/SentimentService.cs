using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot.Part2GUI.Services
{
    // This class checks simple emotions from the user's message.
    // It helps the chatbot respond in a supportive way.
    public class SentimentService
    {
        // Keywords are stored in lists so they are easy for a beginner to edit.
        private readonly Dictionary<string, string[]> _sentimentWords = new()
        {
            ["worried"] = new[] { "worried", "scared", "afraid", "nervous", "unsafe", "panic" },
            ["curious"] = new[] { "curious", "interested", "want to know", "teach me", "explain" },
            ["frustrated"] = new[] { "frustrated", "confused", "angry", "annoyed", "stuck", "lost" }
        };

        // This method returns the detected mood, or neutral if no mood is found.
        public string Detect(string input)
        {
            string lowerInput = input.ToLowerInvariant();

            foreach (var pair in _sentimentWords)
            {
                if (pair.Value.Any(word => lowerInput.Contains(word, StringComparison.OrdinalIgnoreCase)))
                {
                    return pair.Key;
                }
            }

            return "neutral";
        }

        // This method creates a short supportive sentence based on the user's mood.
        public string GetSupportMessage(string sentiment)
        {
            return sentiment switch
            {
                "worried" => "It is understandable to feel worried. Let us deal with it step by step. ",
                "curious" => "I like that you are curious. Learning these habits makes you safer online. ",
                "frustrated" => "Do not stress. Cybersecurity can feel confusing, but I will explain it simply. ",
                _ => string.Empty
            };
        }
    }
}
