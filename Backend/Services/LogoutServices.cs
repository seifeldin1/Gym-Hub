using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
namespace Backend.Services{
    public class LogoutServices{
        private readonly GymDatabase database;

        public LogoutServices(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

        public (bool success , string message) Logout(string token){
            using(var connection = database.ConnectToDatabase()){
                string query = "INSERT INTO BlacklistedTokens VALUES (@token)";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@token" , token);
                    command.ExecuteNonQuery();
                }
                return(true , "logged out successfully");
            }
        }
        
    }
}