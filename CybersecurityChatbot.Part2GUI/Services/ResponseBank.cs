using System.Collections.Generic;

namespace CybersecurityChatbot.Part2GUI.Services
{
    // This class keeps all chatbot responses in one place.
    // A dictionary is used because it is fast and easy to search by topic.
    public static class ResponseBank
    {
        // This method returns wide cybersecurity topics with many possible answers.
        // Each topic has a list so the bot can choose random responses and sound natural.
        public static Dictionary<string, List<string>> GetTopicResponses()
        {
            return new Dictionary<string, List<string>>
            {
                ["password"] = new List<string>
                {
                    "Use a long password with at least 12 characters. Mix letters, numbers and symbols, and avoid names or birthdays.",
                    "A password manager can help you create and store different passwords for every account.",
                    "Never reuse your banking, email or student portal password on other websites. One leak can expose everything."
                },
                ["phishing"] = new List<string>
                {
                    "Phishing messages often create panic by saying your account will be blocked. Stop, check the sender and do not rush.",
                    "Before clicking a link, hover over it or check the full address. Fake pages often use small spelling changes.",
                    "Banks and schools will not ask for your password or OTP through email, SMS or WhatsApp."
                },
                ["scam"] = new List<string>
                {
                    "Online scams often promise quick money, prizes or urgent refunds. If it sounds too good to be true, verify it first.",
                    "For South African scams, be careful of fake delivery fees, fake bank alerts, job application fees and SARS refund messages.",
                    "Never send your ID, bank card photo or OTP to someone who contacts you unexpectedly."
                },
                ["privacy"] = new List<string>
                {
                    "Review privacy settings on social media and limit who can see your phone number, location and personal posts.",
                    "Only give apps the permissions they truly need. A calculator app should not need your contacts or microphone.",
                    "Think before posting personal details online. Criminals can use small details to answer security questions."
                },
                ["malware"] = new List<string>
                {
                    "Malware can hide inside cracked software, fake updates and unknown attachments. Download apps only from trusted sources.",
                    "Keep Windows and your antivirus updated because updates close security gaps criminals use.",
                    "If your device becomes slow, shows pop-ups, or opens strange pages, scan it and remove unknown programs."
                },
                ["wifi"] = new List<string>
                {
                    "Avoid online banking on public Wi-Fi unless you use a trusted VPN or your own mobile data.",
                    "At home, use WPA2 or WPA3 Wi-Fi security and change the default router password.",
                    "Do not auto-connect to random public networks because attackers can create fake hotspots."
                },
                ["otp"] = new List<string>
                {
                    "An OTP is a one-time password. Never share it, even if the person claims to be from the bank or police.",
                    "Criminals use OTPs to approve transactions or reset accounts, so treat every OTP like cash.",
                    "If someone asks for your OTP, end the call and contact the official company using a verified number."
                },
                ["social engineering"] = new List<string>
                {
                    "Social engineering is when criminals manipulate people instead of hacking systems directly.",
                    "Attackers may pretend to be IT support, a bank agent or a delivery company to make you reveal information.",
                    "Always verify a request through an official channel before sharing information or clicking links."
                },
                ["backup"] = new List<string>
                {
                    "Back up important files to the cloud or an external drive so ransomware or device theft does not destroy your data.",
                    "Use the 3-2-1 rule: three copies, two storage types, and one copy kept away from the device.",
                    "Test your backups sometimes. A backup is only useful if you can restore it."
                },
                ["updates"] = new List<string>
                {
                    "Software updates fix security weaknesses, not only bugs. Delaying updates can leave your device exposed.",
                    "Turn on automatic updates for your phone, browser, antivirus and operating system.",
                    "Update apps from official stores only, not from random pop-up messages."
                },
                ["cyberbullying"] = new List<string>
                {
                    "Do not respond with anger. Screenshot the evidence, block the person and report the account.",
                    "If cyberbullying includes threats, share the evidence with a trusted adult, school authority or police.",
                    "Protect your mental health by limiting contact with abusive accounts and strengthening privacy settings."
                }
            };
        }

        // These phrases help the user know what they can ask.
        public static string HelpText =>
            "You can ask me about password safety, phishing, scams, privacy, malware, Wi-Fi, OTPs, social engineering, backups, updates or cyberbullying.";
    }
}
