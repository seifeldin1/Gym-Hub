using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class NutritionPlan
    {
        private readonly GymDatabase database;

        public NutritionPlan(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public (bool success, string message) AddNutritionPlan(NutritionPlanModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "INSERT INTO Nutrition(Goal,Protein_grams,Carbohydrates_grams,Fat_grams,Calories,Name,Description) VALUES (@Goal,@Protein_grams,@Carbohydrates_grams,@Fat_grams,@Calories,@Description);";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Goal", entry.Goal);
                    command.Parameters.AddWithValue("@Protein_grams", entry.Protein_grams);
                    command.Parameters.AddWithValue("@Carbohydrates_grams", entry.Carbohydrates_grams);
                    command.Parameters.AddWithValue("@Fat_grams", entry.Fat_grams);
                    command.Parameters.AddWithValue("@Calories", entry.Calories);
                    command.Parameters.AddWithValue("@Name", entry.Name);
                    command.Parameters.AddWithValue("@Description", entry.Description);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "NutritionPlan added successfully");
                    }
                    else
                    {

                        return (false, "Failed to add NutritionPlan");
                    }
                }
            }

        }
    }
}