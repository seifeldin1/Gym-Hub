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
                    command.Parameters.AddWithValue("@Muscle_Targeted", entry.Muscle_Targeted);
                    command.Parameters.AddWithValue("@Goal", entry.Goal);
                    command.Parameters.AddWithValue("@Created_By_Coach_ID", entry.Created_By_Coach_ID);
                    command.Parameters.AddWithValue("@Calories_Burnt", entry.Calories_Burnt);
                    command.Parameters.AddWithValue("@Reps_Per_Set", entry.Reps_Per_Set);
                    command.Parameters.AddWithValue("@Sets", entry.Sets);
                    command.Parameters.AddWithValue("@Duration_min", entry.Duration_min);
                    int rowsAffected = command.ExecuteNonQuery();
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
        public (bool success, string message) DeleteWorkout(int id)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "DELETE FROM Workout WHERE Workout_ID=@Id;";
                Console.WriteLine("Hello, World!!!!");
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Workout Deleted successfully");
                    }
                    else
                    {

                        return (false, "Failed to Delete Workout");
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
                                Workout_ID = reader.GetInt32("Workout_ID"),
                                Muscle_Targeted = reader.GetString("Muscle_Targeted"),
                                Goal = reader.GetString("Goal"),
                                Created_By_Coach_ID = reader.GetInt32("Created_By_Coach_ID"),
                                Calories_Burnt = reader.GetInt32("Calories_Burnt"),
                                Reps_Per_Set = reader.GetInt32("Reps_Per_Set"),
                                Sets = reader.GetInt32("Sets"),
                                Duration_min = reader.GetInt32("Duration_min"),
                            });
                        }


                        return workoutList;
                    }
                }
            }
        }

        public (bool success, string message) UpdateWorkout(WorkoutModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "UPDATE Workout SET Muscle_Targeted=@Muscle_Targeted,Goal=@Goal,Created_By_Coach_ID=@Created_By_Coach_ID,Calories_Burnt=@Calories_Burnt,Reps_Per_Set=@Reps_Per_Set,Sets=@Sets,Duration_min=@Duration_min WHERE Workout_ID=@Workout_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Muscle_Targeted",entry.Muscle_Targeted);
                    command.Parameters.AddWithValue("@Goal",entry.Goal );
                    command.Parameters.AddWithValue("@Created_By_Coach_ID",entry.Created_By_Coach_ID );
                    command.Parameters.AddWithValue("@Calories_Burnt",entry.Calories_Burnt );
                    command.Parameters.AddWithValue("@Reps_Per_Set",entry.Reps_Per_Set );
                    command.Parameters.AddWithValue("@Sets",entry.Sets );
                    command.Parameters.AddWithValue("@Duration_min",entry.Duration_min );
                    command.Parameters.AddWithValue("@Workout_ID",entry.Workout_ID );
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Workout Updated successfully");
                    }
                    else
                    {

                        return (false, "Failed to Update Workout");
                    }
                }
            }

        }
    }
}