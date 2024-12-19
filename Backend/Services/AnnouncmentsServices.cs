using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services {
    public class AnnouncmentsServices{
        private readonly GymDatabase database;
        public AnnouncmentsServices(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

        public (bool success, string message) AddAnnouncment(AnnouncmentsModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "INSERT INTO Announcments(Author_ID,Author_Role,Title,Content,Date_Posted,Type) VALUES (@Author_ID,@Author_Role,@Title,@Content,@Date_Posted,@Type);";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Author_ID", entry.Author_ID);
                    command.Parameters.AddWithValue("@Author_Role", entry.Author_Role);
                    command.Parameters.AddWithValue("@Title", entry.Title);
                    command.Parameters.AddWithValue("@Content", entry.Content);
                    command.Parameters.AddWithValue("@Date_Posted", entry.Date_Posted);
                    command.Parameters.AddWithValue("@Type", entry.Type);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Announcment added successfully");
                    }
                    else
                    {

                        return (false, "Failed to add Announcment");
                    }
                }
            }
        }

        public List<AnnouncmentsModel> GetAnnouncments()
        {
            var announcmentsList = new List<AnnouncmentsModel>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "SELECT * FROM Announcments ;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //The while loop iterates through each row of the query result.
                        //For each row, the reader.Read() method reads the current row and moves the cursor to the next row.   
                        while (reader.Read())
                        {
                            announcmentsList.Add(new AnnouncmentsModel
                            {
                                // Announcments_ID = reader.GetInt32("Announcments_ID"),
                                Author_ID = reader.GetInt32("Author_ID"),
                                Author_Role = reader.GetString("Author_Role"),
                                Title = reader.GetString("Title"),
                                Content = reader.GetString("Content"),
                                Date_Posted = reader.GetDateTime("Date_Posted"),
                                Type = reader.GetString("Type"),
                            });
                        }

                        return announcmentsList;
                    }
                }
            }
        }
    }
}