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
                    command.Parameters.AddWithValue("@Purchase_Date", entry.Purchase_Date.ToString("yyyy-MM-dd"));
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
            var equipmentsList = new List<EquipmentsModel>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "SELECT * FROM Equipments ;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //The while loop iterates through each row of the query result.
                        //For each row, the reader.Read() method reads the current row and moves the cursor to the next row.   
                        while (reader.Read())
                        {
                            equipmentsList.Add(new EquipmentsModel
                            {
                                Equipment_ID = reader.GetInt32("Equipment_ID"),
                                Status = reader.GetString("Status"),
                                Purchase_Price = reader.GetInt32("Purchase_Price"),
                                Category= reader.GetString("Category"),
                                Purchase_Date = DateOnly.FromDateTime(reader.GetDateTime("Purchase_Date")),
                                Name = reader.GetString("Name"),
                                Serial_Number = reader.GetString("Serial_Number"),
                                Belong_To_Branch_ID = reader.GetInt32("Belong_To_Branch_ID"),
                            });
                        }


                        return equipmentsList;
                    }
                }
            }
        }
         public (bool success, string message) UpdateEquipment(EquipmentsModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "UPDATE Equipments SET Status=@Status,Purchase_Price=@Purchase_Price,Category=@Category,Purchase_Date=@Purchase_Date,Name=@Name,Serial_Number=@Serial_Number,Belong_To_Branch_ID=@Belong_To_Branch_ID WHERE Equipment_ID=@Equipment_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status",entry.Status);
                    command.Parameters.AddWithValue("@Purchase_Price",entry.Purchase_Price);
                    command.Parameters.AddWithValue("@Category",entry.Category );
                    command.Parameters.AddWithValue("@Purchase_Date",entry.Purchase_Date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Name",entry.Name);
                    command.Parameters.AddWithValue("@Serial_Number",entry.Serial_Number);
                    command.Parameters.AddWithValue("@Belong_To_Branch_ID",entry.Belong_To_Branch_ID);
                    command.Parameters.AddWithValue("@Equipment_ID",entry.Equipment_ID );
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Equipment Updated successfully");
                    }
                    else
                    {

                        return (false, "Failed to Update Equipment");
                    }
                }
            }

        }
        public (bool success, string message) DeleteEquipment(int id)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "DELETE FROM Equipments WHERE Equipment_ID=@Id;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Equipment Deleted successfully");
                    }
                    else
                    {

                        return (false, "Failed to Delete Equipment");
                    }
                }

            }
        }

    }
}


