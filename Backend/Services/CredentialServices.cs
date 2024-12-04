using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class CredentialServices
    {
        private readonly GymDatabase database;

        public CredentialServices(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

        public string Login(Credentials entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

                string query = "SELECT Username , PasswordHashed , Type FROM User WHERE Username = @username;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", entry.Username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // If username found in database
                        {
                            string hashedPassword = reader["PasswordHashed"].ToString();
                            string roleType = reader["Type"].ToString();
                          

                            if (BCrypt.Net.BCrypt.Verify(entry.Password, hashedPassword))
                            {
                                // Password is correct
                                var response = new
                                {
                                    success = true,
                                    message = "Login successful",
                                    userType = roleType
                                };
                                return JsonConvert.SerializeObject(response); // JSON response for success
                            }
                            else
                            {
                                // Password is incorrect
                                var response = new
                                {
                                    success = false,
                                    message = "Invalid password"
                                };
                                return JsonConvert.SerializeObject(response); // JSON response for error
                            }
                        }
                        else
                        {
                            // Username not found
                            var response = new
                            {
                                success = false,
                                message = "Username does not exist"
                            };
                            return JsonConvert.SerializeObject(response); // JSON response for error
                        }
                    }
                }
            }
        }
    }
}
