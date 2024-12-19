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
    }
}