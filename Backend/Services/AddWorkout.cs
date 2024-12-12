using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
     public class AddWorkout
    {
        private readonly GymDatabase database;

        public AddWorkout(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public string AddWorkout(string Muscle_Targeted,string Goal,int Created_By_Coach_ID,int Calories_Burnt,int Reps_Per_Set,int Sets,int Duration_min)
        {
            using (var connection = database.ConnectToDatabase())
            {
             connection.Open();
             string query = "INSERT INTO Workout(Muscle_Targeted,Goal ,Created_By_Coach_ID,Calories_Burnt,Reps_Per_Set,Sets,Duration_min) VALUES (@Muscle_Targeted,@Goal,@Created_By_Coach_ID,@Calories_Burnt,@Reps_Per_Set,@Sets,@Duration_min);";
             using (var command = new MySqlCommand(query, connection))
        {
             command.Parameters.AddWithValue("@Muscle_Targeted",Muscle_Targeted);
            command.Parameters.AddWithValue("@Goal", Goal );
            command.Parameters.AddWithValue("@Created_By_Coach_ID",Created_By_Coach_ID);
             command.Parameters.AddWithValue("@Calories_Burnt", Calories_Burnt);
             command.Parameters.AddWithValue("@Reps_Per_Set", Reps_Per_Set);
             command.Parameters.AddWithValue("@Sets", Sets);
             command.Parameters.AddWithValue("@Duration_min",Duration_min);
              int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                var response = new
                {
                    success = true,
                    message = "Workout added successfully"
                };
                return JsonConvert.SerializeObject(response);
            }
            else
            {
                var response = new
                {
                    success = false,
                    message = "Failed to add Workout"
                };
                return JsonConvert.SerializeObject(response);
            }
        }
        }

    }
}
}