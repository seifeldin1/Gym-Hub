using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services {
    public class HolidayService
    {
        private readonly GymDatabase database;

        public HolidayService(GymDatabase gymDatabase){
            this.database = gymDatabase;
        }

        public (bool success, string message) AddHoliday(Holiday holiday){
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "INSERT INTO Holiday (Title,Start_Date,End_Date) VALUES (@Title,@startDate,@endDate)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", holiday.Title);
                    command.Parameters.AddWithValue("@startDate", holiday.Start_Date);
                    command.Parameters.AddWithValue("@endDate", holiday.End_Date);
                    command.ExecuteNonQuery();
                }
                return (true, "Holiday added successfully.");
            }
        }

        public (bool success, string message) UpdateHoliday(Holiday entry){
            // Check if the entry is null
            if (entry == null)
                return (false, "Holiday data is null.");

            try{
                string updateQuery = "UPDATE Holiday SET ";                     // Base query string
                List<string> setClauses = new List<string>();                    // List of clauses to include in query
                List<MySqlParameter> parameters = new List<MySqlParameter>();    // Query parameters

                // Dynamically add fields to be updated
                if (!string.IsNullOrEmpty(entry.Title)){
                    setClauses.Add("Title = @Title");
                    parameters.Add(new MySqlParameter("@Title", entry.Title));
                }

                if (entry.Start_Date != default&& entry.Start_Date > DateTime.MinValue){
                    setClauses.Add("Start_Date = @Start_Date");
                    parameters.Add(new MySqlParameter("@Start_Date", entry.Start_Date));
                }

                if (entry.End_Date != default && entry.End_Date > DateTime.MinValue){
                    setClauses.Add("End_Date = @End_Date");
                    parameters.Add(new MySqlParameter("@End_Date", entry.End_Date));
                }

                if (setClauses.Count == 0)
                    return (false, "No fields to update.");

                // Complete query
                updateQuery += string.Join(", ", setClauses) + " WHERE Holiday_ID = @Holiday_ID";

                parameters.Add(new MySqlParameter("@Holiday_ID", entry.Holiday_ID));

                using (var connection = database.ConnectToDatabase()){
                    connection.Open();
                    using (var command = new MySqlCommand(updateQuery, connection)){
                        foreach (var parameter in parameters)
                            command.Parameters.Add(parameter);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                            return (false, "No holiday data was updated.");

                        return (true, "Holiday data was updated successfully.");
                    }
                }
            }
            catch (Exception ex){
                return (false, $"Error: {ex.Message}");
            }
        }


        public (bool success, string message) DeleteHoliday(int holidayId){
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "DELETE FROM Holiday WHERE Holiday_ID = @holidayId";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@holidayId", holidayId);
                    command.ExecuteNonQuery();
                }
                return (true, "Holiday deleted successfully.");
            }
        }

        public List<Holiday> GetHolidays(){
            var holidays = new List<Holiday>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "SELECT * FROM Holiday ";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            holidays.Add(new Holiday
                            {
                                Holiday_ID= reader.GetInt32("Holiday_ID"),
                                Title= reader.GetString("Title"),
                                Start_Date = reader.GetDateTime("Start_Date"),
                                End_Date = reader.GetDateTime("End_Date")
                            });
                        }
                    }
                }
            }
            return holidays;
        }
    }
}