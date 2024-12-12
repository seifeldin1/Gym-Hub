using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class GetSupplements
    {
        public GetSupplements(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
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