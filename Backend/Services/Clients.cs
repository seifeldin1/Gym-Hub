using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class Clients
    {
    private readonly GymDatabase database;
        public  Clients(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
          public ( bool success, string message) AddClients(ClientsModel entry)
   {
    using (var connection = database.ConnectToDatabase())
    {
        connection.Open();

        string query = "INSERT INTO Client (Client_ID,Join_Date,BMR,Weight_kg,Height_cm,Belong_To_Coach_ID,Start_Date_Membership,End_Date_Membership,Membership_Type,Fees_Of_Membership,Membership_Period_Months) VALUES (@Client_ID, @Join_Date, @BMR,@Weight_kg ,@Height_cm,@Belong_To_Coach_ID ,@Start_Date_Membership,@End_Date_Membership,@Membership_Type,@Fees_Of_Membership,@Membership_Period_Months );";
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Client_ID", entry.Client_ID);
            command.Parameters.AddWithValue("@Join_Date",entry.Join_Date);
            command.Parameters.AddWithValue("@@BMR", entry.BMR);
             command.Parameters.AddWithValue("@Weight_kg", entry.Weight_kg);
             command.Parameters.AddWithValue("@Height_cm", entry.Height_cm);
             command.Parameters.AddWithValue("@Belong_To_Coach_ID", entry.Belong_To_Coach_ID);
             command.Parameters.AddWithValue("@Start_Date_Membership",entry.Start_Date_Membership);
             command.Parameters.AddWithValue("@End_Date_Membership",entry.End_Date_Membership);
             command.Parameters.AddWithValue("@Membership_Type",entry.Membership_Type);
             command.Parameters.AddWithValue("@Fees_Of_Membership",entry.Fees_Of_Membership);
             command.Parameters.AddWithValue("@Membership_Period_Months",entry.Membership_Period_Months);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
               
                return (true,"Client added successfully");
            }
            else
            {
              
                return(false, "Failed to add Client");
            }
        }
    }

}



    }
}