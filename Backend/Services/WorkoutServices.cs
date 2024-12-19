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
                                // Workout_ID = reader.GetInt32("Workout_ID"),
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
            //? Check if An Entry is Given
            if (entry == null)
                return (false, "Workout data is null.");

            try
            {
                string updateQuery = "UPDATE Workout SET ";                        //! Query String
                List<string> setClauses = new List<string>();                   //! List of clauses added to query 
                List<MySqlParameter> parameters = new List<MySqlParameter>();   //! Query params

                //! Check In Entry for Params To Be edited By query
                if (entry.Muscle_Targeted != null)
                {
                    setClauses.Add("Muscle_Targeted = @Muscle_Targeted ");
                    parameters.Add(new MySqlParameter("@Muscle_Targeted ", entry.Muscle_Targeted));
                }
                if (entry.Goal != null)
                {
                    setClauses.Add("Goal = @Goal");
                    parameters.Add(new MySqlParameter("@Goal", entry.Goal));
                }

                setClauses.Add("Created_By_Coach_ID = @Created_By_Coach_ID ");
                parameters.Add(new MySqlParameter("@Created_By_Coach_ID", entry.Created_By_Coach_ID));


                setClauses.Add("Calories_Burnt = @Calories_Burnt ");
                parameters.Add(new MySqlParameter("@Calories_Burnt ", entry.Calories_Burnt));



                setClauses.Add("Reps_Per_Set = @Reps_Per_Set");
                parameters.Add(new MySqlParameter("@Reps_Per_Set", entry.Reps_Per_Set));



                setClauses.Add("Sets = @Sets");
                parameters.Add(new MySqlParameter("@Sets", entry.Sets));



                setClauses.Add("Duration_min = @Duration_min");
                parameters.Add(new MySqlParameter("@Duration_min", entry.Duration_min));

                if (setClauses.Count == 0)
                    return (false, "No fields to update.");

                //? Join Query
                updateQuery += string.Join(", ", setClauses) + " WHERE Workout_ID = @Workout_ID";

                parameters.Add(new MySqlParameter("@Workout_ID", entry.Workout_ID));

                using (var connection = database.ConnectToDatabase())
                {
                    connection.Open();
                    using (var command = new MySqlCommand(updateQuery, connection))
                    {
                        //! Add parameters to the command (Replace @variable with acutal value)
                        foreach (var parameter in parameters)
                            command.Parameters.Add(parameter);

                        int rowsAffected1 = command.ExecuteNonQuery();

                        if (rowsAffected1 == 0)
                            return (false, "No Workout data was updated.");

                        return (true, "Workout Data Was updated");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }


    }
}