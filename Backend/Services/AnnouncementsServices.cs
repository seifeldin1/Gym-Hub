using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services {
    public class AnnouncementsServices{
        private readonly GymDatabase database;
        public AnnouncementsServices(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

        //* AddAnnouncement : Adds an Announcement into Announcement Relation
        public (bool success, string message) AddAnnouncement(AnnouncementsModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "INSERT INTO Announcements(Author_ID,Author_Role,Title,Content,Date_Posted,Type) VALUES (@Author_ID,@Author_Role,@Title,@Content,@Date_Posted,@Type);";
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
                        return (true, "Announcement added successfully");
                    else
                        return (false, "Failed to add Announcement");
                }
            }
        }

        //* AddAnnouncement : Gets an Announcement from Announcement Relation
        public List<AnnouncementsModel> GetAnnouncements()
        {
            var announcementsList = new List<AnnouncementsModel>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "SELECT * FROM Announcements;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //The while loop iterates through each row of the query result.
                        //For each row, the reader.Read() method reads the current row and moves the cursor to the next row.   
                        while (reader.Read())
                        {
                            announcementsList.Add(new AnnouncementsModel
                            {
                                Announcements_ID = reader.GetInt32("Announcements_ID"),
                                Author_ID = reader.GetInt32("Author_ID"),
                                Author_Role = reader.GetString("Author_Role"),
                                Title = reader.GetString("Title"),
                                Content = reader.GetString("Content"),
                                Date_Posted = reader.GetDateTime("Date_Posted"),
                                Type = reader.GetString("Type"),
                            });
                        }
                        return announcementsList;
                    }
                }
            }
        }

        public (bool success , string message) EditAnnouncment(AnnouncementUpdaterModel announcement){
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                var announcementFields = new List<string>();
                var announcmentParameters = new List<MySqlParameter>();
                if(!string.IsNullOrEmpty(announcement.Title)){
                    announcementFields.Add("Title = @Title");
                    announcmentParameters.Add(new MySqlParameter("@Title", announcement.Title));
                }
                if(!string.IsNullOrEmpty(announcement.Content)){
                    announcementFields.Add("Content = @Content");
                    announcmentParameters.Add(new MySqlParameter("@Content", announcement.Content));
                }
                if(!string.IsNullOrEmpty(announcement.Type)){
                    announcementFields.Add("Type = @Type");
                    announcmentParameters.Add(new MySqlParameter("@Type", announcement.Type));
                }
                var updateQuery = announcementFields.Count>0? $"UPDATE Announcements SET {string.Join(",",announcementFields)} WHERE Announcements_ID = @Announcements_ID":null;
                announcmentParameters.Add(new MySqlParameter("@Announcements_ID" , announcement.Announcements_ID));
                int rowsAffected = 0;
                if(updateQuery!=null){
                    using (var command = new MySqlCommand(updateQuery, connection))
                    {
                        foreach(var param in announcmentParameters)
                            command.Parameters.Add(param);
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }

                if(rowsAffected>0){
                    return (true, "Announcement updated successfully");
                }
                else{
                    return (false, "Failed to update announcement");
                }
                  
                
            }
        }

        //* DeleteAnnouncement : Deletes an Announcement from Announcement Relation
        public (bool success, string message) DeleteAnnouncement(int id)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "DELETE FROM Announcements WHERE Announcements_ID=@Id;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return (true, "Announcement Deleted successfully");
                    else
                        return (false, "Failed to Delete Announcement");
                }

            }
        }
    }
}