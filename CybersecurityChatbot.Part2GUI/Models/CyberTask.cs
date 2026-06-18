using System;

namespace CybersecurityChatbot.Part2GUI.Models
{
    // This class represents one cybersecurity task that can be saved in MySQL.
    public class CyberTask
    {
        // This is the unique task number from the database.
        public int Id { get; set; }

        // This is the short name of the cybersecurity task.
        public string Title { get; set; } = string.Empty;

        // This explains what the user must do.
        public string Description { get; set; } = string.Empty;

        // This stores the reminder date if the user chooses one.
        public DateTime? ReminderDate { get; set; }

        // This tells the system whether the task is finished.
        public bool IsCompleted { get; set; }

        // This property is shown in the GUI so the date is easier to read.
        public string ReminderDisplay => ReminderDate.HasValue ? ReminderDate.Value.ToString("yyyy-MM-dd HH:mm") : "No reminder";

        // This property is shown in the GUI instead of displaying True or False.
        public string StatusDisplay => IsCompleted ? "Completed" : "Pending";
    }
}
