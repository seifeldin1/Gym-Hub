using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services{
    public class EventService{
        private readonly GymDatabase database;

        public EventService(GymDatabase gymDatabase){
            this.database = gymDatabase;
        }

        public (bool success, string message) AddEvent(Events events , int creatorId){
            using (var connection = database.ConnectToDatabase()){
                connection.Open();

                string query = @"INSERT INTO Events (Title, Type, Start_Date, End_Date, Location, Description, Created_By)
                             VALUES (@title, @type, @startDate, @endDate, @location, @description, @createdBy)";
                using (var command = new MySqlCommand(query, connection)){
                    command.Parameters.AddWithValue("@title", events.Name);
                    command.Parameters.AddWithValue("@type", events.Type);
                    command.Parameters.AddWithValue("@startDate", events.Start_Date);
                    command.Parameters.AddWithValue("@endDate", events.End_Date);
                    command.Parameters.AddWithValue("@location", events.Location);
                    command.Parameters.AddWithValue("@description", events.Description);
                    command.Parameters.AddWithValue("@createdBy", creatorId);
                    command.ExecuteNonQuery();
                }
                return (true, "Event added successfully.");
            }
        }

        public (bool success, string message) UpdateEvent(Events entry){
              // Check if the entry is null
            if (entry == null)
                return (false, "Event data is null.");

            try{
                string updateQuery = "UPDATE Events SET ";        // Base query string
                List<string> setClauses  = new List<string>();          // List of clauses to include in query
                List<MySqlParameter> parameters  = new List<MySqlParameter>();  // Query parameters

                // Dynamically add fields to be updated
                if (!string.IsNullOrEmpty(entry.Name)){
                    setClauses.Add("Title = @Title");
                    parameters.Add(new MySqlParameter("@Title", entry.Name));
                }

                if (!string.IsNullOrEmpty(entry.Description)){
                    setClauses.Add("Description = @Description");
                    parameters.Add(new MySqlParameter("@Description", entry.Description));
                }

                if (!string.IsNullOrEmpty(entry.Type)){
                    setClauses.Add("Type = @Type");
                    parameters.Add(new MySqlParameter("@Type", entry.Type));
                }

                if (entry.Start_Date != default){
                    setClauses.Add("Start_Date = @Start_Date");
                    parameters.Add(new MySqlParameter("@Start_Date", entry.Start_Date));
                }

                if (entry.End_Date != default){
                    setClauses.Add("End_Date = @End_Date");
                    parameters.Add(new MySqlParameter("@End_Date", entry.End_Date));
                }

                if (!string.IsNullOrEmpty(entry.Location)){
                    setClauses.Add("Location = @Location");
                    parameters.Add(new MySqlParameter("@Location", entry.Location));
                }

                if (setClauses.Count == 0)
                    return (false, "No fields to update.");

                // Complete query
                updateQuery += string.Join(", ", setClauses) + " WHERE Event_ID = @Event_ID";

                parameters.Add(new MySqlParameter("@Event_ID", entry.Id));

                using (var connection = database.ConnectToDatabase()){
                    connection.Open();
                    using (var command = new MySqlCommand(updateQuery, connection)){
                        foreach (var parameter in parameters)
                            command.Parameters.Add(parameter);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        return (false, "No event data was updated.");

                    return (true, "Event data was updated successfully.");
                    }
                }
            }
            catch (Exception ex){
                return (false, $"Error: {ex.Message}");
            }
        }



        public (bool success, string message) DeleteEvent(int eventId){
            using (var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "DELETE FROM Events WHERE Event_ID = @eventId";
                using (var command = new MySqlCommand(query, connection)){
                    command.Parameters.AddWithValue("@eventId", eventId);
                    command.ExecuteNonQuery();
                }
                return (true, "Event deleted successfully.");
            }
        }

        public List<Events> GetEvents(){
            var events = new List<Events>();
            using (var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "SELECT Event_ID, Title, Type, Start_Date, End_Date, Location, Description FROM Events";
                using (var command = new MySqlCommand(query, connection)){
                    using (var reader = command.ExecuteReader()){
                        while (reader.Read()){
                            events.Add(new Events{
                                Id          = reader.GetInt32("Event_ID"),
                                Name        = reader.GetString("Title"),
                                Type        = reader.GetString("Type"),
                                Start_Date  = reader.GetDateTime("Start_Date"),
                                End_Date    = reader.GetDateTime("End_Date"),
                                Location    = reader.GetString("Location"),
                                Description = reader.GetString("Description")
                            });
                        }
                    }
                }
            }
            return events;
        }
    }

}