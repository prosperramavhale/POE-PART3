using System;
using System.IO;
using System.Media;

namespace CybersecurityChatbot.Part2GUI.Services
{
    // This class plays the original Part 1 voice greeting in the GUI.
    // If the audio file is missing, the app continues without crashing.
    public class AudioService
    {
        // This method plays the welcome.wav file from the Resources folder.
        public string PlayGreeting()
        {
            try
            {
                string soundPath = Path.Combine(AppContext.BaseDirectory, "Resources", "welcome.wav");

                if (!File.Exists(soundPath))
                {
                    return "Voice greeting file was not found, but the chatbot is still ready.";
                }

                using SoundPlayer player = new(soundPath);
                player.Play();
                return "Voice greeting played successfully.";
            }
            catch (Exception ex)
            {
                return "Voice greeting could not play: " + ex.Message;
            }
        }
    }
}
