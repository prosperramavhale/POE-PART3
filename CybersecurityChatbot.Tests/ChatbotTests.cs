using Xunit;
using CybersecurityChatbot;

namespace CybersecurityChatbot.Tests;

// This class contains simple tests for the original Part 1 chatbot logic.
public class ChatbotTests
{
    [Fact]
    public void GetResponse_PhishingInput_ReturnsPhishingResponse()
    {
        // Arrange
        var chatbot = new Chatbot();
        string input = "phishing";
        string userName = "TestUser";

        // Act
        string result = chatbot.GetResponse(input, userName);

        // Assert
        Assert.Contains("phishing emails pretend", result);
        Assert.Contains("TestUser", result);
    }

    [Fact]
    public void GetResponse_PasswordInput_ReturnsPasswordResponse()
    {
        // Arrange
        var chatbot = new Chatbot();
        string input = "password";
        string userName = "TestUser";

        // Act
        string result = chatbot.GetResponse(input, userName);

        // Assert
        Assert.Contains("strong passwords", result);
        Assert.Contains("TestUser", result);
    }

    [Fact]
    public void GetResponse_InvalidShortInput_ReturnsError()
    {
        // Arrange
        var chatbot = new Chatbot();
        string input = "a";
        string userName = "TestUser";

        // Act
        string result = chatbot.GetResponse(input, userName);

        // Assert
        Assert.Contains("didn’t understand", result);
    }

    [Fact]
    public void GetResponse_HowAreYou_ReturnsHowAreYouResponse()
    {
        // Arrange
        var chatbot = new Chatbot();
        string input = "how are you";
        string userName = "TestUser";

        // Act
        string result = chatbot.GetResponse(input, userName);

        // Assert
        Assert.Contains("I'm great", result);
    }
}

