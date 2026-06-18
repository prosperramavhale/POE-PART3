using System;
using System.Text.RegularExpressions;
using CybersecurityChatbot.Part2GUI.Models;

namespace CybersecurityChatbot.Part2GUI.Services
{
    // This service simulates simple NLP using lowercase text, Contains checks and regular expressions.
    public class NlpService
    {
        // This method checks whether the user is asking for the activity log.
        public bool WantsActivityLog(string input)
        {
            string text = input.ToLower();
            return text.Contains("activity log") || text.Contains("what have you done") || text.Contains("recent actions") || text.Contains("summary of actions");
        }

        // This method checks whether the user wants to start the quiz.
        public bool WantsQuiz(string input)
        {
            string text = input.ToLower();
            return text.Contains("start quiz") || text.Contains("play quiz") || text.Contains("mini game") || text.Contains("test my knowledge");
        }

        // This method recognises task wording even when the sentence is phrased differently.
        public bool TryCreateTaskFromText(string input, out CyberTask task)
        {
            task = new CyberTask();
            string text = input.ToLower();

            bool taskWords = text.Contains("add task") || text.Contains("create task") || text.Contains("new task") || text.Contains("remind me") || text.Contains("set reminder");
            if (!taskWords)
            {
                return false;
            }

            string title = input;
            title = Regex.Replace(title, "(?i)add a task to|add task|create task|new task|remind me to|set reminder to|please", "").Trim();
            title = Regex.Replace(title, @"(?i)tomorrow|in \d+ days|next week", "").Trim(' ', '.', '-');

            if (string.IsNullOrWhiteSpace(title))
            {
                title = "Cybersecurity task";
            }

            task.Title = MakeTitleFriendly(title);
            task.Description = BuildDescription(task.Title);
            task.ReminderDate = DetectReminderDate(text);
            return true;
        }

        private string MakeTitleFriendly(string title)
        {
            if (title.ToLower().Contains("2fa") || title.ToLower().Contains("two-factor")) return "Enable two-factor authentication";
            if (title.ToLower().Contains("privacy")) return "Review account privacy settings";
            if (title.ToLower().Contains("password")) return "Update password security";
            if (title.ToLower().Contains("backup")) return "Back up important files";
            return char.ToUpper(title[0]) + title.Substring(1);
        }

        private string BuildDescription(string title)
        {
            string lower = title.ToLower();
            if (lower.Contains("privacy")) return "Review privacy settings and limit unnecessary sharing of personal information.";
            if (lower.Contains("password")) return "Create a strong unique password and avoid reusing it on other accounts.";
            if (lower.Contains("two-factor")) return "Enable 2FA to add another layer of account protection.";
            if (lower.Contains("backup")) return "Save important files to a safe backup location.";
            return "Complete this cybersecurity action to improve online safety.";
        }

        private DateTime? DetectReminderDate(string text)
        {
            if (text.Contains("tomorrow")) return DateTime.Now.AddDays(1);
            if (text.Contains("next week")) return DateTime.Now.AddDays(7);

            var match = Regex.Match(text, @"in (\d+) days");
            if (match.Success && int.TryParse(match.Groups[1].Value, out int days))
            {
                return DateTime.Now.AddDays(days);
            }

            return null;
        }
    }
}
