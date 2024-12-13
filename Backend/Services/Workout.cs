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
        public (bool success,string message) AddWorkout(WorkoutModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
             connection.Open();
             string query = "INSERT INTO Workout(Muscle_Targeted,Goal ,Created_By_Coach_ID,Calories_Burnt,Reps_Per_Set,Sets,Duration_min) VALUES (@Muscle_Targeted,@Goal,@Created_By_Coach_ID,@Calories_Burnt,@Reps_Per_Set,@Sets,@Duration_min);";
             using (var command = new MySqlCommand(query, connection))
        {
             command.Parameters.AddWithValue("@Muscle_Targeted",entry.MuscleTargeted);
            command.Parameters.AddWithValue("@Goal", entry.Goal );
            command.Parameters.AddWithValue("@Created_By_Coach_ID",entry.CreatedByCoachId);
             command.Parameters.AddWithValue("@Calories_Burnt", entry.CaloriesBurnt);
             command.Parameters.AddWithValue("@Reps_Per_Set", entry.RepsPerSet);
             command.Parameters.AddWithValue("@Sets", entry.Sets);
             command.Parameters.AddWithValue("@Duration_min",entry.DurationMin);
              int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                
                return (true,"Workout added successfully");
            }
            else
            {

                return (false,"Failed to add Workout");
            }
        }
        }

    }
}
}