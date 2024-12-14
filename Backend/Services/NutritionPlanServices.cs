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
        public List<NutritionPlanModel> GetNutritionPlans()
        {
            var nutritionplanList = new List<NutritionPlanModel>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "SELECT * FROM Nutrition ;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //The while loop iterates through each row of the query result.
                        //For each row, the reader.Read() method reads the current row and moves the cursor to the next row.   
                        while (reader.Read())
                        {
                            nutritionplanList.Add(new NutritionPlanModel
                            {
                                Nutrition_ID = reader.GetInt32(" Nutrition_ID"),
                                Goal = reader.GetString(" Goal"),
                                Protein_grams = reader.GetInt32("Protein_grams"),
                                Carbohydrates_grams = reader.GetInt32("Carbohydrates_grams "),
                                Fat_grams = reader.GetInt32("Fat_grams "),
                                Calories = reader.GetInt32("Calories"),
                                Name = reader.GetString("Name"),
                                Description = reader.GetString("Description "),
                            });
                        }


                        return nutritionplanList;
                    }
                }
            }
        }
    }
}