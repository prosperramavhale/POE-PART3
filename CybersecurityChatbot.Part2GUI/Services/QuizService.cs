using System.Collections.Generic;
using CybersecurityChatbot.Part2GUI.Models;

namespace CybersecurityChatbot.Part2GUI.Services
{
    // This service manages the cybersecurity quiz game.
    public class QuizService
    {
        private readonly List<QuizQuestion> _questions;
        private int _index;
        public int Score { get; private set; }
        public bool IsActive { get; private set; }

        public QuizService()
        {
            _questions = BuildQuestions();
        }

        // This method starts the quiz from question one.
        public QuizQuestion Start()
        {
            _index = 0;
            Score = 0;
            IsActive = true;
            return _questions[_index];
        }

        // This method checks the user's answer and moves to the next question.
        public string Answer(int optionIndex, out QuizQuestion? nextQuestion, out bool finished)
        {
            var current = _questions[_index];
            bool correct = optionIndex == current.CorrectIndex;

            if (correct)
            {
                Score++;
            }

            string feedback = correct ? "Correct! " : "Not quite. ";
            feedback += current.Explanation;

            _index++;
            finished = _index >= _questions.Count;
            nextQuestion = finished ? null : _questions[_index];

            if (finished)
            {
                IsActive = false;
                feedback += $"\n\nFinal score: {Score}/{_questions.Count}. ";
                feedback += Score >= 10 ? "Excellent work. You are building strong cybersecurity habits." :
                            Score >= 7 ? "Good job. Keep practising and you will become even stronger." :
                            "Keep learning. Cybersecurity improves with practice.";
            }

            return feedback;
        }

        public int TotalQuestions => _questions.Count;
        public int CurrentNumber => _index + 1;

        private List<QuizQuestion> BuildQuestions()
        {
            return new List<QuizQuestion>
            {
                new(){ Question="What should you do if an email asks for your password?", Options=new(){"Reply with the password","Report it as phishing","Forward it to friends","Ignore security warnings"}, CorrectIndex=1, Explanation="Reporting phishing protects you and helps stop the scam."},
                new(){ Question="True or False: A strong password should be reused on many accounts.", Options=new(){"True","False"}, CorrectIndex=1, Explanation="Passwords should be unique so one breach does not unlock all accounts."},
                new(){ Question="Which option adds extra protection after your password?", Options=new(){"Two-factor authentication","Public Wi-Fi","Simple passwords","Auto-reply"}, CorrectIndex=0, Explanation="Two-factor authentication adds a second proof before login."},
                new(){ Question="What is social engineering?", Options=new(){"A coding language","Manipulating people to reveal information","A type of antivirus","A Wi-Fi cable"}, CorrectIndex=1, Explanation="Social engineering tricks people using pressure, fear or fake trust."},
                new(){ Question="What should you check before clicking a link?", Options=new(){"The sender and real web address","Only the colour","How long the email is","The font size"}, CorrectIndex=0, Explanation="Checking the real address helps you avoid fake websites."},
                new(){ Question="True or False: OTP codes should be shared with support agents.", Options=new(){"True","False"}, CorrectIndex=1, Explanation="Never share OTP codes. Real support staff should not ask for them."},
                new(){ Question="What is a safe habit on public Wi-Fi?", Options=new(){"Do banking without protection","Use a VPN or avoid sensitive logins","Disable updates forever","Share files publicly"}, CorrectIndex=1, Explanation="Public Wi-Fi can be risky, so avoid sensitive activity or use protection."},
                new(){ Question="Why are backups important?", Options=new(){"They make passwords shorter","They help recover after device loss or ransomware","They remove all malware automatically","They replace antivirus"}, CorrectIndex=1, Explanation="Backups help you recover important files after attacks or accidents."},
                new(){ Question="Which app permission is suspicious for a calculator app?", Options=new(){"Calculator display","Microphone and contacts","Basic storage for settings","Dark mode"}, CorrectIndex=1, Explanation="Apps should only get permissions they truly need."},
                new(){ Question="True or False: Software updates can fix security weaknesses.", Options=new(){"True","False"}, CorrectIndex=0, Explanation="Updates often patch vulnerabilities that attackers may use."},
                new(){ Question="What is the best response to a bank SMS asking you to click a login link?", Options=new(){"Click immediately","Use the official app or website manually","Send your PIN","Reply with your ID number"}, CorrectIndex=1, Explanation="Use official channels instead of links in unexpected messages."},
                new(){ Question="Which one is a sign of a scam?", Options=new(){"Urgent pressure and threats","Clear official contact details","Normal account settings","A known website typed manually"}, CorrectIndex=0, Explanation="Scammers often pressure you so you do not think carefully."}
            };
        }
    }
}
