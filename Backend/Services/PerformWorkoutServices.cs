using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class PerformWorkout
    {
        private readonly GymDatabase database;

        public PerformWorkout(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public List<PerformWorkoutModel> GetPerformWorkoutsByClientId(int id)
        {
            var performWorkoutList = new List<PerformWorkoutModel>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "SELECT * FROM Perform_Workout WHERE Client_ID = @Client_ID;";
                using (var command = new MySqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Client_ID", id);

                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            performWorkoutList.Add(new PerformWorkoutModel
                            {
                                Workout_ID = reader.GetInt32("Workout_ID"),
                                Client_ID = reader.GetInt32("Client_ID"),
                                Order_Of_Workout = reader.GetInt32("Order_Of_Workout"),
                                Type = reader.GetString("Type"),
                                Day_Number = reader.GetInt32("Day_Number"),
                                Performed = reader.GetBoolean("Performed"),
                            });
                        }

                        return performWorkoutList;
                    }
                }
            }
        }
        public (bool success, string message) SetPerformed(int clientid,int workoutid)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "UPDATE Perform_Workout SET Performed = TRUE WHERE Client_ID=@Client_ID AND Workout_ID=@Workout_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Client_ID",clientid);
                    command.Parameters.AddWithValue("@Workout_ID",workoutid );
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, $"Client With ID:{clientid} perform Workout With ID:{workoutid}");
                    }
                    else
                    {

                        return (false, "Failed to add Performed workout ");
                    }
                }
            }

        }
    }
}