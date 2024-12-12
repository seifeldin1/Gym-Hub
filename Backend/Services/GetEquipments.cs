using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class GetEquipments
    {
        public GetEquipments(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

        public string GetEquipments()
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

        string query = "SELECT * FROM Equipments;";
        using (var command = new MySqlCommand(query, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                var equipmentList = new List<object>();
                //The while loop iterates through each row of the query result.
                //For each row, the reader.Read() method reads the current row and moves the cursor to the next row.   
                while (reader.Read())
                {
                    equipmentList.Add(new
                    {
                        Equipment_ID=reader["Equipment_ID"],    
                        Status= reader["Status"],
                        Purchase_Price = reader["Purchase_Price"],
                         Category= reader["Category"],
                        Purchase_Date = reader["Purchase_Date"],
                        Serial_Number = reader["Serial_Number"],
                        Belong_To_Branch_ID = reader["Belong_To_Branch_ID"]
                    });
                }

                var response = new
                {
                    success = true,
                    data = equipmentList
                };
                return JsonConvert.SerializeObject(response);
            }
        }
            }
        }

    }
}