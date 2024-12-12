using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
     public class AddNutritionPlan
    {
        private readonly GymDatabase database;

        public AddNutritionPlan(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public string AddNutritionPlan(string Goal,int Protein_grams,int Carbohydrates_grams,int Fat_grams,int Calories,string Name,string Description )
        {
            using (var connection = database.ConnectToDatabase())
            {
             connection.Open();
             string query = "INSERT INTO Nutrition(Goal,Protein_grams,Carbohydrates_grams,Fat_grams,Calories,Name,Description) VALUES (@Goal,@Protein_grams,@Carbohydrates_grams,@Fat_grams,@Calories,@Description);";
             using (var command = new MySqlCommand(query, connection))
        {
             command.Parameters.AddWithValue("@Goal",Goal);
            command.Parameters.AddWithValue("@Protein_grams", Protein_grams );
            command.Parameters.AddWithValue("@Carbohydrates_grams",Carbohydrates_grams);
             command.Parameters.AddWithValue("@Fat_grams", Fat_grams);
             command.Parameters.AddWithValue("@Calories", Calories);
             command.Parameters.AddWithValue("@Name", Name);
             command.Parameters.AddWithValue("@Description",Description);
              int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                var response = new
                {
                    success = true,
                    message = "NutritionPlan added successfully"
                };
                return JsonConvert.SerializeObject(response);
            }
            else
            {
                var response = new
                {
                    success = false,
                    message = "Failed to add NutritionPlan"
                };
                return JsonConvert.SerializeObject(response);
            }
        }
        }

    }
}
}