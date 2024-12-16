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
        public List<EquipmentsModel> GetEquipments()
        {
            var equipmentList = new List<EquipmentsModel>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

                string query = "SELECT * FROM Equipments;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            equipmentList.Add(new EquipmentsModel
                            {
                                Equipment_ID = reader.GetInt16("Equipment_ID"),
                                Status = reader.GetString("Status"),
                                Purchase_Price = reader.GetInt16("Purchase_Price"),
                                Category = reader.GetString("Category "),
                                Purchase_Date = DateOnly.FromDateTime(reader.GetDateTime("Purchase_Date")),
                                Name = reader.GetString("Name"),
                                Serial_Number = reader.GetString("Serial_Number"),
                                Belong_To_Branch_ID = reader.GetInt16("Belong_To_Branch_ID")
                            });
                        }

                        return equipmentList;
                    }

                }
            }
        }
    }
}


