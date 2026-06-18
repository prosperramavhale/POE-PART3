using System.Collections.Generic;

namespace CybersecurityChatbot.Part2GUI.Models
{
    // This class stores one quiz question, its answer options and the explanation.
    public class QuizQuestion
    {
        // This is the question displayed to the user.
        public string Question { get; set; } = string.Empty;

        // These are the answer choices shown as buttons.
        public List<string> Options { get; set; } = new();

        // This stores the position of the correct answer.
        public int CorrectIndex { get; set; }

        // This explains why the answer is correct or wrong.
        public string Explanation { get; set; } = string.Empty;
    }
}
