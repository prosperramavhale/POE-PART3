using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot.Part2GUI.Services
{
    // This service keeps a record of important actions taken by the chatbot.
    public class ActivityLogService
    {
        private readonly List<string> _actions = new();

        // This method adds a new action with the current time.
        public void Add(string action)
        {
            _actions.Add($"{DateTime.Now:HH:mm} - {action}");
        }

        // This method returns only the latest actions so the log stays neat.
        public string GetRecentLog(int count = 10)
        {
            if (_actions.Count == 0)
            {
                return "No actions have been recorded yet.";
            }

            var recent = _actions.TakeLast(count).ToList();
            var lines = recent.Select((item, index) => $"{index + 1}. {item}");
            return "Here is a summary of recent actions:\n" + string.Join("\n", lines);
        }

        // This method returns all actions for the Activity tab.
        public IReadOnlyList<string> GetAll()
        {
            return _actions.AsReadOnly();
        }
    }
}
