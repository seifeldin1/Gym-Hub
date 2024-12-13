using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class Equipments
    {
        private readonly GymDatabase database;
        public Equipments(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public (bool success, string message) AddEquipments(EquipmentsModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

                string query = "INSERT INTO Equipments (Status,Purchase_Price ,Category,Purchase_Date, Name,Serial_Number,Belong_To_Branch_ID) VALUES (@Status, @Purchase_Price, @Category,@Purchase_Date,@name,@Serial_Number,@Belong_To_Branch_ID);";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", entry.Status);
                    command.Parameters.AddWithValue("@Purchase_Price", entry.Purchase_Price);
                    command.Parameters.AddWithValue("@Category", entry.Category);
                    command.Parameters.AddWithValue("@Purchase_Date", entry.Purchase_Date);
                    command.Parameters.AddWithValue("@Name", entry.Name);
                    command.Parameters.AddWithValue("@Serial_Number", entry.Serial_Number);
                    command.Parameters.AddWithValue("@Belong_To_Branch_ID", entry.Belong_To_Branch_ID);

                     int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Equipment added successfully");
                    }
                    else
                    {

                        return (false, "Failed to add equipment");
                    }
                }
            }

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
                        while (reader.Read())
                        {
                            equipmentList.Add(new
                            {
                                Equipment_ID = reader["Equipment_ID"],
                                Status = reader["Status"],
                                Purchase_Price = reader["Purchase_Price"],
                                Category = reader["Category "],
                                Purchase_Date = reader["Purchase_Date"],
                                Name = reader["Name"],
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


