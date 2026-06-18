using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot
{
    /// <summary>
    /// Core chatbot logic for cybersecurity awareness responses.
    /// Matches user input keywords to predefined responses.
    /// </summary>
    public class Chatbot

    {
        private readonly Dictionary<string, string> responses;

        public Chatbot()
        {
            responses = Responses.GetAll();
        }

        /// <summary>
        /// Processes user input and returns personalized cybersecurity response.
        /// Supports keyword matching and fallback responses.
        /// </summary>
        /// <param name="input">User message</param>
        /// <param name="userName">User's name for personalization</param>
        /// <returns>Appropriate response string</returns>
        public string GetResponse(string input, string userName)

        {
            if (!ValidateInput(input))
            {
                return $"Sorry {userName}, I didn’t understand that. Try asking about phishing, passwords, or type 'quit'.";
            }

            string key = input.ToLowerInvariant();
            string[] words = key.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Find best matching response
            foreach (string word in words)
            {
                foreach (var kvp in responses)
                {
                    if (word.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase) || kvp.Key.Contains(word, StringComparison.OrdinalIgnoreCase))
                    {
                        return kvp.Value.Replace("{user}", userName);
                    }
                }
            }

            // Default responses
            if (key.Contains("how are") || key.Contains("how r u"))
                return $"I'm great, {userName}! Ready to help you stay cyber-safe. What cybersecurity topic interests you?";

            if (key.Contains("purpose") || key.Contains("what do"))
                return $"I'm the Cybersecurity Awareness Assistant, {userName}. I educate South Africans on threats like phishing and safe passwords!";

            if (key.Contains("what can") || key.Contains("help"))
                return $"Ask me about: phishing, passwords, malware, safe browsing, suspicious links, or social engineering.";

return Responses.Default.Replace("{user}", userName);
        }

        private bool ValidateInput(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && input.Length >= 2;
        }
    }
}
