using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
     public class Branch
    {
        private readonly GymDatabase database;

        public Branch(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public (bool success,string message) AddBranch(BranchModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
             connection.Open();
             string query = "INSERT INTO Branch (Branch_Name,Location,Opening_Time, Closing_Time) VALUES (@Branch_Name,@Location,@Opening_Time,@Closing_Time);";
             using (var command = new MySqlCommand(query, connection))
        {
             command.Parameters.AddWithValue("@Branch_Name",entry.Branch_Name);
            command.Parameters.AddWithValue("@Location", entry.Location );
            command.Parameters.AddWithValue("@Opening_Time",entry.Opening_Time);
             command.Parameters.AddWithValue("@Closing_Time", entry.Closing_Time);
              int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                
                return (true,"Branch added successfully");
            }
            else
            {

                return (false,"Failed to add Branch");
            }
        }
        }

    }
    public (bool success,string message) DeleteBranch (int id )
    {
        using (var connection = database.ConnectToDatabase())
            {
             connection.Open();
             string query="DELETE FROM Branch WHERE  Branch_ID =@Id;";
              using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                         int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
                 {
                
                return (true,"Branch Deleted successfully");
                 }
            else
                {

                return (false,"Failed to Delete Branch");
                }
                    }

            }
    }
    
}
}