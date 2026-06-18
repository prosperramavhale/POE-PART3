using System;
using System.Linq;
using System.Text.RegularExpressions;
using CybersecurityChatbot.Part2GUI.Models;

namespace CybersecurityChatbot.Part2GUI.Services
{
    // This class handles memory and recall.
    // It stores the user's name, favourite topic and last topic.
    public class MemoryService
    {
        public UserMemory Memory { get; } = new();

        // These are the cybersecurity topics that the bot is allowed to save as a favourite topic.
        // This prevents the bot from saving random words as a topic.
        private readonly string[] _knownTopics =
        {
            "password", "phishing", "scam", "privacy", "malware", "wifi", "otp", "backup", "social engineering"
        };

        // This method saves the user's name when they type it in the GUI name box.
        // It keeps the GUI simple because the user does not have to type a full sentence in chat.
        public void SetUserName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Memory.Name = CleanName(name);
            }
        }

        // This method checks if the user told the bot their name or favourite topic.
        public string UpdateMemoryFromInput(string input)
        {
            string lowerInput = input.ToLowerInvariant();

            // First check if the user is sharing a favourite cybersecurity topic.
            // Example: "I'm interested in privacy" must save privacy as the topic, not "interested" as the name.
            string topicMessage = TrySaveFavouriteTopic(lowerInput);
            if (!string.IsNullOrWhiteSpace(topicMessage))
            {
                return topicMessage;
            }

            // Then check if the user is clearly sharing a name.
            // This avoids confusing mood/topic sentences like "I am worried" or "I'm interested in privacy" with a name.
            string nameMessage = TrySaveName(input);
            if (!string.IsNullOrWhiteSpace(nameMessage))
            {
                return nameMessage;
            }

            if (lowerInput.Contains("remember my topic") && !string.IsNullOrWhiteSpace(Memory.FavouriteTopic))
            {
                return $"I remember that your favourite cybersecurity topic is {Memory.FavouriteTopic}. ";
            }

            return string.Empty;
        }

        // This method saves favourite topics only when the sentence clearly talks about interest or preference.
        private string TrySaveFavouriteTopic(string lowerInput)
        {
            if (!(lowerInput.Contains("interested in") || lowerInput.Contains("favourite topic") || lowerInput.Contains("favorite topic") || lowerInput.Contains("i like")))
            {
                return string.Empty;
            }

            string? matchedTopic = _knownTopics.FirstOrDefault(topic => lowerInput.Contains(topic));

            if (string.IsNullOrWhiteSpace(matchedTopic))
            {
                return "I can remember your favourite topic, but please choose one of these cybersecurity topics: password, phishing, scam, privacy, malware, Wi-Fi, OTP or backup. ";
            }

            Memory.FavouriteTopic = matchedTopic;
            string namePart = IsDefaultName() ? "" : $", {Memory.Name}";
            return $"Great{namePart}. I will remember that you are interested in {Memory.FavouriteTopic}. ";
        }

        // This method saves the user's name only when the name sentence is clear.
        private string TrySaveName(string input)
        {
            string[] blockedWords = { "interested", "worried", "curious", "frustrated", "scared", "concerned", "confused", "angry", "happy", "privacy", "password", "phishing", "scam", "malware" };

            Match nameMatch = Regex.Match(input, @"\b(my name is|call me|i am called|name is|i am|i'm)\s+([A-Za-z]+)", RegexOptions.IgnoreCase);

            if (!nameMatch.Success)
            {
                return string.Empty;
            }

            string possibleName = CleanName(nameMatch.Groups[2].Value);

            if (blockedWords.Contains(possibleName.ToLowerInvariant()))
            {
                return string.Empty;
            }

            Memory.Name = possibleName;
            return $"Nice to meet you, {Memory.Name}. I will remember your name. ";
        }

        // This method cleans the name so it looks neat in the GUI and chat bubbles.
        private string CleanName(string name)
        {
            string cleaned = name.Trim();

            if (cleaned.Length == 0)
            {
                return Memory.Name;
            }

            return char.ToUpper(cleaned[0]) + cleaned.Substring(1).ToLowerInvariant();
        }

        // This method checks if the user has not provided a real name yet.
        private bool IsDefaultName()
        {
            return string.IsNullOrWhiteSpace(Memory.Name) || Memory.Name.Equals("friend", StringComparison.OrdinalIgnoreCase);
        }
    }
}
