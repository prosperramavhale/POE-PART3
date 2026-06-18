using System;
using System.Media;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot
{
    // This class runs the original Part 1 console chatbot.
    // It is kept in the project to show that Part 3 was built from Part 1.
    class Program
    {
        // Main is the first method that runs when the console version starts.
        static void Main(string[] args)
        {
            // Voice greeting (requires welcome.wav in exe directory)
            try
            {
                string soundPath = Path.Combine(AppContext.BaseDirectory, "welcome.wav");
                Console.WriteLine($"Trying sound file: {soundPath}");
                if (!File.Exists(soundPath))
                {
                    throw new FileNotFoundException("welcome.wav not in output directory.");
                }
                using var player = new SoundPlayer(soundPath);
                player.PlaySync();
                Console.WriteLine("Welcome sound played!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sound failed: {ex.Message}. Continuing...");
            }


            // ASCII Art (Cybersecurity themed)
            string[] asciiArt = {
"██████╗  █████╗ ███╗   ███╗ █████╗ ██╗   ██╗██╗  ██╗ █████╗ ██╗     ███████╗",
"██╔══██╗██╔══██╗████╗ ████║██╔══██╗██║   ██║██║  ██║██╔══██╗██║     ██╔════╝",
"██████╔╝███████║██╔████╔██║███████║██║   ██║███████║███████║██║     █████╗  ",
"██╔══██╗██╔══██║██║╚██╔╝██║██╔══██║╚██╗ ██╔╝██╔══██║██╔══██║██║     ██╔══╝  ",
"██║  ██║██║  ██║██║ ╚═╝ ██║██║  ██║ ╚████╔╝ ██║  ██║██║  ██║███████╗███████╗",
"╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝  ╚═╝  ╚═══╝  ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚══════╝",
"                                                                            ",
"██████╗██╗   ██╗██████╗ ███████╗██████╗ ███████╗ ██████╗██╗   ██╗██████╗ ",
"██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔════╝██╔════╝██║   ██║██╔══██╗",
"██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝███████╗██║     ██║   ██║██████╔╝",
"██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗╚════██║██║     ██║   ██║██╔══██╗",
"╚██████╗   ██║   ██████╔╝███████╗██║  ██║███████║╚██████╗╚██████╔╝██║  ██║",
" ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═╝",
"                                                                            ",
"██████████████████████████████████████████████████████████████████████████",
"              RAMAVHALE CYBERSECURITY AWARENESS CHATBOT                    ",
"             CYBERSECURITY AWARENESS CHATBOT - STAY SAFE!              "
};

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (string line in asciiArt)
            {
                Console.WriteLine(line);
            }
            Console.ResetColor();

            // User name
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine()?.Trim() ?? "User";
            Console.WriteLine($"\nHello, {userName}! Ready to learn about cybersecurity?");
            Console.ResetColor();

            // Chatbot instance
            var chatbot = new Chatbot();

            // Main chat loop
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("> ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                string input = Console.ReadLine()?.Trim() ?? "";

                if (string.Equals(input, "quit", StringComparison.OrdinalIgnoreCase) || 
                    string.Equals(input, "bye", StringComparison.OrdinalIgnoreCase) || 
                    string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nStay safe online, {userName}! Goodbye!");
                    Console.ResetColor();
                    break;
                }

                string response = chatbot.GetResponse(input, userName);

                // Typing effect
                Console.ForegroundColor = ConsoleColor.Magenta;
                foreach (char c in response)
                {
                    Console.Write(c);
                    Thread.Sleep(20); // Typing speed
                }
                Console.WriteLine();
                Console.ResetColor();
            }
        }
    }
}

