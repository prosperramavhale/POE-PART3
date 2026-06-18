using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using CybersecurityChatbot.Part2GUI.Models;
using CybersecurityChatbot.Part2GUI.Services;

namespace CybersecurityChatbot.Part2GUI
{
    // This is the main GUI window for the final POE application.
    // It combines Part 1 voice/identity, Part 2 chatbot memory, and Part 3 tasks, quiz, NLP and activity log.
    public partial class MainWindow : Window
    {
        // These service objects separate the work so the window code stays organised.
        private readonly ChatbotEngine _chatbotEngine = new();
        private readonly AudioService _audioService = new();
        private readonly DatabaseService _databaseService = new();
        private readonly ActivityLogService _activityLogService = new();
        private readonly QuizService _quizService = new();
        private readonly NlpService _nlpService = new();

        // ObservableCollection updates the chat screen automatically when messages are added.
        private readonly ObservableCollection<ChatMessage> _chatMessages = new();

        // The constructor prepares the GUI, database, chat messages and voice greeting.
        public MainWindow()
        {
            InitializeComponent();
            FitWindowToScreen();
            LoadOriginalAsciiArt();
            ChatItemsControl.ItemsSource = _chatMessages;
            DatabaseStatusTextBlock.Text = _databaseService.InitialiseDatabase();
            ShowWelcomeMessage();
            UpdateMemoryPanel();
            RefreshTasks();
            RefreshActivityLog();
            _audioService.PlayGreeting();
            _activityLogService.Add("Application opened and voice greeting played.");
        }

        // This method keeps the app inside the visible Windows screen.
        private void FitWindowToScreen()
        {
            MaxHeight = SystemParameters.WorkArea.Height - 20;
            MaxWidth = SystemParameters.WorkArea.Width - 20;
            if (Height > MaxHeight) Height = MaxHeight;
            if (Width > MaxWidth) Width = MaxWidth;
        }

