using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
     public class Supplements
    {
        private readonly GymDatabase database;

        public Supplements(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public (bool success,string message) addsupplements(SupplementsModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
             connection.Open();
             string query = "INSERT INTO Supplements (Name,Brand ,Selling_Price,Purchased_Price,Type,Flavor,Manufactured_Date,Expiration_Date,Purchase_Date,Scoop_Size_grams,Scoop_Number_package,Scoop_Detail) VALUES (@Name, @Brand, @Selling_Price,@Purchased_Price,@Type,@Flavor,@Manufactured_Date,@Expiration_Date,@Purchase_Date,@Scoop_Size_grams,@Scoop_Number_package,@Scoop_Detail);";
             using (var command = new MySqlCommand(query, connection))
        {
             command.Parameters.AddWithValue("@Name",entry.Name);
            command.Parameters.AddWithValue("@Brand ", entry.Brand);
            command.Parameters.AddWithValue("@Selling_Price", entry.Selling_Price);
             command.Parameters.AddWithValue("@Purchased_Price", entry.Purchased_Price);
             command.Parameters.AddWithValue("@Type", entry.Type);
             command.Parameters.AddWithValue("@Flavor", entry.Flavor);
             command.Parameters.AddWithValue("@Manufactured_Date", entry.Manufactured_Date);
             command.Parameters.AddWithValue("@Expiration_Date",entry.Expiration_Date);
             command.Parameters.AddWithValue("@Purchase_Date",entry.Purchase_Date);
             command.Parameters.AddWithValue("@Scoop_Size_grams", entry.Scoop_Size_grams);
             command.Parameters.AddWithValue("@Scoop_Number_package", entry.Scoop_Number_package);
             command.Parameters.AddWithValue("@Scoop_Detail", entry.Scoop_Detail);
              int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                
                return (true,"Supplement added successfully");
            }
            else
            {
                return (false,"Failed to add Supplement");
            }
        }
        }

    }

        public string GetSupplements()
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
        string query = "SELECT * FROM Supplements ;";
        using (var command = new MySqlCommand(query, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                var supplementList = new List<object>();
                //The while loop iterates through each row of the query result.
                //For each row, the reader.Read() method reads the current row and moves the cursor to the next row.   
                while (reader.Read())
                {
                    supplementList.Add(new
                    {
                        Supplement_ID=reader["Supplement_ID"],    
                        Name= reader["Name"],
                        Brand = reader["Brand"],
                        Selling_Price= reader["Selling_Price"],
                        Purchased_Price = reader["Purchased_Price"],
                        Type = reader["Type"],
                        Flavor= reader["Flavor"],
                        Manufactured_Date= reader["Manufactured_Date"],
                        Expiration_Date= reader["Expiration_Date"],
                        Scoop_Size_grams= reader["Scoop_Size_grams"],
                        Scoop_Number_package= reader["Scoop_Number_package"],
                        Scoop_Detail= reader["Scoop_Detail"]
                    });
                }

                var response = new
                {
                    success = true,
                    data = supplementList
                };
                return JsonConvert.SerializeObject(response);
            }
        }
            }
        }

    }
}
