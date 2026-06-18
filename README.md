# Rofhiwa CyberSafe Companion (Part 3 / POE)

## Student Information
**Student Name:** Rofhiwa Ramavhale  
**Module:** Programming 2A  
**Module Code:** PROG6221  
**Project:** Cybersecurity Awareness Chatbot – Part 3 / POE  

---

# Project Overview

Rofhiwa CyberSafe Companion is a professional Cybersecurity Awareness Chatbot developed using C#, WPF, XAML, and MySQL.

This project builds on Part 1 and Part 2 by introducing advanced cybersecurity learning and task management features inside a modern GUI environment. The chatbot is designed to educate users about cybersecurity while helping them manage reminders, complete cybersecurity tasks, test their cybersecurity knowledge, and interact naturally using NLP simulation.

The application combines:
- Cybersecurity education
- GUI development
- Database integration
- Task management
- NLP simulation
- Activity logging
- Interactive quiz functionality
- Memory and sentiment detection

The chatbot provides a realistic and engaging user experience while demonstrating Object-Oriented Programming principles and professional software development practices.

---

# Features Included

# Part 1 Features
- ASCII art chatbot identity
- Voice greeting using WAV audio
- Cybersecurity awareness chatbot
- Keyword-based responses
- Beginner-friendly chatbot logic
- Console chatbot foundation

---

# Part 2 Features
- Professional WPF GUI
- Dynamic chatbot responses
- Keyword recognition system
- Random cybersecurity responses
- Memory and recall functionality
- Sentiment detection
- Follow-up conversation flow
- Realistic chatbot/user message alignment
- Error handling for invalid inputs
- Improved chatbot interaction system

---

# Part 3 / POE Features

## Task Assistant with MySQL Database
The chatbot includes a full cybersecurity task assistant that allows users to:
- Add cybersecurity tasks
- Add task descriptions
- Set reminders
- View saved tasks
- Mark tasks as completed
- Delete tasks using Task IDs

All tasks are stored inside a MySQL database and automatically updated through the GUI.

### Example Tasks
- Enable Two-Factor Authentication
- Review Privacy Settings
- Change Passwords
- Backup Important Files
- Check Scam Emails

---

## Cybersecurity Quiz Mini-Game
The chatbot includes an interactive cybersecurity quiz that:
- Contains more than 10 cybersecurity questions
- Uses multiple-choice and true/false questions
- Provides instant feedback after each answer
- Tracks the user’s score
- Displays final quiz performance feedback

### Quiz Topics
- Password Safety
- Phishing
- Malware
- Safe Browsing
- Social Engineering
- Privacy Protection
- Scam Awareness

---

## NLP Simulation
The chatbot uses simple Natural Language Processing techniques to recognise different user phrases naturally.

The chatbot understands commands such as:
- “Remind me to update my password”
- “Add a task for privacy settings”
- “Tell me more”
- “Give me another tip”
- “Start quiz”
- “Show activity log”

The chatbot uses:
- String matching
- Keyword detection
- Flexible phrase recognition
- Follow-up conversation handling

---

## Activity Log Feature
The chatbot records important user and chatbot activities including:
- Task creation
- Task completion
- Task deletion
- Reminder creation
- Quiz activity
- NLP interactions

Users can request:
- “Show activity log”
- “What have you done for me?”

The chatbot displays recent actions clearly in the GUI.

---

# Technologies Used

- C#
- WPF (Windows Presentation Foundation)
- XAML
- .NET
- MySQL
- Object-Oriented Programming (OOP)

---

# Project Structure

```text
ROFHIWA CYBERSAFE COMPANION PART 3/

├── CybersecurityChatbot.Part3GUI/                    
│   ├── App.xaml
│   ├── MainWindow.xaml
│   ├── MainWindow.xaml.cs
│   │
│   ├── Models/
│   │   ├── User.cs
│   │   ├── ChatMessage.cs
│   │   ├── UserMemory.cs
│   │   ├── QuizQuestion.cs
│   │   ├── TaskItem.cs
│   │   └── ActivityLogItem.cs
│   │
│   ├── Services/
│   │   ├── AudioService.cs
│   │   ├── ChatbotEngine.cs
│   │   ├── ResponseBank.cs
│   │   ├── SentimentService.cs
│   │   ├── MemoryService.cs
│   │   ├── NlpService.cs
│   │   ├── QuizService.cs
│   │   ├── DatabaseService.cs
│   │   ├── TaskService.cs
│   │   └── ActivityLogService.cs
│   │
│   ├── Database/
│   │   ├── setup.sql
│   │   └── sample_data.sql
│   │
│   ├── Resources/
│   │   ├── welcome.wav
│   │   ├── chatbot_logo.png
│   │   └── background.png
│   │
│   ├── README.md
│   └── appsettings.json
│
├── CybersecurityChatbot.Tests/
│   ├── ChatbotTests.cs
│   ├── QuizTests.cs
│   ├── DatabaseTests.cs
│   └── MemoryTests.cs
│
└── ROFHIWA CYBERSAFE COMPANION PART 3.sln
```

---

# Database Setup

## Step 1 — Install MySQL
Install:
- MySQL Server
- MySQL Workbench

---

## Step 2 — Create the Database
Run the SQL script located in:

```text
Database/setup.sql
```

---

## Step 3 — Configure the Connection String

Open:
```text
DatabaseService.cs
```

Update your MySQL password:

```csharp
server=localhost;
database=cybersafecompanion;
uid=root;
pwd=YOUR_PASSWORD;
```

---

# How to Run the Application

## Step 1
Open the solution in Visual Studio.

---

## Step 2
Restore NuGet packages.

---

## Step 3
Build the solution.

---

## Step 4
Set:
```text
CybersecurityChatbot.Part3GUI
```
as the Startup Project.

---

## Step 5
Run the application.

---

# Example Commands

## Cybersecurity Questions
- Tell me about phishing
- Password safety tips
- Explain malware
- Tell me about scams
- Explain privacy protection

---

## Quiz Commands
- Start quiz
- Begin cybersecurity quiz

---

## Activity Log Commands
- Show activity log
- What have you done for me?

---

## Task Commands
- Add task
- View tasks
- Complete task
- Delete task

---

# OOP Principles Used

This project demonstrates:
- Classes and Objects
- Encapsulation
- Lists and Dictionaries
- Modular Programming
- Service-Based Architecture
- GUI Event Handling
- Database Integration
- Object-Oriented Design

---

# Author

**Rofhiwa Ramavhale**  
Programming 2A – PROG6221  

---

# Final Notes

Rofhiwa CyberSafe Companion was developed to create an interactive cybersecurity learning experience while demonstrating professional software development practices using GUI design, database integration, NLP simulation, and Object-Oriented Programming concepts.

The project combines Parts 1, 2, and 3 into one cohesive application with a modern user experience and educational cybersecurity features.