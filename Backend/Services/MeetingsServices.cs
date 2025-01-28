using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class MeetingsServices
    {
        private readonly GymDatabase database;

        public MeetingsServices(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public (bool success, string message) AddMeeting(MeetingDetails entry)
        {
            try
            {
                using (var connection = database.ConnectToDatabase())
                {
                    connection.Open();
                    string query = "INSERT INTO Meetings (Coach_ID, Title, Time) VALUES (@Coach_ID, @Title, @Time);";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        // Add parameters for query
                        command.Parameters.AddWithValue("@Coach_ID", entry.Coach_ID);
                        command.Parameters.AddWithValue("@Title", entry.Title);
                        command.Parameters.AddWithValue("@Time", entry.Time);

                        // Execute the query and get the auto-generated Meeting_ID
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Get the last inserted Meeting_ID
                            entry.Meeting_ID = (int)command.LastInsertedId;

                            return (true, "Meeting added successfully");
                        }
                        else
                        {
                            return (false, "Failed to add Meeting");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }


        public string GetMeetingTitle(int id)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string title;
                string query = "SELECT Title FROM Meetings WHERE Meeting_ID = @ID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            title = reader.GetString(0); // Get the value from the first column (FullName)
                        }
                        else
                        {
                            title = null;
                        }
                    }
                }
                return title;
            }
        }
        public (bool success, string message) DeleteMeeting(int id)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "DELETE FROM Meetings WHERE Meeting_ID=@Id;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Meeting Deleted successfully");
                    }
                    else
                    {

                        return (false, "Failed to Delete Meeting");
                    }
                }

            }
        }
        public List<MeetingDetails> GetMeetings()
        {
            var meetingsList = new List<MeetingDetails>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "SELECT * FROM Meetings;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //The while loop iterates through each row of the query result.
                        //For each row, the reader.Read() method reads the current row and moves the cursor to the next row.   
                        while (reader.Read())
                        {
                            meetingsList.Add(new MeetingDetails
                            {
                                Meeting_ID = reader.GetInt32("Meeting_ID"),
                                Coach_ID = reader.GetInt32("Coach_ID"),
                                Title = reader.GetString("Title"),
                                Time = reader.GetDateTime("Time"),
                            });
                        }


                        return meetingsList;
                    }
                }
            }
        }

        public (bool success, string message) UpdateMeeting(MeetingDetails entry)
        {
            try
            {
                using (var connection = database.ConnectToDatabase())
                {
                    connection.Open();

                    // Ensure Meeting_ID is valid
                    if (entry.Meeting_ID <= 0)
                    {
                        return (false, "Invalid Meeting ID");
                    }

                    // Update query for updating the meeting
                    string query = @"
                UPDATE Meetings
                SET Coach_ID = @Coach_ID, Title = @Title, Time = @Time
                WHERE Meeting_ID = @Meeting_ID;
            ";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        // Add parameters for the query
                        command.Parameters.AddWithValue("@Coach_ID", entry.Coach_ID);
                        command.Parameters.AddWithValue("@Title", entry.Title);
                        command.Parameters.AddWithValue("@Time", entry.Time);
                        command.Parameters.AddWithValue("@Meeting_ID", entry.Meeting_ID);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            return (true, "Meeting updated successfully");
                        }
                        else
                        {
                            return (false, "Failed to update meeting: No matching record found");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Log the exception (consider logging it to a file or monitoring tool)
                return (false, $"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Catch any other exceptions
                return (false, $"An error occurred: {ex.Message}");
            }
        }

    }
}