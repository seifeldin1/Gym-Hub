using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class Workout
    {
        private readonly GymDatabase database;

        public Workout(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public (bool success, string message) AddWorkout(WorkoutModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "INSERT INTO Workout(Muscle_Targeted,Goal ,Created_By_Coach_ID,Calories_Burnt,Reps_Per_Set,Sets,Duration_min) VALUES (@Muscle_Targeted,@Goal,@Created_By_Coach_ID,@Calories_Burnt,@Reps_Per_Set,@Sets,@Duration_min);";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Muscle_Targeted", entry.MuscleTargeted);
                    command.Parameters.AddWithValue("@Goal", entry.Goal);
                    command.Parameters.AddWithValue("@Created_By_Coach_ID", entry.CreatedByCoachId);
                    command.Parameters.AddWithValue("@Calories_Burnt", entry.CaloriesBurnt);
                    command.Parameters.AddWithValue("@Reps_Per_Set", entry.RepsPerSet);
                    command.Parameters.AddWithValue("@Sets", entry.Sets);
                    command.Parameters.AddWithValue("@Duration_min", entry.DurationMin);
                    int rowsAffected = (int)command.ExecuteScalar();
                    if (rowsAffected > 0)
                    {

                        return (true, "Workout added successfully");
                    }
                    else
                    {

                        return (false, "Failed to add Workout");
                    }
                }
            }

        }
        public List<WorkoutModel> GetWorkouts()
        {
            var workoutList = new List<WorkoutModel>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "SELECT * FROM Workout ;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //The while loop iterates through each row of the query result.
                        //For each row, the reader.Read() method reads the current row and moves the cursor to the next row.   
                        while (reader.Read())
                        {
                            workoutList.Add(new WorkoutModel
                            {
                                ID = reader.GetInt32("ID"),
                                MuscleTargeted = reader.GetString(" MuscleTargeted"),
                                Goal = reader.GetString("Goal"),
                                CreatedByCoachId = reader.GetInt32("CreatedByCoachId"),
                                CaloriesBurnt = reader.GetInt32("CaloriesBurnt "),
                                RepsPerSet = reader.GetInt32("RepsPerSet"),
                                Sets = reader.GetInt32("Sets"),
                                DurationMin = reader.GetInt32("DurationMin "),
                            });
                        }


                        return workoutList;
                    }
                }
            }
        }
    }
}