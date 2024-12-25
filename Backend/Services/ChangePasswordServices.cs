using Backend.Database;
using BCrypt.Net;
using MySql.Data.MySqlClient;

namespace Backend.Services{
    public class PasswordServices{
        public GymDatabase gymDatabase;
        public PasswordServices(GymDatabase gymDatabase){
            this.gymDatabase = gymDatabase;
        }

        public (bool success , string message) ChangePassword(string newPassword  , int id){
            using(var connection = gymDatabase.ConnectToDatabase()){
                connection.Open();
                string query = "UPDATE User SET PasswordHashed = @Password Where User_ID = @id";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@Password" , BCrypt.Net.BCrypt.HashPassword(newPassword));
                    command.Parameters.AddWithValue("@id" , id);
                    command.ExecuteNonQuery();
                }
                return (true , "Password changed");
            }
        }
    }
}