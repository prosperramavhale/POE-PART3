using System.Collections.Generic;

namespace CybersecurityChatbot
{
    /// <summary>
    /// Static responses for cybersecurity topics (South Africa context).
    /// Dictionary keys map to educational content.
    /// </summary>
    public static class Responses

    {
        // This method returns all Part 1 chatbot answers in a dictionary.
        public static Dictionary<string, string> GetAll()
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["phishing"] = "{user}, phishing emails pretend to be banks or government (like SARS). Check sender email, hover links (don't click suspicious), verify URLs start with https://. In SA, common scams target Capitec/FNB users!",
                ["password"] = "{user}, use strong passwords: 12+ chars, mix uppercase/lowercase/numbers/symbols. Enable 2FA everywhere. Use password manager like Bitwarden. Never reuse passwords!",
                ["malware"] = "{user}, malware infects via downloads/USB. Don't open unknown attachments. Use antivirus (Avast/Windows Defender), keep Windows updated. Scan suspicious files.",
                ["browsing"] = "{user}, safe browsing: Use HTTPS sites, incognito for privacy, clear cookies/cache. Avoid free WiFi for banking. VPN for public networks.",
                ["link"] = "{user}, suspicious links: Hover to check real URL. Shortened links (bit.ly)? Avoid. Typos like 'g00gle.com'? Scam!",
                ["social"] = "{user}, social engineering: Scammers trick via phone/email ('your account suspended'). Never share OTP/PIN. Verify by calling official numbers.",
                ["safe"] = "Stay vigilant: Update software, backup data, report cybercrimes to SAPS Cybersecurity Unit."
            };
        }

        // This message is used when the chatbot cannot match a topic.
        public static string Default => "Great ,! For cybersecurity tips, ask about specific topics like 'phishing' or 'passwords'. I'm here to help!";
    }
}