        // This method allows the custom window to be dragged with the mouse.
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is TextBox || e.OriginalSource is Button || e.OriginalSource is Thumb)
            {
                return;
            }
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                try { DragMove(); } catch { }
            }
        }

        // This method places the original Part 1 ASCII art inside the GUI.
        private void LoadOriginalAsciiArt()
        {
            AsciiTextBlock.Text =
@"██████╗  █████╗ ███╗   ███╗
██╔══██╗██╔══██╗████╗ ████║
██████╔╝███████║██╔████╔██║
██╔══██╗██╔══██║██║╚██╔╝██║
██║  ██║██║  ██║██║ ╚═╝ ██║
╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝
RAMAVHALE CYBERSECURITY
AWARENESS CHATBOT";
        }

        // This method shows the first message the user sees when the app opens.
        private void ShowWelcomeMessage()
        {
            AddBotMessage("Welcome to Rofhiwa CyberSafe Companion. I’m here to help you stay safe online with cybersecurity tips, scam awareness, password protection, phishing guidance, privacy advice, tasks, reminders and a quiz. Type your name to begin.");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e) => SendMessage();

        // This lets the user press Enter instead of clicking the Send button.
        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
                e.Handled = true;
            }
        }

        // This method reads the user message, displays it and sends it to the chatbot logic.
        private void SendMessage()
        {
            string userInput = UserInputTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                AddBotMessage("Please type a question or command before pressing Send.");
                return;
            }

            AddUserMessage(userInput);
            UserInputTextBox.Clear();
            HandleUserIntent(userInput);
            UpdateMemoryPanel();
            RefreshActivityLog();
        }

        // This method checks Part 3 commands first, then falls back to the normal Part 2 chatbot brain.
        private void HandleUserIntent(string userInput)
        {
            if (_nlpService.WantsActivityLog(userInput))
            {
                _activityLogService.Add("Activity log requested through NLP command.");
                AddBotMessage(_activityLogService.GetRecentLog());
                return;
            }

            if (_nlpService.WantsQuiz(userInput))
            {
                StartQuiz();
                _activityLogService.Add("Quiz started through NLP command.");
                return;
            }

            if (_nlpService.TryCreateTaskFromText(userInput, out CyberTask detectedTask))
            {
                string dbMessage = _databaseService.AddTask(detectedTask);
                RefreshTasks();
                string reminder = detectedTask.ReminderDate.HasValue ? $" Reminder set for {detectedTask.ReminderDate.Value:yyyy-MM-dd}." : " No reminder was set yet.";
                AddBotMessage($"Task added: '{detectedTask.Title}'. {detectedTask.Description}.{reminder}");
                _activityLogService.Add($"NLP task added: {detectedTask.Title}.{reminder} DB: {dbMessage}");
                return;
            }

            ChatResult result = _chatbotEngine.GetResponse(userInput);
            AddBotMessage(result.Reply);
            _activityLogService.Add("Chatbot answered a cybersecurity question using keyword recognition.");
        }

        // This lets the user press Enter to save their name.
        private void NameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveUserNameFromBox();
                e.Handled = true;
            }
        }

        private void SaveNameButton_Click(object sender, RoutedEventArgs e) => SaveUserNameFromBox();

        // This method stores the user name in memory for personalised replies.
        private void SaveUserNameFromBox()
        {
            string name = NameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                AddBotMessage("Please type your name in the name box, then press Enter or click Save My Name.");
                return;
            }
            _chatbotEngine.MemoryService.SetUserName(name);
            AddBotMessage($"Nice to meet you, {name}. I will remember your name during this chat.");
            NameTextBox.Clear();
            UpdateMemoryPanel();
            _activityLogService.Add($"User name saved as {name}.");
        }

        // This button shows examples of questions and commands the user can ask.
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            AddBotMessage(ResponseBank.HelpText + " For Part 3 you can also type: start quiz, add task to enable 2FA, remind me to update password tomorrow, or show activity log.");
        }

        // This button clears only the visible chat messages, not the database.
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _chatMessages.Clear();
            ShowWelcomeMessage();
            _activityLogService.Add("Visible chat was cleared.");
        }

        // This method adds a user bubble on the right side of the chat.
        private void AddUserMessage(string message)
        {
            string displayName = _chatbotEngine.MemoryService.Memory.Name;
            if (string.IsNullOrWhiteSpace(displayName) || displayName.Equals("Guest", StringComparison.OrdinalIgnoreCase)) displayName = "You";
            _chatMessages.Add(new ChatMessage { Sender = displayName, Text = message, IsFromUser = true });
            ScrollToBottom();
        }

        // This method adds a bot bubble on the left side of the chat.
        private void AddBotMessage(string message)
        {
            _chatMessages.Add(new ChatMessage { Sender = "CyberSafe Companion", Text = message, IsFromUser = false });
            ScrollToBottom();
        }

        // This keeps the latest message visible after a new chat bubble is added.
        private void ScrollToBottom()
        {
            ChatScrollViewer.Dispatcher.BeginInvoke(new Action(() => ChatScrollViewer.ScrollToEnd()));
        }

        // This method refreshes the small memory summary shown in the sidebar.
        private void UpdateMemoryPanel()
        {
            var memory = _chatbotEngine.MemoryService.Memory;
            MemoryTextBlock.Text =
                $"Memory Panel\nName: {memory.Name}\nFavourite topic: {(string.IsNullOrWhiteSpace(memory.FavouriteTopic) ? "not set yet" : memory.FavouriteTopic)}\nLast topic: {(string.IsNullOrWhiteSpace(memory.LastTopic) ? "none yet" : memory.LastTopic)}\nMood: {memory.LastSentiment}";
        }

        // This button saves a new cybersecurity task into the MySQL database.
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleTextBox.Text.Trim();
            string description = TaskDescriptionTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                AddBotMessage("Please enter a task title first.");
                return;
            }
            if (string.IsNullOrWhiteSpace(description)) description = "Complete this cybersecurity action to improve online safety.";

            var task = new CyberTask { Title = title, Description = description, ReminderDate = ReminderDatePicker.SelectedDate };
            string dbMessage = _databaseService.AddTask(task);
            AddBotMessage($"Task added: '{title}'. {description}");
            _activityLogService.Add($"Task added from GUI: {title}. {dbMessage}");
            TaskTitleTextBox.Clear(); TaskDescriptionTextBox.Clear(); ReminderDatePicker.SelectedDate = null;
            RefreshTasks();
        }

        private void RefreshTasksButton_Click(object sender, RoutedEventArgs e) => RefreshTasks();

        // This reloads tasks from the database into the table.
        private void RefreshTasks()
        {
            TasksDataGrid.ItemsSource = _databaseService.GetTasks();
        }

        // This button marks a database task as completed using the Task ID.
        private void CompleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Part 3 requires task management by Task ID, so the user types the ID shown in the table.
            if (!TryGetTaskIdFromBox(out int taskId))
            {
                AddBotMessage("Please type the task ID from the table before clicking Mark Completed.");
                return;
            }

            CyberTask? task = _databaseService.GetTaskById(taskId);
            if (task == null)
            {
                AddBotMessage($"I could not find a task with ID {taskId}. Click Refresh and check the ID again.");
                return;
            }

            string result = _databaseService.MarkTaskCompleted(taskId);
            AddBotMessage($"Task ID {taskId} marked as completed: '{task.Title}'.");
            _activityLogService.Add($"Task ID {taskId} marked completed: {task.Title}. {result}");
            TaskIdTextBox.Clear();
            RefreshTasks();
            RefreshActivityLog();
        }

        // This button deletes a database task using the Task ID.
        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // requires delete to work using the Task ID from the database table.
            if (!TryGetTaskIdFromBox(out int taskId))
            {
                AddBotMessage("Please type the task ID from the table before clicking Delete Task.");
                return;
            }

            CyberTask? task = _databaseService.GetTaskById(taskId);
            if (task == null)
            {
                AddBotMessage($"I could not find a task with ID {taskId}. Click Refresh and check the ID again.");
                return;
            }

            string result = _databaseService.DeleteTask(taskId);
            AddBotMessage($"Task ID {taskId} deleted: '{task.Title}'.");
            _activityLogService.Add($"Task ID {taskId} deleted: {task.Title}. {result}");
            TaskIdTextBox.Clear();
            RefreshTasks();
            RefreshActivityLog();
        }

        private bool TryGetTaskIdFromBox(out int taskId)
        {
            // This helper keeps the completed and delete buttons simple and beginner friendly.
            return int.TryParse(TaskIdTextBox.Text.Trim(), out taskId) && taskId > 0;
        }

        private void StartQuizButton_Click(object sender, RoutedEventArgs e) => StartQuiz();

        // This method starts the cybersecurity mini-game from question one.
        private void StartQuiz()
        {
            QuizQuestion firstQuestion = _quizService.Start();
            QuizStatusTextBlock.Text = $"Question 1 of {_quizService.TotalQuestions}. Score: 0";
            ShowQuizQuestion(firstQuestion);
            AddBotMessage("Quiz started. Answer the question in the Quiz Mini-Game tab.");
            _activityLogService.Add("Quiz started.");
        }

        // This method displays one quiz question and creates answer buttons.
        private void ShowQuizQuestion(QuizQuestion question)
        {
            QuizQuestionTextBlock.Text = question.Question;
            QuizOptionsPanel.Children.Clear();
            for (int i = 0; i < question.Options.Count; i++)
            {
                var button = new Button
                {
                    Content = question.Options[i],
                    Tag = i,
                    Height = 40,
                    Margin = new Thickness(0, 5, 0, 5),
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    Padding = new Thickness(12, 0, 12, 0)
                };
                button.Click += QuizOptionButton_Click;
                QuizOptionsPanel.Children.Add(button);
            }
        }

        // This method checks the selected quiz answer and gives feedback.
        private void QuizOptionButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button || button.Tag is not int selectedIndex) return;
            string feedback = _quizService.Answer(selectedIndex, out QuizQuestion? nextQuestion, out bool finished);
            AddBotMessage(feedback);
            _activityLogService.Add("Quiz question answered.");

            if (finished)
            {
                QuizStatusTextBlock.Text = $"Quiz completed. Final score: {_quizService.Score}/{_quizService.TotalQuestions}";
                QuizQuestionTextBlock.Text = "Quiz completed. Press Start Quiz to try again.";
                QuizOptionsPanel.Children.Clear();
                _activityLogService.Add($"Quiz completed with score {_quizService.Score}/{_quizService.TotalQuestions}.");
            }
            else if (nextQuestion != null)
            {
                QuizStatusTextBlock.Text = $"Question {_quizService.CurrentNumber} of {_quizService.TotalQuestions}. Score: {_quizService.Score}";
                ShowQuizQuestion(nextQuestion);
            }
            RefreshActivityLog();
        }

        private void RefreshLogButton_Click(object sender, RoutedEventArgs e) => RefreshActivityLog();

        // This method shows the latest actions taken by the chatbot.
        private void RefreshActivityLog()
        {
            ActivityLogTextBox.Text = _activityLogService.GetRecentLog(10);
        }
    }
}
