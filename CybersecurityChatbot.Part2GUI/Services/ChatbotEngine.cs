using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot.Part2GUI.Services
{
    // This class is the main brain of the Part 2 chatbot.
    // It combines keyword recognition, random responses, memory, sentiment and follow-up flow.
    public class ChatbotEngine
    {
        // Random is used to choose different answers so the bot does not sound repetitive.
        private readonly Random _random = new();

        // Topic responses are stored in a dictionary for clean and fast searching.
        private readonly Dictionary<string, List<string>> _responses = ResponseBank.GetTopicResponses();

        // This service detects moods like worried, curious and frustrated.
        private readonly SentimentService _sentimentService = new();

        // This service stores the user's details and last topic.
        public MemoryService MemoryService { get; } = new();

        // This delegate is used to format the final message.
        // It meets the Part 2 requirement of using delegates to solve a problem.
        private delegate string ReplyFormatter(string userName, string supportMessage, string memoryMessage, string mainReply);

        // This method receives the user's message and returns a chatbot answer.
        public ChatResult GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Trim().Length < 2)
            {
                return new ChatResult
                {
                    Topic = "unknown",
                    Reply = "Please type a full question. For example, ask: What is phishing?"
                };
            }

            string cleanInput = input.Trim();
            string lowerInput = cleanInput.ToLowerInvariant();

            if (IsExit(lowerInput))
            {
                return new ChatResult
                {
                    Topic = "exit",
                    Reply = $"Goodbye {MemoryService.Memory.Name}. Keep your accounts protected and never share your OTP."
                };
            }

            string memoryMessage = MemoryService.UpdateMemoryFromInput(cleanInput);
            string sentiment = _sentimentService.Detect(cleanInput);
            MemoryService.Memory.LastSentiment = sentiment;
            string supportMessage = _sentimentService.GetSupportMessage(sentiment);

            if (IsHelpRequest(lowerInput))
            {
                return BuildFinalReply("help", supportMessage, memoryMessage, ResponseBank.HelpText);
            }

            if (IsMemoryRecall(lowerInput))
            {
                string recalledTopic = string.IsNullOrWhiteSpace(MemoryService.Memory.FavouriteTopic)
                    ? "You have not told me your favourite cybersecurity topic yet. You can say: I am interested in privacy."
                    : $"I remember that you are interested in {MemoryService.Memory.FavouriteTopic}. A good next step is to practise safe habits around that topic.";

                return BuildFinalReply("memory", supportMessage, memoryMessage, recalledTopic);
            }

            string matchedTopic = FindTopic(lowerInput);

            if (string.IsNullOrWhiteSpace(matchedTopic) && IsFollowUp(lowerInput))
            {
                matchedTopic = MemoryService.Memory.LastTopic;
            }

            if (!string.IsNullOrWhiteSpace(matchedTopic))
            {
                MemoryService.Memory.LastTopic = matchedTopic;
                string mainReply = PickRandomResponse(matchedTopic);

                if (!string.IsNullOrWhiteSpace(MemoryService.Memory.FavouriteTopic) && matchedTopic.Contains(MemoryService.Memory.FavouriteTopic, StringComparison.OrdinalIgnoreCase))
                {
                    mainReply += $" Because {matchedTopic} is your favourite topic, I suggest you practise this tip today.";
                }

                return BuildFinalReply(matchedTopic, supportMessage, memoryMessage, mainReply);
            }

            if (!string.IsNullOrWhiteSpace(memoryMessage))
            {
                return BuildFinalReply("memory", supportMessage, memoryMessage, ResponseBank.HelpText);
            }

            return new ChatResult
            {
                Topic = "unknown",
                Reply = $"I am not fully sure what you mean, {MemoryService.Memory.Name}. Try asking about password safety, phishing, scams, privacy, malware, Wi-Fi, OTPs or backups."
            };
        }

        // This method searches the dictionary to find the topic inside the user's sentence.
        private string FindTopic(string lowerInput)
        {
            foreach (string topic in _responses.Keys)
            {
                if (lowerInput.Contains(topic, StringComparison.OrdinalIgnoreCase))
                {
                    return topic;
                }
            }

            if (lowerInput.Contains("two factor") || lowerInput.Contains("2fa") || lowerInput.Contains("multi factor"))
                return "password";

            if (lowerInput.Contains("fake email") || lowerInput.Contains("fake sms") || lowerInput.Contains("suspicious link"))
                return "phishing";

            if (lowerInput.Contains("public network") || lowerInput.Contains("hotspot"))
                return "wifi";

            if (lowerInput.Contains("one time pin") || lowerInput.Contains("pin"))
                return "otp";

            if (lowerInput.Contains("virus") || lowerInput.Contains("trojan") || lowerInput.Contains("ransomware"))
                return "malware";

            if (lowerInput.Contains("save files") || lowerInput.Contains("lost files"))
                return "backup";

            return string.Empty;
        }

        // This method chooses one answer from the topic list.
        private string PickRandomResponse(string topic)
        {
            List<string> answers = _responses[topic];
            int index = _random.Next(answers.Count);
            return answers[index];
        }

        // This method checks if the user is asking a follow-up question.
        private bool IsFollowUp(string lowerInput)
        {
            string[] followUps = { "tell me more", "explain more", "another tip", "more", "continue", "what else", "give me another" };
            return followUps.Any(lowerInput.Contains);
        }

        // This method checks if the user wants help with available topics.
        private bool IsHelpRequest(string lowerInput)
        {
            return lowerInput.Contains("help") || lowerInput.Contains("what can you do") || lowerInput.Contains("topics");
        }

        // This method checks if the user wants the bot to recall memory.
        private bool IsMemoryRecall(string lowerInput)
        {
            return lowerInput.Contains("what do you remember") || lowerInput.Contains("remember about me") || lowerInput.Contains("my favourite");
        }

        // This method checks exit commands.
        private bool IsExit(string lowerInput)
        {
            return lowerInput == "exit" || lowerInput == "quit" || lowerInput == "bye";
        }

        // This method uses the delegate to format the final response consistently.
        private ChatResult BuildFinalReply(string topic, string supportMessage, string memoryMessage, string mainReply)
        {
            ReplyFormatter formatter = (userName, support, memory, reply) =>
                $"{support}{memory}{reply}\n\nTip for you, {userName}: You can ask 'tell me more' to continue this topic.";

            return new ChatResult
            {
                Topic = topic,
                Reply = formatter(MemoryService.Memory.Name, supportMessage, memoryMessage, mainReply)
            };
        }
    }
}
