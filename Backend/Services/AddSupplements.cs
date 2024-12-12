using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
     public class AddSupplements
    {
        private readonly GymDatabase database;

        public AddSupplements(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public string AddSupplements(string Name, string Brand, float Selling_Price,float Purchased_Price,string Type,string Flavor,string Manufactured_Date,string Expiration_Date,string Purchase_Date,int Scoop_Size_grams,int Scoop_Number_package,string Scoop_Detail)
        {
            using (var connection = database.ConnectToDatabase())
            {
             connection.Open();
             string query = "INSERT INTO Supplements (Name,Brand ,Selling_Price,Purchased_Price,Type,Flavor,Manufactured_Date,Expiration_Date,Purchase_Date,Scoop_Size_grams,Scoop_Number_package,Scoop_Detail) VALUES (@Name, @Brand, @Selling_Price,@Purchased_Price,@Type,@Flavor,@Manufactured_Date,@Expiration_Date,@Purchase_Date,@Scoop_Size_grams,@Scoop_Number_package,@Scoop_Detail);";
             using (var command = new MySqlCommand(query, connection))
        {
             command.Parameters.AddWithValue("@Name",Name);
            command.Parameters.AddWithValue("@Brand ", Brand );
            command.Parameters.AddWithValue("@Selling_Price", Selling_Price);
             command.Parameters.AddWithValue("@Purchased_Price", Purchased_Price);
             command.Parameters.AddWithValue("@Type", Type);
             command.Parameters.AddWithValue("@Flavor", Flavor);
             command.Parameters.AddWithValue("@Manufactured_Date", Manufactured_Date);
             command.Parameters.AddWithValue("@Expiration_Date",Expiration_Date);
             command.Parameters.AddWithValue("@Purchase_Date",Purchase_Date);
             command.Parameters.AddWithValue("@Scoop_Size_grams", Scoop_Size_grams);
             command.Parameters.AddWithValue("@Scoop_Number_package", Scoop_Number_package);
             command.Parameters.AddWithValue("@Scoop_Detail", Scoop_Detail);
              int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                var response = new
                {
                    success = true,
                    message = "Supplement added successfully"
                };
                return JsonConvert.SerializeObject(response);
            }
            else
            {
                var response = new
                {
                    success = false,
                    message = "Failed to add Supplement"
                };
                return JsonConvert.SerializeObject(response);
            }
        }
        }

    }
}
}