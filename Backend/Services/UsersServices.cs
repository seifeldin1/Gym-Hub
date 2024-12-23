using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using MySql.Data.MySqlClient;


namespace Backend.Services
{
    public class UsersServices
    {
        private readonly GymDatabase database;
        public UsersServices(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

    public (bool success, string message) AddUser(UserModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query ="INSERT INTO User (Username,PasswordHashed,Type,First_Name,Last_Name,Email,Phone_Number,Gender,Age,National_Number) VALUES (@Username,@PasswordHashed,@Type,@First_Name,@Last_Name,@Email,@Phone_Number,@Gender,@Age,@National_Number);";
                

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", entry.Username);
                    command.Parameters.AddWithValue("@PasswordHashed", BCrypt.Net.BCrypt.HashPassword(entry.PasswordHashed));
                    command.Parameters.AddWithValue("@Type", entry.Type);
                    command.Parameters.AddWithValue("@First_Name", entry.First_Name);
                    command.Parameters.AddWithValue("@Last_Name", entry.Last_Name);
                    command.Parameters.AddWithValue("@Email", entry.Email);
                    command.Parameters.AddWithValue("@Phone_Number", entry.Phone_Number);
                    command.Parameters.AddWithValue("@Gender", entry.Gender);
                    command.Parameters.AddWithValue("@Age", entry.Age);
                    command.Parameters.AddWithValue("@National_Number", entry.National_Number);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return (true, "User added successfully");
                    else
                        return (false, "Failed to add User");
                }
            }
        }


    }
}