using System;
using System.Collections.Generic;
using CybersecurityChatbot.Part2GUI.Models;
using MySql.Data.MySqlClient;

namespace CybersecurityChatbot.Part2GUI.Services
{
    // This service handles all MySQL work for the task assistant.
    // Change the password in the connection string if your MySQL root user has a password.
    public class DatabaseService
    {
        // This is the MySQL connection text used by all database methods.
        private readonly string _connectionString = "server=localhost;user id=root;password= 123456;database=cybersafe_companion;";

        // This method creates the database table if it does not exist yet.
        public string InitialiseDatabase()
        {
            try
            {
                using var connection = new MySqlConnection("server=localhost;user id=root;password=;");
                connection.Open();

                string sql = @"CREATE DATABASE IF NOT EXISTS cybersafe_companion;
USE cybersafe_companion;
CREATE TABLE IF NOT EXISTS CyberTasks (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(150) NOT NULL,
    Description VARCHAR(700) NOT NULL,
    ReminderDate DATETIME NULL,
    IsCompleted BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);";

                using var command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return "Database is connected and ready.";
            }
            catch (Exception ex)
            {
                return "Database is not connected yet. Check MySQL, root password, and run Database/setup.sql. Details: " + ex.Message;
            }
        }

        // This method saves a new task in the database.
        // This method inserts a new task into the CyberTasks table.
        public string AddTask(CyberTask task)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                string sql = "INSERT INTO CyberTasks (Title, Description, ReminderDate, IsCompleted) VALUES (@title, @description, @reminder, 0);";
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@title", task.Title);
                command.Parameters.AddWithValue("@description", task.Description);
                command.Parameters.AddWithValue("@reminder", task.ReminderDate.HasValue ? task.ReminderDate.Value : DBNull.Value);
                command.ExecuteNonQuery();
                return "Task saved successfully.";
            }
            catch (Exception ex)
            {
                return "Task could not be saved to MySQL. Details: " + ex.Message;
            }
        }

        // This method loads all saved tasks from the database.
        // This method reads all tasks from the database for the GUI table.
        public List<CyberTask> GetTasks()
        {
            var tasks = new List<CyberTask>();

            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                string sql = "SELECT Id, Title, Description, ReminderDate, IsCompleted FROM CyberTasks ORDER BY Id DESC;";
                using var command = new MySqlCommand(sql, connection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new CyberTask
                    {
                        Id = reader.GetInt32("Id"),
                        Title = reader.GetString("Title"),
                        Description = reader.GetString("Description"),
                        ReminderDate = reader.IsDBNull(reader.GetOrdinal("ReminderDate")) ? null : reader.GetDateTime("ReminderDate"),
                        IsCompleted = reader.GetBoolean("IsCompleted")
                    });
                }
            }
            catch
            {
                // The GUI will show an empty table if MySQL is not ready yet.
            }

            return tasks;
        }


        // This method finds one task by its ID. It is used by the Complete and Delete buttons.
        // This method finds one task using its Task ID.
        public CyberTask? GetTaskById(int id)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                string sql = "SELECT Id, Title, Description, ReminderDate, IsCompleted FROM CyberTasks WHERE Id = @id LIMIT 1;";
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new CyberTask
                    {
                        Id = reader.GetInt32("Id"),
                        Title = reader.GetString("Title"),
                        Description = reader.GetString("Description"),
                        ReminderDate = reader.IsDBNull(reader.GetOrdinal("ReminderDate")) ? null : reader.GetDateTime("ReminderDate"),
                        IsCompleted = reader.GetBoolean("IsCompleted")
                    };
                }
            }
            catch
            {
                // If MySQL is not ready, return null instead of crashing the GUI.
            }

            return null;
        }

        // This method marks a task as completed in the database.
        // This method updates one task and marks it as completed.
        public string MarkTaskCompleted(int id)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();
                using var command = new MySqlCommand("UPDATE CyberTasks SET IsCompleted = 1 WHERE Id = @id;", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                return "Task marked as completed.";
            }
            catch (Exception ex)
            {
                return "Task could not be updated. Details: " + ex.Message;
            }
        }

        // This method deletes a task from the database.
        // This method removes one task from the database.
        public string DeleteTask(int id)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();
                using var command = new MySqlCommand("DELETE FROM CyberTasks WHERE Id = @id;", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                return "Task deleted.";
            }
            catch (Exception ex)
            {
                return "Task could not be deleted. Details: " + ex.Message;
            }
        }
    }
}
