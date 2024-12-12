using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class AddEquipments
    {

    
    private readonly GymDatabase database;

        public  AddEquipments(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public string AddEquipments(string status, int  Purchase_Price, string Category,string Purchase_Date,string Name,string Serial_Number)
{
    using (var connection = database.ConnectToDatabase())
    {
        connection.Open();

        string query = "INSERT INTO Equipments (Status,Purchase_Price ,Category,Purchase_Date, Name,Serial_Number,Belong_To_Branch_ID) VALUES (@Status, @Purchase_Price, @Category,@Purchase_Date,@name,@Serial_Number,@Belong_To_Branch_ID);";
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@Purchase_Price", Purchase_Price);
            command.Parameters.AddWithValue("@Category", Category);
             command.Parameters.AddWithValue("@Purchase_Date", Purchase_Date);
             command.Parameters.AddWithValue("@Name", Name);
             command.Parameters.AddWithValue("@Serial_Number", Serial_Number);
             command.Parameters.AddWithValue("@Belong_To_Branch_ID", Belong_To_Branch_ID);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                var response = new
                {
                    success = true,
                    message = "Equipment added successfully"
                };
                return JsonConvert.SerializeObject(response);
            }
            else
            {
                var response = new
                {
                    success = false,
                    message = "Failed to add equipment"
                };
                return JsonConvert.SerializeObject(response);
            }
        }
    }

}
}
}